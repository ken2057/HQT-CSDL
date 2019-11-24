using System;
using System.Collections.Generic;
using System.Windows;
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
