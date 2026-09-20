# Lab 03 - Quản lý sinh viên bằng Console (C# OOP)

- **Học phần:** COMP1019 - Lập trình trên Windows
- **Sinh viên thực hiện:** [Họ tên] - MSSV: [MSSV]
- **Công cụ:** Visual Studio, Console App C# (.NET 6 trở lên)

## 1. Giới thiệu

Bài lab xây dựng một chương trình Console quản lý danh sách sinh viên theo hướng đối tượng. Người dùng thao tác qua menu để thêm, xem, tìm kiếm, sửa điểm, xóa, sắp xếp và lọc sinh viên. Dữ liệu chỉ lưu trong bộ nhớ (`List<SinhVien>`), tắt chương trình là mất.

Qua bài này em luyện các phần: class, property, constructor, kế thừa, override, `List<T>` và LINQ cơ bản.

## 2. Cấu trúc project

```
Lab03_QuanLySinhVienOOP
|-- Nguoi.cs
|-- SinhVien.cs
|-- QuanLySinhVien.cs
|-- Program.cs
```

## 3. Các class chính

**Nguoi** là class cha, có `HoTen` và `NgaySinh`. Hai property này đều có kiểm tra: họ tên không được rỗng, ngày sinh không được ở tương lai. Method `LayThongTin()` khai báo `virtual` để class con ghi đè.

**SinhVien** kế thừa `Nguoi`, thêm `MaSinhVien`, `MaLop`, `DiemTrungBinh`. Property `DiemTrungBinh` chỉ nhận giá trị từ 0 đến 10, ngoài khoảng này sẽ ném `ArgumentException`. Method `XepLoai()` xếp loại theo điểm:

| Điểm | Xếp loại |
|---|---|
| từ 9 trở lên | Xuất sắc |
| từ 8 đến dưới 9 | Giỏi |
| từ 7 đến dưới 8 | Khá |
| từ 5 đến dưới 7 | Trung bình |
| dưới 5 | Yếu |

`LayThongTin()` được override để in thêm mã sinh viên, lớp, điểm và xếp loại.

**QuanLySinhVien** giữ `List<SinhVien>` ở dạng `private`, các class khác chỉ làm việc với danh sách qua các method: `Them`, `SuaDiem`, `Xoa`, `TimTheoMa`, `TimTheoTen`, `SapXepTheoDiem`, `LocSinhVienDat`, `LayDanhSach`. Cách này giúp việc kiểm tra trùng mã nằm ở một chỗ duy nhất.

**Program** chứa `Main`, menu và các hàm nhập dữ liệu. Class này chỉ nhận dữ liệu từ bàn phím, gọi `QuanLySinhVien` xử lý rồi in kết quả.

## 4. Chức năng

| Mã | Chức năng |
|---|---|
| 1 | Thêm sinh viên (không cho trùng mã) |
| 2 | Xuất danh sách dạng bảng: mã, họ tên, lớp, điểm, xếp loại |
| 3 | Tìm sinh viên theo mã |
| 4 | Tìm sinh viên theo tên (chứa từ khóa, không phân biệt hoa thường) |
| 5 | Sửa điểm trung bình |
| 6 | Xóa sinh viên |
| 7 | Sắp xếp theo điểm giảm dần |
| 8 | Lọc sinh viên đạt (điểm từ 5 trở lên) |
| 0 | Thoát |

## 5. Kỹ thuật sử dụng

- **Kế thừa và override:** `SinhVien : Nguoi`, override `LayThongTin()`.
- **Đóng gói:** danh sách để `private`, `LayDanhSach()` trả về `IReadOnlyList<SinhVien>` nên bên ngoài chỉ xem được.
- **LINQ:** `FirstOrDefault` để tìm theo mã, `Where` để tìm theo tên và lọc sinh viên đạt, `OrderByDescending` để sắp xếp. Việc sắp xếp trả về danh sách mới, không đổi danh sách gốc.
- **Xử lý nhập sai:** dùng `TryParse` và `TryParseExact` trong vòng lặp, nhập sai thì báo lỗi và cho nhập lại nên chương trình không bị dừng. Điểm nhận cả dấu chấm lẫn dấu phẩy (8.2 hoặc 8,2).
- **Tiếng Việt:** đặt `Console.OutputEncoding` và `Console.InputEncoding` là UTF-8 để hiển thị có dấu.

## 6. Cách chạy

1. Tạo project Console App (C#) tên `Lab03_QuanLySinhVienOOP` trong Visual Studio.
2. Thêm 4 file `.cs` vào project, thay thế `Program.cs` mặc định.
3. Nhấn Ctrl + F5 để chạy.

## 7. Kiểm thử

Cột "Kết quả thực tế" điền lại sau khi chạy chương trình.

| STT | Tình huống | Kết quả mong đợi | Kết quả thực tế |
|---|---|---|---|
| 1 | Thêm SV001, Nguyễn Văn A, điểm 8.2 | Thêm thành công, xếp loại Giỏi | |
| 2 | Thêm lại mã SV001 | Báo mã đã tồn tại | |
| 3 | Nhập điểm -1 hoặc 11 | Báo điểm không hợp lệ, yêu cầu nhập lại | |
| 4 | Tìm theo tên với từ khóa "Nguyễn" | In các sinh viên có họ tên chứa từ khóa | |
| 5 | Xóa mã không tồn tại | Báo không tìm thấy | |
| 6 | Lọc sinh viên đạt | Chỉ in sinh viên có điểm từ 5 trở lên | |
| 7 | Nhập chữ ở chỗ nhập điểm hoặc chọn menu | Báo lỗi, không bị dừng chương trình | |

## 8. Nhận xét

[Phần này bạn tự viết: khó khăn gặp phải khi làm, phần nào em thấy dễ hiểu / khó hiểu, nếu có thời gian sẽ cải tiến gì, ví dụ lưu dữ liệu ra file.]
