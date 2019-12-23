use qlmuahang
go
create trigger trg_TinhTongTien_DonMua_i
	on CTMua
	for insert
as
begin
	update DonMuaHang
	set TongTienMua += (select SLMua * DonGia from inserted)
	where MaDonMuaHang = (select MaDonMuaHang from inserted)	
end
go
create trigger trg_TinhTongTien_DonMua_u
	on CTMua
	for update
as
begin 
	if update(slmua) or update(dongia)
		update DonMuaHang
		set TongTienMua += (select SLMua * DonGia from inserted) - (select SLMua * DonGia from deleted)
		where MaDonMuaHang = (select MaDonMuaHang from inserted)	
end
go
create trigger trg_TinhTongTien_DonMua_d
	on CTMua
	for delete
as
begin
	update DonMuaHang
	set TongTienMua -= (select SLMua * DonGia from deleted)
	where MaDonMuaHang = (select MaDonMuaHang from deleted)	
end
go
-- Cập nhật giá bản của nhà cung cấp
create trigger trg_CapNhat_GiaSP
	on CTYCBaoGia
	for update
as
begin
	declare cr_trg_update_CTYCBG cursor forward_only
	for select MaNPP, MaSP, GiaDaBao, MaYCBaoGia from inserted

	declare @maNPP varchar(10), @maSP int, @gia money, @maYCBG int
	
	open cr_trg_update_CTYCBG
	fetch next from cr_trg_update_CTYCBG into @maNPP, @maSP, @gia, @maYCBG

	while @@FETCH_STATUS=0
	begin
		update CTSP
		set GiaMua = @gia
		where MaSP = @maSP
			and manpp = @maNPP

		fetch next from cr_trg_update_CTYCBG into @maNPP, @maSP, @gia, @maYCBG
	end
	close cr_trg_update_CTYCBG 
	deallocate cr_trg_update_CTYCBG 
	
	-- update tinh trang cua YCBG
	if not exists (select masp from CTYCBaoGia 
					where maycbaogia = @maYCBG
						and GiaDaBao = 0)
		update yeucaubaogia
		set tinhtrang = 'Da bao xong'
		where maycbaogia = @maYCBG
end
go