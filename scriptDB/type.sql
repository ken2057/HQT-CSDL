use qlmuahang
go
-- declare type to input from WPF to SQL
create type CTYCBGType as table (
	MaNCC varchar(20),
	MaSP int,
	SLSeMua int
)
go