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
    /// Interaction logic for TimKiem.xaml
    /// </summary>
    public partial class TimKiem : Window
    {
        MyDbContext db;
        List<SinhVienVMM> listSV;
        public TimKiem()
        {
            InitializeComponent();
            Load();
        }
        void Load()
        {
            cbLuaChon.SelectedIndex = 0;
            if (listSV == null)
            {
                listSV = new List<SinhVienVMM>();
                db = new MyDbContext();
                List<SinhVien> tmpSV = db.SinhVien.OrderBy(x => x.TenSinhVien).ToList();
                foreach (SinhVien sinhVien in tmpSV)
                {
                    List<ThamGia> thamGias = db.ThamGia.Where(x => x.MaSinhVien == sinhVien.MaSinhVien).ToList();
                    if (thamGias.Count != 0)
                    {
                        foreach (ThamGia thamGia in thamGias)
                        {
                            Lop lop = db.Lop.FirstOrDefault(x => x.MaLop == thamGia.MaLop);
                            MonHoc monHoc = db.MonHoc.FirstOrDefault(x => x.MaMonHoc == lop.MaMonHoc);
                            SinhVienVMM sinhVienVMM = new SinhVienVMM(sinhVien, monHoc.TenMon, lop.TenLop, thamGia.Diem);
                            listSV.Add(sinhVienVMM);
                        }
                    }
                    else
                    {
                        SinhVienVMM sinhVienVMM = new SinhVienVMM(sinhVien);
                        listSV.Add(sinhVienVMM);
                    }
                }
                dataGridKetQua.ItemsSource = null;
                dataGridKetQua.ItemsSource = listSV.OrderBy(x => x.TenLop);
                dataGridKetQua.Items.Refresh();
            }
            else
            {
                dataGridKetQua.ItemsSource = listSV;
            }

        }

        private void cbLuaChon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbLuaChon.SelectedIndex == 0)
            {
                txtNoiDungTimKiem.IsEnabled = false;
                txtNoiDungTimKiem.Text = "";
            }
            else
            {
                txtNoiDungTimKiem.Text = "";
                txtNoiDungTimKiem.IsEnabled = true;
            }
                
        }
        
        private void btnTimKiem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbLuaChon.SelectedItem != null)
                {
                    //Ten Sv
                    if (cbLuaChon.SelectedIndex == 0)
                    {
                        db = new MyDbContext();
                        List<SinhVienVMM> tmp = new List<SinhVienVMM>();
                        if (!String.IsNullOrEmpty(txtTenSV.Text))
                        {
                            List<SinhVien> tmpSV = db.SinhVien.Where(x => x.TenSinhVien.ToLower().Contains(txtTenSV.Text.ToLower())).OrderByDescending(x => x.TenSinhVien).ToList();
                            foreach (SinhVien sinhVien in tmpSV)
                            {
                                List<ThamGia> thamGias = db.ThamGia.Where(x => x.MaSinhVien == sinhVien.MaSinhVien).ToList();
                                if (thamGias.Count != 0)
                                {
                                    foreach (ThamGia thamGia in thamGias)
                                    {
                                        Lop lop = db.Lop.FirstOrDefault(x => x.MaLop == thamGia.MaLop);
                                        MonHoc monHoc = db.MonHoc.FirstOrDefault(x => x.MaMonHoc == lop.MaMonHoc);
                                        SinhVienVMM sinhVienVMM = new SinhVienVMM(sinhVien, monHoc.TenMon, lop.TenLop, thamGia.Diem);
                                        tmp.Add(sinhVienVMM);
                                    }
                                }
                                else
                                {
                                    SinhVienVMM sinhVienVMM = new SinhVienVMM(sinhVien);
                                    tmp.Add(sinhVienVMM);
                                }
                            }
                            dataGridKetQua.ItemsSource = null;
                            dataGridKetQua.ItemsSource = tmp.OrderBy(x => x.TenLop);
                            dataGridKetQua.Items.Refresh();
                        }
                        else
                        {
                            List<SinhVien> tmpSV = db.SinhVien.OrderBy(x => x.TenSinhVien).ToList();
                            foreach (SinhVien sinhVien in tmpSV)
                            {
                                List<ThamGia> thamGias = db.ThamGia.Where(x => x.MaSinhVien == sinhVien.MaSinhVien).ToList();
                                if (thamGias.Count != 0)
                                {
                                    foreach (ThamGia thamGia in thamGias)
                                    {
                                        Lop lop = db.Lop.FirstOrDefault(x => x.MaLop == thamGia.MaLop);
                                        MonHoc monHoc = db.MonHoc.FirstOrDefault(x => x.MaMonHoc == lop.MaMonHoc);
                                        SinhVienVMM sinhVienVMM = new SinhVienVMM(sinhVien, monHoc.TenMon, lop.TenLop, thamGia.Diem);
                                        tmp.Add(sinhVienVMM);
                                    }
                                }
                                else
                                {
                                    SinhVienVMM sinhVienVMM = new SinhVienVMM(sinhVien);
                                    tmp.Add(sinhVienVMM);
                                }
                            }
                            dataGridKetQua.ItemsSource = null;
                            dataGridKetQua.ItemsSource = tmp.OrderBy(x => x.TenLop);
                            dataGridKetQua.Items.Refresh();
                        }

                    }
                    //Diem
                    else if (cbLuaChon.SelectedIndex == 1)
                    {
                        if (!String.IsNullOrEmpty(txtTenSV.Text))
                        {
                            List<SinhVienVMM> tmp = listSV.Where(x => x.TenSinhVien.ToLower().Contains(txtTenSV.Text.ToLower())).ToList();
                            if (!String.IsNullOrEmpty(txtNoiDungTimKiem.Text))
                            {
                                double diem;
                                if (Double.TryParse(txtNoiDungTimKiem.Text, out diem))
                                {

                                    tmp = tmp.Where(x => x.Diem == diem - 1 || x.Diem == diem || x.Diem == diem + 1).ToList();
                                    dataGridKetQua.ItemsSource = null;
                                    dataGridKetQua.ItemsSource = tmp.OrderBy(x => x.Diem);
                                    dataGridKetQua.Items.Refresh();
                                }
                                else
                                    MessageBox.Show("Vui lòng xem lại điểm đã nhập");
                            }
                            else
                            {
                                //dataGridKetQua.ItemsSource = null;
                                //dataGridKetQua.ItemsSource = listSV.OrderBy(x => x.TenLop);
                                //dataGridKetQua.Items.Refresh();
                                MessageBox.Show("Vui lòng nhập điểm hợp lệ");
                            }
                        }
                        else
                        {
                            if (!String.IsNullOrEmpty(txtNoiDungTimKiem.Text))
                            {
                                double diem;
                                if (Double.TryParse(txtNoiDungTimKiem.Text, out diem))
                                {
                                    List<SinhVienVMM> tmp = listSV.Where(x => x.Diem == diem - 1 || x.Diem == diem || x.Diem == diem + 1).ToList();
                                    dataGridKetQua.ItemsSource = null;
                                    dataGridKetQua.ItemsSource = tmp.OrderBy(x => x.Diem);
                                    dataGridKetQua.Items.Refresh();
                                }
                                else
                                    MessageBox.Show("Vui lòng xem lại điểm đã nhập");
                            }
                        }
                        
                    }
                    //Môn học
                    else if (cbLuaChon.SelectedIndex == 2)
                    {
                        List<SinhVienVMM> tmp = listSV;
                        if (!String.IsNullOrEmpty(txtTenSV.Text))
                        {
                            tmp = tmp.Where(x => x.TenSinhVien.ToLower().Contains(txtTenSV.Text.ToLower())).OrderByDescending(x=>x.TenSinhVien).ToList();
                            if (!String.IsNullOrEmpty(txtNoiDungTimKiem.Text))
                            {
                                tmp = tmp.Where(x => x.TenMonHoc.ToLower().Contains(txtNoiDungTimKiem.Text.ToLower())).OrderBy(x => x.TenMonHoc).ToList();
                                dataGridKetQua.ItemsSource = null;
                                dataGridKetQua.ItemsSource = tmp;
                                dataGridKetQua.Items.Refresh();
                            }
                            else
                            {
                                dataGridKetQua.ItemsSource = null;
                                dataGridKetQua.ItemsSource = tmp;
                                dataGridKetQua.Items.Refresh();
                            }
                        }
                        else
                        {
                            tmp = tmp.Where(x => x.TenMonHoc.ToLower().Contains(txtNoiDungTimKiem.Text.ToLower())).OrderBy(x => x.TenMonHoc).ToList();
                            dataGridKetQua.ItemsSource = null;
                            dataGridKetQua.ItemsSource = tmp;
                            dataGridKetQua.Items.Refresh();
                        }
                    }
                }
                else MessageBox.Show("Vui lòng chọn mục tìm kiếm");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void txtTenSV_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if (!String.IsNullOrEmpty(txtTenSV.Text))
            //{

            //    listSV = db.SinhVien.Where(x => x.TenSinhVien.Contains(txtTenSV.Text)).OrderBy(x => x.TenSinhVien).ToList();
            //    dataGridKetQua.ItemsSource = null;
            //    dataGridKetQua.ItemsSource = listSV;
            //    dataGridKetQua.Items.Refresh();
            //}
            //else
            //{
            //    listSV = db.SinhVien.ToList().GetRange(0, 100);
            //    dataGridKetQua.ItemsSource = null;
            //    dataGridKetQua.ItemsSource = listSV;
            //    dataGridKetQua.Items.Refresh();
            //}
        }

        private void txtNoiDungTimKiem_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if (cbLuaChon.SelectedIndex == 1)
            //{
            //    if (String.IsNullOrEmpty(txtNoiDungTimKiem.Text) == false)
            //    {
            //        double diem;
            //        try
            //        {
            //            if (double.TryParse(txtNoiDungTimKiem.Text, out diem))
            //            {
            //                db = new MyDbContext();
            //                List<SinhVien> list = db.SinhVien.Where(x => x.TenSinhVien.Contains(txtTenSV.Text) && x.DiemThi == diem).OrderBy(x => x.DiemThi).ToList();
            //                dataGridKetQua.ItemsSource = list;
            //                dataGridKetQua.Items.Refresh();

            //            }
            //        }
            //        catch (Exception ex) { }

            //    }
            //}
            //else if (cbLuaChon.SelectedIndex == 2)
            //{
            //    if (String.IsNullOrEmpty(txtNoiDungTimKiem.Text) == false)
            //    {
            //        db = new MyDbContext();
            //        List<MonHoc> tmpListMonHoc = db.MonHoc.Where(x => x.TenMon.Contains(txtNoiDungTimKiem.Text)).ToList();
            //        List<Lop> tmpListLop = new List<Lop>();
            //        List<ThamGia> tmpListThamGia = new List<ThamGia>();
            //        foreach (MonHoc item in tmpListMonHoc)
            //        {
            //            tmpListLop.AddRange(db.Lop.Where(x => x.MaMonHoc == item.MaMonHoc));
            //        }
            //        if (tmpListLop.Count != 0)
            //        {
            //            List<SinhVien> list = new List<SinhVien>();

            //            foreach (Lop lop in tmpListLop)
            //            {
            //                List<ThamGia> thamGias = db.ThamGia.Where(x => x.MaLop == lop.MaLop).ToList();
            //                if (thamGias.Count != 0)
            //                {
            //                    foreach (ThamGia thamgia in thamGias)
            //                    {
            //                        list.Add(db.SinhVien.FirstOrDefault(x => x.MaSinhVien == thamgia.MaSinhVien));
            //                    }
            //                }
            //            }
            //            dataGridKetQua.ItemsSource = list;
            //            dataGridKetQua.Items.Refresh();
            //        }
                    
            //    }
            //}
        }
    }
}
