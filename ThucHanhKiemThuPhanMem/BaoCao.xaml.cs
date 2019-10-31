using Aspose.Cells;
using Microsoft.Office.Interop.Excel;
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
    /// Interaction logic for BaoCao.xaml
    /// </summary>
    public partial class BaoCao : System.Windows.Window
    {
        MyDbContext db;
        List<SinhVienVMM> listSV;

        public BaoCao()
        {
            InitializeComponent();
            Load();
        }
        void Load()
        {
            db = new MyDbContext();
            listSV = new List<SinhVienVMM>();
            rdbGiangVien.IsChecked=true;

            cbGiangVien.ItemsSource = db.GiangVien.ToList();
            cbGiangVien.SelectedValuePath = "MaGiangVien";
            cbGiangVien.DisplayMemberPath = "TenGiangVien";
            cbGiangVien.SelectedIndex = 0;

            cbMonHoc.ItemsSource = db.MonHoc.ToList();
            cbMonHoc.SelectedValuePath = "MaMonHoc";
            cbMonHoc.DisplayMemberPath = "TenMon";
            cbGiangVien.SelectedIndex = 0;
        }
        private void btnXuat_Click(object sender, RoutedEventArgs e)
        {

        }

        private void rdbGiangVien_Checked(object sender, RoutedEventArgs e)
        {
            cbGiangVien.IsEnabled = true;
            cbMonHoc.IsEnabled = false;
        }

        private void rdbGiangVien_Unchecked(object sender, RoutedEventArgs e)
        {
            cbGiangVien.IsEnabled = false;
            cbMonHoc.IsEnabled = true;
        }

        private void cbGiangVien_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cbGiangVien.SelectedValue != null)
                {
                    db = new MyDbContext();
                    listSV = new List<SinhVienVMM>();
                    GiangVien gv = db.GiangVien.Find(cbGiangVien.SelectedValue);
                    List<Lop> listLop = db.Lop.Where(x => x.MaGiangVien == gv.MaGiangVien).ToList();
                    if (listLop.Count!=0)
                    {
                        foreach (Lop lop in listLop)
                        {
                            List<ThamGia> listThamGia = new List<ThamGia>();
                            foreach (ThamGia thamgia in db.ThamGia.Where(x => x.MaLop == lop.MaLop))
                            {
                                listThamGia.Add(thamgia);
                            }
                            foreach (ThamGia thamGia in listThamGia)
                            {
                                MonHoc monhoc = db.MonHoc.Find(lop.MaMonHoc);
                                SinhVien sv = db.SinhVien.Find(thamGia.MaSinhVien);
                                SinhVienVMM svvmm = new SinhVienVMM(sv, monhoc.TenMon, lop.TenLop, thamGia.Diem);
                                listSV.Add(svvmm);
                            }
                        }
                        dataGridDanhSachSV.ItemsSource = listSV;
                    } else
                        dataGridDanhSachSV.ItemsSource = null;
                }   
            }catch(Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void cbMonHoc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cbMonHoc.SelectedValue != null)
                {
                    db = new MyDbContext();
                    listSV = new List<SinhVienVMM>();
                    MonHoc monhoc = db.MonHoc.Find(cbMonHoc.SelectedValue);
                    List<Lop> lops = db.Lop.Where(x => x.MaMonHoc == monhoc.MaMonHoc).ToList();
                    if (lops.Count != 0)
                    {
                        foreach (Lop lop in lops)
                        {
                            List<ThamGia> thamGias = db.ThamGia.Where(x => x.MaLop == lop.MaLop).ToList();
                            foreach (ThamGia thamGia in thamGias)
                            {
                                SinhVien sv = db.SinhVien.Find(thamGia.MaSinhVien);
                                SinhVienVMM svvmm = new SinhVienVMM(sv, monhoc.TenMon, lop.TenLop, thamGia.Diem);
                                listSV.Add(svvmm);
                            }
                        }
                        dataGridDanhSachSV.ItemsSource = listSV;
                    }
                    else
                        dataGridDanhSachSV.ItemsSource = null;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void cbDauRot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                List<SinhVienVMM> tmpList = listSV;
                if (cbDauRot.SelectedIndex == 0)
                {
                    tmpList = tmpList.Where(x => x.Diem >= 4).ToList();
                    dataGridDanhSachSV.ItemsSource = null;
                    dataGridDanhSachSV.ItemsSource = tmpList;
                }
                else if(cbDauRot.SelectedIndex == 1)
                {
                    tmpList = tmpList.Where(x => x.Diem < 4).ToList();
                    dataGridDanhSachSV.ItemsSource = null;
                    dataGridDanhSachSV.ItemsSource = tmpList;
                }
            }catch(Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnXuatBaoCao_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook wb = excel.Workbooks.Add(XlSheetType.xlWorksheet);
                Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)excel.ActiveSheet;
                excel.Visible = false;
                int index = 2;
                int process = (dataGridDanhSachSV.ItemsSource as List<SinhVienVMM>).Count;

                ws.Cells[1, 1] = "Mã sinh viên";
                ws.Cells[1, 2] = "Tên sinh viên";
                ws.Cells[1, 3] = "Ngày sinh";
                ws.Cells[1, 4] = "Mã ngành";
                ws.Cells[1, 5] = "Tên môn học";
                ws.Cells[1, 6] = "Tên lớp";
                ws.Cells[1, 7] = "Điểm";

                foreach (SinhVienVMM sinhVien in (dataGridDanhSachSV.ItemsSource as List<SinhVienVMM>))
                {
                    ws.Cells[index, 1] = sinhVien.MaSinhVien;
                    ws.Cells[index, 2] = sinhVien.TenSinhVien;
                    ws.Cells[index, 3] = sinhVien.NgaySinh;
                    ws.Cells[index, 4] = sinhVien.MaNganh;
                    ws.Cells[index, 5] = sinhVien.TenMonHoc;
                    ws.Cells[index, 6] = sinhVien.TenLop;
                    ws.Cells[index, 7] = sinhVien.Diem;
                    index += 1;
                }
                ws.SaveAs(@path + "/BaoCao.xlsx", XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                excel.Quit();

                MessageBox.Show("Đã xuất");
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
            
        }
    }
}
