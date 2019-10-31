using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for NhapSV.xaml
    /// </summary>
    public partial class NhapSV : Window
    {
        MyDbContext db;

        public NhapSV()
        {
            InitializeComponent();
            db = new MyDbContext();
            txtNganh.ItemsSource = db.Nganh.ToList();
            txtDiemThi.Text = txtDiemChuan.Text;
            txtNgaySinh.Text = "1/1/1999";
        }

        private void TxtNganh_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            db = new MyDbContext();
            Nganh obj = db.Nganh.Find(txtNganh.SelectedValue);
            txtDiemChuan.Text = obj.DiemChuan.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            double diemThi;
            DateTime ngaySinh = DateTime.Parse(txtNgaySinh.ToString());
            int tuoi = DateTime.Now.Year - ngaySinh.Year;
            db = new MyDbContext();

            if (String.IsNullOrEmpty(txtHoTen.Text) || String.IsNullOrEmpty(txtTHPT.Text) || String.IsNullOrEmpty(txtDiemThi.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (tuoi < 19 || tuoi >30)
            {
                MessageBox.Show("Tuổi không nhỏ hơn 19", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (double.TryParse(txtDiemThi.Text, out diemThi) == false)
            {
                MessageBox.Show("Vui lòng đúng điểm thi", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (double.Parse(txtDiemThi.Text) < double.Parse(txtDiemChuan.Text))
            {
                MessageBox.Show("Điểm thi không thể bé hơn điểm chuẩn", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (db.SinhVien.FirstOrDefault(x => x.TenSinhVien == txtHoTen.Text) != null)
                {
                    if(MessageBox.Show("Tên sinh viên đã tồn tại. Bạn có chắc muốn thêm?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        try
                        {
                            SinhVienDAO dao = new SinhVienDAO();
                            SinhVien sv = new SinhVien();
                            sv.MaSinhVien = Helper.getMSSV(txtNganh.SelectedValue.ToString());
                            sv.TenSinhVien = txtHoTen.Text;
                            sv.NgaySinh = DateTime.Parse(txtNgaySinh.Text);
                            if (txtNam.IsChecked == true)
                            {
                                sv.GioiTinh = false;
                            }
                            else
                            {
                                sv.GioiTinh = false;
                            }
                            sv.TruongTHPT = txtTHPT.Text;
                            sv.MaNganh = txtNganh.SelectedValue.ToString();
                            sv.DiemChuan = double.Parse(txtDiemChuan.Text);
                            sv.DiemThi = double.Parse(txtDiemThi.Text);
                            if (dao.Add(sv))
                            {
                                MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                                this.Close();
                            }
                        }
                        catch (Exception ex) { }
                    }
                }
                else
                {
                    try
                    {
                        SinhVienDAO dao = new SinhVienDAO();
                        SinhVien sv = new SinhVien();
                        sv.MaSinhVien = Model.DAO.Helper.getMSSV(txtNganh.SelectedValue.ToString());
                        sv.TenSinhVien = txtHoTen.Text;
                        sv.NgaySinh = DateTime.Parse(txtNgaySinh.Text);
                        if (txtNam.IsChecked == true)
                        {
                            sv.GioiTinh = false;
                        }
                        else
                        {
                            sv.GioiTinh = true;
                        }
                        sv.TruongTHPT = txtTHPT.Text;
                        sv.MaNganh = txtNganh.SelectedValue.ToString();
                        sv.DiemChuan = double.Parse(txtDiemChuan.Text);
                        sv.DiemThi = double.Parse(txtDiemThi.Text);
                        if(dao.Add(sv))
                        {
                            MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                            this.Close();
                        }
                        
                    }
                    catch (Exception ex) { }
                }
            }
        }
    }
}
