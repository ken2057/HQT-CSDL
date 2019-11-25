using System;
using System.Collections.Generic;
using System.Configuration;
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
using YCBG_HeQtCSDL.ViewModel;

namespace YCBG_HeQtCSDL
{
    /// <summary>
    /// Interaction logic for CTBaoGia.xaml
    /// </summary>
    public partial class CTBaoGia : Window
    {
        public bool isClosed;
        string connectionString;

        public CTBaoGia(string connectionString)
        {
            InitializeComponent();
            this.connectionString = connectionString;
            isClosed = false;

            get_CTYCBH();
        }
        private void get_CTYCBH()
        {

            SqlDataReader rdr = null;
            //SqlDataReader rdr_getTenSP = null;
            using (var conn = new SqlConnection(connectionString))
            using (var command = new SqlCommand("sp_getDetailCTYCBG", conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                try
                {
                    conn.Open();
                    command.Parameters.AddWithValue("@maYCBaoGia", main_YCBG.yeuCauBaoGiaVM.MaYCBG);
                    command.Parameters.AddWithValue("@maNCC", list_CTYCBG.chiTietYeuCauBaoGiaVM.TenNCC);
                    command.Parameters.AddWithValue("@maSP", list_CTYCBG.chiTietYeuCauBaoGiaVM.MaSP);
                    rdr = command.ExecuteReader();
                    
                    while (rdr.Read())
                    {
                        list_CTYCBG.chiTietYeuCauBaoGiaVM = new ChiTietYeuCauBaoGiaVM();
                        list_CTYCBG.chiTietYeuCauBaoGiaVM.NgayYCBG = main_YCBG.yeuCauBaoGiaVM.NgayYCBG;
                        list_CTYCBG.chiTietYeuCauBaoGiaVM.MaNhanVien = main_YCBG.yeuCauBaoGiaVM.MaNV;
                        list_CTYCBG.chiTietYeuCauBaoGiaVM.TenNCC = rdr["MaNCC"].ToString();
                        list_CTYCBG.chiTietYeuCauBaoGiaVM.MaSP = rdr["MaSP"].ToString();
                        list_CTYCBG.chiTietYeuCauBaoGiaVM.SL = rdr["SLSeMua"].ToString();
                        list_CTYCBG.chiTietYeuCauBaoGiaVM.Gia = rdr["GiaDaBao"].ToString();
                        list_CTYCBG.chiTietYeuCauBaoGiaVM.TenSP = rdr["TenSanPham"].ToString();
                    }

                    //Lấy tên sản phẩm thông qua mã sp
                    //using (var con2 = new SqlConnection(connectionString))
                    //using (var getTenSP = new SqlCommand("sp_get_tenSP", con2)
                    //{
                    //    CommandType = CommandType.StoredProcedure
                    //})
                    //{
                    //    con2.Open();
                    //    getTenSP.Parameters.AddWithValue("@maSP", list_CTYCBG.chiTietYeuCauBaoGiaVM.MaSP);
                    //    rdr_getTenSP = getTenSP.ExecuteReader();
                    //    while (rdr_getTenSP.Read())
                    //    {
                    //        list_CTYCBG.chiTietYeuCauBaoGiaVM.TenSP = rdr_getTenSP["TenSanPham"].ToString();
                    //    }
                    //    con2.Close();
                    //}

                    lbNgay.Content = list_CTYCBG.chiTietYeuCauBaoGiaVM.NgayYCBG;
                    lbNguoiPhuTrach.Content = list_CTYCBG.chiTietYeuCauBaoGiaVM.MaNhanVien;
                    lbNCC.Content = list_CTYCBG.chiTietYeuCauBaoGiaVM.TenNCC;
                    lbMaSP.Content = list_CTYCBG.chiTietYeuCauBaoGiaVM.MaSP;
                    lbTenSP.Content = list_CTYCBG.chiTietYeuCauBaoGiaVM.TenSP;
                    lbSL.Content = list_CTYCBG.chiTietYeuCauBaoGiaVM.SL;
                    lbGiaDaBao.Content = list_CTYCBG.chiTietYeuCauBaoGiaVM.Gia;
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

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            isClosed = true;
        }
    }
}
