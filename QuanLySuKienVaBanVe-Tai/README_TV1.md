# TV1 — Đăng nhập, phân quyền và MDI cho đề tài 20

Bộ mã tham khảo cho **C# WinForms / .NET Framework 4.7.2 / SQL Server**. Giao diện được viết trực tiếp bằng C# (không cần Designer và không dùng HTML/CSS), có nền chuyển sắc hồng, lưới nền, thẻ bo góc, đổi ngôn ngữ Việt/Anh, hover đổi màu và bounce khi nhấn nút.

> Đây là bộ mã để thử nghiệm và tích hợp, không phải toàn bộ dự án quản lý sự kiện. Các form của TV2/TV3 chưa có trong bộ này sẽ hiện màn hình placeholder; khi merge đúng form vào solution, menu MDI sẽ tự tìm một số tên form đã quy ước để mở.

## 1. File nào làm gì?

| File | Trách nhiệm |
|---|---|
| `DTO/TaiKhoanDTO.cs` | Đối tượng dữ liệu của bảng `TaiKhoan`: mã, tên đăng nhập, hash mật khẩu, vai trò, trạng thái. |
| `DAL/Database.cs` | Mở SQL connection và cung cấp các hàm truy vấn thường / transaction. Đây là **bản tham khảo** theo hợp đồng nhóm. |
| `DAL/TaiKhoanDAL.cs` | Chỉ đọc/ghi bảng `TaiKhoan` bằng SQL có tham số, tránh nối chuỗi SQL. |
| `BLL/TaiKhoanBLL.cs` | Kiểm tra đầu vào, kiểm chứng mật khẩu, khóa thử đăng nhập sai tạm thời, tạo tài khoản nhân viên và đổi mật khẩu. |
| `Common/PasswordHasher.cs` | Hash mật khẩu mới bằng PBKDF2 + salt. Tương thích dữ liệu demo cũ `123456` và nâng cấp sang hash sau đăng nhập đúng đầu tiên. |
| `Common/AppSession.cs` | Lưu tài khoản hiện tại trong bộ nhớ của tiến trình; checkbox chỉ ghi nhớ tên đăng nhập, không lưu mật khẩu. |
| `Common/UiLanguage.cs` | Trạng thái ngôn ngữ Việt/Anh và sự kiện thông báo form cập nhật chữ. |
| `Common/AuthUiControls.cs` | Nền gradient/lưới, panel bo góc, textbox có placeholder/ẩn-hiện mật khẩu, nút hover/bounce. |
| `GUI/Forms/FrmDangNhap.cs` | Giao diện + nhận thao tác; chỉ gọi BLL, không chạy SQL trực tiếp. |
| `GUI/Forms/FrmDangKy.cs` | Phần mở rộng theo ảnh mẫu; tài khoản tự đăng ký chỉ được cấp vai trò `NhanVien`. |
| `GUI/Forms/FrmDoiMatKhau.cs` | Đổi mật khẩu sau khi đăng nhập. Mục này được bổ sung theo nhận xét hồ sơ. |
| `GUI/Forms/FrmMain.cs` | MDI Parent, MenuStrip, ToolStrip, StatusStrip và lọc menu theo `VaiTro`. |
| `Program.cs` | Khởi chạy `FrmDangNhap`. |

## 2. Chạy thử bộ demo độc lập

### Điều kiện

- Windows 10/11.
- Visual Studio 2019/2022 với workload **.NET desktop development**.
- .NET Framework Developer Pack 4.7.2.
- SQL Server và SSMS.

### Bước chạy

1. Giải nén thư mục và mở `TV1AuthDemo.csproj` trong Visual Studio.
2. Mở SSMS, kết nối đúng SQL Server instance.
3. Chạy **script tạo database và bảng chung của nhóm trước**. Script `Scripts/02_TaiKhoanDemo.sql` trong gói này chỉ bổ sung 2 tài khoản nếu chúng chưa tồn tại; nó giả định bảng `dbo.TaiKhoan` đã có đủ 5 cột: `MaTK`, `TenDangNhap`, `MatKhau`, `VaiTro`, `TrangThai`.
4. Mở `App.config`, sửa `Data Source=.` thành instance của máy bạn. Nếu dùng SQL Express thường là `Data Source=.\SQLEXPRESS`.
5. Trong Visual Studio chọn **Rebuild Solution**, rồi F5.
6. Đăng nhập bằng `admin / 123456` hoặc `staff / 123456`, nếu script dữ liệu mẫu của nhóm đã tạo đúng các tài khoản này.

