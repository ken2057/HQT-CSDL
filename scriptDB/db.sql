use master
go
if exists (select * from sysdatabases where name = 'qlmuahang')
	drop database qlmuahang
go
create database qlmuahang
go
use qlmuahang
go
Create table [NhanVien] (
	[MaNV] Varchar(10) NOT NULL,
	[TenPhongBan] Varchar(10) NOT NULL,
	[ChucVu] Varchar(20) NULL,
	[TenNhanVien] Nvarchar(50) NULL,
Primary Key  ([MaNV])
) 
go

Create table [PhongBan] (
	[TenPhongBan] Varchar(10) NOT NULL,
	[MaNV] Varchar(10) NULL,
Primary Key  ([TenPhongBan])
) 
go

Create table [YeuCauMuaHang] (
	[MaYeuCau] Varchar(10) NOT NULL,
	[MaDonMuaHang] Varchar(10) NOT NULL,
	[MaNV] Varchar(10) NOT NULL,
Primary Key  ([MaYeuCau])
) 
go

Create table [NhaPhanPhoi] (
	[MaNPP] Varchar(10) NOT NULL,
	[SoTienCanTra] Money Default 0 NULL Check (SoTienCanTra > -1 ),
	[Email] Varchar(50) NULL Check (Email like '%@%' ),
Primary Key  ([MaNPP])
) 
go

Create table [SanPham] (
	[MaSP] Integer NOT NULL,
	[TenSanPham] Nvarchar(50) NOT NULL,
	[SoLuongTon] Integer Default 0 NULL Check (SoLuongTon > -1 ),
	[Thue] Money NULL,
Primary Key  ([MaSP])
) 
go

Create table [CTSP] (
	[MaNPP] Varchar(10) NOT NULL,
	[MaSP] Integer NOT NULL,
	[GiaMua] Money Default 0 NULL Check (GiaMua > -1 ),
	[NgayCapNhat] Datetime NULL,
Primary Key  ([MaNPP],[MaSP])
) 
go

Create table [DonMuaHang] (
	[MaDonMuaHang] Varchar(10) NOT NULL,
	[NguoiPhuTrachMua] Varchar(10) NOT NULL,
	[NgayDat] Datetime NULL,
	[TinhTrang] Text NULL,
	[ThoiGianGiao] Datetime NULL,
	[TongTienMua] Money NULL Check (TongTienMua > -1 ),
Primary Key  ([MaDonMuaHang])
) 
go

Create table [CTMua] (
	[MaNPP] Varchar(10) NOT NULL,
	[MaSP] Integer NOT NULL,
	[MaDonMuaHang] Varchar(10) NOT NULL,
	[SLMua] Integer NULL,
	[DonGia] Money NULL,
Primary Key  ([MaNPP],[MaSP],[MaDonMuaHang])
) 
go

Create table [KeHoachMuaHang] (
	[MaKeHoachMuaHang] Varchar(10) NOT NULL,
	[MaNV] Varchar(10) NOT NULL,
	[NgayBatDauKeHoach] Datetime NOT NULL,
	[DinhKyNgayMua] Tinyint NULL,
Primary Key  ([MaKeHoachMuaHang])
) 
go

Create table [CTKeHoachMua] (
	[MaKeHoachMuaHang] Varchar(10) NOT NULL,
	[MaNPP] Varchar(10) NOT NULL,
	[MaSP] Integer NOT NULL,
	[SLDuTinhMua] Integer Default 0 NULL Check (SLDuTinhMua > -1 ),
Primary Key  ([MaKeHoachMuaHang],[MaNPP],[MaSP])
) 
go

Create table [CTYeuCauMua] (
	[MaSP] Integer NOT NULL,
	[MaYeuCau] Varchar(10) NOT NULL,
	[SLCanMua] Integer Default 0 NOT NULL Check (SLCanMua > -1 ),
	[PheDuyetYeuCau] Varchar(20) NULL,
Primary Key  ([MaSP],[MaYeuCau])
) 
go

Create table [PhieuNhapKho] (
	[MaNhapKho] Varchar(10) NOT NULL,
	[ThoiGian] Datetime NULL,
	[MaDonMuaHang] Varchar(10) NOT NULL,
	[MaNV] Varchar(10) NOT NULL,
Primary Key  ([MaNhapKho])
) 
go

Create table [CTNhapKho] (
	[MaSP] Integer NOT NULL,
	[MaNhapKho] Varchar(10) NOT NULL,
	[SLNhap] Integer Default 0 NOT NULL Check (SLNhap > -1 ),
Primary Key  ([MaSP],[MaNhapKho])
) 
go

