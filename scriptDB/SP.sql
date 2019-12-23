use qlmuahang
go
-- Lấy tất cả YCBG
create proc sp_get_ycbg
	@ngayYCBG date
as
begin
	if(not @ngayYCBG is null)
		select * from YeuCauBaoGia where datediff(day, NgayYCBaoGia, @ngayYCBG) = 0
	else
		select * from YeuCauBaoGia
end
go
-- Lấy tất cả Mã npp có sản phẩm cung cấp
create proc sp_get_all_manpp
	@masp int
as
begin
	if not @masp is null
		select manpp from CTSP where MaSP = @masp
	else
		select manpp from CTSP
		group by manpp
end
go
-- lấy mã sp và tên sản phẩm mà tồn tại trong CTSP hoặc do npp nào đó cung cấp
go
create proc sp_get_all_masp
	@manpp varchar(10)
as
begin
	if @manpp <> ''
		select MaSP, TenSanPham
		from SanPham
		where MaSP in (select MaSP from CTSP where manpp = @manpp)
	else
		select MaSP, TenSanPham
		from SanPham
		where MaSP in (select MaSP from CTSP)
end
go
-- Thêm từng dòng trong CTYCBG gửi từ WPF
create proc sp_add_CTYCBG
	@maYCBG varchar(10),
	@ctycbg CTYCBGType readonly
as
begin
	-- declare
	declare cr_CTYCBG cursor forward_only
	for select * from @ctycbg
	declare @manpp varchar(10), @masp varchar(10), @sl int
	open cr_CTYCBG
	fetch next from cr_CTYCBG into @manpp, @masp, @sl
	-- tran
	set xact_abort on
	begin tran
		while @@FETCH_STATUS = 0
		begin
			insert into CTYCBaoGia(manpp, MaSP, SLSeMua, MaYCBaoGia)
			values (@manpp, @masp, @sl, @maYCBG)

			fetch next from cr_CTYCBG into @manpp, @masp, @sl
		end
	commit tran
	close cr_CTYCBG
	deallocate cr_CTYCBG
end
go
-- Tạo YCBG và CTYCBG
create proc sp_add_YCBG
	@ctYCBG CTYCBGType readonly
as
begin
	-- find MaYCBH valid
	declare @maYCBG int
	select @maYCBG = count(MaYCBaoGia) from YeuCauBaoGia
	if exists (select * from YeuCauBaoGia with (updlock) where MaYCBaoGia = @maYCBG+'')
		select @maYCBG = @maYCBG + 1
	-- create
	set xact_abort on
	begin tran
		declare @ma varchar(10), @error int
		set @ma = (select cast(@maYCBG as varchar(10)))
		-- create YCBG
		insert into YeuCauBaoGia
		values (
			@ma,
			GETDATE(), 
			N'Da tao', 
			(select manv from Account where tendangnhap in (select ORIGINAL_LOGIN()))
		)
		-- create CTYCBH
		exec @error = sp_add_CTYCBG @ma, @ctYCBG
	commit tran
	if @@ERROR <> 0 or @error <> 0
	begin
		ROLLBACK
		DECLARE @ErrorMessage VARCHAR(2000)
		SELECT @ErrorMessage = 'Lỗi: ' + ERROR_MESSAGE()
		RAISERROR(@ErrorMessage, 16, 1)
	end
end
go
-- Lấy tát cả CTYCBG của YCBG
create proc sp_get_CTYCBG
	@maYCBG varchar(10)
as
begin
	select * 
	from CTYCBaoGia A, (select MaSP, TenSanPham from SanPham) as B
	where MaYCBaoGia = @maYCBG
		and A.MaSP = B.MaSP
end
go
--
create proc sp_get_tenSP 
	@maSP int
as
begin
	select TenSanPham from SanPham where MaSP = @maSP
end
go
--Chi tiết 1 YCBG
--create proc sp_getDetailCTYCBG @maYCBaoGia char(10), @manpp char(10), @maSP char(10)
--as
--begin
--	select * 
--	from CTYCBaoGia A, (select MaSP, TenSanPham from SanPham) as B
--	where MaYCBaoGia = @maYCBaoGia
--		and A.manpp = @manpp
--		and A.MaSP = B.MaSP
--end
go
create proc sp_get_all_donMua
	@maNV varchar(20)
as
begin
	select * from DonMuaHang where NguoiPhuTrachMua like '%'+@maNV+'%'
end
go
--
-- Thêm từng dòng trong CTYCBG gửi từ WPF
create proc sp_add_CTHDMua
	@maHD varchar(10),
	@ctycbg CTYCBGType readonly
