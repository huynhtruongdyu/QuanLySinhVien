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
    /// Interaction logic for PhanLop.xaml
    /// </summary>
    public partial class PhanLop : Window
    {
        MyDbContext db;
        MonHoc monHoc;
        string tenLop;

        List<SinhVien> svListPre;
        List<SinhVien> svListCur;

        public PhanLop()
        {
            InitializeComponent();


            db = new MyDbContext();
            monHoc = DanhSachMonHoc.monHoc;
            txtMaGiangVien.ItemsSource = db.GiangVien.ToList();
            //_____________



            List<Lop> tmpLop = db.Lop.Where(x => x.MaMonHoc == monHoc.MaMonHoc).ToList();
            List<SinhVien> tmpSinhViens = db.SinhVien.Where(x=>x.MaNganh==monHoc.MaNganh).ToList();
            if (tmpLop != null)
            {
                foreach (var item in tmpLop)
                {
                    List<ThamGia> tmpThamGia = db.ThamGia.Where(x => x.MaLop == item.MaLop).ToList();
                    if (tmpThamGia != null)
                    {
                        foreach (var item2 in tmpThamGia)
                        {
                            tmpSinhViens.Remove(tmpSinhViens.Where(x => x.MaSinhVien == item2.MaSinhVien ).FirstOrDefault());
                        }   
                    }
                }
            }

            //__________________

            //dataGridDanhSachSV.ItemsSource = db.SinhVien.Where(x => x.MaNganh == monHoc.MaNganh).ToList();
            dataGridDanhSachSV.ItemsSource = tmpSinhViens;
            svListCur = new List<SinhVien>();
            svListPre = dataGridDanhSachSV.ItemsSource as List<SinhVien>;
            txtSiSo.Content = svListCur.Count() + "/40";
        }


        private void BtnLuu_Click(object sender, RoutedEventArgs e)
        {
            if (svListCur.Count != 0)
            {
                if (txtTenLop.Text != "Nhập tên lớp" && String.IsNullOrEmpty(txtTenLop.Text) == false) {
                    db = new MyDbContext();
                    ThamSo ts = db.ThamSo.Find(4);
                    LopDAO dao = new LopDAO();

                    //Tạo lớp
                    Lop lop = new Lop();
                    lop.MaLop = monHoc.MaMonHoc + "_L" + ts.GiaTri;
                    lop.TenLop = txtTenLop.Text;
                    lop.MaGiangVien = txtMaGiangVien.SelectedValue.ToString();
                    lop.MaMonHoc = monHoc.MaMonHoc;
                    dao.Add(lop);

                    //Cập nhật giá trị tham sô
                    ts.GiaTri = (int.Parse(ts.GiaTri) + 1).ToString();


                    for (int i = 0; i < svListCur.Count; i++)
                    {
                        ThamGia tg = new ThamGia();
                        tg.MaLop = lop.MaLop;
                        tg.MaSinhVien = svListCur[i].MaSinhVien;
                        db.ThamGia.Add(tg);
                    }
                    db.SaveChanges();
                    this.Close();
                }
            }

        }


        private void TxtTenLop_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtTenLop.Text == "Nhập tên lớp" && txtTenLop.IsFocused)
            {
                txtTenLop.Text = "";
            }
        }

        private void TxtTenLop_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtTenLop.Text.Length > 20)
            {
                txtTenLop.Text = tenLop;
            }
            else
            {
                tenLop = txtTenLop.Text;
            }
        }

        private void BtnChuyenToi_Click(object sender, RoutedEventArgs e)
        {
            if (svListCur.Count < 40 && dataGridDanhSachSV.SelectedItems.Count <= (40 - svListCur.Count))
            {
                if (txtTenLop.Text != "Nhập tên lớp" || String.IsNullOrEmpty(txtTenLop.Text) == false)
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
                    
            }
            txtSiSo.Content = svListCur.Count + "/40";
        }

        private void RefreshDatagird()
        {
            dataGridDanhSachSV.ItemsSource = null;
            dataGridDanhSachSVDuocchon.ItemsSource = null;

            dataGridDanhSachSVDuocchon.ItemsSource = svListCur;
            dataGridDanhSachSV.ItemsSource = svListPre;
        }

        private void BtnChuyenLui_Click(object sender, RoutedEventArgs e)
        {

            for (int i = 0; i < dataGridDanhSachSVDuocchon.SelectedItems.Count; i++)
            {
                svListPre.Add((SinhVien)dataGridDanhSachSVDuocchon.SelectedItems[i]);
                svListCur.Remove((SinhVien)dataGridDanhSachSVDuocchon.SelectedItems[i]);
            }
            RefreshDatagird();
            txtSiSo.Content = svListCur.Count + "/40";
        }
    }
}