Create table [Role] (
	[RoleName] Char(20) NOT NULL,
	[Ghi_chu] Text NULL,
Primary Key  ([RoleName])
) 
go

Create table [YeuCauBaoGia] (
	[MaYCBaoGia] int NOT NULL,
	[NgayYCBaoGia] Datetime NULL,
	[TinhTrang] Text NULL,
	[MaNV] Varchar(10) NOT NULL,
Primary Key  ([MaYCBaoGia])
) 
go

Create table [CTYCBaoGia] (
	[MaNPP] Varchar(10) NOT NULL,
	[MaSP] Integer NOT NULL,
	[MaYCBaoGia] int NOT NULL,
	[SLSeMua] Integer Default 0 NULL Check (SLSeMua > -1 ),
	[GiaDaBao] Money Default 0 NULL Check (GiaDaBao > -1 ),
Primary Key  ([MaNPP],[MaSP],[MaYCBaoGia])
) 
go

Create table [Account] (
	[TenDangNhap] Varchar(20) NOT NULL,
	[MaNV] Varchar(10) NOT NULL,
	[RoleName] Char(20) NOT NULL,
	[MatKhau] Varchar(50) NULL,
	[NgayTao] Datetime NULL,
	[HieuLuc] Datetime NULL,
Primary Key  ([TenDangNhap])
) 
go


Alter table [DonMuaHang] add  foreign key([NguoiPhuTrachMua]) references [NhanVien] ([MaNV]) 
go
Alter table [KeHoachMuaHang] add  foreign key([MaNV]) references [NhanVien] ([MaNV]) 
go
Alter table [YeuCauMuaHang] add  foreign key([MaNV]) references [NhanVien] ([MaNV]) 
go
Alter table [PhongBan] add  foreign key([MaNV]) references [NhanVien] ([MaNV]) 
go
Alter table [PhieuNhapKho] add  foreign key([MaNV]) references [NhanVien] ([MaNV]) 
go
Alter table [YeuCauBaoGia] add  foreign key([MaNV]) references [NhanVien] ([MaNV]) 
go
Alter table [Account] add  foreign key([MaNV]) references [NhanVien] ([MaNV]) 
go
Alter table [NhanVien] add  foreign key([TenPhongBan]) references [PhongBan] ([TenPhongBan]) 
go
Alter table [CTYeuCauMua] add  foreign key([MaYeuCau]) references [YeuCauMuaHang] ([MaYeuCau]) 
go
Alter table [CTSP] add  foreign key([MaNPP]) references [NhaPhanPhoi] ([MaNPP]) 
go
Alter table [CTSP] add  foreign key([MaSP]) references [SanPham] ([MaSP]) 
go
Alter table [CTYeuCauMua] add  foreign key([MaSP]) references [SanPham] ([MaSP]) 
go
Alter table [CTNhapKho] add  foreign key([MaSP]) references [SanPham] ([MaSP]) 
go
Alter table [CTMua] add  foreign key([MaNPP],[MaSP]) references [CTSP] ([MaNPP],[MaSP]) 
go
Alter table [CTKeHoachMua] add  foreign key([MaNPP],[MaSP]) references [CTSP] ([MaNPP],[MaSP]) 
go
Alter table [CTYCBaoGia] add  foreign key([MaNPP],[MaSP]) references [CTSP] ([MaNPP],[MaSP]) 
go
Alter table [CTMua] add  foreign key([MaDonMuaHang]) references [DonMuaHang] ([MaDonMuaHang]) 
go
Alter table [YeuCauMuaHang] add  foreign key([MaDonMuaHang]) references [DonMuaHang] ([MaDonMuaHang]) 
go
Alter table [PhieuNhapKho] add  foreign key([MaDonMuaHang]) references [DonMuaHang] ([MaDonMuaHang]) 
go
Alter table [CTKeHoachMua] add  foreign key([MaKeHoachMuaHang]) references [KeHoachMuaHang] ([MaKeHoachMuaHang]) 
go
Alter table [CTNhapKho] add  foreign key([MaNhapKho]) references [PhieuNhapKho] ([MaNhapKho]) 
go
Alter table [Account] add  foreign key([RoleName]) references [Role] ([RoleName]) 
go
Alter table [CTYCBaoGia] add  foreign key([MaYCBaoGia]) references [YeuCauBaoGia] ([MaYCBaoGia]) 
go