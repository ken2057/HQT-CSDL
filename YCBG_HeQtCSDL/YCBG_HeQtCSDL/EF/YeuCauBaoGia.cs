namespace YCBG_HeQtCSDL.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("YeuCauBaoGia")]
    public partial class YeuCauBaoGia
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public YeuCauBaoGia()
        {
            CTYCBaoGias = new HashSet<CTYCBaoGia>();
        }

        [Key]
        [StringLength(10)]
        public string MaYCBaoGia { get; set; }

        public DateTime? NgayYCBaoGia { get; set; }

        [Column(TypeName = "text")]
        public string TinhTrang { get; set; }

        [Required]
        [StringLength(10)]
        public string MaNV { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTYCBaoGia> CTYCBaoGias { get; set; }

        public virtual NhanVien NhanVien { get; set; }
    }
}
