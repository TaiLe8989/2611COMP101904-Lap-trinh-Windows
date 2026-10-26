UPDATE TaiKhoan
SET MatKhau = '123456',
    TrangThai = 1
WHERE TenDangNhap IN ('admin', 'staff');

SELECT MaTK, TenDangNhap, MatKhau, VaiTro, TrangThai FROM TaiKhoan;
DELETE FROM TaiKhoan WHERE TenDangNhap = 'hello8989';