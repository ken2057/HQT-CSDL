use qlmuahang
go
-- declare type to input from WPF to SQL
create type CTYCBGType as table (
	MaNPP varchar(20),
	MaSP int,
	SLSeMua int
)
go
create type update_CTYCBGType as table (
	MaNPP varchar(20),
	MaSP int,
	Gia money
)
go