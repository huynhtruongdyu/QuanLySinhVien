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
    /// Interaction logic for DanhSachSinhVien.xaml
    /// </summary>
    public partial class DanhSachSinhVien : Window
    {
        SinhVienDAO dao;
        MyDbContext db;
        public static string MSSV;
        public DanhSachSinhVien()
        {
            InitializeComponent();

            Reload();

        }

        void Reload()
        {
            dao = new SinhVienDAO();
            dataGridDanhSachSV.ItemsSource = null;
            dataGridDanhSachSV.ItemsSource = dao.GetSinhViens();
            dataGridDanhSachSV.Items.Refresh();
        }

        private void BtnThemSV_Click(object sender, RoutedEventArgs e)
        {
            NhapSV nhapSV = new NhapSV();
            nhapSV.ShowInTaskbar = false;
            nhapSV.Owner = this;
            nhapSV.ShowDialog();
            dataGridDanhSachSV.ItemsSource = dao.GetSinhViens();
            Reload();
        }

        private void btnCapNhatSV_Click(object sender, RoutedEventArgs e)
        {

            List<SinhVien> listSV = new List<SinhVien>(); ;
            try
            {
                var obj = dataGridDanhSachSV.SelectedItems;
                if (obj == null)
                    MessageBox.Show("Vui lòng chọn sinh viên cần xóa", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                {
                    db = new MyDbContext();
                    foreach (var item in obj)
                    {
                        SinhVien sv = item as SinhVien;
                        listSV.Add(sv);
                    }
                    foreach (var item in listSV)
                    {
                        MSSV = (item as SinhVien).MaSinhVien;
                        CapNhatSinhVien c = new CapNhatSinhVien();
                        c.Owner = this;
                        c.ShowDialog();
                    }
                    Reload();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information); }
            //finally
            //{
            //    if (listSV.Count != 0)
            //    {
            //        MSSV = listSV[0].MaSinhVien;
            //        CapNhatSinhVien c = new CapNhatSinhVien();
            //        c.Owner = this;
            //        c.ShowDialog();
            //    }
            //}
        }

        private void btnXoaSV_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var obj = dataGridDanhSachSV.SelectedItems;
                if (obj == null)
                    MessageBox.Show("Vui lòng chọn sinh viên cần xóa", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                {
                    SinhVienDAO dao = new SinhVienDAO();
                    db = new MyDbContext();
                    List<SinhVien> listSV = new List<SinhVien>();
                    foreach (var item in obj)
                    {
                        SinhVien sv = item as SinhVien;
                        listSV.Add(sv);
                    }
                    for (int i = 0; i < listSV.Count; i++)
                    {
                        if (String.IsNullOrEmpty((listSV[i] as SinhVien).MaSinhVien) == false)
                        {
                            SinhVien sv = db.SinhVien.Find((listSV[i] as SinhVien).MaSinhVien);
                            dao.Remove(sv.MaSinhVien);
                        }

                    }
                    Reload();
                    MessageBox.Show("Đã xóa", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information); }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
