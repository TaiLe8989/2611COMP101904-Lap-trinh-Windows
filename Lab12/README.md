# BÁO CÁO LAB 12 - DATA BINDING, STORED PROCEDURE VÀ TRANSACTION

**Trường Đại học Sư phạm TP. Hồ Chí Minh - Khoa Công nghệ thông tin**
**Học phần:** COMP1019 - Lập trình trên Windows
**Sinh viên thực hiện:** Họ tên - MSSV
**Giảng viên:** Lê Thanh Thoại

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

## 10. Kết quả kiểm thử

| Trường hợp | Kết quả mong đợi |
|---|---|
| Mở form | ComboBox hiện đủ lớp và môn học |
| Tải danh sách | Lưới hiện sinh viên của lớp, kèm điểm nếu đã có |
| Nhập điểm hợp lệ rồi Lưu tất cả | Thông báo đã lưu N dòng; bảng `KETQUA` có dữ liệu mới |
| Nhập điểm 15 hoặc chữ | Báo lỗi, không lưu |
| Để trống điểm | Báo chưa nhập điểm, không lưu |
| Một dòng lỗi khi đang lưu | Rollback, không dòng nào được ghi |

## 11. Hướng dẫn chạy

1. Mở SSMS, mở `Database/QLSinhVienLab12.sql`, nhấn Ctrl+A rồi Execute.
2. Nếu cần, sửa `Database.ConnectionString` trong `DAL/Database.cs` cho đúng tên server.
3. Mở `QLSinhVienLab12.csproj` bằng Visual Studio 2022 và nhấn F5.
4. Kiểm tra dữ liệu bằng `SELECT * FROM KETQUA;` trong SSMS.

## 12. Phần mở rộng

- Tìm kiếm sinh viên theo mã hoặc họ tên.
- Thống kê số sinh viên, số đã nhập điểm, số đạt (điểm từ 5 trở lên) và điểm trung bình.

## 13. Ảnh chụp màn hình

| Trước khi lưu | Sau khi lưu |
|---|---|
| ![Trước khi lưu](images/truoc-khi-luu.png) | ![Sau khi lưu](images/sau-khi-luu.png) |

## 14. Kết luận

Bài lab đáp ứng đầy đủ yêu cầu: tạo đúng database và stored procedure, binding dữ liệu vào ComboBox và DataGridView, kiểm tra điểm trước khi lưu, và lưu nhiều dòng bằng transaction có rollback khi lỗi. Qua bài lab, em nắm được cách phối hợp `DataTable`, `BindingSource`, stored procedure và transaction để xây dựng chức năng nhập dữ liệu hàng loạt an toàn.
