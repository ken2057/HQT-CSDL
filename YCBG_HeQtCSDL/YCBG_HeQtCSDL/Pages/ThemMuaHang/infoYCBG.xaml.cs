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
    /// Interaction logic for infoYCBG.xaml
    /// </summary>
    public partial class infoYCBG : Page
    {
        ChiTietYeuCauBaoGiaVM chiTietYeuCauBaoGiaVM;
        public bool isClosed;
        string connectionString;
        Window parent;

        YeuCauBaoGiaVM clickedYCBG;
        ListChiTIetYeuCauBaoGia listChiTIetYeuCauBaoGia;
        List<ListChiTIetYeuCauBaoGia> listChiTIetYeuCauBaoGias;

        public infoYCBG(string connectionString, Window parent, YeuCauBaoGiaVM ycbgVM)
        {
            this.connectionString = connectionString;
            this.clickedYCBG = ycbgVM;
            this.parent = parent;

            InitializeComponent();
            get_listCTYCBG();
        }
        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            chiTietYeuCauBaoGiaVM = new ChiTietYeuCauBaoGiaVM();
            chiTietYeuCauBaoGiaVM.TenNCC = ((ListChiTIetYeuCauBaoGia)dtgList_YCBG.SelectedItem).NhaCungCap;
            chiTietYeuCauBaoGiaVM.MaSP = ((ListChiTIetYeuCauBaoGia)dtgList_YCBG.SelectedItem).TenSanPham;

            CTBaoGia cTBaoGia = new CTBaoGia(connectionString, clickedYCBG, chiTietYeuCauBaoGiaVM);

            cTBaoGia.ShowDialog();
            // Some operations with this row

            //refresh when windows closed
            if (cTBaoGia.isClosed)
            {
                get_listCTYCBG();
                dtgList_YCBG.Items.Refresh();
            }
        }

        private void get_listCTYCBG()
        {
            listChiTIetYeuCauBaoGias = Func.getData.get_listCTYCBG(connectionString, clickedYCBG);
            dtgList_YCBG.ItemsSource = listChiTIetYeuCauBaoGias;
        }

        private void btnChonYCBG_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navService = NavigationService.GetNavigationService(this);
            ThemMoi sO = new ThemMoi(connectionString, parent, clickedYCBG);
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