### Tài khoản mẫu và kết quả cần thấy

| Tên đăng nhập | Mật khẩu | Vai trò | Kết quả |
|---|---|---|---|
| `admin` | `123456` | `QuanLy` | Có menu Quản trị và Nghiệp vụ. |
| `staff` | `123456` | `NhanVien` | Chỉ có menu Nghiệp vụ và Hệ thống. |

Nếu `MatKhau` trong CSDL đã được hash bằng thuật toán khác, hàm xác thực cần được điều chỉnh cho đồng nhất với script seed. Bộ mã hiện hỗ trợ mật khẩu demo lưu dạng rõ và chuỗi định dạng `PBKDF2$...` do `PasswordHasher` tạo.

## 3. Tích hợp vào repository hiện tại của nhóm

**Không chép đè toàn bộ solution.** Thực hiện trên nhánh riêng.

**Cần thống nhất phân công trước khi merge:** `KeHoach_TienDo_Deadline.md` giao `FrmMain` cho TV1, nhưng README hiện tại trên GitHub ghi TV2 phụ trách MDI; README trên GitHub cũng dùng một số tên form/nhánh khác. Gói này đi theo file kế hoạch TV1 bạn cung cấp (`FrmDangNhap` + `FrmMain`). Hãy nhắn nhóm trưởng xác nhận đây là phân công chính thức trước khi push, tránh hai thành viên cùng sửa MDI.

```bash
git checkout main
git pull origin main
git checkout -b feature/auth-main
```

1. Trong Visual Studio, bấm phải project `QuanLySuKienVaBanVe` → **Add → Existing Item**; thêm các file `DTO`, `Common`, `DAL`, `BLL`, `GUI/Forms` từ gói vào đúng thư mục. Cách này giúp Visual Studio cập nhật `.csproj` cũ vốn liệt kê từng file một.
2. Nếu TV3 **đã** viết `DAL/Database.cs`, **không thêm thêm một `Database.cs` thứ hai**. So sánh chữ ký với hợp đồng trong `BaoCaoBanGiao_Nhom3.md`: `GetConnection`, `ExecuteQuery`, `ExecuteNonQuery`, `ExecuteScalar` và overload có `SqlConnection`/`SqlTransaction`. Giữ duy nhất bản chung đã được nhóm thống nhất.
3. Đảm bảo project tham chiếu `System.Configuration`, `System.Data`, `System.Drawing`, `System.Windows.Forms`.
4. Kiểm tra `App.config`. Repository hiện dùng connection string có tên `ConnStr`; `Database.GetConnection()` trong gói hỗ trợ cả `ConnStr` và `QuanLySuKienDB`.
5. `Program.cs` cần gọi `Application.Run(new FrmDangNhap());`. Nếu TV3/nhóm đã sửa `Program.cs`, hãy hòa trộn thay vì ghi đè mù quáng.
6. Build và kiểm tra 2 vai trò. Khi thành viên khác merge form, `FrmMain` thử tìm các tên form như `FrmQuanLySuKien` hoặc `FrmSuKien`; cần thống nhất đúng tên class thực tế.
7. Chụp ảnh giao diện, commit và mở Pull Request; đừng commit trực tiếp lên `main`.

**Lưu ý với file dự án WinForms cũ:** nếu bạn chỉ chép file bằng File Explorer mà không Add Existing Item, file mới có thể chưa được đưa vào `.csproj` và build báo thiếu namespace/class.

## 4. Luồng xử lý đăng nhập

```text
Người dùng nhập tên đăng nhập + mật khẩu
        ↓
FrmDangNhap kiểm tra rỗng và hiển thị lỗi cạnh ô nhập
        ↓
TaiKhoanBLL.DangNhap(...)
        ↓
TaiKhoanDAL.LayTheoTenDangNhap(...) → SQL Server
        ↓
BLL kiểm tra trạng thái tài khoản + verify mật khẩu
        ↓
Thành công → AppSession.SignIn(account)
        ↓
FrmMain mở dưới dạng MDI Parent, dựng menu theo VaiTro
        ↓
Đăng xuất → đóng FrmMain → trở lại FrmDangNhap
```

