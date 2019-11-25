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
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using YCBG_HeQtCSDL.ViewModel;

namespace YCBG_HeQtCSDL.Pages.ThemMuaHang
{
    /// <summary>
    /// Interaction logic for ThemMoi.xaml
    /// </summary>
    public partial class ThemMoi : Page
    {
        string connectionString;
        List<string> allMaNCC;
        Dictionary<int, string> allMaSP;
        List<ThemSanPhamYCBGVM> themSanPhamYCBGVMs;
        Window parent;

        public ThemMoi(string connectionString, Window parent)
        {
            InitializeComponent();
            this.connectionString = connectionString;
            this.parent = parent;

            themSanPhamYCBGVMs = new List<ThemSanPhamYCBGVM>();
            dtgThemHDMua.ItemsSource = themSanPhamYCBGVMs;

            resetValue();
        }

       

        private void resetValue(int? masp = null, string mancc = "")
        {
            allMaNCC = Func.getData.getAllMaNCC(connectionString, masp);
            allMaSP = Func.getData.getAllMaSP(connectionString, mancc);
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
            txtSoLuong.Text = "1";
            dtgThemHDMua.Items.Refresh();
            
            // tính tổng tiền
            if(themSanPhamYCBGVMs.Count > 0)
               txtTongTien.Text = themSanPhamYCBGVMs.Sum(t => t.Gia * t.SoLuong).ToString("#,##;(#,##)");
        }

        private void taoHDMua()
        {
            using (var conn = new SqlConnection(this.connectionString))
            using (var command = new SqlCommand("sp_add_HDMua", conn)
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

                    command.Parameters.AddWithValue("@ctMua", table);
                    command.ExecuteNonQuery();

                    MessageBox.Show("Done");
                    parent.Close();
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

        private void add_CTMua()
        {
            try
            {
                // số lượng tổi thiểu 1
                if (int.Parse(txtSoLuong.Text) < 1)
                    throw new FormatException();

                // both comboBox not selected any thing
                if (cboMaSP.SelectedItem != null && cboMaNCC.SelectedItem != null)
                {
                    int key = int.Parse(cboMaSP.SelectedValue.ToString());
                    if (checkExistCTYCBG(key, cboMaNCC.SelectedValue.ToString()))
                    {
                        var data = Func.getData.gia_ton_sp(connectionString, key, cboMaNCC.SelectedValue.ToString());
                        themSanPhamYCBGVMs.Add(new ThemSanPhamYCBGVM(
                                key,
                                allMaSP[key],
                                cboMaNCC.SelectedValue.ToString(),
                                int.Parse(txtSoLuong.Text),
                                decimal.Parse(data[1].ToString()),
                                int.Parse(data[0].ToString())
                            )) ;
                        resetValue();
                    }
                    else // thông báo lỗi khi đã tồn tại CTBG SP của NCC này trong danh sách
                    {
                        MessageBox.Show("Đã tồn tại mua Sản phẩm của NCC này trong danh sách", "Lỗi");
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
                            var data = Func.getData.gia_ton_sp(connectionString, key, maNCC);
                            themSanPhamYCBGVMs.Add(new ThemSanPhamYCBGVM(
                                key,
                                allMaSP[key],
                                maNCC,
                                int.Parse(txtSoLuong.Text),
                                decimal.Parse(data[1].ToString()),
                                int.Parse(data[0].ToString())
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
                            var data = Func.getData.gia_ton_sp(connectionString, SP, cboMaNCC.SelectedValue.ToString());
                            themSanPhamYCBGVMs.Add(new ThemSanPhamYCBGVM(
                                SP,
                                allMaSP[SP],
                                cboMaNCC.SelectedValue.ToString(),
                                int.Parse(txtSoLuong.Text),
                                decimal.Parse(data[1].ToString()),
                                int.Parse(data[0].ToString())
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
                MessageBox.Show("Số lượng phải lớn hơn hoặc bằng 1", "Lỗi");
                txtSoLuong.Text = "1";
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
                allMaNCC = Func.getData.getAllMaNCC(connectionString);
            else
                allMaNCC = Func.getData.getAllMaNCC(connectionString, int.Parse(cboMaSP.SelectedValue.ToString()));
            cboMaNCC.ItemsSource = allMaNCC;
        }

        private void cboMaNCC_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            allMaSP = Func.getData.getAllMaSP(connectionString, cboMaNCC.SelectedValue == null ? "" : cboMaNCC.SelectedValue.ToString());
            cboMaSP.ItemsSource = allMaSP;
        }

        private void btn_XoaCTYCBG_Click(object sender, RoutedEventArgs e)
        {
            foreach (ThemSanPhamYCBGVM item in dtgThemHDMua.SelectedItems)
            {
                txtTongTien.Text = (decimal.Parse(txtTongTien.Text) - item.Gia * item.SoLuong).ToString("#,##;(#,##)");
                themSanPhamYCBGVMs.RemoveAt(themSanPhamYCBGVMs.IndexOf(item));
            }
            dtgThemHDMua.Items.Refresh();
        }

        private void txtSoLuong_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void txtSoLuong_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                add_CTMua();
            }
        }

        private void btn_Huy_Click(object sender, RoutedEventArgs e)
        {
            Content = null;
        }

        private void btn_addCTYCBG_Click(object sender, RoutedEventArgs e)
        {
            add_CTMua();
        }

        private void btn_addYCBG_Click(object sender, RoutedEventArgs e)
        {
            // only create when have atleast 1 CTYCBG
            if (themSanPhamYCBGVMs.Count > 0)
                taoHDMua();
            else
                MessageBox.Show("Phải có ít nhất 1 sản phẩm mua");
        }
    }
}
