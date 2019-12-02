use qlmuahang
go
-- Lấy tất cả YCBG
create proc sp_get_ycbg
	@maYCBG varchar(10)
as
begin
	if(@maYCBG <> '')
		select * from YeuCauBaoGia where MaYCBaoGia like '%'+@maYCBG+'%'
	else
	select * from YeuCauBaoGia
end
go
-- Lấy tất cả Mã NCC có sản phẩm cung cấp
create proc sp_get_all_maNCC
	@masp int
as
begin
	if not @masp is null
		select MaNCC from CTSP where MaSP = @masp
	else
		select MaNCC from CTSP
		group by MaNCC
end
go
-- lấy mã sp và tên sản phẩm mà tồn tại trong CTSP hoặc do NCC nào đó cung cấp
go
create proc sp_get_all_masp
	@mancc varchar(10)
as
begin
	if @mancc <> ''
		select MaSP, TenSanPham
		from SanPham
		where MaSP in (select MaSP from CTSP where MaNCC = @mancc)
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
	declare @mancc varchar(10), @masp varchar(10), @sl int
	open cr_CTYCBG
	fetch next from cr_CTYCBG into @mancc, @masp, @sl
	-- tran
	set xact_abort on
	begin tran
		while @@FETCH_STATUS = 0
		begin
			insert into CTYCBaoGia(MaNCC, MaSP, SLSeMua, MaYCBaoGia)
			values (@mancc, @masp, @sl, @maYCBG)

			fetch next from cr_CTYCBG into @mancc, @masp, @sl
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
			N'Đã tạo', 
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
--create proc sp_getDetailCTYCBG @maYCBaoGia char(10), @maNCC char(10), @maSP char(10)
--as
--begin
--	select * 
--	from CTYCBaoGia A, (select MaSP, TenSanPham from SanPham) as B
--	where MaYCBaoGia = @maYCBaoGia
--		and A.MaNCC = @maNCC
--		and A.MaSP = B.MaSP
--end
go
create proc sp_get_all_donMua
as
begin
	select * from DonMuaHang
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
	declare @mancc varchar(10), @masp varchar(10), @sl int
	open cr_CTMua

	fetch next from cr_CTMua into @mancc, @masp, @sl
	while @@FETCH_STATUS = 0
	begin 
		declare @tien money
		select @tien = giamua from CTSP where MaSP = @masp and MaNCC = @mancc

		insert into CTMua
		values (@mancc, @masp, @maHD, @sl, @tien)

		fetch next from cr_CTMua into @mancc, @masp, @sl
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
			N'Đã tạo', 
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
	@mancc varchar(10)
as
begin
	select GiaMua, SoLuongTon
	from ctsp A, (select SoLuongTon from SanPham where MaSP = @masp) B
	where A.MaSP = @masp and A.MaNCC = @mancc
end
go
-- Update Chi Tiết YCBG
create proc sp_update_CTYCBG
				@maYCBG varchar(10),
				@maNCC_now varchar(10),
				@maSP_now int,
				@maNCC_to_change varchar(10),
				@maSP_to_change varchar(10),
				@sl int,
				@gia money
as
begin
	update CTYCBaoGia
	set MaNCC = @maNCC_to_change, MaSP = @maNCC_to_change, SLSeMua = @sl, GiaDaBao = @gia
	where MaYCBaoGia = @maYCBG and MaNCC = @maNCC_now and MaSP = @maSP_now
end

go
-- Update (Thêm) Chi Tiết YCBG
create proc sp_update_add_CTYCBG
				@maYCBG varchar(10),
				@maNCC varchar(10),
				@maSP int,
				@sl int,
				@gia money
as
begin
	Insert into CTYCBaoGia values(@maNCC,@maSP,@maYCBG,@sl,@gia)
end

go

-- Update (Xoá) Chi Tiết YCBG
create proc sp_update_delete_CTYCBG
				@maYCBG varchar(10),
				@maNCC varchar(10),
				@maSP int
as
begin
	delete from CTYCBaoGia where MaYCBaoGia = @maYCBG and MaNCC = @maNCC and MaSP = @maSP
end

go

-- Update Giá Chi Tiết YCBG sau khi nhận được phản hồi
create proc sp_update_rep_price_CTYCBG
				@maYCBG varchar(10),
				@maNCC varchar(10),
				@maSP int,
				@gia money
as
begin
	update CTYCBaoGia
	set GiaDaBao = @gia
	where MaYCBaoGia = @maYCBG and MaNCC = @maNCC and MaSP = @maSP
end