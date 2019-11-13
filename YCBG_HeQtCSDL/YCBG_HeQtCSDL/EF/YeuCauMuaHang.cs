namespace YCBG_HeQtCSDL.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("YeuCauMuaHang")]
    public partial class YeuCauMuaHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public YeuCauMuaHang()
        {
            CTYeuCauMuas = new HashSet<CTYeuCauMua>();
        }

        [Key]
        [StringLength(10)]
        public string MaYeuCau { get; set; }

        [StringLength(10)]
        public string MaDonMuaHang { get; set; }

        [Required]
        [StringLength(10)]
        public string MaNV { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTYeuCauMua> CTYeuCauMuas { get; set; }

        public virtual DonMuaHang DonMuaHang { get; set; }

        public virtual NhanVien NhanVien { get; set; }
    }
}
