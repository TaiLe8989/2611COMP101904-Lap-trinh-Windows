# QLSinhVienLab12 - Quản lý kết quả học tập

Bài Lab 12 - COMP1019 Lập trình trên Windows: Data Binding, Stored Procedure và Transaction.

## Công nghệ
C# WinForms (.NET 8), SQL Server, ADO.NET, Microsoft.Data.SqlClient.

## Cấu trúc
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

## Cách chạy
1. Mở SSMS, mở file `Database/QLSinhVienLab12.sql`, nhấn Ctrl+A rồi Execute để tạo database, bảng, dữ liệu mẫu và 4 stored procedure.
2. Nếu cần, sửa `Database.ConnectionString` trong `DAL/Database.cs` cho đúng tên server.
3. Mở `QLSinhVienLab12.csproj` bằng Visual Studio 2022 rồi nhấn F5.

## Chức năng
- Tải danh sách lớp và môn học vào ComboBox khi mở form.
- Chọn lớp, môn học và bấm Tải danh sách để hiển thị sinh viên cùng điểm (nếu đã có).
- Nhập hoặc sửa cột Điểm, Ghi chú; kiểm tra điểm không rỗng, là số, từ 0 đến 10.
- Lưu tất cả lưu nhiều dòng trong một transaction; có lỗi thì rollback toàn bộ.
- Mở rộng: tìm kiếm theo mã hoặc tên sinh viên, thống kê số lượng, số sinh viên đạt và điểm trung bình.
