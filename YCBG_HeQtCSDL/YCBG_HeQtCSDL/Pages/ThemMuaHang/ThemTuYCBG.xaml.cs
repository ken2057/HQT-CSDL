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

        private void get_YCBG(DateTime? ngayYCBG = null)
        {
            yeuCauBaoGiaVMs = Func.getData.get_YCBG(this.connectionString, ngayYCBG);
            dtgYCBG.ItemsSource = yeuCauBaoGiaVMs;
            dtgYCBG.Items.Refresh();
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

        private void dateSearch_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            get_YCBG(dateSearch.SelectedDate);
        }
    }
}
