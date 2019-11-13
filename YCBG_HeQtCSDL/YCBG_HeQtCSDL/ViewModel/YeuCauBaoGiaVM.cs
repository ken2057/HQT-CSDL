using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YCBG_HeQtCSDL.ViewModel
{
    public class YeuCauBaoGiaVM
    {
        [DisplayName("Mã yêu cầu báo giá")]
        public string MaYCBG { get; set; }
        [DisplayName("Ngày")]
        public string NgayYCBG { get; set; }
        [DisplayName("Tình trạng")]
        public string TinhTrang { get; set; }
        [DisplayName("Nhân viên")]
        public string MaNV { get; set; }
    }
}
