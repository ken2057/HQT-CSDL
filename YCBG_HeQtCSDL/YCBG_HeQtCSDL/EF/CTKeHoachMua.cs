namespace YCBG_HeQtCSDL.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CTKeHoachMua")]
    public partial class CTKeHoachMua
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string MaKeHoachMuaHang { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string MaNPP { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaSP { get; set; }

        public int? SLDuTinhMua { get; set; }

        public virtual KeHoachMuaHang KeHoachMuaHang { get; set; }

        public virtual CTSP CTSP { get; set; }
    }
}
