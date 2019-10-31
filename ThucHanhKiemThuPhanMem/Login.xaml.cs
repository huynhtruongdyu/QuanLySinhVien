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
using ThucHanhKiemThuPhanMem.Model.EF;

namespace ThucHanhKiemThuPhanMem
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnDangKy_Click(object sender, RoutedEventArgs e)
        {
            Register register = new Register();
            register.Owner = this;
            this.Hide();
            register.ShowDialog();
            this.Show();
        }

        private void btnDangNhap_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using(var db= new MyDbContext())
                {
                    if(db.TaiKhoan.Any(x=>x.Username==txtUsername.Text&& x.Password == txtPassword.Password))
                    {
                        MainWindow main = new MainWindow();
                        this.Close();
                        main.Show();
                    }
                    else
                        MessageBox.Show("Đăng nhập không thành công", "Thông tin", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }catch(Exception ex)
            {
                MessageBox.Show("Đăng nhập không thành công", "Thông tin", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
