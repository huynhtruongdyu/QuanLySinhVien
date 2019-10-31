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

namespace ThucHanhKiemThuPhanMem
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
        }

        private void btnDangKy_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(txtUsername.Text.Length!=0 && txtPassword.Password.Length != 0)
                {
                    TaiKhoanDAO dao = new TaiKhoanDAO();
                    
                    if(dao.Add(txtUsername.Text, txtPassword.Password))
                    {
                        MessageBox.Show("Đã đăng ký");
                        this.Close();
                    }
                    else 
                        MessageBox.Show("Lỗi đăng ký, vui lòng xem lại thông tin");
                }
            }catch(Exception ex) { MessageBox.Show("Lỗi", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error); }
            
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
