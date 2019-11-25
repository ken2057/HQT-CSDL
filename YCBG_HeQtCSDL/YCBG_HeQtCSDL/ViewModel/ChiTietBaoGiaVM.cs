using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;

namespace YCBG_HeQtCSDL.ViewModel
{
    public class ChiTietBaoGiaVM
    {
        [DisplayName("Mã mặt hàng")]
        public string MaSP { get; set; }
        [DisplayName("Tên mặt hàng")]
        public string TenMatHang { get; set; }
        [DisplayName("Nhà cung cấp")]
        public string TenNCC { get; set; }
        [DisplayName("Đơn giá")]
        public decimal DonGia{ get; set; }
        [DisplayName("Số lượng")]
        public int SoLuong { get; set; }
        [DisplayName("Ghi chú")]
        public string GhiChu { get; set; }

        public ChiTietBaoGiaVM() { }
        public ChiTietBaoGiaVM(string maSP, string tenMatHang, string tenNCC, decimal donGia, int soLuong, string ghiChu)
        {
            MaSP = maSP;
            TenMatHang = tenMatHang;
            TenNCC = tenNCC;
            DonGia = donGia;
            SoLuong = soLuong;
            GhiChu = ghiChu;
        }
        public ChiTietBaoGiaVM(ChiTietBaoGiaVM ob)
        {
            MaSP = ob.MaSP;
            TenMatHang = ob.TenMatHang;
            TenNCC = ob.TenNCC;
            DonGia = ob.DonGia;
            SoLuong = ob.SoLuong;
            GhiChu = ob.GhiChu;
        }
    }
}
