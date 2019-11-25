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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace YCBG_HeQtCSDL.Pages
{
    /// <summary>
    /// Interaction logic for DonMuaHang.xaml
    /// </summary>
    public partial class DonMuaHang : Page
    {
        List<EF.DonMuaHang> lstDonMua;
        EF.DonMuaHang donMua;
        string connectionString;
        public DonMuaHang(string connectionString)
        {
            InitializeComponent();
            this.connectionString = connectionString;
            get_all_donMuaHang();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navService = NavigationService.GetNavigationService(this);
            showOption pg = new showOption(connectionString);
            navService.Navigate(pg);
        }

        private void get_all_donMuaHang()
        {
            lstDonMua = new List<EF.DonMuaHang>();
            SqlDataReader rdr = null;

            using (var conn = new SqlConnection(this.connectionString))
            using (var command = new SqlCommand("sp_get_all_donMua", conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                try
                {
                    conn.Open();
                    rdr = command.ExecuteReader();

                    while (rdr.Read())
                    {
                        donMua = new EF.DonMuaHang();
                        donMua.MaDonMuaHang = rdr["MaDonMuaHang"].ToString();
                        donMua.NguoiPhuTrachMua = rdr["NguoiPhuTrachMua"].ToString();
                        donMua.NgayDat = DateTime.Parse(rdr["NgayDat"].ToString());
                        donMua.TinhTrang = rdr["TinhTrang"].ToString();
                        donMua.TongTienMua = decimal.Parse(rdr["TongTienMua"].ToString());
                        //
                        if (rdr["ThoiGianGiao"].ToString() == "")
                            donMua.ThoiGianGiao = null;
                        else
                            donMua.ThoiGianGiao = DateTime.Parse(rdr["ThoiGianGiao"].ToString());

                        lstDonMua.Add(donMua);
                    }
                    dtgHDMua.ItemsSource = lstDonMua;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
                finally
                {
                    conn.Close();
                }

            }
        }

        private void txtMaSP_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void dtgYCBG_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void btnThemMuaHang_Click(object sender, RoutedEventArgs e)
        {
            Views.ThemMuaHang v = new Views.ThemMuaHang(connectionString);
            v.ShowDialog();

            if(v.isClosed)
            {
                get_all_donMuaHang();
                dtgHDMua.Items.Refresh();
            }
        }
    }
}
