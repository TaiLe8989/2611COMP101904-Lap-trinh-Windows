
USE EduManagerDB;
GO
SELECT MaSV, HoTen, NgaySinh, GioiTinh, MaLop
FROM SINHVIEN;
GO
SELECT sv.MaSV, sv.HoTen, l.TenLop, k.TenKhoa
FROM SINHVIEN sv
INNER JOIN LOP  l ON sv.MaLop  = l.MaLop
INNER JOIN KHOA k ON l.MaKhoa  = k.MaKhoa;
GO

SELECT MaSV, HoTen, MaLop
FROM SINHVIEN
WHERE HoTen LIKE N'%An%'
ORDER BY HoTen ASC;
GO

SELECT MaSV, HoTen, GioiTinh
FROM SINHVIEN
WHERE MaLop = 'CNTT01';
GO

UPDATE SINHVIEN
SET HoTen = N'Nguyễn Văn An Khang'
WHERE MaSV = 'SV001';

UPDATE SINHVIEN
SET MaLop = 'CNTT01'
WHERE MaSV = 'SV004';

SELECT MaSV, HoTen, MaLop FROM SINHVIEN WHERE MaSV IN ('SV001', 'SV004');
GO

DELETE FROM SINHVIEN
WHERE MaSV = 'SV007'
  AND NOT EXISTS (SELECT 1 FROM KETQUA WHERE MaSV = 'SV007');

SELECT MaSV, HoTen, MaLop FROM SINHVIEN;
GO

SELECT sv.MaSV, sv.HoTen, mh.TenMH, kq.Diem
FROM KETQUA kq
INNER JOIN SINHVIEN sv ON kq.MaSV = sv.MaSV
INNER JOIN MONHOC   mh ON kq.MaMH = mh.MaMH
ORDER BY sv.MaSV, mh.TenMH;
GO

SELECT sv.MaSV, sv.HoTen, CAST(AVG(kq.Diem) AS DECIMAL(4,2)) AS DiemTrungBinh
FROM SINHVIEN sv
INNER JOIN KETQUA kq ON sv.MaSV = kq.MaSV
GROUP BY sv.MaSV, sv.HoTen
ORDER BY sv.MaSV;
GO

SELECT l.MaLop, l.TenLop, COUNT(sv.MaSV) AS SoSinhVien
FROM LOP l
LEFT JOIN SINHVIEN sv ON l.MaLop = sv.MaLop
GROUP BY l.MaLop, l.TenLop;
GO

SELECT l.MaLop, l.TenLop, COUNT(sv.MaSV) AS SoSinhVien
FROM LOP l
LEFT JOIN SINHVIEN sv ON l.MaLop = sv.MaLop
GROUP BY l.MaLop, l.TenLop
HAVING COUNT(sv.MaSV) >= 2;
GO

BEGIN TRY
    INSERT INTO SINHVIEN (MaSV, HoTen, NgaySinh, GioiTinh, MaLop)
    VALUES ('SV999', N'Sinh viên lỗi', '2005-01-01', N'Nam', 'CNTT99');
    PRINT N'11. KHÔNG có lỗi -> ràng buộc khóa ngoại chưa hoạt động!';
END TRY
BEGIN CATCH
    PRINT N'11. Lỗi như mong đợi (khóa ngoại): ' + ERROR_MESSAGE();
END CATCH
GO

BEGIN TRY
    INSERT INTO KETQUA (MaSV, MaMH, Diem)
    VALUES ('SV005', 'COMP1019', 12);
    PRINT N'12. KHÔNG có lỗi -> ràng buộc điểm chưa hoạt động!';
END TRY
BEGIN CATCH
    PRINT N'12. Lỗi như mong đợi (CHECK điểm): ' + ERROR_MESSAGE();
END CATCH
GO

BEGIN TRY
    INSERT INTO SINHVIEN (MaSV, HoTen, NgaySinh, GioiTinh, MaLop)
    VALUES ('SV998', N'Giới tính lỗi', '2005-01-01', N'Khác', 'CNTT01');
    PRINT N'13. KHÔNG có lỗi -> ràng buộc giới tính chưa hoạt động!';
END TRY
BEGIN CATCH
    PRINT N'13. Lỗi như mong đợi (CHECK giới tính): ' + ERROR_MESSAGE();
END CATCH
GO

BEGIN TRY
    INSERT INTO SINHVIEN (MaSV, HoTen, NgaySinh, GioiTinh, MaLop)
    VALUES ('SV001', N'Trùng mã', '2005-01-01', N'Nam', 'CNTT01');
    PRINT N'14. KHÔNG có lỗi -> ràng buộc khóa chính chưa hoạt động!';
END TRY
BEGIN CATCH
    PRINT N'14. Lỗi như mong đợi (khóa chính): ' + ERROR_MESSAGE();
END CATCH
GO
