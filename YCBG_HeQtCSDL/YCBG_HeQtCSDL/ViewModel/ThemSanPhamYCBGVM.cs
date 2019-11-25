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
        [DisplayName("Mã sản phẩm")]
        public int MaSP { get; set; }
        [DisplayName("Tên Sản Phẩm")]
        public string TenSanPham { get; set; }
        [DisplayName("Nhà cung cấp")]
        public string NhaCungCap { get; set; }
        [DisplayName("Số lượng")]
        public int SoLuong { get; set; }

        [DisplayName("Giá")]
        public decimal Gia { get; set; }

        [DisplayName("Tồn")]
        public decimal Ton { get; set; }

        public ThemSanPhamYCBGVM(int sp = -1, string tenSP = "", string ncc = "", int sl = 0, decimal tienMua = 0, int ton = 0)
        {
            MaSP = sp;
            TenSanPham = tenSP;
            NhaCungCap = ncc;
            SoLuong = sl;
            Gia = tienMua;
            Ton = ton;
        }
    }
}
