using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using YCBG_HeQtCSDL.ViewModel;

namespace YCBG_HeQtCSDL.Func
{
    class getData
    {
        public static List<object> gia_ton_sp(string connectionString,int masp, string MaNPP)
        {
            SqlDataReader rdr = null;

            using (var conn = new SqlConnection(connectionString))
            using (var command = new SqlCommand("sp_get_gia_ton_sp", conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                int slTon = 0;
                decimal gia = 0;

                try
                {
                    conn.Open();

                    command.Parameters.AddWithValue("@masp", masp);
                    command.Parameters.AddWithValue("@MaNPP", MaNPP);
                    rdr = command.ExecuteReader();


                    while (rdr.Read())
                    {
                        gia = decimal.Parse(rdr["giamua"].ToString());
                        slTon = int.Parse(rdr["SoLuongTon"].ToString());
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Error");
                }
                finally
                {
                    conn.Close();
                }
                return new List<object>() { slTon, gia };
            }
        }

        public static Dictionary<int, string> getAllMaSP(string connectionString, string MaNPP = "")
        {
            Dictionary<int, string> allMaSP = new Dictionary<int, string>();
            using (var conn = new SqlConnection(connectionString))
            using (var command = new SqlCommand("sp_get_all_masp", conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                try
                {
                    conn.Open();
                    command.Parameters.AddWithValue("@MaNPP", MaNPP);
                    var rdr = command.ExecuteReader();

                    allMaSP = new Dictionary<int, string>();
                    while (rdr.Read())
                        allMaSP.Add(int.Parse(rdr["masp"].ToString()), rdr["TenSanPham"].ToString());
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Error");
                }
                finally
                {
                    conn.Close();
                }

                return allMaSP;
            }
        }

        public static List<string> getAllMaNPP(string connectionString, int? masp = null)
        {
            List<string> allMaNPP = new List<string>();

            using (var conn = new SqlConnection(connectionString))
            using (var command = new SqlCommand("sp_get_all_MaNPP", conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                try
                {
                    conn.Open();
                    // không thể dùng toán tử ? để truyền int và DBNull cùng 1 dòng
                    if (masp == null)
                        command.Parameters.AddWithValue("@masp", DBNull.Value);
                    else
                        command.Parameters.AddWithValue("@masp", masp);

                    var rdr = command.ExecuteReader();

                    allMaNPP = new List<string>();
                    while (rdr.Read())
                        allMaNPP.Add(rdr["MaNPP"].ToString());
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Error");
                }
                finally
                {
                    conn.Close();
                }
                return allMaNPP;
            }
        }

        public static List<ListChiTIetYeuCauBaoGia> get_listCTYCBG(string connectionString, YeuCauBaoGiaVM ycbgVM)
        {
            List<ListChiTIetYeuCauBaoGia> listChiTIetYeuCauBaoGias = new List<ListChiTIetYeuCauBaoGia>();
            ListChiTIetYeuCauBaoGia listChiTIetYeuCauBaoGia;

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
                    command.Parameters.AddWithValue("@maYCBG", ycbgVM.MaYCBG);

                    rdr = command.ExecuteReader();
                    listChiTIetYeuCauBaoGias = new List<ListChiTIetYeuCauBaoGia>();
                    while (rdr.Read())
                    {
                        listChiTIetYeuCauBaoGia = new ListChiTIetYeuCauBaoGia();
                        listChiTIetYeuCauBaoGia.NhaPhanPhoi = rdr["MaNPP"].ToString();
                        listChiTIetYeuCauBaoGia.TenSanPham = rdr["TenSanPham"].ToString();
                        listChiTIetYeuCauBaoGia.SoLuong = int.Parse(rdr["SLSeMua"].ToString());
                        listChiTIetYeuCauBaoGia.GiaDaBao = decimal.Parse(rdr["GiaDaBao"].ToString());
                        listChiTIetYeuCauBaoGia.TinhTrang = ycbgVM.TinhTrang;
                        listChiTIetYeuCauBaoGia.MaSP = int.Parse(rdr["MaSP"].ToString());

                        listChiTIetYeuCauBaoGias.Add(listChiTIetYeuCauBaoGia);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Lỗi");
                }
                finally
                {
                    conn.Close();
                }
                return listChiTIetYeuCauBaoGias;
            }
        }

        public static List<ChiTietYeuCauBaoGiaVM> get_min_CTYCBG(string connectionString, string maYCBG)
        {
            List<ChiTietYeuCauBaoGiaVM> lstCTYCBG = new List<ChiTietYeuCauBaoGiaVM>();

            SqlDataReader rdr = null;
            using (var conn = new SqlConnection(connectionString))
            using (var command = new SqlCommand("sp_get_min_CTYCBG", conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                try
                {
                    conn.Open();
                    command.Parameters.AddWithValue("@maYCBG", maYCBG);

                    rdr = command.ExecuteReader();
                    
                    while (rdr.Read())
                    {
                        lstCTYCBG.Add(new ChiTietYeuCauBaoGiaVM
                        {
                            MaSP = rdr["maSP"].ToString(),
                            TenNPP = rdr["MaNPP"].ToString(),
                            SL = rdr["SLSeMua"].ToString()
                        });
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Lỗi");
                }
                finally
                {
                    conn.Close();
                }
                return lstCTYCBG;
            }
        }

        public static List<YeuCauBaoGiaVM> get_YCBG(string connectionString, DateTime? ngayYCBG = null)
        {

            List<YeuCauBaoGiaVM> yeuCauBaoGiaVMs = new List<YeuCauBaoGiaVM>();
            YeuCauBaoGiaVM yeuCauBaoGiaVM;
            SqlDataReader rdr = null;

            using (var conn = new SqlConnection(connectionString))
            using (var command = new SqlCommand("sp_get_ycbg", conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                try
                {
                    conn.Open();
                    if (ngayYCBG == null)
                        command.Parameters.AddWithValue("@ngayYCBG", DBNull.Value);
                    else
                        command.Parameters.AddWithValue("@ngayYCBG", ngayYCBG);

                    rdr = command.ExecuteReader();

                    while (rdr.Read())
                    {
                        yeuCauBaoGiaVM = new YeuCauBaoGiaVM();
                        yeuCauBaoGiaVM.MaYCBG = rdr["MaYCBaoGia"].ToString();
                        yeuCauBaoGiaVM.NgayYCBG = rdr["NgayYCBaoGia"].ToString();
                        yeuCauBaoGiaVM.TinhTrang = rdr["TinhTrang"].ToString();
                        yeuCauBaoGiaVM.MaNV = rdr["MaNV"].ToString();
                        yeuCauBaoGiaVMs.Add(yeuCauBaoGiaVM);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Lỗi");
                }
                finally
                {
                    conn.Close();
                }
                return yeuCauBaoGiaVMs;
            }
        }

        public static List<EF.KeHoachMuaHang> get_KHMua(string connectionString, string maNV = "")
        {

            List<EF.KeHoachMuaHang> listKHMua = new List<EF.KeHoachMuaHang>();
            EF.KeHoachMuaHang khMua;
            SqlDataReader rdr = null;

            using (var conn = new SqlConnection(connectionString))
            using (var command = new SqlCommand("sp_get_kehoachmua", conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                try
                {
                    conn.Open();
                    command.Parameters.AddWithValue("@maNV", maNV);

                    rdr = command.ExecuteReader();

                    while (rdr.Read())
                    {
                        khMua = new EF.KeHoachMuaHang();
                        khMua.MaNV = rdr["MaNV"].ToString();
                        khMua.MaKeHoachMuaHang = rdr["MaKeHoachMuaHang"].ToString();
                        khMua.NgayBatDauKeHoach = DateTime.Parse(rdr["NgayBatDauKeHoach"].ToString());
                        khMua.DinhKyNgayMua = byte.Parse(rdr["DinhKyNgayMua"].ToString());
                        listKHMua.Add(khMua);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Lỗi");
                }
                finally
                {
                    conn.Close();
                }
                return listKHMua;
            }
        }

        public static List<CTKHMuaVM> get_CTKHMua(string connectionString, string maKHMua = "")
        {

            List<CTKHMuaVM> listCTKHMua = new List<CTKHMuaVM>();
            CTKHMuaVM CTKHMua;
            SqlDataReader rdr = null;

            using (var conn = new SqlConnection(connectionString))
            using (var command = new SqlCommand("sp_get_ctkhmua", conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                try
                {
                    conn.Open();
                    command.Parameters.AddWithValue("@maKHMua", maKHMua);

                    rdr = command.ExecuteReader();

                    while (rdr.Read())
                    {
                        CTKHMua = new CTKHMuaVM();
                        CTKHMua.MaNPP = rdr["MaNPP"].ToString();
                        CTKHMua.TenSP = rdr["TenSanPham"].ToString();
                        CTKHMua.MaSP = int.Parse(rdr["MaSP"].ToString());
                        CTKHMua.SLMua = int.Parse(rdr["SLDuTinhMua"].ToString());
                        listCTKHMua.Add(CTKHMua);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Lỗi");
                }
                finally
                {
                    conn.Close();
                }
                return listCTKHMua;
            }
        }

        public static List<EF.DonMuaHang> get_all_donMuaHang(string connectionString, string maNV = "")
        {
            List<EF.DonMuaHang> lstDonMua = new List<EF.DonMuaHang>();
            EF.DonMuaHang donMua = new EF.DonMuaHang();
            SqlDataReader rdr = null;

            using (var conn = new SqlConnection(connectionString))
            using (var command = new SqlCommand("sp_get_all_donMua", conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                try
                {
                    conn.Open();
                    command.Parameters.AddWithValue("@maNV", maNV);

                    rdr = command.ExecuteReader();

                    while (rdr.Read())
                    {
                        donMua = new EF.DonMuaHang();
                        donMua.MaDonMuaHang = rdr["MaDonMuaHang"].ToString();
                        donMua.NguoiPhuTrachMua = rdr["NguoiPhuTrachMua"].ToString();
                        donMua.NgayDat = DateTime.Parse(rdr["NgayDat"].ToString());
                        donMua.TinhTrang = rdr["TinhTrang"].ToString();
                        string tongTien = rdr["TongTienMua"].ToString();
                        donMua.TongTienMua = tongTien == "" ? 0 : decimal.Parse(tongTien);
                        //
                        if (rdr["ThoiGianGiao"].ToString() == "")
                            donMua.ThoiGianGiao = null;
                        else
                            donMua.ThoiGianGiao = DateTime.Parse(rdr["ThoiGianGiao"].ToString());

                        lstDonMua.Add(donMua);
                    }

                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Lỗi");
                }
                finally
                {
                    conn.Close();
                }
                return lstDonMua;
            }
        }

        public static List<ViewModel.CTHDMua> get_ctMua(string connectionString, string maHDMua)
        {

            List<CTHDMua> listCTMua = new List<CTHDMua>();
            CTHDMua ctMua;
            SqlDataReader rdr = null;

            using (var conn = new SqlConnection(connectionString))
            using (var command = new SqlCommand("sp_get_ctmua", conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                try
                {
                    conn.Open();
                    command.Parameters.AddWithValue("@MaDonMuaHang", maHDMua);

                    rdr = command.ExecuteReader();

                    while (rdr.Read())
                    {
                        ctMua = new CTHDMua();
                        ctMua.MaNPP = rdr["MaNPP"].ToString();
                        ctMua.TenSP = rdr["TenSanPham"].ToString();
                        ctMua.MaSP = int.Parse(rdr["MaSP"].ToString());
                        ctMua.SoLuong = int.Parse(rdr["SLMua"].ToString());
                        ctMua.Gia = double.Parse(rdr["DonGia"].ToString());

                        listCTMua.Add(ctMua);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Lỗi");
                }
                finally
                {
                    conn.Close();
                }
                return listCTMua;
            }
        }
    }
}
