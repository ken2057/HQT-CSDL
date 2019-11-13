namespace YCBG_HeQtCSDL.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CTYCBaoGia")]
    public partial class CTYCBaoGia
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string MaNCC { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string MaSP { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(10)]
        public string MaYCBaoGia { get; set; }

        public int? SLSeMua { get; set; }

        [Column(TypeName = "money")]
        public decimal? GiaDaBao { get; set; }

        public virtual CTSP CTSP { get; set; }

        public virtual YeuCauBaoGia YeuCauBaoGia { get; set; }
    }
}
