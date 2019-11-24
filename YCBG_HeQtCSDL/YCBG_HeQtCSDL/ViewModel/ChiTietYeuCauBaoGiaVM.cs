using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace YCBG_HeQtCSDL.ViewModel
{
    class ChiTietYeuCauBaoGiaVM
    {
        [DisplayName("Ngày yêu cầi")]
        public string NgayYCBG { get; set; }
        [DisplayName("Người gởi")]
        public string MaNhanVien { get; set; }
        [DisplayName("Tên nhà cung cấp")]
        public string TenNCC { get; set; }
        [DisplayName("Mã sản phẩm")]
        public string MaSP { get; set; }
        [DisplayName("Tên sản phẩm")]
        public string TenSP { get; set; }
        
        [DisplayName("Số lượng mua")]
        public string SL { get; set; }
        [DisplayName("Giá đã báo")]
        public string Gia { get; set; }
    }
}
