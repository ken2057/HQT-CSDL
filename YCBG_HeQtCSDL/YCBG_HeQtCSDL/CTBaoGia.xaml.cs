using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for CTBaoGia.xaml
    /// </summary>
    public partial class CTBaoGia : Window
    {
        public bool isClosed;
        public bool isSaved;
        public YeuCauBaoGiaVM clickedYCGB;
        public ChiTietYeuCauBaoGiaVM clickedCTYCBG;

        string connectionString;

        public CTBaoGia(string connectionString, YeuCauBaoGiaVM ycbgVM, ChiTietYeuCauBaoGiaVM ctYCBGVM)
        {
            InitializeComponent();
            this.connectionString = connectionString;
            isClosed = false;
            this.clickedCTYCBG = ctYCBGVM;
            this.clickedYCGB = ycbgVM;

            lbNCC.Content = ctYCBGVM.TenNPP;
            lbTenSP.Content = ctYCBGVM.MaSP;
            lbSL.Content = ctYCBGVM.SL;
            if(ctYCBGVM.Gia != null)
            {
                txtGiaDaBao.Text = ctYCBGVM.Gia;
                txtGiaDaBao.IsEnabled = false;
                btnLuu.IsEnabled = false;
            }

        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            isClosed = true;
        }

        private void txtGiaDaBao_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {
            isSaved = true;
            clickedCTYCBG.Gia = txtGiaDaBao.Text;
            Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
