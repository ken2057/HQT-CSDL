use qlmuahang
go
-- Thêm yêu cầu báo giá
	-- Stored procedure
create proc sp_Them_YCBaoGia_Tu_YCMua
	@MaNV varchar(10),
	@MaYCMua varchar(10)
as
begin
	declare @Result integer
	exec @Result = run_Them_YCBaoGia_Tu_YCMua @MaNV, @MaYCMua
	-- case
	declare @Status nvarchar
	select @Status = case @Result
			when 0 then 'Thành công'
			when 1 then 'Yêu cầu mua không tồn tại'
			when 2 then 'Yêu cầu mua không có sản phẩm nào'
			else @Result
	end
end
go
	-- Fucntion sẽ chạy 
create proc run_Them_YCBaoGia_Tu_YCMua
	@MaNV varchar(10), 
	@MaYCMua varchar(10)
as
begin
	-- lấy thông tin yêu cầu mua
	select *
	into #YCMua
	from YeuCauMuaHang with (updlock)
	where MaYeuCau = @MaYCMua
	-- kiểm tra tồn tại
	if not exists (select * from #YCMua)
		return 1
	
	-- create cursor để lấy dữ liệu CT yêu cầu mua
	declare @maSP varchar(10)
	declare @slMua int

	declare @Cur_YCMua cursor 
	set @Cur_YCMua = cursor
	read_only forward_only
	for select MaSP, SLCanMua from CTYeuCauMua 
		where MaYeuCau = 'YC1'
	open @Cur_YCMua
	fetch next from @Cur_YCMua into @maSP, @slMua
	select @maSP, @slMua
	-- kiểm tra có ít nhất 1 CT yêu cầu mua
	if @@FETCH_STATUS <> 0
		return 2

	select * from YeuCauMuaHang	
	select * from CTYeuCauMua
	select * from NhanVien
	insert into NhanVien(MaNV) values ('1')
	insert into PhongBan(TenPhongBan) values ('CNTT')
	insert into YeuCauMuaHang values ('YC1', null, '1')
	insert into CTYeuCauMua values ('SP1', 'YC1', 1, 'not')
	
	insert into SanPham values ('SP1', 'a', 0, 0)
	select * from PhongBan
	use qlmuahang
end

