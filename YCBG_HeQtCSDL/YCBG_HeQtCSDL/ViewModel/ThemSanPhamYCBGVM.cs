using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YCBG_HeQtCSDL.ViewModel
{
    class ThemSanPhamYCBGVM
    {
        public int MaSP { get; set; }
        public string TenSanPham { get; set; }
        public string NhaPhanPhoi { get; set; }
        public int SoLuong { get; set; }

        public decimal Gia { get; set; }

        public decimal Ton { get; set; }

        public ThemSanPhamYCBGVM(int sp = -1, string tenSP = "", string npp = "", int sl = 0, decimal tienMua = 0, int ton = 0)
        {
            MaSP = sp;
            TenSanPham = tenSP;
            NhaPhanPhoi = npp;
            SoLuong = sl;
            Gia = tienMua;
            Ton = ton;
        }
    }
}
