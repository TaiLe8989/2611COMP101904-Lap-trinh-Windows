# BÁO CÁO BÀI LAB BUỔI 5 – WINDOWS FORMS CƠ BẢN (ĐÃ NỘP NGÀY 29/9 - CẬP NHẬT MỚI NHẤT 16/10) <img width="767" height="55" alt="Screenshot 2026-10-18 230132" src="https://github.com/user-attachments/assets/6cf5f1c4-72cb-4ea6-a3dc-467844a464e0" />

- **MSSV:** 51.01.104.087
- **Họ và Tên:** Lê Minh Tài
- **Lớp:** 51.01.CNTT.C



---

## 1. Mục tiêu

- Tạo project Windows Forms App bằng C#.
- Thiết kế giao diện bằng Form Designer, Toolbox và Properties.
- Sử dụng các control cơ bản: `Label`, `TextBox`, `Button`, `ComboBox`, `RadioButton`, `CheckBox`, `DateTimePicker`, `NumericUpDown`, `GroupBox`.
- Đặt tên control đúng quy ước để code dễ đọc.
- Xử lý các sự kiện `Click`, `Load`, `SelectedIndexChanged`, `ValueChanged`.
- Kiểm tra dữ liệu nhập trước khi xử lý và hiển thị kết quả bằng `MessageBox`.

## 2. Mô tả bài toán

Xây dựng ứng dụng WinForms cho phép học viên **đăng ký khóa học**. Ứng dụng chỉ xử lý dữ liệu trên Form, **chưa lưu cơ sở dữ liệu**. Học phí được tính tự động theo khóa học và số tháng đăng ký.

**Công nghệ:** C#, Windows Forms, Visual Studio 2022.

## 3. Thiết kế giao diện

Form gồm tiêu đề, hai `GroupBox` (thông tin học viên, thông tin khóa học) và một vùng nút lệnh. Các control được đặt tên theo quy ước tiền tố (`txt`, `cbo`, `rad`, `chk`, `dtp`, `num`, `lbl`, `btn`, `grp`).

| Nhóm | Control | Tên control | Chức năng |
|---|---|---|---|
| Thông tin học viên | TextBox | `txtHoTen` | Nhập họ tên (tối đa 50 ký tự) |
| Thông tin học viên | TextBox | `txtSoDienThoai` | Nhập số điện thoại (chỉ số, tối đa 10 số) |
| Thông tin học viên | DateTimePicker | `dtpNgaySinh` | Chọn ngày sinh |
| Thông tin học viên | CheckBox | `chkNhanEmail` | Nhận email thông báo |
| Thông tin khóa học | ComboBox | `cboKhoaHoc` | Chọn khóa học |
| Thông tin khóa học | RadioButton | `radOnline` | Hình thức học Online |
| Thông tin khóa học | RadioButton | `radOffline` | Hình thức học Trực tiếp |
| Thông tin khóa học | NumericUpDown | `numSoThang` | Số tháng đăng ký (1 – 12) |
| Thông tin khóa học | Label | `lblTongTien` | Hiển thị tổng học phí |
| Nút lệnh | Button | `btnDangKy` | Xử lý đăng ký |
| Nút lệnh | Button | `btnLamMoi` | Xóa dữ liệu nhập |
| Nút lệnh | Button | `btnThoat` | Thoát chương trình |

Ngoài ra có các control phụ trợ: `lblDemKyTu` (đếm ký tự họ tên, dạng `x/50`), `lblTrangThaiEmail` (hiển thị trạng thái nhận email), `lblHocPhiThang` (học phí một tháng).

Thứ tự Tab đi từ trên xuống dưới, từ trái sang phải.

## 4. Dữ liệu khóa học

| Khóa học | Học phí / tháng |
|---|---|
| C# WinForms cơ bản | 800.000 VNĐ |
| SQL Server cơ bản | 700.000 VNĐ |
| Web Frontend cơ bản | 750.000 VNĐ |
| Lập trình Python cơ bản | 650.000 VNĐ |

## 5. Chức năng đã cài đặt

### 5.1. Khi Form Load (`Form1_Load`)
- Nạp danh sách khóa học vào `cboKhoaHoc`, chọn mặc định khóa học đầu tiên.
- Chọn mặc định hình thức **Online**.
- Thiết lập số tháng tối thiểu **1**, tối đa **12**.
- Hiển thị tổng học phí ban đầu.

### 5.2. Tính học phí
- **Tổng học phí = học phí một tháng × số tháng.**
- Tự động cập nhật khi thay đổi khóa học (`SelectedIndexChanged`) hoặc số tháng (`ValueChanged`).

### 5.3. Nút Đăng ký (`btnDangKy_Click`)
Kiểm tra dữ liệu theo thứ tự, sai ở bước nào thì báo lỗi và đưa con trỏ về ô tương ứng:

1. Họ tên không được rỗng.
2. Số điện thoại không được rỗng.
3. Số điện thoại phải đủ **10 chữ số**; nếu sai hiện thông báo *"Số điện thoại không đúng định dạng, yêu cầu nhập lại"*.
4. Phải chọn khóa học.

Nếu hợp lệ, hiển thị **phiếu đăng ký** bằng `MessageBox` gồm: họ tên, số điện thoại, ngày sinh, khóa học, hình thức học, số tháng, tổng tiền và trạng thái nhận email.

### 5.4. Nút Làm mới (`btnLamMoi_Click`)
Xóa họ tên và số điện thoại; đưa ngày sinh về ngày hiện tại; bỏ chọn nhận email; chọn lại khóa học đầu tiên; chọn lại Online; đưa số tháng về 1; đưa con trỏ về ô họ tên.

### 5.5. Nút Thoát (`btnThoat_Click`)
Hiển thị hộp thoại xác nhận, **chỉ đóng Form khi người dùng chọn Yes**.

### 5.6. Chức năng bổ sung
- Giới hạn ô số điện thoại: chỉ cho nhập chữ số, tối đa 10 ký tự (sự kiện `KeyPress`).
- Đếm số ký tự họ tên theo thời gian thực.
- Hiển thị trạng thái "Có / Không nhận email thông báo" khi tick `chkNhanEmail`.

## 6. Cấu trúc project

```
Lab05_AppDangKyKhoaHoc/
├── Program.cs               // Điểm khởi chạy ứng dụng
├── Form1.cs                 // Xử lý logic và sự kiện
├── Form1.Designer.cs        // Thiết kế giao diện
├── Form1.resx
├── App.config
└── Lab05_AppDangKyKhoaHoc.csproj
```

## 7. Ảnh mẫu minh họa
<img width="328" height="323" alt="Screenshot 2026-09-29 230309" src="https://github.com/user-attachments/assets/5c6373b6-754f-42c7-b864-14716ec3524f" />
<img width="1156" height="772" alt="Screenshot 2026-09-29 230327" src="https://github.com/user-attachments/assets/c4024d39-014f-4064-9baf-71456bc809b1" />
<img width="1162" height="775" alt="Screenshot 2026-09-29 230349" src="https://github.com/user-attachments/assets/44eebd14-360e-4251-9796-9bbbf2be5a75" />
<img width="1165" height="773" alt="Screenshot 2026-09-29 230413" src="https://github.com/user-attachments/assets/98fe0afb-b0d7-40bb-a2c2-18e4063ef3a8" />
<img width="1156" height="770" alt="Screenshot 2026-09-29 230256" src="https://github.com/user-attachments/assets/416e625c-b690-4127-ab87-5b7a1bb53dbc" />
