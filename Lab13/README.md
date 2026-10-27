# BÁO CÁO LAB 13 - BÁO CÁO SINH VIÊN BẰNG LINQ

**Trường Đại học Sư phạm TP. Hồ Chí Minh - Khoa Công nghệ thông tin**
**Học phần:** COMP1019 - Lập trình trên Windows
**Sinh viên thực hiện:** `<Họ và tên>` - MSSV: `<MSSV>`
**Công nghệ:** C# WinForms (.NET 8), LINQ, DataGridView, SaveFileDialog

---

## 1. Mục tiêu

- Củng cố kỹ năng xử lý dữ liệu bằng LINQ trong C#.
- Thiết kế Form báo cáo trong WinForms.
- Dùng DataGridView để hiển thị kết quả lọc và thống kê.
- Xuất dữ liệu báo cáo ra file CSV hoặc TXT.

## 2. Bài toán

Xây dựng module **Báo cáo danh sách sinh viên theo lớp**. Người dùng chọn lớp, chọn kết quả học tập, nhập từ khóa rồi bấm **Xem báo cáo**. Chương trình dùng LINQ để lọc, sắp xếp, thống kê và hiển thị dữ liệu, đồng thời cho phép xuất báo cáo ra file.

## 3. Cấu trúc project

```
StudentReportDemo
├── Models
│   └── SinhVien.cs
├── Forms
│   ├── FrmBaoCaoSinhVien.cs
│   └── FrmBaoCaoSinhVien.Designer.cs
├── Program.cs
└── StudentReportDemo.csproj
```

| Thành phần | Vai trò |
|---|---|
| `SinhVien.cs` | Lớp dữ liệu: `MaSV`, `HoTen`, `MaLop`, `TenLop`, `Diem`, và `KetQua` (chỉ đọc, "Đạt" nếu `Diem >= 5`, ngược lại "Không đạt") |
| `FrmBaoCaoSinhVien.cs` | Xử lý logic của Form: dữ liệu mẫu, lọc, thống kê, xuất file |
| `FrmBaoCaoSinhVien.Designer.cs` | Khai báo và bố trí các control của giao diện |
| `Program.cs` | Điểm khởi chạy ứng dụng |

## 4. Giao diện

Form `FrmBaoCaoSinhVien` gồm:

| Control | Chức năng |
|---|---|
| `cboLop` | Chọn lớp |
| `cboKetQua` | Chọn Tất cả / Đạt / Không đạt |
| `txtTuKhoa` | Tìm theo mã sinh viên hoặc họ tên |
| `btnXemBaoCao` | Lọc và hiển thị báo cáo |
| `btnThongKe` | Thống kê theo từng lớp |
| `btnXuatFile` | Xuất dữ liệu đang hiển thị ra CSV/TXT |
| `dgvBaoCao` | Hiển thị kết quả |
| `lblThongKe` (StatusStrip) | Hiển thị tổng số sinh viên, số đạt, số không đạt, điểm trung bình |

## 5. Các hàm chính

| Hàm | Mô tả |
|---|---|
| `TaoDuLieuMau` | Tạo danh sách 12 sinh viên thuộc 3 lớp (CTK47A, CTK47B, CTK48A) |
| `LoadComboBox` | Nạp danh sách lớp (`Select` + `Distinct` + `OrderBy`) và các lựa chọn kết quả |
| `LocDuLieu` | Lọc theo lớp, kết quả, từ khóa bằng `Where`; sắp xếp bằng `OrderByDescending`, `ThenBy` |
| `XemBaoCao` | Hiển thị kết quả lọc lên DataGridView; thông báo khi không có dữ liệu phù hợp |
| `HienThiThongKeTheoLop` | Thống kê theo lớp bằng `GroupBy` kết hợp `Count`, `Average` |
| `CapNhatThongKe` | Cập nhật dòng tổng kết trên thanh trạng thái |
| `XuatFile` | Lưu dữ liệu ra CSV/TXT bằng `SaveFileDialog`, có `try/catch` |

## 6. Các kỹ thuật LINQ đã sử dụng

| Mục đích | Phương thức |
|---|---|
| Chọn dữ liệu | `Select` |
| Loại trùng | `Distinct` |
| Lọc | `Where`, `Contains` |
| Sắp xếp | `OrderBy`, `OrderByDescending`, `ThenBy` |
| Nhóm | `GroupBy` |
| Thống kê | `Count`, `Average` |

## 7. Luồng hoạt động

1. Khi mở Form: tạo dữ liệu mẫu, nạp ComboBox, hiển thị báo cáo ban đầu.
2. **Xem báo cáo**: lọc theo lớp, kết quả, từ khóa; kết quả sắp xếp giảm dần theo điểm; cập nhật thanh trạng thái. Nếu không có dữ liệu phù hợp, hiện thông báo.
3. **Thống kê**: hiển thị số lượng, số đạt, số không đạt và điểm trung bình của từng lớp.
4. **Xuất file**: chọn nơi lưu và định dạng CSV hoặc TXT.
   - CSV: ô chứa dấu phẩy hoặc dấu nháy được bọc nháy kép.
   - TXT: các cột ngăn cách bằng Tab.
   - File lưu UTF-8 để hiển thị đúng tiếng Việt khi mở bằng Excel.
   - Lỗi ghi file (file đang mở, không có quyền ghi) được bắt và thông báo.

## 8. Xử lý tình huống

- Không có dữ liệu phù hợp: hiện thông báo rõ ràng.
- DataGridView trống khi xuất file: thông báo, không tạo file.
- Danh sách rỗng khi tính trung bình: trả về 0, tránh lỗi.
- Chưa chọn lớp: bỏ qua, không gây lỗi null.

## 9. Hướng dẫn chạy

1. Cài **Visual Studio 2022** với workload ".NET desktop development" (hoặc .NET 8 SDK).
2. Mở file `StudentReportDemo.csproj`.
3. Nhấn **F5** để chạy.

## 10. Kết quả

- Ảnh chụp Form sau khi lọc: `<chèn ảnh vào thư mục Screenshots và dẫn link tại đây>`
- File báo cáo đã xuất: `<đính kèm BaoCaoSinhVien.csv / .txt>`

## 11. Kết luận

Bài lab đã xây dựng được module báo cáo sinh viên theo lớp, trong đó toàn bộ thao tác lọc, sắp xếp, nhóm và thống kê dùng LINQ. Code được tách thành các hàm nhỏ, tên biến và control rõ ràng, có xử lý ngoại lệ khi xuất file. Qua bài lab, em hiểu cách LINQ giúp xử lý dữ liệu ngắn gọn, dễ đọc và là nền tảng để chuyển sang ORM (LINQ to SQL, Entity Framework) ở các buổi sau.
