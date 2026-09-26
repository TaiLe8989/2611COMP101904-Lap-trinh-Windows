# Lab 03 - Quản lý sinh viên bằng Console (C# OOP)

- **Học phần:** COMP1019 - Lập trình trên Windows
- **Sinh viên thực hiện:** Lê Minh Tài - MSSV: 51.01.104.087
- **Lớp:** 51.01.CNTT.C


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

## 6. Kiểm thử

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

## 7. Ảnh ví dụ chương trình
<img width="456" height="466" alt="Screenshot 2026-09-20 123944" src="https://github.com/user-attachments/assets/143b5bc8-6393-4e49-aef9-d4d60ccb5ef5" />
<img width="932" height="478" alt="Screenshot 2026-09-20 124337" src="https://github.com/user-attachments/assets/3bc0439f-1c56-44d1-83f9-9d2dd038cd7d" />
<img width="741" height="285" alt="Screenshot 2026-09-20 125152" src="https://github.com/user-attachments/assets/90ec4014-902c-42f8-8959-d6ae7a17b1bc" />
<img width="940" height="168" alt="Screenshot 2026-09-20 124602" src="https://github.com/user-attachments/assets/c8dabfd0-3f49-4c73-a767-f92b5ca03948" />
<img width="940" height="168" alt="Screenshot 2026-09-20 124602" src="https://github.com/user-attachments/assets/3ef8a5c2-afd5-4eff-b795-a7cc1818088b" />
<img width="735" height="246" alt="Screenshot 2026-09-20 124633" src="https://github.com/user-attachments/assets/2608932d-df8e-4ed2-b371-249e7c54d523" />
<img width="933" height="416" alt="Screenshot 2026-09-20 124726" src="https://github.com/user-attachments/assets/1f5b549d-cdfb-4bec-b556-3410a0a79a57" />
<img width="755" height="282" alt="Screenshot 2026-09-20 125330" src="https://github.com/user-attachments/assets/0e6476b7-5620-445d-b25d-e6eefc9bc839" />
<img width="746" height="458" alt="Screenshot 2026-09-20 125352" src="https://github.com/user-attachments/assets/7bb4bd2e-b8e2-4b20-88fc-518c40e014d0" />
<img width="536" height="137" alt="Screenshot 2026-09-20 125412" src="https://github.com/user-attachments/assets/6e0fd8ff-1131-42ac-8258-cc0d701ad858" />
<img width="760" height="605" alt="Screenshot 2026-09-20 125447" src="https://github.com/user-attachments/assets/f167bdf9-89de-431c-a238-96d283bdb87e" />
<img width="615" height="542" alt="Screenshot 2026-09-20 170229" src="https://github.com/user-attachments/assets/104da1a1-dce6-4507-86f0-671e5242e75d" />
<img width="766" height="406" alt="Screenshot 2026-09-20 170321" src="https://github.com/user-attachments/assets/93c29579-aba3-4a17-a641-9ed6c6ae7757" />
<img width="988" height="377" alt="Screenshot 2026-09-20 171118" src="https://github.com/user-attachments/assets/d324d65a-a5ae-4deb-8fd6-6ed5a69f0c14" />
<img width="483" height="142" alt="Screenshot 2026-09-20 171349" src="https://github.com/user-attachments/assets/2a23727a-cde5-4aae-88a2-58dc49d76cd7" />
