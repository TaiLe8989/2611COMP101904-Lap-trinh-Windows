# BÁO CÁO LAB 10 – THIẾT KẾ VÀ CÀI ĐẶT CSDL EDUMANAGERDB

- **MSSV:** 51.01.104.087
- **Họ và Tên:** Lê Minh Tài
- **Lớp:** 51.01.CNTT.C
- **Công cụ:** SQL Server + SQL Server Management Studio (SSMS 22)

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


## 7. Ảnh mẫu minh họa 
<img width="1473" height="510" alt="Screenshot 2026-10-18 173807" src="https://github.com/user-attachments/assets/c2589a38-574d-484e-ba88-c60cafaae664" />
<img width="1438" height="457" alt="Screenshot 2026-10-18 173748" src="https://github.com/user-attachments/assets/6d1aff52-8523-4e19-a80d-89c304a5b899" />
<img width="1462" height="522" alt="Screenshot 2026-10-18 173829" src="https://github.com/user-attachments/assets/ce267aa4-90ba-4613-b237-39251dcc857e" />
<img width="1458" height="527" alt="Screenshot 2026-10-18 173820" src="https://github.com/user-attachments/assets/5f9d58ba-c0d8-470b-8bf3-ca5c377a1be9" />
