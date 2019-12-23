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
        public string MaSP { get; set; }
        public string TenMatHang { get; set; }
        public string TenNPP { get; set; }
        public decimal DonGia{ get; set; }
        public int SoLuong { get; set; }
        public string GhiChu { get; set; }

        public ChiTietBaoGiaVM() { }
        public ChiTietBaoGiaVM(string maSP, string tenMatHang, string tenNPP, decimal donGia, int soLuong, string ghiChu)
        {
            MaSP = maSP;
            TenMatHang = tenMatHang;
            TenNPP = tenNPP;
            DonGia = donGia;
            SoLuong = soLuong;
            GhiChu = ghiChu;
        }
        public ChiTietBaoGiaVM(ChiTietBaoGiaVM ob)
        {
            MaSP = ob.MaSP;
            TenMatHang = ob.TenMatHang;
            TenNPP = ob.TenNPP;
            DonGia = ob.DonGia;
            SoLuong = ob.SoLuong;
            GhiChu = ob.GhiChu;
        }
    }
}
