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
using System.Windows.Shapes;
using YCBG_HeQtCSDL.ViewModel;

namespace YCBG_HeQtCSDL
{
    /// <summary>
    /// Interaction logic for ThemSanPhamYCBG.xaml
    /// </summary>
    public partial class ThemSanPhamYCBG : Window
    {
        public ThemSanPhamYCBG()
        {
            InitializeComponent();
            ThemSanPhamYCBGVM themSanPhamYCBGVM = new ThemSanPhamYCBGVM();
            themSanPhamYCBGVM.TenSanPham = "";
            themSanPhamYCBGVM.NhaCungCap = "";
            themSanPhamYCBGVM.SoLuong =null;
            themSanPhamYCBGVM.GhiChu = null;
            List<ThemSanPhamYCBGVM> themSanPhamYCBGVMs = new List<ThemSanPhamYCBGVM>();
            themSanPhamYCBGVMs.Add(themSanPhamYCBGVM);
            dtgThemSanPhamYCBG.ItemsSource = themSanPhamYCBGVMs;
        }


    }
}
