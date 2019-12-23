namespace YCBG_HeQtCSDL.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DonMuaHang")]
    public partial class DonMuaHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DonMuaHang()
        {
            CTMuas = new HashSet<CTMua>();
            PhieuNhapKhoes = new HashSet<PhieuNhapKho>();
            YeuCauMuaHangs = new HashSet<YeuCauMuaHang>();
        }

        [Key]
        [StringLength(10)]
        public string MaDonMuaHang { get; set; }

        [Required]
        [StringLength(10)]
        public string NguoiPhuTrachMua { get; set; }

        public DateTime? NgayDat { get; set; }

        [Column(TypeName = "text")]
        public string TinhTrang { get; set; }

        public DateTime? ThoiGianGiao { get; set; }

        [Column(TypeName = "money")]
        public decimal? TongTienMua { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTMua> CTMuas { get; set; }

        public virtual NhanVien NhanVien { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuNhapKho> PhieuNhapKhoes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<YeuCauMuaHang> YeuCauMuaHangs { get; set; }
    }
}
