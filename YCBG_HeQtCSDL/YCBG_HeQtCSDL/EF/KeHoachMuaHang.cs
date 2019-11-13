namespace YCBG_HeQtCSDL.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KeHoachMuaHang")]
    public partial class KeHoachMuaHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KeHoachMuaHang()
        {
            CTKeHoachMuas = new HashSet<CTKeHoachMua>();
        }

        [Key]
        [StringLength(10)]
        public string MaKeHoachMuaHang { get; set; }

        [Required]
        [StringLength(10)]
        public string MaNV { get; set; }

        public DateTime NgayBatDauKeHoach { get; set; }

        public byte? DinhKyNgayMua { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTKeHoachMua> CTKeHoachMuas { get; set; }

        public virtual NhanVien NhanVien { get; set; }
    }
}
