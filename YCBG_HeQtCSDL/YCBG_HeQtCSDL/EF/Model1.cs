namespace YCBG_HeQtCSDL.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<CTKeHoachMua> CTKeHoachMuas { get; set; }
        public virtual DbSet<CTMua> CTMuas { get; set; }
        public virtual DbSet<CTNhapKho> CTNhapKhoes { get; set; }
        public virtual DbSet<CTSP> CTSPs { get; set; }
        public virtual DbSet<CTYCBaoGia> CTYCBaoGias { get; set; }
        public virtual DbSet<CTYeuCauMua> CTYeuCauMuas { get; set; }
        public virtual DbSet<DonMuaHang> DonMuaHangs { get; set; }
        public virtual DbSet<KeHoachMuaHang> KeHoachMuaHangs { get; set; }
        public virtual DbSet<NhanVien> NhanViens { get; set; }
        public virtual DbSet<NhaPhanPhoi> NhaPhanPhois { get; set; }
        public virtual DbSet<PhieuNhapKho> PhieuNhapKhoes { get; set; }
        public virtual DbSet<PhongBan> PhongBans { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }
        public virtual DbSet<YeuCauBaoGia> YeuCauBaoGias { get; set; }
        public virtual DbSet<YeuCauMuaHang> YeuCauMuaHangs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .Property(e => e.TenDangNhap)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.MaNV)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.RoleName)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.MatKhau)
                .IsUnicode(false);

            modelBuilder.Entity<CTKeHoachMua>()
                .Property(e => e.MaKeHoachMuaHang)
                .IsUnicode(false);

            modelBuilder.Entity<CTKeHoachMua>()
                .Property(e => e.MaNPP)
                .IsUnicode(false);

            modelBuilder.Entity<CTMua>()
                .Property(e => e.MaNPP)
                .IsUnicode(false);

            modelBuilder.Entity<CTMua>()
                .Property(e => e.MaDonMuaHang)
                .IsUnicode(false);

            modelBuilder.Entity<CTMua>()
                .Property(e => e.DonGia)
                .HasPrecision(19, 4);

            modelBuilder.Entity<CTNhapKho>()
                .Property(e => e.MaNhapKho)
                .IsUnicode(false);

            modelBuilder.Entity<CTSP>()
                .Property(e => e.MaNPP)
                .IsUnicode(false);

            modelBuilder.Entity<CTSP>()
                .Property(e => e.GiaMua)
                .HasPrecision(19, 4);

            modelBuilder.Entity<CTSP>()
                .HasMany(e => e.CTKeHoachMuas)
                .WithRequired(e => e.CTSP)
                .HasForeignKey(e => new { e.MaNPP, e.MaSP })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CTSP>()
                .HasMany(e => e.CTMuas)
                .WithRequired(e => e.CTSP)
                .HasForeignKey(e => new { e.MaNPP, e.MaSP })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CTSP>()
                .HasMany(e => e.CTYCBaoGias)
                .WithRequired(e => e.CTSP)
                .HasForeignKey(e => new { e.MaNPP, e.MaSP })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CTYCBaoGia>()
                .Property(e => e.MaNPP)
                .IsUnicode(false);

            modelBuilder.Entity<CTYCBaoGia>()
                .Property(e => e.GiaDaBao)
                .HasPrecision(19, 4);

            modelBuilder.Entity<CTYeuCauMua>()
                .Property(e => e.MaYeuCau)
                .IsUnicode(false);

            modelBuilder.Entity<CTYeuCauMua>()
                .Property(e => e.PheDuyetYeuCau)
                .IsUnicode(false);

            modelBuilder.Entity<DonMuaHang>()
                .Property(e => e.MaDonMuaHang)
                .IsUnicode(false);

            modelBuilder.Entity<DonMuaHang>()
                .Property(e => e.NguoiPhuTrachMua)
                .IsUnicode(false);

            modelBuilder.Entity<DonMuaHang>()
                .Property(e => e.TinhTrang)
                .IsUnicode(false);

            modelBuilder.Entity<DonMuaHang>()
                .Property(e => e.TongTienMua)
                .HasPrecision(19, 4);

            modelBuilder.Entity<DonMuaHang>()
                .HasMany(e => e.CTMuas)
                .WithRequired(e => e.DonMuaHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DonMuaHang>()
                .HasMany(e => e.PhieuNhapKhoes)
                .WithRequired(e => e.DonMuaHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DonMuaHang>()
                .HasMany(e => e.YeuCauMuaHangs)
                .WithRequired(e => e.DonMuaHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KeHoachMuaHang>()
                .Property(e => e.MaKeHoachMuaHang)
                .IsUnicode(false);

            modelBuilder.Entity<KeHoachMuaHang>()
                .Property(e => e.MaNV)
                .IsUnicode(false);

            modelBuilder.Entity<KeHoachMuaHang>()
                .HasMany(e => e.CTKeHoachMuas)
                .WithRequired(e => e.KeHoachMuaHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.MaNV)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.TenPhongBan)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.ChucVu)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .HasMany(e => e.Accounts)
                .WithRequired(e => e.NhanVien)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NhanVien>()
                .HasMany(e => e.DonMuaHangs)
                .WithRequired(e => e.NhanVien)
                .HasForeignKey(e => e.NguoiPhuTrachMua)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NhanVien>()
                .HasMany(e => e.KeHoachMuaHangs)
                .WithRequired(e => e.NhanVien)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NhanVien>()
                .HasMany(e => e.PhieuNhapKhoes)
                .WithRequired(e => e.NhanVien)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NhanVien>()
                .HasMany(e => e.PhongBans)
                .WithOptional(e => e.NhanVien)
                .HasForeignKey(e => e.MaNV);

            modelBuilder.Entity<NhanVien>()
                .HasMany(e => e.YeuCauBaoGias)
                .WithRequired(e => e.NhanVien)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NhanVien>()
                .HasMany(e => e.YeuCauMuaHangs)
                .WithRequired(e => e.NhanVien)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NhaPhanPhoi>()
                .Property(e => e.MaNPP)
                .IsUnicode(false);

            modelBuilder.Entity<NhaPhanPhoi>()
                .Property(e => e.SoTienCanTra)
                .HasPrecision(19, 4);

            modelBuilder.Entity<NhaPhanPhoi>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<NhaPhanPhoi>()
                .HasMany(e => e.CTSPs)
                .WithRequired(e => e.NhaPhanPhoi)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PhieuNhapKho>()
                .Property(e => e.MaNhapKho)
                .IsUnicode(false);

            modelBuilder.Entity<PhieuNhapKho>()
                .Property(e => e.MaDonMuaHang)
                .IsUnicode(false);

            modelBuilder.Entity<PhieuNhapKho>()
                .Property(e => e.MaNV)
                .IsUnicode(false);

            modelBuilder.Entity<PhieuNhapKho>()
                .HasMany(e => e.CTNhapKhoes)
                .WithRequired(e => e.PhieuNhapKho)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PhongBan>()
                .Property(e => e.TenPhongBan)
                .IsUnicode(false);

            modelBuilder.Entity<PhongBan>()
                .Property(e => e.MaNV)
                .IsUnicode(false);

            modelBuilder.Entity<PhongBan>()
                .HasMany(e => e.NhanViens)
                .WithRequired(e => e.PhongBan)
                .HasForeignKey(e => e.TenPhongBan)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Role>()
                .Property(e => e.RoleName)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Role>()
                .Property(e => e.Ghi_chu)
                .IsUnicode(false);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.Accounts)
                .WithRequired(e => e.Role)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.Thue)
                .HasPrecision(19, 4);

            modelBuilder.Entity<SanPham>()
                .HasMany(e => e.CTNhapKhoes)
                .WithRequired(e => e.SanPham)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SanPham>()
                .HasMany(e => e.CTSPs)
                .WithRequired(e => e.SanPham)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SanPham>()
                .HasMany(e => e.CTYeuCauMuas)
                .WithRequired(e => e.SanPham)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<YeuCauBaoGia>()
                .Property(e => e.TinhTrang)
                .IsUnicode(false);

            modelBuilder.Entity<YeuCauBaoGia>()
                .Property(e => e.MaNV)
                .IsUnicode(false);

            modelBuilder.Entity<YeuCauBaoGia>()
                .HasMany(e => e.CTYCBaoGias)
                .WithRequired(e => e.YeuCauBaoGia)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<YeuCauMuaHang>()
                .Property(e => e.MaYeuCau)
                .IsUnicode(false);

            modelBuilder.Entity<YeuCauMuaHang>()
                .Property(e => e.MaDonMuaHang)
                .IsUnicode(false);

            modelBuilder.Entity<YeuCauMuaHang>()
                .Property(e => e.MaNV)
                .IsUnicode(false);

            modelBuilder.Entity<YeuCauMuaHang>()
                .HasMany(e => e.CTYeuCauMuas)
                .WithRequired(e => e.YeuCauMuaHang)
                .WillCascadeOnDelete(false);
        }
    }
}
