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

        private void dtgYCBG_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            NavigationService navService = NavigationService.GetNavigationService(this);
            infoDonMua pg = new infoDonMua(connectionString, lstDonMua[dtgHDMua.SelectedIndex]);
            navService.Navigate(pg);
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

        private void get_all_donMuaHang(string maNV = "")
        {
            lstDonMua = Func.getData.get_all_donMuaHang(connectionString, maNV);
            dtgHDMua.ItemsSource = lstDonMua;
            dtgHDMua.Items.Refresh();

        }

        private void txtMaNV_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                get_all_donMuaHang(txtMaNV.Text);
        }
    }
}
