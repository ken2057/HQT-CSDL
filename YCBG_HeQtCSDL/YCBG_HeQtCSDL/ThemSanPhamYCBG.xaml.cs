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
        Dictionary<int, string> allMaSP;
        List<ThemSanPhamYCBGVM> themSanPhamYCBGVMs;
        public bool isClosed;

        public ThemSanPhamYCBG(string connectionString)
        {
            isClosed = false;
            this.connectionString = connectionString;
            InitializeComponent();

            themSanPhamYCBGVMs = new List<ThemSanPhamYCBGVM>();
            dtgThemSanPhamYCBG.ItemsSource = themSanPhamYCBGVMs;

            resetValue();
        }

        private void resetValue(int? masp = null, string mancc = "")
        {
            getAllMaNCC(masp);
            getAllMaSP(mancc);
            // 
            cboMaNCC.SelectedIndex = -1;
            cboMaNCC.ItemsSource = allMaNCC;
            // do không thể set Index = -1 khi cboMaSP đang reference tới Dictionary
            // nên tạo 1 List rỗng để set Index = -1
            cboMaSP.ItemsSource = new List<string>();
            cboMaSP.SelectedIndex = -1;
            //
            cboMaSP.ItemsSource = allMaSP;
            cboMaSP.SelectedValuePath = "Key";
            cboMaSP.DisplayMemberPath = "Value";

            //txtGhiChu.Text = "";
            txtSoLuong.Text = "0";
            dtgThemSanPhamYCBG.Items.Refresh();
        }

         private ThemSanPhamYCBGVM create_New_CTYCBG(int sp = -1, string tenSP = "",string ncc = "", int sl = 0, string note = "")
        {
            ThemSanPhamYCBGVM themSanPhamYCBGVM = new ThemSanPhamYCBGVM();
            themSanPhamYCBGVM.MaSP = sp;
            themSanPhamYCBGVM.TenSanPham = tenSP;
            themSanPhamYCBGVM.NhaCungCap = ncc;
            themSanPhamYCBGVM.SoLuong = sl;
            themSanPhamYCBGVM.GhiChu = note;
            return themSanPhamYCBGVM;
        }

        private void getAllMaNCC(int? masp = null)
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

                    allMaSP = new Dictionary<int, string>();
                    while (rdr.Read())
                    {
                        allMaSP.Add(int.Parse(rdr["masp"].ToString()), rdr["TenSanPham"].ToString());
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
                        table.Rows.Add(t.NhaCungCap, t.MaSP, t.SoLuong);
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
            // only create when have atleast 1 CTYCBG
            if (themSanPhamYCBGVMs.Count > 0)
                createNewYCBG();
            else
                MessageBox.Show("Phải có ít nhất 1 CTYBG");
        }

        // check duplicate in CTYCBG
        // if exists not add into the list
        private bool checkExistCTYCBG(int maSP, string maNCC)
        {
            var flag = true;
            themSanPhamYCBGVMs.ForEach(ct => {
                if (ct.MaSP == maSP && ct.NhaCungCap == maNCC)
                    flag = false;
            });
            return flag;
        }
        
        private void btn_addCTYCBG_Click(object sender, RoutedEventArgs e)
        {
            add_CTYCBG();
        }

        private void add_CTYCBG()
        {
            try
            {
                // both comboBox not selected any thing
                if (cboMaSP.SelectedItem != null && cboMaNCC.SelectedItem != null)
                {
                    int key = int.Parse(cboMaSP.SelectedValue.ToString());
                    if (checkExistCTYCBG(key, cboMaNCC.SelectedValue.ToString()))
                    {
                        themSanPhamYCBGVMs.Add(create_New_CTYCBG(
                                key,
                                allMaSP[key],
                                cboMaNCC.SelectedValue.ToString(),
                                int.Parse(txtSoLuong.Text),
                                ""
                            ));
                        resetValue();
                    }
                    else // thông báo lỗi khi đã tồn tại CTBG SP của NCC này trong danh sách
                    {
                        MessageBox.Show("Đã tồn tại YCBG Sản phẩm của NCC này trong danh sách", "Lỗi");
                    }
                }
                // when only cbo MaSP selected 
                //=> thêm yêu cầu báo giá cho các nhà cung cấp có sản phẩm đó
                else if (cboMaSP.SelectedItem != null)
                {
                    allMaNCC.ForEach(maNCC =>
                    {
                        int key = int.Parse(cboMaSP.SelectedValue.ToString());
                        if (checkExistCTYCBG(key, maNCC))
                        {
                            themSanPhamYCBGVMs.Add(create_New_CTYCBG(
                                key,
                                allMaSP[key],
                                maNCC,
                                int.Parse(txtSoLuong.Text),
                                ""
                            ));
                        }

                    });
                    resetValue();
                }
                // when only cbo MaNCC selected 
                //=> thêm yêu cầu báo giá cho các sản phẩm của NCC đó
                else if (cboMaNCC.SelectedItem != null)
                {
                    allMaSP.Keys.ToList().ForEach(SP =>
                    {
                        if (checkExistCTYCBG(SP, cboMaNCC.SelectedValue.ToString()))
                        {
                            themSanPhamYCBGVMs.Add(create_New_CTYCBG(
                                SP,
                                allMaSP[SP],
                                cboMaNCC.SelectedValue.ToString(),
                                int.Parse(txtSoLuong.Text),
                                ""
                            ));
                        }
                    });
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
                MessageBox.Show(ex.Message, "Lỗi");
            }
        }

        private void cboMaSP_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //getAllMaNCC(cboMaSP.SelectedValue == null ? "" : cboMaSP.SelectedValue.ToString());
            if (cboMaSP.SelectedItem == null)
                getAllMaNCC();
            else
                getAllMaNCC(int.Parse(cboMaSP.SelectedValue.ToString()));
            cboMaNCC.ItemsSource = allMaNCC;
        }

        private void cboMaNCC_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            getAllMaSP(cboMaNCC.SelectedValue == null ? "" : cboMaNCC.SelectedValue.ToString());
            cboMaSP.ItemsSource = allMaSP;
        }

        private void btn_XoaCTYCBG_Click(object sender, RoutedEventArgs e)
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

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            isClosed = true;
        }

        private void txtSoLuong_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                add_CTYCBG();
            }
        }
    }
}
