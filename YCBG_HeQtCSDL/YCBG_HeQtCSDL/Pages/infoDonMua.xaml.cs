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
    /// Interaction logic for infoDonMua.xaml
    /// </summary>
    public partial class infoDonMua : Page
    {
        string connectionString;
        EF.DonMuaHang donMua;
        List<ViewModel.CTHDMua> listCTHDMua;

        public infoDonMua(string connectionString, EF.DonMuaHang donMua)
        {
            InitializeComponent();
            this.connectionString = connectionString;
            this.donMua = donMua;

            get_ctMua();
        }

        private void get_ctMua()
        {
            listCTHDMua = Func.getData.get_ctMua(connectionString, donMua.MaDonMuaHang);
            setValue();
        }

        private void setValue()
        {
            txtTenNV.Text = donMua.NguoiPhuTrachMua;
            txtNgayDat.Text = donMua.NgayDat.ToString();
            txtNgayGiao.Text = donMua.ThoiGianGiao.ToString();
            txtTongTien.Text = double.Parse(donMua.TongTienMua.ToString()).ToString("#,##;(#,##)");

            dtgCTDonMua.ItemsSource = listCTHDMua;
            dtgCTDonMua.Items.Refresh();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navService = NavigationService.GetNavigationService(this);
            DonMuaHang pg = new DonMuaHang(connectionString);
            navService.Navigate(pg);
        }
    }
}
