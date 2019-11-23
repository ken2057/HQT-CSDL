use qlmuahang
go
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
create proc sp_get_maNCC
as
begin
	select MaNCC from NCC
end
go
create proc sp_get_all_maNCC
	@masp varchar(20)
as
begin
	if @masp <> ''
		select MaNCC from CTSP where MaSP = @masp
	else
		select MaNCC from CTSP
		group by MaNCC
end
go
create proc sp_get_all_masp
	@mancc varchar(20)
as
begin
	if @mancc <> ''
		select masp from CTSP where MaNCC = @mancc
	else
		select MaSP from CTSP
		group by MaSP
end
go
-- declare type to input from WPF to SQL
create type CTYCBGType as table (
	MaNCC varchar(max),
	MaSP varchar(max),
	SLSeMua int
)
go
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
		select  * from YeuCauBaoGia
		insert into YeuCauBaoGia
		values (
			@ma,
			GETDATE(), 
			NULL, 
			(select manv from Account
				where tendangnhap in (select ORIGINAL_LOGIN()))
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
