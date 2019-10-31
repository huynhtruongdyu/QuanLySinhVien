using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ThucHanhKiemThuPhanMem.Model.DAO;
using ThucHanhKiemThuPhanMem.Model.EF;

namespace ThucHanhKiemThuPhanMem
{
    /// <summary>
    /// Interaction logic for NhapDiem.xaml
    /// </summary>
    public partial class NhapDiem : Window
    {
        MyDbContext db;
        List<SinhVienVMM> listSV;
        List<Nganh> listNganh;
        List<MonHoc> listMonHoc;
        List<Lop> listLop;
        public NhapDiem()
        {
            InitializeComponent();
            Load();
        }
        void Load()
        {
            db = new MyDbContext();
            listNganh = db.Nganh.ToList();
            listMonHoc = db.MonHoc.ToList();
            listLop = db.Lop.ToList();

            cbNganh.ItemsSource = listNganh;
            cbNganh.SelectedValuePath = "MaNganh";
            cbNganh.DisplayMemberPath = "TenNganh";

            cbMon.ItemsSource = listMonHoc;
            cbMon.SelectedValuePath = "MaMonHoc";
            cbMon.DisplayMemberPath = "TenMon";

            cbLop.ItemsSource = listLop;
            cbLop.SelectedValuePath = "MaLop";
            cbLop.DisplayMemberPath = "TenLop";
        }

        private void cbNganh_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                cbMon.ItemsSource = null;
                cbMon.ItemsSource = listMonHoc.Where(x => x.MaNganh == cbNganh.SelectedValue.ToString());
            }catch(Exception ex) {  }
        }

        private void cbMon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                cbLop.ItemsSource = null;
                cbLop.ItemsSource = listLop.Where(x => x.MaMonHoc == cbMon.SelectedValue.ToString());
                if ((cbLop.ItemsSource as List<Lop>) == null || (cbLop.ItemsSource as List<Lop>).Count == 0)
                    dataGridDanhSachSV.ItemsSource = null;
            }
            catch (Exception ex) { }
        }

        private void cbLop_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                List<ThamGia> thamGias = db.ThamGia.Where(x => x.MaLop == cbLop.SelectedValue.ToString()).ToList();
                if (thamGias.Count != 0)
                {
                    listSV = new List<SinhVienVMM>();
                    db = new MyDbContext();
                    foreach (ThamGia thamgia in thamGias)
                    {
                        string tenMon = db.MonHoc.Find(cbMon.SelectedValue).TenMon;
                        string tenLop = db.Lop.Find(cbLop.SelectedValue).TenLop;

                        SinhVien sv = db.SinhVien.Find(thamgia.MaSinhVien);
                        SinhVienVMM svvmm = new SinhVienVMM(sv, tenMon, tenLop, thamgia.Diem);
                        listSV.Add(svvmm);
                    }
                    dataGridDanhSachSV.ItemsSource = listSV;
                }
                
            }catch(Exception ex) {}
        }

        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<SinhVienVMM> sinhViens = dataGridDanhSachSV.ItemsSource as List<SinhVienVMM>;
                

                if (sinhViens.Count != 0)
                {
                    if (sinhViens.Where(x => x.Diem > 10 || x.Diem < 0).ToList().Count != 0)
                    {
                        MessageBox.Show("Điểm không hợp lệ");
                    }
                    else
                    {
                        db = new MyDbContext();
                        List<ThamGia> listThamGia = db.ThamGia.Where(x => x.MaLop == cbLop.SelectedValue.ToString()).ToList();
                        if (listThamGia.Count != 0)
                        {
                            foreach (ThamGia thamGia in listThamGia)
                            {
                                ThamGia tg = db.ThamGia.Find(thamGia.MaLop, thamGia.MaSinhVien);
                                if (tg != null)
                                {
                                    tg.Diem = (float)Math.Round(sinhViens.FirstOrDefault(x => x.MaSinhVien == thamGia.MaSinhVien).Diem, 2);
                                    db.SaveChanges();
                                    dataGridDanhSachSV.Items.Refresh();
                                }
                            }
                        }
                        dataGridDanhSachSV.Items.Refresh();
                        MessageBox.Show("Đã lưu","Thông báo",MessageBoxButton.OK,MessageBoxImage.Information);
                    }
                    
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
