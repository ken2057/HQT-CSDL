namespace YCBG_HeQtCSDL.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NhaPhanPhoi")]
    public partial class NhaPhanPhoi
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NhaPhanPhoi()
        {
            CTSPs = new HashSet<CTSP>();
        }

        [Key]
        [StringLength(10)]
        public string MaNPP { get; set; }

        [Column(TypeName = "money")]
        public decimal? SoTienCanTra { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTSP> CTSPs { get; set; }
    }
}
