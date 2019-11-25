using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using YCBG_HeQtCSDL.Pages;
using YCBG_HeQtCSDL.ViewModel;

namespace YCBG_HeQtCSDL
{
    /// <summary>
    /// Interaction logic for list_CTYCBG.xaml
    /// </summary>
    ///
    
    public partial class list_CTYCBG : Window
    {
        public static ChiTietYeuCauBaoGiaVM chiTietYeuCauBaoGiaVM { get; set; }
        List<YeuCauBaoGiaVM> yeuCauBaoGiaVMs;
        public bool isClosed;
        string connectionString;
        ListChiTIetYeuCauBaoGia listChiTIetYeuCauBaoGia;
        List<ListChiTIetYeuCauBaoGia> listChiTIetYeuCauBaoGias;
        public list_CTYCBG(string connectionString)
        {
            this.connectionString = connectionString;
            InitializeComponent();
            get_listCTYCBG();
        }
        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            chiTietYeuCauBaoGiaVM = new ChiTietYeuCauBaoGiaVM();
            chiTietYeuCauBaoGiaVM.TenNCC = ((ListChiTIetYeuCauBaoGia)dtgList_YCBG.SelectedItem).NhaCungCap;
            chiTietYeuCauBaoGiaVM.MaSP = ((ListChiTIetYeuCauBaoGia)dtgList_YCBG.SelectedItem).TenSanPham;
            CTBaoGia cTBaoGia = new CTBaoGia(connectionString);
            //chiTietBaoGiaVM = new ChiTietBaoGiaVM(((ChiTietBaoGiaVM)chiTietBaoGiaVMTemp));
            //MessageBox.Show(yeuCauBaoGiaVM.MaYCBG);
            cTBaoGia.ShowDialog();
            // Some operations with this row

            //refresh when windows closed
            if (cTBaoGia.isClosed)
            {
                get_listCTYCBG();
                dtgList_YCBG.Items.Refresh();
            }
        }

        private void get_listCTYCBG()
        {
            SqlDataReader rdr = null;
            //SqlDataReader rdr_getTenSP = null;
            using (var conn = new SqlConnection(connectionString))
            using (var command = new SqlCommand("sp_get_CTYCBG", conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                try
                {
                    conn.Open();
                    command.Parameters.AddWithValue("@maYCBG", YCBaoGia.yeuCauBaoGiaVM.MaYCBG);

                    rdr = command.ExecuteReader();
                    listChiTIetYeuCauBaoGias = new List<ListChiTIetYeuCauBaoGia>();
                    while (rdr.Read())
                    {
                        listChiTIetYeuCauBaoGia = new ListChiTIetYeuCauBaoGia();
                        listChiTIetYeuCauBaoGia.NhaCungCap = rdr["MaNCC"].ToString();
                        listChiTIetYeuCauBaoGia.TenSanPham = rdr["TenSanPham"].ToString();
                        listChiTIetYeuCauBaoGia.SoLuong = int.Parse(rdr["SLSeMua"].ToString());
                        listChiTIetYeuCauBaoGia.TinhTrang = YCBaoGia.yeuCauBaoGiaVM.TinhTrang;
                        listChiTIetYeuCauBaoGias.Add(listChiTIetYeuCauBaoGia);
                    }
                    dtgList_YCBG.ItemsSource = listChiTIetYeuCauBaoGias;
                    //Lấy tên sản phẩm thông qua mã sp
                    //using (var con2 = new SqlConnection(connectionString))
                    //using (var getTenSP = new SqlCommand("sp_get_tenSP", con2)
                    //{
                    //    CommandType = CommandType.StoredProcedure
                    //})
                    //{
                    //    con2.Open();
                    //    getTenSP.Parameters.AddWithValue("@maSP", chiTietYeuCauBaoGiaVM.MaSP);
                    //    rdr_getTenSP = getTenSP.ExecuteReader();
                    //    while (rdr_getTenSP.Read())
                    //    {
                    //        chiTietYeuCauBaoGiaVM.TenSP = rdr_getTenSP["TenSanPham"].ToString();
                    //    }
                    //    con2.Close();
                    //}
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Lỗi");
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}
