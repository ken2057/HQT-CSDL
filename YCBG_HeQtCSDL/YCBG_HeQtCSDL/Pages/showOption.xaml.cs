using System;
using System.Collections.Generic;
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
    /// Interaction logic for showOption.xaml
    /// </summary>
    public partial class showOption : Page
    {
        string connectionString;
        public showOption(string connectionString)
        {
            InitializeComponent();

            this.connectionString = connectionString;
        }

        private void btnOpenYCBaoGia_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navService = NavigationService.GetNavigationService(this);
            YCBaoGia sO = new YCBaoGia(connectionString);
            navService.Navigate(sO);
        }

        private void btnOpenDonMuaHang_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navService = NavigationService.GetNavigationService(this);
            DonMuaHang sO = new DonMuaHang(connectionString);
            navService.Navigate(sO);
        }
    }
}
