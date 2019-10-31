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
    /// Interaction logic for CapNhatLop.xaml
    /// </summary>
    public partial class CapNhatLop : Window
    {
        Lop lop;
        MonHoc monHoc;
        MyDbContext db;
        List<ThamGia> svThamGia;
        List<SinhVien> svListPre;
        List<SinhVien> svListCur;

        public CapNhatLop()
        {
            InitializeComponent();

            Reload();
        }
        void Reload()
        {
            db = new MyDbContext();
            txtMaGiangVien.ItemsSource = db.GiangVien.ToList();
            lop = db.Lop.Find(DanhSachMonHoc.maLop);
            monHoc = db.MonHoc.Find(lop.MaMonHoc);
            svThamGia = db.ThamGia.Where(x => x.MaLop == lop.MaLop).ToList();
            txtTenLop.Text = lop.TenLop;
            txtMaGiangVien.SelectedValue = lop.MaGiangVien;


            List<Lop> tmpLop = db.Lop.Where(x => x.MaMonHoc == monHoc.MaMonHoc).ToList();
            List<SinhVien> tmpSinhViens = db.SinhVien.Where(x => x.MaNganh == monHoc.MaNganh).ToList();
            if (tmpLop != null)
            {
                foreach (var item in tmpLop)
                {
                    List<ThamGia> tmpThamGia = db.ThamGia.Where(x => x.MaLop == item.MaLop).ToList();
                    if (tmpThamGia != null)
                    {
                        foreach (var item2 in tmpThamGia)
                        {
                            tmpSinhViens.Remove(tmpSinhViens.Where(x => x.MaSinhVien == item2.MaSinhVien).FirstOrDefault());
                        }
                    }
                }
            }


            dataGridDanhSachSV.ItemsSource = tmpSinhViens;
            svListCur = new List<SinhVien>();
            foreach (var item in svThamGia)
            {
                svListCur.Add(db.SinhVien.Find(item.MaSinhVien));
            }
            svListPre = tmpSinhViens;
            dataGridDanhSachSVDuocchon.ItemsSource = svListCur;
            txtSiSo.Content = svListCur.Count() + "/40";
        }


        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {
            if (svListCur.Count != 0)
            {
                if (!String.IsNullOrEmpty(txtTenLop.Text))
                {
                    db = new MyDbContext();
                    ThamSo ts = db.ThamSo.Find(4);

                    //Cập nhật lớp
                    Lop newLop = db.Lop.Find(lop.MaLop);
                    newLop.TenLop = txtTenLop.Text;
                    newLop.MaGiangVien = txtMaGiangVien.SelectedValue.ToString();
                    newLop.MaMonHoc = monHoc.MaMonHoc;
                    LopDAO dao = new LopDAO();
                    dao.Update(newLop);

                    //Cập nhật giá trị tham sô
                    ts.GiaTri = (int.Parse(ts.GiaTri) + 1).ToString();

                    List<ThamGia> thamgias = db.ThamGia.Where(x => x.MaLop == lop.MaLop).ToList();
                    foreach (ThamGia item in thamgias)
                    {
                        db.ThamGia.Remove(item);
                    }
                    db.SaveChanges();

                    for (int i = 0; i < svListCur.Count; i++)
                    {
                        ThamGia tg = new ThamGia();
                        tg.MaLop = lop.MaLop;
                        tg.MaSinhVien = svListCur[i].MaSinhVien;
                        db.ThamGia.Add(tg);
                    }

                    db.SaveChanges();
                    Reload();
                    this.Close();
                }
                else
                    MessageBox.Show("Tên lớp không được để trống");                
            }
            else
                MessageBox.Show("Vui lòng chọn sinh viên");
        }

        private void btnChuyenToi_Click(object sender, RoutedEventArgs e)
        {
            if (svListCur.Count < 40 && dataGridDanhSachSV.SelectedItems.Count <= (40 - svListCur.Count))
            {
                for (int i = 0; i < dataGridDanhSachSV.SelectedItems.Count; i++)
                {
                    svListCur.Add((SinhVien)dataGridDanhSachSV.SelectedItems[i]);
                }
                for (int i = 0; i < svListCur.Count; i++)
                {
                    svListPre.Remove(svListCur[i]);
                }
                RefreshDatagird();
            }
            txtSiSo.Content = svListCur.Count + "/40";
        }

        private void btnChuyenLui_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < dataGridDanhSachSVDuocchon.SelectedItems.Count; i++)
            {
                svListPre.Add((SinhVien)dataGridDanhSachSVDuocchon.SelectedItems[i]);
                svListCur.Remove((SinhVien)dataGridDanhSachSVDuocchon.SelectedItems[i]);
            }
            RefreshDatagird();
            txtSiSo.Content = svListCur.Count + "/40";
            RefreshDatagird();
        }

        private void txtTenLop_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtTenLop.Text == "Nhập tên lớp" && txtTenLop.IsFocused)
            {
                txtTenLop.Text = "";
            }
        }

        private void RefreshDatagird()
        {
            dataGridDanhSachSV.ItemsSource = null;
            dataGridDanhSachSVDuocchon.ItemsSource = null;

            dataGridDanhSachSVDuocchon.ItemsSource = svListCur;
            dataGridDanhSachSV.ItemsSource = svListPre;
        }

    }
}
