# BÁO CÁO LAB 11 – ADO.NET VÀ CRUD VỚI SQL SERVER (QUẢN LÝ LỚP HỌC)

**Môn học:** COMP1019 – Lập trình trên Windows
**Sinh viên:** ……………………………… **MSSV:** ………………… **Lớp:** …………………
**Công cụ:** Visual Studio 2022, .NET 8 Windows Forms, SQL Server + SSMS, Microsoft.Data.SqlClient

---

## 1. Mục tiêu
- Kết nối ứng dụng Windows Forms với database `EduManagerDB` (thiết kế ở Lab 10).
- Hiển thị dữ liệu bảng LOP lên `DataGridView`.
- Thêm, sửa, xóa, tìm kiếm lớp học bằng câu SQL có tham số (`SqlParameter`).
- Kiểm tra dữ liệu nhập, kiểm tra trùng mã lớp và xử lý lỗi khóa ngoại khi xóa.
- Viết code rõ ràng, tách hàm xử lý cơ bản để dễ kiểm thử.

## 2. Mô tả bài toán
Xây dựng Form **FrmLop** quản lý lớp học. Dữ liệu lưu trực tiếp trong SQL Server (không lưu tạm bằng `List<T>`), dùng lại các bảng **KHOA – LOP – SINHVIEN** của Lab 10.

## 3. Thiết kế

### 3.1. Cơ sở dữ liệu (dùng lại từ Lab 10)
| Bảng | Vai trò trong Lab 11 |
|---|---|
| LOP (MaLop, TenLop, MaKhoa) | Bảng được quản lý (CRUD) |
| KHOA (MaKhoa, TenKhoa, DienThoai) | Nạp vào `cboKhoa`; hiển thị tên khoa |
| SINHVIEN (…, MaLop) | Đếm số sinh viên của lớp; chặn xóa lớp đang có sinh viên |

> **Lưu ý:** bảng LOP của Lab 10 có cột `MaKhoa NOT NULL` (khóa ngoại → KHOA), khác với script tối thiểu trong đề. Vì vậy Form có thêm `cboKhoa` để chọn khoa khi thêm/sửa lớp.

### 3.2. Giao diện (FrmLop)
| Control | Tên | Chức năng |
|---|---|---|
| Label + TextBox | lblMaLop, txtMaLop | Mã lớp |
| Label + TextBox | lblTenLop, txtTenLop | Tên lớp |
| Label + ComboBox | lblKhoa, cboKhoa | Chọn khoa (bổ sung do Lab 10) |
| TextBox | txtTimKiem | Từ khóa tìm theo mã hoặc tên lớp |
| DataGridView | dgvLop | Danh sách lớp (Mã lớp, Tên lớp, Khoa, Số sinh viên) |
| Button | btnThem, btnSua, btnXoa | Thêm / Sửa / Xóa |
| Button | btnLamMoi | Xóa nội dung nhập, mở khóa `txtMaLop`, load lại dữ liệu |
| Button | btnTimKiem | Tìm kiếm |

## 4. Cài đặt chức năng

| Mã | Chức năng | Cách thực hiện |
|---|---|---|
| F1 | Load danh sách khi mở Form | `FrmLop_Load` → `LoadKhoa()`, `LoadData()` (SELECT + LEFT JOIN, `SqlDataAdapter` → `DataTable`) |
| F2 | Click dòng đưa lên TextBox | `dgvLop_CellClick`; khóa `txtMaLop` (ReadOnly) vì là khóa chính |
| F3 | Thêm lớp | `ValidateInput()` + `IsMaLopExists()` → `INSERT` có tham số |
| F4 | Sửa lớp | Chỉ chạy khi đã chọn lớp; `UPDATE` tên lớp/khoa theo mã lớp |
| F5 | Xóa lớp | `CountSinhVienByLop()` chặn lớp có sinh viên → hỏi xác nhận (Yes/No) → `DELETE`; bắt thêm `SqlException 547` |
| F6 | Tìm kiếm | `WHERE MaLop LIKE @kw OR TenLop LIKE @kw` với `@kw = "%" + từ khóa + "%"` |
| F7 | Làm mới | `ClearInput()` + `LoadData()` |
| F8 | Chống SQL Injection | Toàn bộ dữ liệu người dùng đều truyền qua `SqlParameter`, không nối chuỗi SQL |

**Validation:** mã lớp rỗng, tên lớp rỗng, chưa chọn khoa, mã lớp đã tồn tại, chưa chọn lớp khi sửa/xóa, lớp đang có sinh viên khi xóa.
**Xử lý lỗi:** mọi thao tác truy cập database đều nằm trong `try-catch`, hiển thị `MessageBox` thân thiện.

## 5. Kiểm thử

| STT | Tình huống | Dữ liệu | Kết quả mong đợi |
|---|---|---|---|
| 1 | Load Form | Mở FrmLop | Hiển thị danh sách lớp (CNTT01, CNTT02, SPTIN01…) |
| 2 | Thêm hợp lệ | MaLop = TEST01, TenLop = Lớp test, chọn khoa | Thêm thành công, lưới cập nhật |
| 3 | Thêm thiếu mã | MaLop rỗng | Thông báo nhập mã lớp |
| 4 | Thêm trùng mã | MaLop = CNTT01 | Thông báo mã lớp đã tồn tại |
| 5 | Sửa hợp lệ | Chọn TEST01, đổi tên | Cập nhật thành công |
| 6 | Sửa khi chưa chọn lớp | – | Thông báo chọn lớp |
| 7 | Xóa lớp chưa có sinh viên | TEST01 | Hỏi xác nhận → xóa thành công |
| 8 | Xóa lớp đang có sinh viên | CNTT01 | Không cho xóa |
| 9 | Tìm kiếm | keyword = CNTT | Hiển thị các lớp phù hợp |

### Ảnh chụp kết quả
> Chèn ảnh chụp Form đang load dữ liệu từ SQL Server tại đây (kèm ảnh thêm/xóa/tìm kiếm nếu có).

## 6. Cấu trúc project
```
COMP1019_Buoi11_Lab11
├── COMP1019_Buoi11_Lab11.csproj
├── Program.cs
├── FrmLop.cs
├── FrmLop.Designer.cs
├── Database/
│   ├── 01_CreateDatabase.sql
│   └── 02_InsertSampleData.sql
└── README.md
```

## 7. Cách chạy
1. Mở SSMS, chạy `Database/01_CreateDatabase.sql` rồi `Database/02_InsertSampleData.sql`.
2. Mở file `.csproj` bằng Visual Studio 2022, restore NuGet (`Microsoft.Data.SqlClient`), nhấn **F5**.

**Connection string (máy cá nhân):**
```
Data Source=.;Initial Catalog=EduManagerDB;Integrated Security=True;TrustServerCertificate=True
```
Nếu dùng SQL Express, đổi thành `Data Source=.\SQLEXPRESS` (biến `connectionString` trong `FrmLop.cs`).

## 8. Kết luận
Bài lab đã xây dựng Form quản lý lớp học kết nối trực tiếp SQL Server bằng ADO.NET, thực hiện đầy đủ thêm, sửa, xóa, tìm kiếm với `SqlParameter`. Chương trình kiểm tra dữ liệu nhập, chặn trùng mã và chặn xóa lớp đang có sinh viên, qua đó đảm bảo toàn vẹn dữ liệu của EduManagerDB. Code được tách hàm rõ ràng, có xử lý lỗi bằng try-catch.