GUI không gọi DAL trực tiếp. BLL mới là nơi thực hiện quy tắc nghiệp vụ; DAL chỉ truy vấn CSDL. Các truy vấn sử dụng `SqlParameter`, tránh ghép chuỗi đầu vào vào SQL.

## 5. Thiết kế giao diện và giới hạn

- Đổi ngôn ngữ Việt/Anh trong menu góc phải; phần đăng nhập/đăng ký/đổi mật khẩu cập nhật nhãn.
- Hover đổi nền, nút chính có bounce ngắn khi nhấn, ô nhập đổi viền khi được focus.
- UI dùng bố cục WinForms nên không có CSS `:hover` hay HTML; hiệu ứng tương đương được xử lý bằng event C# và `Timer`.
- Ô đăng nhập dùng **tên đăng nhập**, không dùng email, vì bảng `TaiKhoan` theo tài liệu không có cột Email.
- Không thêm nút Google giả: đăng nhập Google đòi hỏi cấu hình OAuth riêng, không có trong hợp đồng đề tài.
- Đăng ký là **phần mở rộng**, không phải tiêu chí nghiệm thu ở mốc 20–22/10. Nếu nhóm không muốn cho người dùng tự tạo tài khoản, bỏ `FrmDangKy` và nút đăng ký; tài khoản vẫn lấy từ script mẫu. Khi bật đăng ký, vai trò được cố định là `NhanVien` để người lạ không tự tạo `QuanLy`.
- Khóa sau 5 lần nhập sai hiện giữ trong bộ nhớ ứng dụng; để khóa qua lần khởi động lại cần bổ sung cột thời gian/thử sai ở CSDL.

## 6. Checklist TV1 nên test ngay

- [ ] `admin/123456` đăng nhập được và nhìn thấy menu Quản trị.
- [ ] `staff/123456` đăng nhập được và không thấy menu Quản trị.
- [ ] Mật khẩu sai có thông báo, không vào được `FrmMain`.
- [ ] Nhập sai 5 lần thì khóa tạm trong 1 phút.
- [ ] Nút hiện/ẩn mật khẩu hoạt động.
- [ ] Đổi VN ↔ EN cập nhật nhãn form.
- [ ] Checkbox chỉ ghi nhớ username; đóng mở lại không điền sẵn mật khẩu.
- [ ] Đăng xuất trở lại form đăng nhập.
- [ ] Đổi mật khẩu yêu cầu mật khẩu cũ và kiểm tra xác nhận mật khẩu mới.
- [ ] Thử đăng nhập khi SQL Server tắt: thông báo dễ hiểu, ứng dụng không crash.
- [ ] Kiểm tra log `git status`, commit riêng cho tính năng đăng nhập.

## 7. Việc TV1 tiếp theo theo kế hoạch

Sau khi merge `feature/auth-main`, phần TV1 chưa kết thúc. Theo `KeHoach_TienDo_Deadline.md`, việc tiếp theo gồm:

1. **26–29/10:** `UcSoDoGhe` (UserControl sơ đồ ghế, chọn/bỏ chọn, màu theo trạng thái, event `OnSeatSelectionChanged`). Hãy cung cấp sớm một bản khung API cho TV3 để TV3 làm `FrmBanVe` song song.
2. **02–04/11:** `FrmInVe` (PrintDocument/Print Preview) và `FrmNhatKy`/`NhatKyDAL` (ghi nhận đăng nhập, bán vé, hủy vé). Nhận xét hồ sơ cũng nhắc cần bổ sung đổi mật khẩu và in danh sách khách tham dự; phần in danh sách đang gán TV3 trong file bàn giao nên cần nhóm thống nhất.
3. **08–09/11:** biên tập báo cáo đồ án, README, ảnh chụp giao diện.

Các mốc 20–22/10 trong tài liệu đã qua vào ngày 23/10/2026, vì vậy ưu tiên chạy được login, MDI và phân quyền trước; sau đó gửi PR để nhóm review.
