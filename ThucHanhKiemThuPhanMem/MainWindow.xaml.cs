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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ThucHanhKiemThuPhanMem.Model.EF;

namespace ThucHanhKiemThuPhanMem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MyDbContext db;
        public MainWindow()
        {
            InitializeComponent();
            
            //KhoiTao();

        }

        void KhoiTao()
        {
            db = new MyDbContext();
            if (db.Database.Exists()==false)
            {
                Model.DAO.Helper.InitializedGiangVien();
                Model.DAO.Helper.InitializedNganh();
                Model.DAO.Helper.InitializedThamSo();
                Model.DAO.Helper.IntitialzedSinhVien();
                Model.DAO.Helper.IntitialzedMonHoc();
            }
        }

        private void BtnNhapSinhVien_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            DanhSachSinhVien dsSV = new DanhSachSinhVien();
            dsSV.Owner = Application.Current.MainWindow;
            dsSV.ShowDialog();
            this.Show();
        }

        private void BtnPhanLop_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            DanhSachMonHoc dsMonHoc = new DanhSachMonHoc();
            dsMonHoc.Owner = Application.Current.MainWindow;
            dsMonHoc.ShowDialog();
            this.Show();
        }

        private void BtnNhapDiem_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            NhapDiem nhapDiem = new NhapDiem();
            nhapDiem.Owner = Application.Current.MainWindow;
            nhapDiem.ShowDialog();
            this.Show();
        }

        private void BtnTimKiem_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            TimKiem tiemKiem = new TimKiem();
            tiemKiem.Owner = Application.Current.MainWindow;
            tiemKiem.ShowDialog();
            this.Show();
        }

        private void BtnBaoCao_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            BaoCao baoCao = new BaoCao();
            baoCao.Owner = Application.Current.MainWindow;
            baoCao.ShowDialog();
            this.Show();
        }
    }
}
