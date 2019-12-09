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
	if update(GiaDaBao)
	begin
		update CTSP
		set GiaMua = (select GiaDaBao from inserted)
		where MaSP = (select MaSP from inserted)
		and MaNCC = (select MaNCC from inserted)
	end
end
go
