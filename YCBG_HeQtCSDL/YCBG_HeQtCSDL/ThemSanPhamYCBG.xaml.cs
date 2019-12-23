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
        List<string> allMaNPP;
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

        private void resetValue(int? masp = null, string MaNPP = "")
        {
            allMaNPP = Func.getData.getAllMaNPP(connectionString, masp);
            allMaSP = Func.getData.getAllMaSP(connectionString, MaNPP);
            // 
            cboMaNPP.SelectedIndex = -1;
            cboMaNPP.ItemsSource = allMaNPP;
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
            dtgThemSanPhamYCBG.Items.Refresh();
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
                    table.Columns.Add("MaNPP", colString);
                    table.Columns.Add("MaSP", colString);
                    table.Columns.Add("SLSeMua", colInt32);

                    themSanPhamYCBGVMs.ForEach(t => {
                        table.Rows.Add(t.NhaPhanPhoi, t.MaSP, t.SoLuong);
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
                    //    ct.TenNPP = rdr["MaNPP"].ToString();
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
        private bool checkExistCTYCBG(int maSP, string MaNPP)
        {
            var flag = true;
            themSanPhamYCBGVMs.ForEach(ct => {
                if (ct.MaSP == maSP && ct.NhaPhanPhoi == MaNPP)
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
                // số lượng tổi thiểu 1
                if (int.Parse(txtSoLuong.Text) < 1)
                    throw new FormatException();

                // số lượng phải bé hơn 10000
                if (int.Parse(txtSoLuong.Text) > 10000)
                    throw new IndexOutOfRangeException();

                // both comboBox not selected any thing
                if (cboMaSP.SelectedItem != null && cboMaNPP.SelectedItem != null)
                {
                    int key = int.Parse(cboMaSP.SelectedValue.ToString());
                    if (checkExistCTYCBG(key, cboMaNPP.SelectedValue.ToString()))
                    {
                        themSanPhamYCBGVMs.Add(new ThemSanPhamYCBGVM(
                                key,
                                allMaSP[key],
                                cboMaNPP.SelectedValue.ToString(),
                                int.Parse(txtSoLuong.Text)
                            ));
                        resetValue();
                    }
                    else // thông báo lỗi khi đã tồn tại CTBG SP của NCC này trong danh sách
                    {
                        MessageBox.Show("Đã tồn tại YCBG Sản phẩm của NCC này trong danh sách", "Lỗi");
                    }
                }
                // when only cbo MaSP selected 
                //=> thêm yêu cầu báo giá cho các Nhà phân phối có sản phẩm đó
                else if (cboMaSP.SelectedItem != null)
                {
                    allMaNPP.ForEach(MaNPP =>
                    {
                        int key = int.Parse(cboMaSP.SelectedValue.ToString());
                        if (checkExistCTYCBG(key, MaNPP))
                        {
                            themSanPhamYCBGVMs.Add(new ThemSanPhamYCBGVM(
                                key,
                                allMaSP[key],
                                MaNPP,
                                int.Parse(txtSoLuong.Text)
                            ));
                        }

                    });
                    resetValue();
                }
                // when only cbo MaNPP selected 
                //=> thêm yêu cầu báo giá cho các sản phẩm của NCC đó
                else if (cboMaNPP.SelectedItem != null)
                {
                    allMaSP.Keys.ToList().ForEach(SP =>
                    {
                        if (checkExistCTYCBG(SP, cboMaNPP.SelectedValue.ToString()))
                        {
                            themSanPhamYCBGVMs.Add(new ThemSanPhamYCBGVM(
                                SP,
                                allMaSP[SP],
                                cboMaNPP.SelectedValue.ToString(),
                                int.Parse(txtSoLuong.Text)
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

        private void cboMaSP_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //getAllMaNPP(cboMaSP.SelectedValue == null ? "" : cboMaSP.SelectedValue.ToString());
            if (cboMaSP.SelectedItem == null)
                allMaNPP = Func.getData.getAllMaNPP(connectionString);
            else
                allMaNPP = Func.getData.getAllMaNPP(connectionString, int.Parse(cboMaSP.SelectedValue.ToString()));
            cboMaNPP.ItemsSource = allMaNPP;
        }

        private void cboMaNPP_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            allMaSP = Func.getData.getAllMaSP(connectionString, cboMaNPP.SelectedValue == null ? "" : cboMaNPP.SelectedValue.ToString());
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
