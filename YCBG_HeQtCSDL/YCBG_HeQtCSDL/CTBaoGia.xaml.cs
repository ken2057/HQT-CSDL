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
        ChiTietYeuCauBaoGiaVM chiTietYeuCauBaoGiaVM;
        List<ChiTietBaoGiaVM> chiTietBaoGiaVMs;

        public CTBaoGia(string connectionString)
        {
            InitializeComponent();
            this.connectionString = connectionString;
            isClosed = false;

            get_CTYCBH();
        }
        private void get_CTYCBH()
        {
            chiTietBaoGiaVMs = new List<ChiTietBaoGiaVM>();

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
                    command.Parameters.AddWithValue("@maYCBG", main_YCBG.yeuCauBaoGiaVM.MaYCBG);

                    rdr = command.ExecuteReader();

                    while (rdr.Read())
                    {
                        chiTietYeuCauBaoGiaVM = new ChiTietYeuCauBaoGiaVM();
                        chiTietYeuCauBaoGiaVM.NgayYCBG = main_YCBG.yeuCauBaoGiaVM.NgayYCBG;
                        chiTietYeuCauBaoGiaVM.MaNhanVien = main_YCBG.yeuCauBaoGiaVM.MaNV;
                        chiTietYeuCauBaoGiaVM.TenNCC = rdr["MaNCC"].ToString();
                        chiTietYeuCauBaoGiaVM.MaSP = rdr["MaSP"].ToString();
                        chiTietYeuCauBaoGiaVM.SL = rdr["SLSeMua"].ToString();
                        chiTietYeuCauBaoGiaVM.Gia = rdr["GiaDaBao"].ToString();
                        chiTietYeuCauBaoGiaVM.TenSP = rdr["TenSanPham"].ToString();
                    }

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

                    lbNgay.Content = chiTietYeuCauBaoGiaVM.NgayYCBG;
                    lbNguoiPhuTrach.Content = chiTietYeuCauBaoGiaVM.MaNhanVien;
                    lbNCC.Content = chiTietYeuCauBaoGiaVM.TenNCC;
                    lbMaSP.Content = chiTietYeuCauBaoGiaVM.MaSP;
                    lbTenSP.Content = chiTietYeuCauBaoGiaVM.TenSP;
                    lbSL.Content = chiTietYeuCauBaoGiaVM.SL;
                    lbGiaDaBao.Content = chiTietYeuCauBaoGiaVM.Gia;
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
