namespace YCBG_HeQtCSDL.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Account")]
    public partial class Account
    {
        [Key]
        [StringLength(20)]
        public string TenDangNhap { get; set; }

        [Required]
        [StringLength(10)]
        public string MaNV { get; set; }

        [Required]
        [StringLength(20)]
        public string RoleName { get; set; }

        [StringLength(50)]
        public string MatKhau { get; set; }

        public DateTime? NgayTao { get; set; }

        public DateTime? HieuLuc { get; set; }

        public virtual NhanVien NhanVien { get; set; }

        public virtual Role Role { get; set; }
    }
}
