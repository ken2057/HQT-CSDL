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