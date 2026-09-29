# BÁO CÁO BÀI LAB BUỔI 5 – WINDOWS FORMS CƠ BẢN

**Học phần:** COMP1019 – Lập trình trên Windows
**Giảng viên:** ThS. Lê Thanh Thoại – Khoa Công nghệ Thông tin, HCMUE
**Sinh viên thực hiện:** `<Họ và tên>` – MSSV: `<Mã số sinh viên>` – Lớp: `<Lớp>`
**Đề tài:** Ứng dụng đăng ký khóa học ngắn hạn (`Lab05_AppDangKyKhoaHoc`)

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

## 7. Hướng dẫn chạy

1. Clone repository về máy.
2. Mở file `Lab05_AppDangKyKhoaHoc.sln` bằng **Visual Studio 2022**.
3. Nhấn `Ctrl + Shift + B` để build, sau đó nhấn `F5` để chạy.

## 8. Kết quả chạy thử

> Chèn ảnh chụp màn hình vào thư mục `images/` rồi đổi đúng tên file bên dưới.

**Hình 1 – Giao diện khi mở chương trình**

![Giao diện chính](images/01-giao-dien.png)

**Hình 2 – Thay đổi khóa học / số tháng, tổng tiền tự cập nhật**

![Tính học phí](images/02-tinh-hoc-phi.png)

**Hình 3 – Báo lỗi khi để trống họ tên hoặc số điện thoại**

![Báo lỗi bỏ trống](images/03-bao-loi-bo-trong.png)

**Hình 4 – Báo lỗi số điện thoại không đúng định dạng**

![Báo lỗi số điện thoại](images/04-bao-loi-sdt.png)

**Hình 5 – Phiếu đăng ký khi nhập đầy đủ**

![Phiếu đăng ký](images/05-phieu-dang-ky.png)

**Hình 6 – Sau khi bấm Làm mới**

![Làm mới](images/06-lam-moi.png)

**Hình 7 – Hộp thoại xác nhận khi bấm Thoát**

![Xác nhận thoát](images/07-xac-nhan-thoat.png)

### Bảng kiểm thử

| STT | Tình huống | Kết quả mong đợi | Kết quả |
|---|---|---|---|
| 1 | Mở chương trình | Khóa đầu tiên, Online, 1 tháng, tổng tiền 800,000 VND | Đạt |
| 2 | Chọn Python, 3 tháng | Tổng tiền 1,950,000 VND | Đạt |
| 3 | Để trống họ tên, bấm Đăng ký | Báo "Vui lòng nhập họ tên!" | Đạt |
| 4 | Để trống số điện thoại, bấm Đăng ký | Báo "Vui lòng nhập số điện thoại!" | Đạt |
| 5 | Nhập số điện thoại 9 số | Báo "Số điện thoại không đúng định dạng, yêu cầu nhập lại" | Đạt |
| 6 | Gõ chữ vào ô số điện thoại | Ký tự chữ bị chặn | Đạt |
| 7 | Nhập đầy đủ hợp lệ, bấm Đăng ký | Hiện phiếu đăng ký đầy đủ thông tin | Đạt |
| 8 | Bấm Làm mới | Form trở về trạng thái ban đầu, con trỏ ở ô họ tên | Đạt |
| 9 | Bấm Thoát, chọn No / Yes | No: giữ Form; Yes: đóng Form | Đạt |

## 9. Kết luận

Bài lab đã hoàn thành các yêu cầu: thiết kế giao diện bằng các control cơ bản, đặt tên control theo quy ước, nạp dữ liệu khi Form Load, tính học phí tự động, kiểm tra dữ liệu khi đăng ký, và xử lý các nút Làm mới, Thoát. Qua bài lab, em nắm được cách làm việc với Form Designer và xử lý các sự kiện cơ bản trong Windows Forms.
