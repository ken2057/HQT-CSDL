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

namespace YCBG_HeQtCSDL.Pages
{
    /// <summary>
    /// Interaction logic for YCBaoGia.xaml
    /// </summary>
    public partial class YCBaoGia : Page
    {
        YeuCauBaoGiaVM yeuCauBaoGiaVM;
        List<YeuCauBaoGiaVM> yeuCauBaoGiaVMs;
        string connectionString;
        public YCBaoGia(string connectionString)
        {
            InitializeComponent();
            this.connectionString = connectionString;
            get_YCBG();
        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            yeuCauBaoGiaVM = (YeuCauBaoGiaVM)dtgYCBG.SelectedItem;
            list_CTYCBG list_CTYCBG = new list_CTYCBG(connectionString, this.yeuCauBaoGiaVM);
            //chiTietBaoGiaVM = new ChiTietBaoGiaVM(((ChiTietBaoGiaVM)chiTietBaoGiaVMTemp));
            //MessageBox.Show(yeuCauBaoGiaVM.MaYCBG);
            list_CTYCBG.ShowDialog();
            // Some operations with this row

            //refresh when windows closed
            if (list_CTYCBG.isClosed)
            {
                get_YCBG();
            }
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ThemSanPhamYCBG themSanPhamYCBG = new ThemSanPhamYCBG(this.connectionString);
            themSanPhamYCBG.ShowDialog();

            //refresh when windows closed
            if (themSanPhamYCBG.isClosed)
            {
                get_YCBG();
            }
        }

        private void get_YCBG(DateTime? ngayYCBG = null)
        {
            yeuCauBaoGiaVMs = Func.getData.get_YCBG(this.connectionString, ngayYCBG);
            dtgYCBG.ItemsSource = yeuCauBaoGiaVMs;
            dtgYCBG.Items.Refresh();
        }

        //private void txtMaSP_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.Key == Key.Enter)
        //    {
        //        get_YCBG(txtMaSP.Text);
        //        dtgYCBG.Items.Refresh();
        //    }
        //}

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navService = NavigationService.GetNavigationService(this);
            showOption pg = new showOption(connectionString);
            navService.Navigate(pg);
        }

        private void dateSearch_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            get_YCBG(dateSearch.SelectedDate);
        }
    }
}
