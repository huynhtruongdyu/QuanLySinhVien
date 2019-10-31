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
    /// Interaction logic for CapNhatSinhVien.xaml
    /// </summary>
    public partial class CapNhatSinhVien : Window
    {

        SinhVien sv;
        MyDbContext db;
        public CapNhatSinhVien()
        {
            InitializeComponent();
            db = new MyDbContext();
            txtNganh.ItemsSource = db.Nganh.ToList();
            
            sv = db.SinhVien.Find(DanhSachSinhVien.MSSV);
            txtHoTen.Text = sv.TenSinhVien;
            if (sv.GioiTinh == false)
                txtNam.IsChecked = true;
            else
                txtNu.IsChecked = true;
            txtNgaySinh.Text = sv.NgaySinh.Date.ToShortDateString();
            txtTHPT.Text = sv.TruongTHPT;
            txtNganh.SelectedValue = sv.MaNganh;
            txtDiemThi.Text = sv.DiemThi.ToString();
        }

        private void btnCapNhat_Click(object sender, RoutedEventArgs e)
        {
            double diemThi;
            DateTime ngaySinh = DateTime.Parse(txtNgaySinh.ToString());
            int tuoi = DateTime.Now.Year - ngaySinh.Year;
            db = new MyDbContext();

            if (String.IsNullOrEmpty(txtHoTen.Text) || String.IsNullOrEmpty(txtTHPT.Text) || String.IsNullOrEmpty(txtDiemThi.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (tuoi < 19 )
            {
                MessageBox.Show("Tuổi không nhỏ hơn 19", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (double.TryParse(txtDiemThi.Text, out diemThi) == false)
            {
                MessageBox.Show("Vui lòng đúng điểm thi", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if ((double.Parse(txtDiemThi.Text) < double.Parse(txtDiemChuan.Text)) || (double.Parse(txtDiemThi.Text) > 30))
            {
                MessageBox.Show("Điểm thi không thể bé hơn điểm chuẩn & không lớn hơn 30", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (db.SinhVien.FirstOrDefault(x => x.TenSinhVien == txtHoTen.Text) != null)
                {
                    if (MessageBox.Show("Tên sinh viên đã tồn tại. Bạn có chắc muốn thêm?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        try
                        {
                            SinhVienDAO dao = new SinhVienDAO();
                            SinhVien nSv = db.SinhVien.Find(sv.MaSinhVien);
                            nSv.TenSinhVien = txtHoTen.Text;
                            nSv.NgaySinh = DateTime.Parse(txtNgaySinh.Text);
                            if (txtNam.IsChecked == true)
                            {
                                nSv.GioiTinh = false;
                            }
                            else
                            {
                                nSv.GioiTinh = false;
                            }
                            nSv.TruongTHPT = txtTHPT.Text;
                            nSv.MaNganh = txtNganh.SelectedValue.ToString();
                            nSv.DiemChuan = double.Parse(txtDiemChuan.Text);
                            nSv.DiemThi = double.Parse(txtDiemThi.Text);

                            if(dao.Update(nSv))
                            {
                                MessageBox.Show("Cập nhật thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
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
                        SinhVien nSv = db.SinhVien.Find(sv.MaSinhVien);
                        nSv.TenSinhVien = txtHoTen.Text;
                        nSv.NgaySinh = DateTime.Parse(txtNgaySinh.Text);
                        if (txtNam.IsChecked == true)
                        {
                            nSv.GioiTinh = false;
                        }
                        else
                        {
                            sv.GioiTinh = true;
                        }
                        nSv.TruongTHPT = txtTHPT.Text;
                        nSv.MaNganh = txtNganh.SelectedValue.ToString();
                        nSv.DiemChuan = double.Parse(txtDiemChuan.Text);
                        nSv.DiemThi = double.Parse(txtDiemThi.Text);
                        if (dao.Update(nSv))
                        {
                            MessageBox.Show("Cập nhật thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                            this.Close();
                        }
                    }
                    catch (Exception ex) { }
                }
            }
        }

        private void txtNganh_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            db = new MyDbContext();
            Nganh obj = db.Nganh.Find(txtNganh.SelectedValue);
            txtDiemChuan.Text = obj.DiemChuan.ToString();
        }
    }
}
