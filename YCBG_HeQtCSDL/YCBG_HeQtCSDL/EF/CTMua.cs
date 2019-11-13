namespace YCBG_HeQtCSDL.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CTMua")]
    public partial class CTMua
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
        public string MaDonMuaHang { get; set; }

        public int? SLMua { get; set; }

        [Column(TypeName = "money")]
        public decimal? DonGia { get; set; }

        public virtual CTSP CTSP { get; set; }

        public virtual DonMuaHang DonMuaHang { get; set; }
    }
}
