/* Chỉ chạy nếu bảng dbo.TaiKhoan đã được tạo bởi script cơ sở dữ liệu của nhóm.
   Script không ghi đè tài khoản đã tồn tại. Mật khẩu demo plaintext sẽ được BLL hash lại
   sau lần đăng nhập thành công đầu tiên để tương thích với dữ liệu mẫu hiện có. */
IF NOT EXISTS (SELECT 1 FROM dbo.TaiKhoan WHERE TenDangNhap = N'admin')
BEGIN
    INSERT INTO dbo.TaiKhoan(TenDangNhap, MatKhau, VaiTro, TrangThai)
    VALUES (N'admin', N'123456', N'QuanLy', 1);
END;

IF NOT EXISTS (SELECT 1 FROM dbo.TaiKhoan WHERE TenDangNhap = N'staff')
BEGIN
    INSERT INTO dbo.TaiKhoan(TenDangNhap, MatKhau, VaiTro, TrangThai)
    VALUES (N'staff', N'123456', N'NhanVien', 1);
END;
GO
