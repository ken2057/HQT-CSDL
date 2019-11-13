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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using YCBG_HeQtCSDL.ViewModel;

namespace YCBG_HeQtCSDL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ChiTietBaoGiaVM chiTietBaoGiaVM = new ChiTietBaoGiaVM();
            chiTietBaoGiaVM.MaSP = "1";
            chiTietBaoGiaVM.TenMatHang = "1";
            chiTietBaoGiaVM.TenNCC = "1";
            chiTietBaoGiaVM.SoLuong = 1;
            chiTietBaoGiaVM.DonGia = 1;
            chiTietBaoGiaVM.GhiChu = "1";
            List<ChiTietBaoGiaVM> chiTietBaoGiaVMs = new List<ChiTietBaoGiaVM>();
            chiTietBaoGiaVMs.Add(chiTietBaoGiaVM);
            chiTietBaoGiaVMs.Add(chiTietBaoGiaVM);
            chiTietBaoGiaVMs.Add(chiTietBaoGiaVM);
            chiTietBaoGiaVMs.Add(chiTietBaoGiaVM);
            dtgCtYCBG.ItemsSource = chiTietBaoGiaVMs;
            lbToDay.Content = DateTime.Today.Day.ToString() +"/"+ DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString();
        }
    }
    
}
