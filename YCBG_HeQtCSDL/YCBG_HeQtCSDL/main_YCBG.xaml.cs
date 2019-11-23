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
    /// Interaction logic for main_YCBG.xaml
    /// </summary>
    public partial class main_YCBG : Window
    {
        YeuCauBaoGiaVM yeuCauBaoGiaVM;
        List<YeuCauBaoGiaVM> yeuCauBaoGiaVMs;
        string connectionString;
        public main_YCBG(string connectionString)
        {
            this.connectionString = connectionString;

            InitializeComponent();

            get_YCBH();
            //MessageBoxResult t = MessageBox.Show(connectionString, "t");
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ThemSanPhamYCBG themSanPhamYCBG = new ThemSanPhamYCBG(this.connectionString);
            themSanPhamYCBG.Owner = this;
            themSanPhamYCBG.ShowDialog();

            //refresh when windows closed
            if (themSanPhamYCBG.isClosed)
            {
                get_YCBH();
                dtgYCBG.Items.Refresh();
            }
        }

        private void get_YCBH(string maYCBG = "")
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
            if(e.Key == Key.Enter)
            {
                get_YCBH(txtMaSP.Text);
                dtgYCBG.Items.Refresh();
            }
        }
    }
}
