# Checklist nhanh trước khi mở Pull Request

- [ ] Đã chạy SQL script tạo bảng và dữ liệu mẫu từ nhánh main.
- [ ] `App.config` kết nối đúng SQL Server trên máy.
- [ ] `admin/123456` → menu Quản trị + Nghiệp vụ.
- [ ] `staff/123456` → chỉ Nghiệp vụ + Hệ thống.
- [ ] Đăng nhập sai / tài khoản bị khóa / SQL Server tắt đều có thông báo.
- [ ] Đăng xuất quay lại form đăng nhập.
- [ ] Nhãn VI/EN đổi được; hover và bounce quan sát được.
- [ ] Không có mật khẩu lưu trong file cục bộ; chỉ nhớ username.
- [ ] Không thêm `Database.cs` trùng với bản TV3.
- [ ] Visual Studio Rebuild thành công trên Windows với .NET Framework 4.7.2.
- [ ] Commit nhỏ, rõ ý; PR kèm mô tả cách test và ảnh giao diện.
