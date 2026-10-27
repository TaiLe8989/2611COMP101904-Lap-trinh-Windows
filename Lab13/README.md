# BÁO CÁO LAB 13 - BÁO CÁO SINH VIÊN BẰNG LINQ

# 2611COMP101904 - Lập trình Windows
- **MSSV:** 51.01.104.087
- **Họ và Tên:** Lê Minh Tài
- **Lớp:** 51.01.CNTT.C
- **Nhóm:** 3


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

## 9. Ảnh mẫu thực tế 
<img width="1141" height="642" alt="Screenshot 2026-10-27 130453" src="https://github.com/user-attachments/assets/11e3147e-adb8-4120-ac30-0d54a3793fea" />
<img width="1137" height="652" alt="Screenshot 2026-10-27 130512" src="https://github.com/user-attachments/assets/869693ec-088d-4d5b-ba96-f1204715cc28" />
<img width="1133" height="641" alt="Screenshot 2026-10-27 130524" src="https://github.com/user-attachments/assets/de051f2e-c7b7-41ca-8b99-92a512d0b33d" />
<img width="1145" height="646" alt="Screenshot 2026-10-27 130535" src="https://github.com/user-attachments/assets/098f5a37-777d-4ca7-a025-abf8ef73a508" />
<img width="1133" height="650" alt="Screenshot 2026-10-27 130651" src="https://github.com/user-attachments/assets/5bc15388-b01a-405f-af2e-083ef3c5c7cf" />
<img width="1138" height="648" alt="Screenshot 2026-10-27 130713" src="https://github.com/user-attachments/assets/edffa5eb-dd50-45f1-b71a-8705fd2acfd9" />
<img width="1138" height="651" alt="Screenshot 2026-10-27 130823" src="https://github.com/user-attachments/assets/45f18ce4-01b5-4f74-83de-20a8d0bb5bc0" />
<img width="1193" height="687" alt="Screenshot 2026-10-27 130816" src="https://github.com/user-attachments/assets/9fcf72f5-aaf3-4276-8b75-97cc0dd1c2bd" />
<img width="542" height="386" alt="Screenshot 2026-10-27 130837" src="https://github.com/user-attachments/assets/4eb40a28-46fe-40ff-b6a1-728645da039d" />


