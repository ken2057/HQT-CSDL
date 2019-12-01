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
        public static List<object> gia_ton_sp(string connectionString,int masp, string mancc)
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
                    command.Parameters.AddWithValue("@mancc", mancc);
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

        public static Dictionary<int, string> getAllMaSP(string connectionString, string mancc = "")
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
                    command.Parameters.AddWithValue("@mancc", mancc);
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

        public static List<string> getAllMaNCC(string connectionString, int? masp = null)
        {
            List<string> allMaNCC = new List<string>();

            using (var conn = new SqlConnection(connectionString))
            using (var command = new SqlCommand("sp_get_all_maNCC", conn)
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

                    allMaNCC = new List<string>();
                    while (rdr.Read())
                        allMaNCC.Add(rdr["MaNCC"].ToString());
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Error");
                }
                finally
                {
                    conn.Close();
                }
                return allMaNCC;
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
                        listChiTIetYeuCauBaoGia.NhaCungCap = rdr["MaNCC"].ToString();
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
                            TenNCC = rdr["MaNCC"].ToString(),
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
    }
}
