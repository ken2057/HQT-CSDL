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
using YCBG_HeQtCSDL.Pages;
using YCBG_HeQtCSDL.ViewModel;

namespace YCBG_HeQtCSDL
{
    /// <summary>
    /// Interaction logic for list_CTYCBG.xaml
    /// </summary>
    ///
    
    public partial class list_CTYCBG : Window
    {
        public static ChiTietYeuCauBaoGiaVM chiTietYeuCauBaoGiaVM { get; set; }
        public bool isClosed;
        string connectionString;

        YeuCauBaoGiaVM clickedYCBG;
        List<YeuCauBaoGiaVM> yeuCauBaoGiaVMs;
        ListChiTIetYeuCauBaoGia listChiTIetYeuCauBaoGia;
        List<ListChiTIetYeuCauBaoGia> listChiTIetYeuCauBaoGias;

        public list_CTYCBG(string connectionString, YeuCauBaoGiaVM ycbgVM)
        {
            this.connectionString = connectionString;
            InitializeComponent();
            this.clickedYCBG = ycbgVM;
            get_listCTYCBG();
        }
        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            chiTietYeuCauBaoGiaVM = new ChiTietYeuCauBaoGiaVM();
            chiTietYeuCauBaoGiaVM.TenNCC = ((ListChiTIetYeuCauBaoGia)dtgList_YCBG.SelectedItem).NhaCungCap;
            chiTietYeuCauBaoGiaVM.MaSP = ((ListChiTIetYeuCauBaoGia)dtgList_YCBG.SelectedItem).TenSanPham;

            CTBaoGia cTBaoGia = new CTBaoGia(connectionString, clickedYCBG, chiTietYeuCauBaoGiaVM);
            //chiTietBaoGiaVM = new ChiTietBaoGiaVM(((ChiTietBaoGiaVM)chiTietBaoGiaVMTemp));
            //MessageBox.Show(yeuCauBaoGiaVM.MaYCBG);
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
    }
}
