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
	[TenPhongBan] Varchar(10) NULL,
	[ChucVu] Varchar(20) NULL,
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
	[MaDonMuaHang] Varchar(10) NULL,
	[MaNV] Varchar(10) NOT NULL,
Primary Key  ([MaYeuCau])
) 
go

Create table [NCC] (
	[MaNCC] Varchar(10) NOT NULL,
	[SoTienCanTra] Money Default 0 NULL Check (SoTienCanTra > -1 ),
	[Email] Varchar(50) NULL Check (Email like '%@%' ),
Primary Key  ([MaNCC])
) 
go

Create table [SanPham] (
	[MaSP] Varchar(10) NOT NULL,
	[TenSanPham] Nvarchar(50) NOT NULL,
	[SoLuongTon] Integer Default 0 NULL Check (SoLuongTon > -1 ),
	[Thue] Money NULL,
Primary Key  ([MaSP])
) 
go

Create table [CTSP] (
	[MaNCC] Varchar(10) NOT NULL,
	[MaSP] Varchar(10) NOT NULL,
	[GiaMua] Money Default 0 NULL Check (GiaMua > -1 ),
	[NgayCapNhat] Datetime NULL,
Primary Key  ([MaNCC],[MaSP])
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
	[MaNCC] Varchar(10) NOT NULL,
	[MaSP] Varchar(10) NOT NULL,
	[MaDonMuaHang] Varchar(10) NOT NULL,
	[SLMua] Integer NULL,
	[DonGia] Money NULL,
Primary Key  ([MaNCC],[MaSP],[MaDonMuaHang])
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
	[MaNCC] Varchar(10) NOT NULL,
	[MaSP] Varchar(10) NOT NULL,
	[SLDuTinhMua] Integer Default 0 NULL Check (SLDuTinhMua > -1 ),
Primary Key  ([MaKeHoachMuaHang],[MaNCC],[MaSP])
) 
go

Create table [CTYeuCauMua] (
	[MaSP] Varchar(10) NOT NULL,
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
	[MaSP] Varchar(10) NOT NULL,
	[MaNhapKho] Varchar(10) NOT NULL,
	[SLNhap] Integer Default 0 NOT NULL Check (SLNhap > -1 ),
Primary Key  ([MaSP],[MaNhapKho])
) 
go

Create table [Role] (
	[RoleName] VarChar(20) NOT NULL,
	[Ghi_chu] Text NULL,
Primary Key  ([RoleName])
) 
go

Create table [YeuCauBaoGia] (
	[MaYCBaoGia] Varchar(10) NOT NULL,
	[NgayYCBaoGia] Datetime NULL,
	[TinhTrang] Text NULL,
	[MaNV] Varchar(10) NOT NULL,
Primary Key  ([MaYCBaoGia])
) 
go

Create table [CTYCBaoGia] (
	[MaNCC] Varchar(10) NOT NULL,
	[MaSP] Varchar(10) NOT NULL,
	[MaYCBaoGia] Varchar(10) NOT NULL,
	[SLSeMua] Integer Default 0 NULL Check (SLSeMua > -1 ),
	[GiaDaBao] Money Default 0 NULL Check (GiaDaBao > -1 ),
Primary Key  ([MaNCC],[MaSP],[MaYCBaoGia])
) 
go

