namespace YCBG_HeQtCSDL.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CTYeuCauMua")]
    public partial class CTYeuCauMua
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaSP { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string MaYeuCau { get; set; }

        public int SLCanMua { get; set; }

        [StringLength(20)]
        public string PheDuyetYeuCau { get; set; }

        public virtual YeuCauMuaHang YeuCauMuaHang { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
