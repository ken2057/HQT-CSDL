namespace YCBG_HeQtCSDL.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PhieuNhapKho")]
    public partial class PhieuNhapKho
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PhieuNhapKho()
        {
            CTNhapKhoes = new HashSet<CTNhapKho>();
        }

        [Key]
        [StringLength(10)]
        public string MaNhapKho { get; set; }

        public DateTime? ThoiGian { get; set; }

        [Required]
        [StringLength(10)]
        public string MaDonMuaHang { get; set; }

        [Required]
        [StringLength(10)]
        public string MaNV { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTNhapKho> CTNhapKhoes { get; set; }

        public virtual DonMuaHang DonMuaHang { get; set; }

        public virtual NhanVien NhanVien { get; set; }
    }
}
