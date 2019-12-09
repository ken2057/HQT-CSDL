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
        YeuCauBaoGiaVM selectedYCBG;
        EF.KeHoachMuaHang selectedKHMua;

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

        public ThemMoi(string connectionString, Window parent, YeuCauBaoGiaVM ycbgVM)
        {
            InitializeComponent();
            this.selectedYCBG = ycbgVM;
            this.connectionString = connectionString;
            this.parent = parent;

            themSanPhamYCBGVMs = new List<ThemSanPhamYCBGVM>();
            lapMuaTuYCBG();

            resetValue();
        }

        public ThemMoi(string connectionString, Window parent, EF.KeHoachMuaHang kmMua)
        {
            InitializeComponent();
            this.selectedKHMua = kmMua;
            this.connectionString = connectionString;
            this.parent = parent;

            themSanPhamYCBGVMs = new List<ThemSanPhamYCBGVM>();
            lapMuaTuKHMua();

            resetValue();
        }

        private void lapMuaTuKHMua()
        {
            List<CTKHMuaVM> listCTKHMua = Func.getData.get_CTKHMua(connectionString, selectedKHMua.MaKeHoachMuaHang);
            // add each item in listCTYCBG to list MuaHang
            listCTKHMua.ForEach(ctKHMua =>
            {
                if (checkExistCTYCBG(ctKHMua.MaSP, ctKHMua.MaNCC))
                {
                    var data = Func.getData.gia_ton_sp(connectionString, ctKHMua.MaSP, ctKHMua.MaNCC);
                    themSanPhamYCBGVMs.Add(new ThemSanPhamYCBGVM(
                            ctKHMua.MaSP,
                            ctKHMua.TenSP,
                            ctKHMua.MaNCC,
                            ctKHMua.SLMua,
                            decimal.Parse(data[1].ToString()),
                            int.Parse(data[0].ToString())
                        ));
                }
            });
            dtgThemHDMua.ItemsSource = themSanPhamYCBGVMs;
        }

        private void lapMuaTuYCBG()
        {
            List<ListChiTIetYeuCauBaoGia> listCTYCBG = Func.getData.get_listCTYCBG(connectionString, selectedYCBG);
            // add each item in listCTYCBG to list MuaHang
            listCTYCBG.ForEach(ctYCBG =>
            {
                if (checkExistCTYCBG(ctYCBG.MaSP, ctYCBG.NhaCungCap))
                {
                    var data = Func.getData.gia_ton_sp(connectionString, ctYCBG.MaSP, ctYCBG.NhaCungCap);
                    themSanPhamYCBGVMs.Add(new ThemSanPhamYCBGVM(
                            ctYCBG.MaSP,
                            ctYCBG.TenSanPham,
                            ctYCBG.NhaCungCap,
                            ctYCBG.SoLuong,
                            decimal.Parse(data[1].ToString()),
                            int.Parse(data[0].ToString())
                        ));
                }
            });
            dtgThemHDMua.ItemsSource = themSanPhamYCBGVMs;
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
        private void add_CTMua()
        {
            try
            {
                // số lượng tổi thiểu 1
                if (int.Parse(txtSoLuong.Text) < 1)
                    throw new FormatException();
                // số lượng phải bé hơn 10000
                if (int.Parse(txtSoLuong.Text) > 10000)
                    throw new IndexOutOfRangeException();

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
                            ));
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
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("Số lượng phải bé hơn 10000", "Lỗi");
                txtSoLuong.Focus();
            }

            catch (FormatException)
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

        private void btn_addYCBG_Click(object sender, RoutedEventArgs e)
        {
            // only create when have atleast 1 CTYCBG
            if (themSanPhamYCBGVMs.Count > 0)
            {
                // check out of money type size
                long money = 0;

                themSanPhamYCBGVMs.ForEach(t => money += (long)double.Parse((t.SoLuong * t.Gia).ToString()));

                if (money > (long)double.Parse("922337203685477.5807"))
                {
                    MessageBox.Show("Số tiền quá lớn, hãy xoá bớt sản phẩm");
                    return;
                }


                taoHDMua();
            }
                
            else
                MessageBox.Show("Phải có ít nhất 1 sản phẩm mua");
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
                    table.Columns.Add("MaSP", colInt32);
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


