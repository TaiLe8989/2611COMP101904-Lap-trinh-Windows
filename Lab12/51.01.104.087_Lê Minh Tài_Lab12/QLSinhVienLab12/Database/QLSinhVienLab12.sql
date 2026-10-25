USE master;
GO

IF DB_ID(N'QLSinhVienLab12') IS NOT NULL
BEGIN
    ALTER DATABASE QLSinhVienLab12 SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE QLSinhVienLab12;
END
GO

CREATE DATABASE QLSinhVienLab12;
GO

USE QLSinhVienLab12;
GO

CREATE TABLE LOP (
    MaLop  NVARCHAR(20)  NOT NULL PRIMARY KEY,
    TenLop NVARCHAR(100) NOT NULL
);

CREATE TABLE SINHVIEN (
    MaSV     NVARCHAR(20)  NOT NULL PRIMARY KEY,
    HoTen    NVARCHAR(100) NOT NULL,
    NgaySinh DATE          NULL,
    MaLop    NVARCHAR(20)  NOT NULL,
    CONSTRAINT FK_SINHVIEN_LOP FOREIGN KEY (MaLop) REFERENCES LOP(MaLop)
);

CREATE TABLE MONHOC (
    MaMon    NVARCHAR(20)  NOT NULL PRIMARY KEY,
    TenMon   NVARCHAR(100) NOT NULL,
    SoTinChi INT           NOT NULL CHECK (SoTinChi > 0)
);

CREATE TABLE KETQUA (
    MaSV   NVARCHAR(20)  NOT NULL,
    MaMon  NVARCHAR(20)  NOT NULL,
    Diem   FLOAT         NOT NULL,
    GhiChu NVARCHAR(255) NULL,
    CONSTRAINT PK_KETQUA PRIMARY KEY (MaSV, MaMon),
    CONSTRAINT FK_KETQUA_SINHVIEN FOREIGN KEY (MaSV) REFERENCES SINHVIEN(MaSV),
    CONSTRAINT FK_KETQUA_MONHOC FOREIGN KEY (MaMon) REFERENCES MONHOC(MaMon),
    CONSTRAINT CK_KETQUA_DIEM CHECK (Diem >= 0 AND Diem <= 10)
);
GO

INSERT INTO LOP (MaLop, TenLop) VALUES
(N'K51.CNTT.A', N'Công nghệ thông tin K51A'),
(N'K51.CNTT.B', N'Công nghệ thông tin K51B'),
(N'K50.CNTT.A', N'Công nghệ thông tin K50A'),
(N'K50.CNTT.B', N'Công nghệ thông tin K50B'),
(N'K49.CNTT.A', N'Công nghệ thông tin K49A'),
(N'K48.CNTT.A', N'Công nghệ thông tin K48A');

INSERT INTO SINHVIEN (MaSV, HoTen, NgaySinh, MaLop) VALUES
(N'SV001', N'Nguyễn Văn A',    '2007-01-15', N'K51.CNTT.A'),
(N'SV002', N'Trần Thị B',      '2007-05-20', N'K51.CNTT.B'),
(N'SV003', N'Lê Minh C',       '2007-03-11', N'K51.CNTT.A'),
(N'SV004', N'Phạm Thanh D',    '2004-07-09', N'K48.CNTT.A'),
(N'SV005', N'Hoàng Gia H',     '2006-09-23', N'K50.CNTT.B'),
(N'SV006', N'Võ Thị N',        '2005-11-02', N'K49.CNTT.A');

INSERT INTO MONHOC (MaMon, TenMon, SoTinChi) VALUES
(N'COMP1019', N'Lập trình trên Windows', 3),
(N'COMP1017', N'Cơ sở lập trình', 3);

INSERT INTO KETQUA (MaSV, MaMon, Diem, GhiChu) VALUES
(N'SV001', N'COMP1019', 8.5, N'Khá tốt'),
(N'SV003', N'COMP1019', 7.0, N'');
GO

CREATE OR ALTER PROCEDURE sp_Lop_GetAll
AS
BEGIN
    SELECT MaLop, TenLop
    FROM LOP
    ORDER BY TenLop;
END;
GO

CREATE OR ALTER PROCEDURE sp_MonHoc_GetAll
AS
BEGIN
    SELECT MaMon, TenMon, SoTinChi
    FROM MONHOC
    ORDER BY TenMon;
END;
GO

CREATE OR ALTER PROCEDURE sp_KetQua_GetByLopMon
    @MaLop NVARCHAR(20),
    @MaMon NVARCHAR(20)
AS
BEGIN
    SELECT sv.MaSV,
           sv.HoTen,
           CAST(kq.Diem AS FLOAT) AS Diem,
           ISNULL(kq.GhiChu, N'') AS GhiChu
    FROM SINHVIEN sv
    LEFT JOIN KETQUA kq
           ON sv.MaSV = kq.MaSV AND kq.MaMon = @MaMon
    WHERE sv.MaLop = @MaLop
    ORDER BY sv.MaSV;
END;
GO

CREATE OR ALTER PROCEDURE sp_KetQua_Save
    @MaSV   NVARCHAR(20),
    @MaMon  NVARCHAR(20),
    @Diem   FLOAT,
    @GhiChu NVARCHAR(255)
AS
BEGIN
    IF EXISTS (SELECT 1 FROM KETQUA WHERE MaSV = @MaSV AND MaMon = @MaMon)
    BEGIN
        UPDATE KETQUA
        SET Diem = @Diem,
            GhiChu = @GhiChu
        WHERE MaSV = @MaSV AND MaMon = @MaMon;
    END
    ELSE
    BEGIN
        INSERT INTO KETQUA (MaSV, MaMon, Diem, GhiChu)
        VALUES (@MaSV, @MaMon, @Diem, @GhiChu);
    END
END;
GO


USE QLSinhVienLab12;
SELECT name FROM sys.tables;
SELECT * FROM LOP;
SELECT * FROM SINHVIEN;
USE QLSinhVienLab12; SELECT * FROM KETQUA;