as
begin
	-- declare
	declare cr_CTMua cursor forward_only
	for select * from @ctycbg
	--
	declare @manpp varchar(10), @masp varchar(10), @sl int
	open cr_CTMua

	fetch next from cr_CTMua into @manpp, @masp, @sl
	while @@FETCH_STATUS = 0
	begin 
		declare @tien money
		select @tien = giamua from CTSP where MaSP = @masp and manpp = @manpp

		insert into CTMua
		values (@manpp, @masp, @maHD, @sl, @tien)

		fetch next from cr_CTMua into @manpp, @masp, @sl
	end
	close cr_CTMua
	deallocate cr_CTMua
end
go
-- Tạo YCBG và CTYCBG
create proc sp_add_HDMua
	@ctMua CTYCBGType readonly
as
begin
	-- find MaYCBH valid
	declare @maHD int
	select @maHD = count(MaDonMuaHang) from DonMuaHang
	if exists (select * from DonMuaHang with (updlock) where MaDonMuaHang = @maHD+'')
		set @maHD = @maHD + 1
	-- create
	set xact_abort on
	begin tran
		declare @ma varchar(10), @error int
		set @ma = (select cast(@maHD as varchar(10)))
		-- create YCBG
		
		insert into DonMuaHang(MaDonMuaHang, NgayDat, TinhTrang, NguoiPhuTrachMua, TongTienMua)
		values (
			@ma,
			GETDATE(), 
			N'Da tao', 
			(select manv from Account where tendangnhap in (select ORIGINAL_LOGIN())),
			0
		)
		-- create CTMua
		exec @error = sp_add_CTHDMua @ma, @ctMua
	commit tran
	if @@ERROR <> 0 or @error <> 0
	begin
		ROLLBACK
		DECLARE @ErrorMessage VARCHAR(2000)
		SELECT @ErrorMessage = 'Lỗi: ' + ERROR_MESSAGE()
		RAISERROR(@ErrorMessage, 16, 1)
	end
end
go
create proc sp_get_gia_ton_sp
	@masp int,
	@manpp varchar(10)
as
begin
	select GiaMua, SoLuongTon
	from ctsp A, (select SoLuongTon from SanPham where MaSP = @masp) B
	where A.MaSP = @masp and A.manpp = @manpp
end
go
create proc sp_get_kehoachmua
	@maNV varchar(20)
as
begin
	if(not @maNV = "") 
		select * from KeHoachMuaHang where MaNV = @maNV 
	else
		select * from KeHoachMuaHang 
end
go
create proc sp_get_ctkhmua
	@maKHMua varchar(20)
as
begin
	select manpp, A.MaSP, SLDuTinhMua, TenSanPham
	from CTKeHoachMua A, SanPham B
	where MaKeHoachMuaHang = @maKHMua
	and A.MaSP = B.MaSP
end
go
create proc sp_get_ctmua
	@MaDonMuaHang varchar(20)
as
begin
	select manpp, A.MaSP, SLMua, TenSanPham, A.DonGia
	from CTMua A, SanPham B
	where MaDonMuaHang = @MaDonMuaHang
	and A.MaSP = B.MaSP
end
go
create proc sp_gui_YCBG
	@maYCBG varchar(10)
as
begin
	begin tran
		update yeucaubaogia
		set TinhTrang = 'Da gui'
		where MaYCBaoGia = @maYCBG
	commit tran
	if @@error <> 0
	begin
		ROLLBACK
		DECLARE @ErrorMessage VARCHAR(2000)
		SELECT @ErrorMessage = 'Lỗi: ' + ERROR_MESSAGE()
		RAISERROR(@ErrorMessage, 16, 1)
	end
end
go
create proc sp_update_gia_ctycbg
	@dsCTYCBG update_CTYCBGType readonly
as
begin
	declare cr_update_CTYCBG cursor forward_only
	for select * from @dsCTYCBG

	declare @maNPP varchar(10), @maSP int, @gia money
	
	open cr_update_CTYCBG
	fetch next from cr_update_CTYCBG into @maNPP, @maSP, @gia

	set xact_abort on
	begin tran
		while @@FETCH_STATUS=0
		begin
			update CTYCBaoGia
			set giadabao = @gia
			where manpp = @maNPP
				and maSP = @maSP
			fetch next from cr_update_CTYCBG into @maNPP, @maSP, @gia
		end
		close cr_update_CTYCBG
		deallocate cr_update_CTYCBG
	commit tran
	
	if @@error <> 0
	begin
		ROLLBACK
		DECLARE @ErrorMessage VARCHAR(2000)
		SELECT @ErrorMessage = 'Lỗi: ' + ERROR_MESSAGE()
		RAISERROR(@ErrorMessage, 16, 1)
	end
end
go