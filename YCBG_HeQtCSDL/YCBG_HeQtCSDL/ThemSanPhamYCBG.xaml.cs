using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
        List<string> allMaNCC;
        List<string> allMaSP;
        List<ThemSanPhamYCBGVM> themSanPhamYCBGVMs;

        public ThemSanPhamYCBG(string connectionString)
        {
            this.connectionString = connectionString;
            InitializeComponent();

            themSanPhamYCBGVMs = new List<ThemSanPhamYCBGVM>();
            dtgThemSanPhamYCBG.ItemsSource = themSanPhamYCBGVMs;

            resetValue();
        }

        private void resetValue(string mancc = "", string masp = "")
        {
            getAllMaNCC(masp);
            getAllMaSP(mancc);

            cboMaSP.SelectedIndex = -1;
            cboMaNCC.SelectedIndex = -1;

            cboMaNCC.ItemsSource = allMaNCC;
            cboMaSP.ItemsSource = allMaSP;
            //txtGhiChu.Text = "";
            txtSoLuong.Text = "0";
            dtgThemSanPhamYCBG.Items.Refresh();
        }

         private ThemSanPhamYCBGVM create_New_CTYCBG(string sp = "", string ncc = "", int sl = 0, string note = "")
        {
            ThemSanPhamYCBGVM themSanPhamYCBGVM = new ThemSanPhamYCBGVM();
            themSanPhamYCBGVM.TenSanPham = sp;
            themSanPhamYCBGVM.NhaCungCap = ncc;
            themSanPhamYCBGVM.SoLuong = sl;
            themSanPhamYCBGVM.GhiChu = note;
            return themSanPhamYCBGVM;
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

        private void getAllMaNCC(string masp = "")
        {
            using (var conn = new SqlConnection(this.connectionString))
            using (var command = new SqlCommand("sp_get_all_maNCC", conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                try
                {
                    conn.Open();
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

            }
        }


        private void getAllMaSP(string mancc = "")
        {
            using (var conn = new SqlConnection(this.connectionString))
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

                    allMaSP = new List<string>();
                    while (rdr.Read())
                        allMaSP.Add(rdr["masp"].ToString());
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

        private void createNewYCBG()
        {
            List<ChiTietBaoGiaVM> yeuCauBaoGiaVMs = new List<ChiTietBaoGiaVM>();

            using (var conn = new SqlConnection(this.connectionString))
            using (var command = new SqlCommand("sp_add_YCBG", conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                try
                {
                    
                    conn.Open();

                    DataTable table = new DataTable();
                    var colString = System.Type.GetType("System.String");
                    var colInt32 = System.Type.GetType("System.Int32");
                    table.Columns.Add("MaNCC", colString);
                    table.Columns.Add("MaSP", colString);
                    table.Columns.Add("SLSeMua", colInt32);

                    themSanPhamYCBGVMs.ForEach(t => {
                        table.Rows.Add(t.NhaCungCap, t.TenSanPham, t.SoLuong);
                    });

                    command.Parameters.AddWithValue("@ctYCBG", table);
                    command.ExecuteNonQuery();

                    MessageBox.Show("Done");
                    this.Close();
                    //var rdr = command.ExecuteReader();

                    //while (rdr.Read())
                    //{
                    //    ChiTietBaoGiaVM ct = new ChiTietBaoGiaVM();
                    //    ct.MaSP = rdr["MaSP"].ToString();
                    //    ct.TenNCC = rdr["MaNCC"].ToString();
                    //    //ct.TenMatHang = rdr["MatHang"].ToString();
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

        
        private void btn_addCTYCBH_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cboMaSP.SelectedItem != null && cboMaNCC.SelectedItem != null)
                {
                    themSanPhamYCBGVMs.Add(create_New_CTYCBG(
                            cboMaSP.SelectedValue.ToString(),
                            cboMaNCC.SelectedValue.ToString(),
                            int.Parse(txtSoLuong.Text),
                            ""
                        ));
                    resetValue();
                }
                else
                {
                    MessageBox.Show("Các trường không hợp lệ", "Lỗi");
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Số lượng phải lớn hơn hoặc bằng 0", "Lỗi");
                txtSoLuong.Text = "0";
                txtSoLuong.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cboMaSP_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            getAllMaNCC(cboMaSP.SelectedValue == null ? "" : cboMaSP.SelectedValue.ToString());
            cboMaNCC.ItemsSource = allMaNCC;
        }

        private void cboMaNCC_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            getAllMaSP(cboMaNCC.SelectedValue == null ? "" : cboMaNCC.SelectedValue.ToString());
            cboMaSP.ItemsSource = allMaSP;
        }

        private void btn_XoaCTYCBH_Click(object sender, RoutedEventArgs e)
        {
            var index = dtgThemSanPhamYCBG.SelectedIndex;
            if(index != -1)
            {
                themSanPhamYCBGVMs.RemoveAt(index);
                dtgThemSanPhamYCBG.Items.Refresh();
            }
        }

        private void txtSoLuong_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
