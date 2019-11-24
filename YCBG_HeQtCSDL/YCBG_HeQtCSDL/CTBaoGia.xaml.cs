using System;
using System.Collections.Generic;
<<<<<<< HEAD
using System.Configuration;
using System.Data;
=======
using System.Data;
>>>>>>> 94ec9e8c7ece0a560bb9796508ff575386f341f6
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
        string connectionString;
        ChiTietYeuCauBaoGiaVM chiTietYeuCauBaoGiaVM;
        public CTBaoGia(string connectionString)
        {
            InitializeComponent();
<<<<<<< HEAD
            this.connectionString = connectionString; // :)

            get_CTYCBH();
            //MessageBox.Show(connectionString);
        }
        private void get_CTYCBH()
        {
            List<ChiTietBaoGiaVM> chiTietBaoGiaVMs = new List<ChiTietBaoGiaVM>();
            SqlDataReader rdr = null; 
            SqlDataReader rdr_getTenSP = null;
            using (var conn = new SqlConnection(connectionString))
            using (var command = new SqlCommand("sp_get_ctbg", conn) 
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                try
                {
                    conn.Open();
                    command.Parameters.AddWithValue("@maYCBG", main_YCBG.yeuCauBaoGiaVM.MaYCBG); // :)
                    
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
                    }

                    //Lấy tên sản phẩm thông qua mã sp
                    using (var con2 = new SqlConnection(connectionString))
                    using (var getTenSP = new SqlCommand("sp_get_tenSP", con2)
                    {
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        con2.Open();
                        getTenSP.Parameters.AddWithValue("@maSP", chiTietYeuCauBaoGiaVM.MaSP);
                        rdr_getTenSP = getTenSP.ExecuteReader();
                            while (rdr_getTenSP.Read())
                            {
                                chiTietYeuCauBaoGiaVM.TenSP = rdr_getTenSP["TenSanPham"].ToString();
                            }
                        con2.Close();
                    }
                        
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
                    MessageBox.Show(e.ToString(), "Lỗi");
                }
                finally
                {
                    conn.Close();
                }
=======
            this.connectionString = connectionString;
            //get_CTYCBH();
        }
        //private void get_CTYCBH()
        //{
        //    ChiTietBaoGiaVM yeuCauBaoGiaVM = new ChiTietBaoGiaVM();
        //    List<ChiTietBaoGiaVM> yeuCauBaoGiaVMs = new List<ChiTietBaoGiaVM>();
        //    SqlDataReader rdr = null;

        //    using (var conn = new SqlConnection(this.connectionString))
        //    using (var command = new SqlCommand("sp_get_ycbg", conn)
        //    {
        //        CommandType = CommandType.StoredProcedure
        //    })
        //    {
        //        try
        //        {
        //            conn.Open();
        //            rdr = command.ExecuteReader();

        //            while (rdr.Read())
        //            {
        //                yeuCauBaoGiaVM = new YeuCauBaoGiaVM();
        //                yeuCauBaoGiaVM.MaYCBG = rdr["MaYCBaoGia"].ToString();
        //                yeuCauBaoGiaVM.NgayYCBG = rdr["NgayYCBaoGia"].ToString();
        //                yeuCauBaoGiaVM.TinhTrang = rdr["TinhTrang"].ToString();
        //                yeuCauBaoGiaVM.MaNV = rdr["MaNV"].ToString();
        //                yeuCauBaoGiaVMs.Add(yeuCauBaoGiaVM);
        //            }
        //            dtgYCBG.ItemsSource = yeuCauBaoGiaVMs;
        //        }
        //        catch (Exception e)
        //        {
        //            MessageBox.Show(e.Message);
        //        }
        //        finally
        //        {
        //            conn.Close();
        //        }
>>>>>>> 94ec9e8c7ece0a560bb9796508ff575386f341f6

        //    }
        //}
    }
}
