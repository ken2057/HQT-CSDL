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

namespace YCBG_HeQtCSDL.Views
{
    /// <summary>
    /// Interaction logic for ThemMuaHang.xaml
    /// </summary>
    public partial class ThemMuaHang : Window
    {
        public bool isClosed;
        string connectionString;
        public ThemMuaHang(string connectionString)
        {
            InitializeComponent();
            this.connectionString = connectionString;
            isClosed = false;
        }

        private void btnThemMoiHD_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navService = NavigationService.GetNavigationService(this);
            frameThemMuaHang.Content = new Pages.ThemMuaHang.ThemMoi(connectionString, this);
        }

        private void btnThemTuYCBG_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navService = NavigationService.GetNavigationService(this);
            frameThemMuaHang.Content = new Pages.ThemMuaHang.ThemTuYCBG(connectionString, this);
        }

        private void btnThemTuKeHoachMua_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navService = NavigationService.GetNavigationService(this);
            frameThemMuaHang.Content = new Pages.ThemMuaHang.ThemTuKHMua(connectionString);
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            isClosed = true;
        }
    }
}
