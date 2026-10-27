# BÁO CÁO LAB 12 - DATA BINDING, STORED PROCEDURE VÀ TRANSACTION

# 2611COMP101904 - Lập trình Windows
- **MSSV:** 51.01.104.087
- **Họ và Tên:** Lê Minh Tài
- **Lớp:** 51.01.CNTT.C
- **Nhóm:** 3
- **Giảng viên:** Lê Thanh Thoại

---

## 1. Giới thiệu

Bài lab xây dựng module **Quản lý kết quả học tập** bằng C# WinForms kết nối SQL Server. Người dùng chọn lớp và môn học, hệ thống hiển thị danh sách sinh viên của lớp; người dùng nhập điểm, ghi chú rồi bấm **Lưu tất cả**. Nếu mọi dòng hợp lệ thì dữ liệu được lưu xuống database, nếu có một dòng lỗi thì toàn bộ thao tác bị hủy.

## 2. Mục tiêu

- Dùng `DataTable`, `SqlDataAdapter` và `BindingSource` để hiển thị dữ liệu lên WinForms.
- Binding dữ liệu vào `ComboBox` và `DataGridView`.
- Viết stored procedure để lấy danh sách và lưu kết quả học tập.
- Dùng transaction để lưu nhiều dòng dữ liệu an toàn.
- Kiểm tra dữ liệu điểm trước khi lưu xuống database.

## 3. Công nghệ sử dụng

| Thành phần | Nội dung |
|---|---|
| Ngôn ngữ, giao diện | C# WinForms (.NET 8) |
| Cơ sở dữ liệu | SQL Server |
| Truy cập dữ liệu | ADO.NET, Microsoft.Data.SqlClient |
| Công cụ | Visual Studio 2022, SQL Server Management Studio |

## 4. Cơ sở dữ liệu `QLSinhVienLab12`

| Bảng | Cột | Ràng buộc |
|---|---|---|
| LOP | MaLop, TenLop | MaLop là khóa chính |
| SINHVIEN | MaSV, HoTen, NgaySinh, MaLop | MaSV là khóa chính; MaLop tham chiếu LOP |
| MONHOC | MaMon, TenMon, SoTinChi | MaMon là khóa chính; SoTinChi > 0 |
| KETQUA | MaSV, MaMon, Diem, GhiChu | Khóa chính (MaSV, MaMon); khóa ngoại tới SINHVIEN và MONHOC; Diem từ 0 đến 10 |

Script tạo database, bảng, dữ liệu mẫu và stored procedure nằm ở `Database/QLSinhVienLab12.sql`.

## 5. Stored procedure

| Tên | Chức năng |
|---|---|
| `sp_Lop_GetAll` | Lấy danh sách lớp |
| `sp_MonHoc_GetAll` | Lấy danh sách môn học |
| `sp_KetQua_GetByLopMon` | Lấy sinh viên theo lớp và môn học, dùng LEFT JOIN để vẫn hiện sinh viên chưa có điểm |
| `sp_KetQua_Save` | Nếu sinh viên đã có điểm môn học thì UPDATE, chưa có thì INSERT |

## 6. Cấu trúc project

```
QLSinhVienLab12
├── Forms
│   ├── FrmNhapDiemTheoLop.cs
│   └── FrmNhapDiemTheoLop.Designer.cs
├── DAL
│   ├── Database.cs
│   └── KetQuaDAL.cs
├── Database
│   └── QLSinhVienLab12.sql
└── Program.cs
```

- `Database.cs`: chứa chuỗi kết nối (connection string).
- `KetQuaDAL.cs`: lớp truy cập dữ liệu, gọi stored procedure bằng parameter, không nối chuỗi SQL.
- `FrmNhapDiemTheoLop.cs`: xử lý giao diện, gọi `KetQuaDAL`, không viết SQL trong form.

## 7. Giao diện

Form `FrmNhapDiemTheoLop` gồm:

- `cboLop`, `cboMonHoc`: chọn lớp và môn học.
- `btnTaiDanhSach`: tải danh sách sinh viên.
- `dgvKetQua`: hiển thị STT, Mã SV, Họ tên, Điểm, Ghi chú. Hàng tiêu đề màu xanh, các hàng dữ liệu nền trắng, khi rê chuột vào hàng thì ô STT của hàng đó đổi sang màu xanh.
- `btnLuuTatCa`: lưu toàn bộ điểm.
- `StatusStrip` và `lblThongKe`: hiển thị trạng thái thao tác và thống kê.

## 8. Luồng xử lý

1. **Mở form:** gọi `sp_Lop_GetAll` và `sp_MonHoc_GetAll`, binding kết quả vào hai ComboBox (`DisplayMember`, `ValueMember`).
2. **Tải danh sách:** gọi `sp_KetQua_GetByLopMon`, đưa `DataTable` vào `BindingSource` rồi gán cho `dgvKetQua`.
3. **Nhập liệu:** người dùng sửa cột Điểm và Ghi chú; hai cột Mã SV, Họ tên chỉ đọc.
4. **Kiểm tra dữ liệu:** điểm không được rỗng, phải là số, nằm trong khoảng 0 đến 10; ghi chú tối đa 255 ký tự. Dòng lỗi được đánh dấu ngay trên lưới.
5. **Lưu tất cả:** `KetQuaDAL.SaveAll` mở một `SqlTransaction`, gọi `sp_KetQua_Save` cho từng dòng.
6. **Kết quả:** tất cả thành công thì `Commit` và thông báo số dòng đã lưu; có lỗi thì `Rollback` và thông báo lỗi.

## 9. Điểm kỹ thuật chính

- **Data binding:** `DataTable` là dữ liệu trong bộ nhớ; `BindingSource` làm cầu nối giữa dữ liệu và control. Gọi `EndEdit()` trước khi lấy dữ liệu để giá trị đang sửa trên lưới được đẩy về `DataTable`.
- **Transaction:** mọi `SqlCommand` đều gắn với cùng một transaction; `Commit` chỉ gọi sau khi tất cả dòng đã lưu thành công.
- **An toàn dữ liệu:** dùng parameter cho mọi lời gọi stored procedure; ràng buộc `CHECK` trong database là lớp bảo vệ thứ hai sau bước kiểm tra trên form.
- **Tổ chức code:** tách ba tầng giao diện, truy cập dữ liệu và cấu hình kết nối.

## 10. Ảnh mẫu thực tế 
<img width="862" height="590" alt="Screenshot 2026-10-26 003944" src="https://github.com/user-attachments/assets/3de299a5-479d-4c7f-b27e-34a965ef6090" />
<img width="867" height="600" alt="Screenshot 2026-10-26 004028" src="https://github.com/user-attachments/assets/51191beb-335f-4d8f-953f-884f074ed0a9" />
<img width="871" height="601" alt="Screenshot 2026-10-26 004041" src="https://github.com/user-attachments/assets/f38b136d-4f84-487c-b5ae-3b5ff7328fdb" />
<img width="632" height="375" alt="Screenshot 2026-10-26 003925" src="https://github.com/user-attachments/assets/25c209c9-61af-4076-bfed-f10e58d857be" />
<img width="733" height="380" alt="Screenshot 2026-10-26 003918" src="https://github.com/user-attachments/assets/7c52ca13-d478-4afd-b461-d2615f2f3130" />
