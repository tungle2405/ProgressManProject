-- create tables
CREATE TABLE PhanQuyen (
    MaPhanQuyen nchar(10) PRIMARY KEY,
    TenPhanQuyen nvarchar(50) NOT NULL,
);

CREATE TABLE DonVi (
    MaDonVi nchar(10) PRIMARY KEY,
    TenDonVi nvarchar(50)
);

CREATE TABLE PhongDaoTao (
    MaNhanVien nchar(10) PRIMARY KEY,
    HoTen nvarchar(50) NOT NULL,
    GioiTinh nvarchar(50),
    TaiKhoan nvarchar(50) NOT NULL,
  	MatKhau nvarchar(50) NOT NULL
);

CREATE TABLE GiangVien (
    MaGiangVien nchar(10) PRIMARY KEY,
    HoTen nvarchar(50) NOT NULL,
    GioiTinh nvarchar(50),
    TrinhDo nvarchar(50),
	ChuyenMon nvarchar(50),
	MaDonVi nchar(10),
	MaPhanQuyen nchar(10),
    TaiKhoan nvarchar(50) NOT NULL,
  	MatKhau nvarchar(50) NOT NULL
	FOREIGN KEY (MaDonVi) REFERENCES DonVi (MaDonVi) ON UPDATE CASCADE,
	FOREIGN KEY (MaPhanQuyen) REFERENCES PhanQuyen (MaPhanQuyen) ON UPDATE CASCADE,
);

CREATE TABLE SinhVien (
    MaSinhVien nchar(10) PRIMARY KEY,
    HoTen nvarchar(50) NOT NULL,
    GioiTinh nvarchar(50),
    LopNienChe nvarchar(50),
    TaiKhoan nvarchar(50) NOT NULL,
  	MatKhau nvarchar(50) NOT NULL
);
INSERT INTO SinhVien VALUES ('1', 'Nguyen Van A', 'Nam', '20A1', 'adminer', '123')
INSERT INTO SinhVien VALUES ('2A02', 'Hoang Vi Oanh', 'Nu', '20A1', 'student', '000')

select * from SinhVien

CREATE TABLE ChiTietDD (
    IDDiemDanh INT IDENTITY (1, 1) PRIMARY KEY,
    MaSinhVien nchar(10) NOT NULL,
    SoTietVang int,
    LyDo nvarchar(50),
	FOREIGN KEY (MaSinhVien) REFERENCES SinhVien (MaSinhVien) ON UPDATE CASCADE,
);

CREATE TABLE MonHoc (
    MaMonHoc nchar(10) PRIMARY KEY,
    TenMonHoc nvarchar(50) NOT NULL,
    TongSoTiet nvarchar(50),
    SoTietLyThuyet nvarchar(50) NOT NULL,
    SoTietThucHanh nvarchar(50) NOT NULL
);

CREATE TABLE LopMonHoc (
    MaLopMonHoc nchar(10) PRIMARY KEY,
    TenLopMonHoc nvarchar(50),
    MaGiangVien nchar(10) NOT NULL,
	FOREIGN KEY (MaGiangVien) REFERENCES GiangVien (MaGiangVien) ON UPDATE CASCADE,
);

CREATE TABLE PhongHoc (
    MaPhongHoc nchar(10) PRIMARY KEY,
    TenPhongHoc nvarchar(50) NOT NULL,
    DiaChi nvarchar(50),
	SoCho int
);

CREATE TABLE CaHoc (
    MaCaHoc nchar(10) PRIMARY KEY,
    GioBD nchar(20) NOT NULL,
    GioKT nchar(20),
	BuoiHoc nvarchar(20)
);

CREATE TABLE Ca_Phong_LopHoc (
    MaCaHoc nchar(10) NOT NULL,
    MaPhongHoc nchar(10) NOT NULL,
    MaLopHoc nchar(10) NOT NULL,
	Thu nchar(10),
	FOREIGN KEY (MaCaHoc) REFERENCES CaHoc (MaCaHoc) ON UPDATE CASCADE,
	FOREIGN KEY (MaPhongHoc) REFERENCES PhongHoc (MaPhongHoc) ON UPDATE CASCADE,
	FOREIGN KEY (MaLopHoc) REFERENCES LopMonHoc (MaLopMonHoc) ON UPDATE CASCADE,
);

CREATE TABLE MonHoc_LopMonHoc (
    MaLopMonHoc nchar(10) NOT NULL,
	HocKy nchar(10),
	NamHoc nchar(10),
	MaMonHoc nchar(10) NOT NULL,
	SoSinhVien int,
	FOREIGN KEY (MaLopMonHoc) REFERENCES LopMonHoc (MaLopMonHoc) ON UPDATE CASCADE,
	FOREIGN KEY (MaMonHoc) REFERENCES MonHoc (MaMonHoc) ON UPDATE CASCADE,
);

CREATE TABLE SinhVien_Hoc_LopMonHoc (
    MaLopMonHoc nchar(10) NOT NULL,
	MaSinhVien nchar(10) NOT NULL,
	HocKy nchar(10),
	NamHoc nchar(10),
	FOREIGN KEY (MaLopMonHoc) REFERENCES LopMonHoc (MaLopMonHoc) ON UPDATE CASCADE,
	FOREIGN KEY (MaSinhVien) REFERENCES SinhVien (MaSinhVien) ON UPDATE CASCADE,
);

CREATE TABLE GV_DiemDanh_SV (
    IDDiemDanh int NOT NULL,
	MaGiangVien nchar(10) NOT NULL,
	MaLopMonHoc nchar(10) NOT NULL,
	NgayDiemDanh nchar(10),
	LanDiemDanh int,
	FOREIGN KEY (IDDiemDanh) REFERENCES ChiTietDD (IDDiemDanh) ON UPDATE NO ACTION,
	FOREIGN KEY (MaLopMonHoc) REFERENCES LopMonHoc (MaLopMonHoc) ON UPDATE NO ACTION,
	FOREIGN KEY (MaGiangVien) REFERENCES GiangVien (MaGiangVien) ON UPDATE NO ACTION,
);


CREATE PROCEDURE spLogin
@tenTaiKhoan VARCHAR(50),
@matKhau VARCHAR(50)
AS
BEGIN
	select * from SinhVien where SinhVien.TaiKhoan = @tenTaiKhoan and SinhVien.MatKhau = @matKhau
END

DROP PROC spLogin

EXEC spLogin "adminer","123"

CREATE PROCEDURE spRegister
    @tenTaiKhoan NVARCHAR(50),
    @matKhau NVARCHAR(50),
    @maSV NVARCHAR(50),
    @lop nvarchar(50),
	@gioiTinh nvarchar(50)
AS
BEGIN
	DECLARE @Result BIT;
    SET NOCOUNT ON;
    BEGIN TRY
        INSERT INTO SinhVien
        VALUES (@maSV, @tenTaiKhoan, @gioiTinh,@lop,@tenTaiKhoan,@matKhau);
        SET @Result = 1;
    END TRY
    BEGIN CATCH
        SET @Result = 0;
    END CATCH
END
select * from SinhVien