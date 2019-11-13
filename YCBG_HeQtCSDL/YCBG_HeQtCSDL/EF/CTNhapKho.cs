namespace YCBG_HeQtCSDL.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CTNhapKho")]
    public partial class CTNhapKho
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string MaSP { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string MaNhapKho { get; set; }

        public int SLNhap { get; set; }

        public virtual PhieuNhapKho PhieuNhapKho { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
