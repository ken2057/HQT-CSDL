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

namespace YCBG_HeQtCSDL.Pages.ThemMuaHang
{
    /// <summary>
    /// Interaction logic for infoKHMua.xaml
    /// </summary>
    public partial class infoKHMua : Page
    {
        string connectionString;
        EF.KeHoachMuaHang khMua;
        List<ViewModel.CTKHMuaVM> listCTKHMua;
        Window parent;

        public infoKHMua(string connectionString, Window parent, EF.KeHoachMuaHang khMua)
        {
            InitializeComponent();

            this.connectionString = connectionString;
            this.khMua = khMua;
            this.parent = parent;

            get_CTKHMua();
        }

        private void get_CTKHMua()
        {
            listCTKHMua = Func.getData.get_CTKHMua(connectionString, khMua.MaKeHoachMuaHang);
            dtgList_KHMua.ItemsSource = listCTKHMua;
            dtgList_KHMua.Items.Refresh();
        }

        private void btnChonYCBG_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navService = NavigationService.GetNavigationService(this);
            ThemMoi sO = new ThemMoi(connectionString, parent, khMua);
            navService.Navigate(sO);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navService = NavigationService.GetNavigationService(this);
            ThemTuYCBG sO = new ThemTuYCBG(connectionString, parent);
            navService.Navigate(sO);
        }
    }
}
