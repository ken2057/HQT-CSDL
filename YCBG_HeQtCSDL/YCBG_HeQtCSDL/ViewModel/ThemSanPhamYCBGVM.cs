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
        [DisplayName("Ghi chú")]
        public string GhiChu { get; set; }
    }
}
