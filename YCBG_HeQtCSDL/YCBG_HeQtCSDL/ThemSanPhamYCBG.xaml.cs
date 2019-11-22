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
using YCBG_HeQtCSDL.ViewModel;

namespace YCBG_HeQtCSDL
{
    /// <summary>
    /// Interaction logic for ThemSanPhamYCBG.xaml
    /// </summary>
    public partial class ThemSanPhamYCBG : Window
    {
        string connectionString;
        public ThemSanPhamYCBG(string connectionString)
        {
            this.connectionString = connectionString;
            InitializeComponent();


            //ThemSanPhamYCBGVM themSanPhamYCBGVM = new ThemSanPhamYCBGVM();
            //themSanPhamYCBGVM.TenSanPham = "";
            //themSanPhamYCBGVM.NhaCungCap = "";
            //themSanPhamYCBGVM.SoLuong =null;
            //themSanPhamYCBGVM.GhiChu = null;
            //List<ThemSanPhamYCBGVM> themSanPhamYCBGVMs = new List<ThemSanPhamYCBGVM>();
            //themSanPhamYCBGVMs.Add(themSanPhamYCBGVM);
            //dtgThemSanPhamYCBG.ItemsSource = themSanPhamYCBGVMs;

            //List<ThemSanPhamYCBGVM> themSanPhamYCBGVMs = new List<ThemSanPhamYCBGVM>();
            //dtgThemSanPhamYCBG.ItemsSource = themSanPhamYCBGVMs;
            
        }

        private class CT {
            public string MaNCC;
            public string MaSP;
            public string MaYCBaoGia;
            public int SLSeMua;

            public CT(string ncc, string sp, string maYCBG, int sl)
            {
                this.MaNCC = ncc;
                MaSP = sp;
                MaYCBaoGia = maYCBG;
                SLSeMua = sl;
            }

        }

        private void createNewYCBG()
        {
            List<ChiTietBaoGiaVM> yeuCauBaoGiaVMs = new List<ChiTietBaoGiaVM>();

            using (var conn = new SqlConnection(this.connectionString))
            using (var command = new SqlCommand("sp_t1", conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                try
                {
                    
                    conn.Open();
                    // "," split fields same row
                    // "#" split multi row
                    List<CT> temp = new List<CT>();
                    temp.Add(new CT("1", "1", "1", 1));
                    temp.Add(new CT("1", "1", "1", 2));
                    temp.Add(new CT("1", "1", "1", 3));
                    temp.Add(new CT("1", "1", "1", 4));
                    temp.Add(new CT("1", "1", "1", 5));

                    DataTable table = new DataTable();
                    var colString = System.Type.GetType("System.String");
                    var colInt32 = System.Type.GetType("System.Int32");
                    table.Columns.Add("MaNCC", colString);
                    table.Columns.Add("MaSP", colString);
                    table.Columns.Add("MaYCBaoGia", colString);
                    table.Columns.Add("SLSeMua", colInt32);

                    temp.ForEach(t => {
                        table.Rows.Add(t.MaNCC, t.MaSP, t.MaYCBaoGia, t.SLSeMua);
                    });

                    command.Parameters.AddWithValue("@dsCTYCBG", table);
                    command.ExecuteNonQuery();
                    //var rdr = command.ExecuteReader();

                    //while (rdr.Read())
                    //{
                    //    ChiTietBaoGiaVM ct = new ChiTietBaoGiaVM();
                    //    ct.MaSP = rdr["MaSP"].ToString();
                    //    ct.TenNCC = rdr["MaNCC"].ToString();
                    //    ct.TenMatHang = rdr["MatHang"].ToString();
                    //    ct.SoLuong = int.Parse(rdr["SLSeMua"].ToString());

                    //    yeuCauBaoGiaVMs.Add(ct);
                    //}
                    //dtgThemSanPhamYCBG.ItemsSource = yeuCauBaoGiaVMs;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Error");
                }
                finally
                {
                    conn.Close();
                }

            }
        }

        private void btn_addYCBG_Click(object sender, RoutedEventArgs e)
        {
            createNewYCBG();
        }
    }
}
