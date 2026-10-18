# BÁO CÁO LAB 10 – THIẾT KẾ VÀ CÀI ĐẶT CSDL EDUMANAGERDB

**Môn học:** COMP1019 – Lập trình trên Windows
**Sinh viên:** ……………………………… **MSSV:** ………………… **Lớp:** …………………
**Công cụ:** SQL Server + SQL Server Management Studio (SSMS 22)

---

## 1. Mục tiêu
- Thiết kế CSDL quan hệ cho bài toán quản lý sinh viên.
- Tạo database, bảng, khóa chính, khóa ngoại và ràng buộc bằng T-SQL.
- Nhập dữ liệu mẫu đúng quan hệ giữa các bảng.
- Viết truy vấn SELECT, JOIN, GROUP BY, UPDATE, DELETE.
- Kiểm thử các ràng buộc bằng những trường hợp nhập sai.

## 2. Mô tả bài toán
CSDL **EduManagerDB** quản lý: khoa, lớp, sinh viên, môn học, kết quả học tập và tài khoản đăng nhập.

## 3. Thiết kế CSDL

**Quan hệ:** KHOA 1–n LOP 1–n SINHVIEN 1–n KETQUA n–1 MONHOC (TAIKHOAN độc lập).

| Bảng | Khóa chính | Khóa ngoại | Ghi chú |
|---|---|---|---|
| KHOA | MaKhoa | – | Thông tin khoa |
| LOP | MaLop | MaKhoa → KHOA | Mỗi lớp thuộc một khoa |
| SINHVIEN | MaSV | MaLop → LOP | Mỗi sinh viên thuộc một lớp |
| MONHOC | MaMH | – | Thông tin môn học |
| KETQUA | (MaSV, MaMH) | MaSV → SINHVIEN, MaMH → MONHOC | Điểm theo môn |
| TAIKHOAN | TenDangNhap | – | Tài khoản demo |

**Ràng buộc CHECK:**
- `KETQUA.Diem` nằm trong khoảng 0 đến 10.
- `SINHVIEN.GioiTinh` chỉ nhận `Nam` hoặc `Nữ`.
- `MONHOC.SoTinChi` lớn hơn 0.

KETQUA dùng **khóa chính ghép** (MaSV, MaMH) để mỗi sinh viên chỉ có một điểm cho mỗi môn.

## 4. Dữ liệu mẫu
2 khoa, 3 lớp, 7 sinh viên, 4 môn học, 8 kết quả, 2 tài khoản.
Nhập theo thứ tự: KHOA → LOP → SINHVIEN, MONHOC → KETQUA, TAIKHOAN.
(SV007 chưa có điểm, dùng để thử lệnh xóa ở câu 6.)

## 5. Các truy vấn đã thực hiện (file `03_Queries.sql`)

| Câu | Nội dung | Kỹ thuật |
|---|---|---|
| 1 | Liệt kê toàn bộ sinh viên | SELECT |
| 2 | Sinh viên kèm tên lớp và tên khoa | INNER JOIN |
| 3 | Tìm sinh viên theo một phần họ tên | LIKE |
| 4 | Liệt kê sinh viên theo mã lớp | WHERE |
| 5 | Cập nhật họ tên / lớp của sinh viên | UPDATE |
| 6 | Xóa sinh viên chưa có điểm | DELETE + NOT EXISTS |
| 7 | Điểm của từng sinh viên theo từng môn | JOIN 3 bảng |
| 8 | Điểm trung bình từng sinh viên | AVG, GROUP BY |
| 9 | Số sinh viên theo từng lớp | COUNT, LEFT JOIN, GROUP BY |
| 10 | Các lớp có từ 2 sinh viên trở lên | HAVING |
| 11 | Thêm sinh viên vào lớp không tồn tại | Test khóa ngoại |
| 12 | Nhập điểm ngoài khoảng 0–10 | Test CHECK |

Kiểm thử thêm: giới tính ngoài Nam/Nữ (câu 13) và trùng khóa chính (câu 14).

## 6. Kết quả
- Tạo đủ **6 bảng**, dữ liệu mẫu nhập thành công, các truy vấn 1–10 trả về kết quả đúng.
- Sau câu 5 và 6, lớp CNTT01 có 3 sinh viên, CNTT02 có 1, SPTIN01 có 2; câu 10 chỉ hiển thị CNTT01 và SPTIN01.
- Các lệnh sai ở câu 11–14 đều bị SQL Server từ chối (vi phạm FOREIGN KEY / CHECK / PRIMARY KEY), chứng tỏ ràng buộc hoạt động đúng.

### Ảnh chụp kết quả
> Chèn ảnh chụp tại đây: câu 2, 8, 9, 10 (tab Results) và câu 11, 12 (tab Messages).

## 7. Cách chạy
Mở SSMS và chạy lần lượt: `01_CreateDatabase.sql` → `02_InsertSampleData.sql` → `03_Queries.sql`.
Muốn chạy lại từ đầu, chạy lại file 01 (script tự xóa database cũ).

## 8. Kết luận
Bài lab đã xây dựng hoàn chỉnh CSDL EduManagerDB với khóa chính, khóa ngoại và các ràng buộc hợp lý. Các truy vấn SELECT, JOIN, GROUP BY, UPDATE, DELETE hoạt động đúng, và việc kiểm thử cho thấy CSDL ngăn được dữ liệu sai, bảo đảm tính toàn vẹn dữ liệu.