Create table [Account] (
	[TenDangNhap] Varchar(20) NOT NULL,
	[MaNV] Varchar(10) NOT NULL,
	[RoleName] VarChar(20) NOT NULL,
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
Alter table [CTSP] add  foreign key([MaNCC]) references [NCC] ([MaNCC]) 
go
Alter table [CTSP] add  foreign key([MaSP]) references [SanPham] ([MaSP]) 
go
Alter table [CTYeuCauMua] add  foreign key([MaSP]) references [SanPham] ([MaSP]) 
go
Alter table [CTNhapKho] add  foreign key([MaSP]) references [SanPham] ([MaSP]) 
go
Alter table [CTMua] add  foreign key([MaNCC],[MaSP]) references [CTSP] ([MaNCC],[MaSP]) 
go
Alter table [CTKeHoachMua] add  foreign key([MaNCC],[MaSP]) references [CTSP] ([MaNCC],[MaSP]) 
go
Alter table [CTYCBaoGia] add  foreign key([MaNCC],[MaSP]) references [CTSP] ([MaNCC],[MaSP]) 
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


Set quoted_identifier on
go


Set quoted_identifier off
go


sp_AddRole [Admin]
go
sp_AddRole [QuanLy]
go
sp_AddRole [ThuKho]
go
sp_AddRole [NhanVien]
go


/* Roles permissions */
Grant select on [NhanVien] to [Admin]
go
Grant update on [NhanVien] to [Admin]
go
Grant delete on [NhanVien] to [Admin]
go
Grant insert on [NhanVien] to [Admin]
go
Grant references on [NhanVien] to [Admin]
go
Grant select on [NhanVien] to [QuanLy]
go
Grant update on [NhanVien] to [QuanLy]
go
Grant insert on [NhanVien] to [QuanLy]
go
Grant select on [PhongBan] to [Admin]
go
Grant update on [PhongBan] to [Admin]
go
Grant delete on [PhongBan] to [Admin]
go
Grant insert on [PhongBan] to [Admin]
go
Grant references on [PhongBan] to [Admin]
go
Grant select on [PhongBan] to [QuanLy]
go
Grant select on [YeuCauMuaHang] to [Admin]
go
Grant update on [YeuCauMuaHang] to [Admin]
go
Grant delete on [YeuCauMuaHang] to [Admin]
go
Grant insert on [YeuCauMuaHang] to [Admin]
go
Grant references on [YeuCauMuaHang] to [Admin]
go
Grant select on [YeuCauMuaHang] to [QuanLy]
go
Grant update on [YeuCauMuaHang] to [QuanLy]
go
Grant delete on [YeuCauMuaHang] to [QuanLy]
go
Grant insert on [YeuCauMuaHang] to [QuanLy]
go
Grant references on [YeuCauMuaHang] to [QuanLy]
go
Grant select on [YeuCauMuaHang] to [NhanVien]
go
Grant update on [YeuCauMuaHang] to [NhanVien]
go
Grant delete on [YeuCauMuaHang] to [NhanVien]
go
Grant insert on [YeuCauMuaHang] to [NhanVien]
go
Grant references on [YeuCauMuaHang] to [NhanVien]
go
Grant select on [NCC] to [Admin]
go
Grant update on [NCC] to [Admin]
go
Grant delete on [NCC] to [Admin]
go
Grant insert on [NCC] to [Admin]
go
Grant references on [NCC] to [Admin]
go
Grant select on [NCC] to [ThuKho]
go
Grant update on [NCC] to [ThuKho]
go
Grant delete on [NCC] to [ThuKho]
go
Grant insert on [NCC] to [ThuKho]
go
Grant references on [NCC] to [ThuKho]
go
Grant select on [SanPham] to [Admin]
go
Grant update on [SanPham] to [Admin]
go
Grant delete on [SanPham] to [Admin]
go
Grant insert on [SanPham] to [Admin]
go
Grant references on [SanPham] to [Admin]
go
Grant select on [SanPham] to [ThuKho]
go
Grant update on [SanPham] to [ThuKho]
go
Grant delete on [SanPham] to [ThuKho]
go
Grant insert on [SanPham] to [ThuKho]
go
Grant references on [SanPham] to [ThuKho]
go
Grant select on [CTSP] to [Admin]
go
Grant update on [CTSP] to [Admin]
go
Grant delete on [CTSP] to [Admin]
go
Grant insert on [CTSP] to [Admin]
go
Grant references on [CTSP] to [Admin]
go
Grant select on [CTSP] to [ThuKho]
go
Grant update on [CTSP] to [ThuKho]
go
Grant delete on [CTSP] to [ThuKho]
go
Grant insert on [CTSP] to [ThuKho]
go
Grant references on [CTSP] to [ThuKho]
go
Grant select on [DonMuaHang] to [Admin]
go
Grant update on [DonMuaHang] to [Admin]
go
Grant delete on [DonMuaHang] to [Admin]
go
Grant insert on [DonMuaHang] to [Admin]
go
Grant references on [DonMuaHang] to [Admin]
go
Grant select on [DonMuaHang] to [QuanLy]
go
Grant update on [DonMuaHang] to [QuanLy]
go
Grant delete on [DonMuaHang] to [QuanLy]
go
Grant insert on [DonMuaHang] to [QuanLy]
go
Grant references on [DonMuaHang] to [QuanLy]
go
Grant select on [CTMua] to [Admin]
go
Grant update on [CTMua] to [Admin]
go
Grant delete on [CTMua] to [Admin]
go
Grant insert on [CTMua] to [Admin]
go
Grant references on [CTMua] to [Admin]
go
Grant select on [CTMua] to [QuanLy]
go
Grant update on [CTMua] to [QuanLy]
go
Grant delete on [CTMua] to [QuanLy]
go
Grant insert on [CTMua] to [QuanLy]
go
Grant references on [CTMua] to [QuanLy]
go
Grant select on [KeHoachMuaHang] to [Admin]
go
Grant update on [KeHoachMuaHang] to [Admin]
go
Grant delete on [KeHoachMuaHang] to [Admin]
go
Grant insert on [KeHoachMuaHang] to [Admin]
go
Grant references on [KeHoachMuaHang] to [Admin]
go
Grant select on [KeHoachMuaHang] to [QuanLy]
go
Grant update on [KeHoachMuaHang] to [QuanLy]
go
Grant delete on [KeHoachMuaHang] to [QuanLy]
go
Grant insert on [KeHoachMuaHang] to [QuanLy]
go
Grant references on [KeHoachMuaHang] to [QuanLy]
go
Grant select on [CTKeHoachMua] to [Admin]
go
Grant update on [CTKeHoachMua] to [Admin]
go
Grant delete on [CTKeHoachMua] to [Admin]
go
Grant insert on [CTKeHoachMua] to [Admin]
go
Grant references on [CTKeHoachMua] to [Admin]
go
Grant select on [CTKeHoachMua] to [QuanLy]
go
Grant update on [CTKeHoachMua] to [QuanLy]
go
Grant delete on [CTKeHoachMua] to [QuanLy]
go
Grant insert on [CTKeHoachMua] to [QuanLy]
go
Grant references on [CTKeHoachMua] to [QuanLy]
go
Grant select on [CTYeuCauMua] to [Admin]
go
Grant update on [CTYeuCauMua] to [Admin]
go
Grant delete on [CTYeuCauMua] to [Admin]
go
Grant insert on [CTYeuCauMua] to [Admin]
go
Grant references on [CTYeuCauMua] to [Admin]
go
Grant select on [CTYeuCauMua] to [QuanLy]
go
Grant update on [CTYeuCauMua] to [QuanLy]
go
Grant delete on [CTYeuCauMua] to [QuanLy]
go
Grant insert on [CTYeuCauMua] to [QuanLy]
go
Grant references on [CTYeuCauMua] to [QuanLy]
go
Grant select on [PhieuNhapKho] to [Admin]
go
Grant update on [PhieuNhapKho] to [Admin]
go
Grant delete on [PhieuNhapKho] to [Admin]
go
Grant insert on [PhieuNhapKho] to [Admin]
go
Grant references on [PhieuNhapKho] to [Admin]
go
Grant select on [PhieuNhapKho] to [ThuKho]
go
Grant update on [PhieuNhapKho] to [ThuKho]
go
Grant delete on [PhieuNhapKho] to [ThuKho]
go
Grant insert on [PhieuNhapKho] to [ThuKho]
go
Grant references on [PhieuNhapKho] to [ThuKho]
go
Grant select on [CTNhapKho] to [Admin]
go
Grant update on [CTNhapKho] to [Admin]
go
Grant delete on [CTNhapKho] to [Admin]
go
Grant insert on [CTNhapKho] to [Admin]
go
Grant references on [CTNhapKho] to [Admin]
go
Grant select on [CTNhapKho] to [ThuKho]
go
Grant update on [CTNhapKho] to [ThuKho]
go
Grant delete on [CTNhapKho] to [ThuKho]
go
Grant insert on [CTNhapKho] to [ThuKho]
go
Grant references on [CTNhapKho] to [ThuKho]
go
Grant select on [Role] to [Admin]
go
Grant update on [Role] to [Admin]
go
Grant delete on [Role] to [Admin]
go
Grant insert on [Role] to [Admin]
go
Grant references on [Role] to [Admin]
go
Grant select on [YeuCauBaoGia] to [Admin]
go
Grant update on [YeuCauBaoGia] to [Admin]
go
Grant delete on [YeuCauBaoGia] to [Admin]
go
Grant insert on [YeuCauBaoGia] to [Admin]
go
Grant references on [YeuCauBaoGia] to [Admin]
go
Grant select on [YeuCauBaoGia] to [QuanLy]
go
Grant update on [YeuCauBaoGia] to [QuanLy]
go
Grant delete on [YeuCauBaoGia] to [QuanLy]
go
Grant insert on [YeuCauBaoGia] to [QuanLy]
go
Grant references on [YeuCauBaoGia] to [QuanLy]
go
Grant select on [CTYCBaoGia] to [Admin]
go
Grant update on [CTYCBaoGia] to [Admin]
go
Grant delete on [CTYCBaoGia] to [Admin]
go
Grant insert on [CTYCBaoGia] to [Admin]
go
Grant references on [CTYCBaoGia] to [Admin]
go
Grant select on [CTYCBaoGia] to [QuanLy]
go
Grant update on [CTYCBaoGia] to [QuanLy]
go
Grant delete on [CTYCBaoGia] to [QuanLy]
go
Grant insert on [CTYCBaoGia] to [QuanLy]
go
Grant references on [CTYCBaoGia] to [QuanLy]
go
Grant select on [CTYCBaoGia] to [NhanVien]
go
Grant update on [CTYCBaoGia] to [NhanVien]
go
Grant delete on [CTYCBaoGia] to [NhanVien]
go
Grant insert on [CTYCBaoGia] to [NhanVien]
go
Grant references on [CTYCBaoGia] to [NhanVien]
go
Grant select on [Account] to [Admin]
go
Grant update on [Account] to [Admin]
go
Grant delete on [Account] to [Admin]
go
Grant insert on [Account] to [Admin]
go
Grant references on [Account] to [Admin]
go
use master