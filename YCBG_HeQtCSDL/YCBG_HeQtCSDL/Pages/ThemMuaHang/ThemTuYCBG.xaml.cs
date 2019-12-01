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
using YCBG_HeQtCSDL.ViewModel;

namespace YCBG_HeQtCSDL.Pages.ThemMuaHang
{
    /// <summary>
    /// Interaction logic for ThemTuYCBG.xaml
    /// </summary>
    public partial class ThemTuYCBG : Page
    {
        YeuCauBaoGiaVM yeuCauBaoGiaVM;
        List<YeuCauBaoGiaVM> yeuCauBaoGiaVMs;

        string connectionString;
        public static string selectedYCBG;

        Window parent;
        public ThemTuYCBG(string connectionString, Window parent)
        {
            InitializeComponent();
            
            this.connectionString = connectionString;
            this.parent = parent;
            
            get_YCBG();
        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            yeuCauBaoGiaVM = (YeuCauBaoGiaVM)dtgYCBG.SelectedItem;

            NavigationService navService = NavigationService.GetNavigationService(this);
            infoYCBG sO = new infoYCBG(connectionString, parent, yeuCauBaoGiaVM);
            navService.Navigate(sO);
        }

        private void get_YCBG(string maYCBG = "")
        {

            yeuCauBaoGiaVMs = new List<YeuCauBaoGiaVM>();
            SqlDataReader rdr = null;

            using (var conn = new SqlConnection(this.connectionString))
            using (var command = new SqlCommand("sp_get_ycbg", conn)
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
                        yeuCauBaoGiaVM = new YeuCauBaoGiaVM();
                        yeuCauBaoGiaVM.MaYCBG = rdr["MaYCBaoGia"].ToString();
                        yeuCauBaoGiaVM.NgayYCBG = rdr["NgayYCBaoGia"].ToString();
                        yeuCauBaoGiaVM.TinhTrang = rdr["TinhTrang"].ToString();
                        yeuCauBaoGiaVM.MaNV = rdr["MaNV"].ToString();
                        yeuCauBaoGiaVMs.Add(yeuCauBaoGiaVM);
                    }
                    dtgYCBG.ItemsSource = yeuCauBaoGiaVMs;
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

        private void txtMaSP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                get_YCBG(txtMaSP.Text);
                dtgYCBG.Items.Refresh();
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Content = null;
        }

        private void btnChon_Click(object sender, RoutedEventArgs e)
        {
            int index = dtgYCBG.SelectedIndex;
            if(index == -1)
            {
                MessageBox.Show("Hãy chọn YCBG", "Lỗi");
                return;
            }

            NavigationService navService = NavigationService.GetNavigationService(this);
            ThemMoi sO = new ThemMoi(connectionString, parent, yeuCauBaoGiaVMs[index]);
            navService.Navigate(sO);
        }
    }
}
