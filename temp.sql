create proc sp_get_ycbg
as
begin
	select * from YeuCauBaoGia
end

exec sp_test 1

select * from nhanvien
select * from PhongBan
select * from chucvu

insert into PhongBan values ("A", NULL)
insert into NhanVien values (1, "A", "admin")
select * from YeuCauBaoGia

insert into YeuCauBaoGia values ("1", GETDATE(), "Chua", 1)

create user "duy", "123"

create login duy with password='123'
go
create user admin_db for login duy
go
--sp_addrolemember @rolename='db_owner', @membername='admin_db'
sp_addrolemember @rolename='Admin', @membername='admin_db'
go

create proc sp_add_YCBG
	
alter proc sp_test
	@dsCTYCBG [CTYCBaoGiaType] readonly
as
begin
	--declare @u varchar(10)
	--select @u = CURRENT_USER
	--raiserror(@u,16,1)
	declare #table table(
		MaYCBaoGia varchar(5),
		NgayYCBaoGia varchar(5),
		TinhTrang int,
		MaNV varchar(10)
	)
	-- "," split fields same row
	-- "#" split multi row

	-- split '#'
	SELECT A.value as ctycbh
	into #ctycbh
	FROM string_split(@stringInput, '#') as A
	-- read each row in #ctycbh
	declare @value varchar(max)
	declare cur_ctycbh cursor
	forward_only
	for select * from #ctycbh
	open cur_ctycbh
	fetch next from cur_ctycbh into @value
	--
	while @@FETCH_STATUS = 0
	begin
		select * from string_split(@value, ',')
	end
	

end
go

drop proc sp_t1
drop type [CTYCBaoGiaType]
go
create type [CTYCBaoGiaType] as table (
	[MaNCC] varchar(max),
	[MaSP] varchar(max),
	[MatHang] varchar(max),
	[SLSeMua] int
) 
go
create proc sp_t1
	@dsCTYCBG [CTYCBaoGiaType] readonly
as
begin
	insert into CTYCBaoGia(MaNCC, MaSP, MaYCBaoGia, SLSeMua)
		select MaNCC, MaSP, 1, SLSeMua from @dsCTYCBG
end

declare @test CTYCBaoGiaType
insert into @test values ('1', '1', '1', 1), ('1', '1', '1', 2),('1', '1', '1', 3),('1', '1', '1', 4),('1', '1', '1', 1),('1', '1', '1', 5)
exec sp_t1 @test

select * from CTYCBaoGia


go
create proc sp_t2
	@maYCBH varchar(10)
as
begin
	select * from CTYCBaoGia where MaYCBaoGia = @maYCBH
end

select SUSER_SNAME()