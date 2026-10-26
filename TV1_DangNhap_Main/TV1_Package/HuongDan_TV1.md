# HƯỚNG DẪN TV1 — MỐC 19/10 VÀ 20/10 → 22/10

> Đề tài 20 · Quản lý sự kiện và bán vé · Nhóm 3 · COMP1019

---

## 1. Nhiệm vụ của bạn thực chất là gì?

Trong kế hoạch, mốc **20/10 – 22/10** của TV1 gồm 3 việc. Cả 3 đều là phần **đăng nhập và khung chương trình** mà cả nhóm phải dùng.

| Việc trong kế hoạch | Ý nghĩa | File đã làm |
|---|---|---|
| Viết `TaiKhoanDTO`, `TaiKhoanDAL`, `TaiKhoanBLL` | Tầng dữ liệu của tài khoản theo kiến trúc 3 lớp: DTO chứa dữ liệu, DAL chạy SQL, BLL chứa quy tắc | `DTO/TaiKhoanDTO.cs`, `DAL/TaiKhoanDAL.cs`, `BLL/TaiKhoanBLL.cs` |
| `FrmDangNhap`: kiểm tra thông tin, báo lỗi sai pass, **lưu thông tin phiên đăng nhập** | Màn hình đăng nhập. "Phiên" là biến toàn cục cho biết ai đang đăng nhập (TV3 cần `MaTK` này để ghi `DonVe.MaTK`) | `GUI/Forms/FrmDangNhap.cs`, `BLL/PhienDangNhap.cs` |
| `FrmMain` dạng MDI: MenuStrip, ToolStrip, StatusStrip, **ẩn/hiện menu theo vai trò** | Cửa sổ cha chứa mọi form con. Nhân viên chỉ thấy bán vé và tra cứu | `GUI/Forms/FrmMain.cs`, `Program.cs` |

Phần thêm theo yêu cầu của bạn: form đăng nhập/đăng ký giao diện web, đổi ngôn ngữ VN/EN, hiệu ứng hover và bounce.

**Mốc 19/10 (cả nhóm):** TV3 đẩy khung dự án lên `main`. Bạn clone về, chạy 2 script SQL trong SSMS, mở `.sln`, Rebuild. Kết quả mong đợi: 0 Error, 0 Warning, kết nối được CSDL.

---

## 2. Làm theo thứ tự này

1. **19/10:** `git pull`. Chạy script tạo bảng và dữ liệu mẫu trong SSMS. Sửa `App.config` cho đúng tên SQL Server của máy bạn (không commit file này).
2. Tạo nhánh: `git checkout -b feature/auth-main`.
3. Copy các thư mục trong gói này vào project (cùng cấp với `BLL`, `DAL`, `DTO`, `GUI`):

   ```
   DTO/TaiKhoanDTO.cs
   DAL/TaiKhoanDAL.cs
   BLL/TaiKhoanBLL.cs
   BLL/PhienDangNhap.cs
   GUI/Helpers/Fx.cs
   GUI/Helpers/Lang.cs
   GUI/Controls/FxControls.cs
   GUI/Forms/FrmDangNhap.cs
   GUI/Forms/FrmMain.cs
   Program.cs                  <- ghi đè file cũ (chỉ TV1 được sửa file này)
   ```
4. Trong Visual Studio, bấm **Show All Files**, chuột phải từng file mới → **Include In Project**. Nếu project dùng kiểu SDK mới thì file tự được nhận.
5. **Xóa `Form1.cs` và `Form1.Designer.cs`** (form mẫu, không dùng nữa).
6. Kiểm tra `namespace`: các file dùng `QuanLySuKienVaBanVe`. Nếu namespace gốc của project khác, dùng Find and Replace.
7. **Rebuild Solution** rồi chạy thử bằng `admin / 123456` và `staff / 123456`.
8. `git add .`, bỏ `App.config` ra khỏi staging (`git reset HEAD App.config`), commit rõ nghĩa (ví dụ `Add login form and MDI main with role-based menu`), push và mở Pull Request cho TV2 review.

> **Cần có `Database.GetConnection()` của TV3.** Code của bạn chỉ gọi hàm này (trả về `SqlConnection` chưa mở, đúng hợp đồng mục 7.1). Nếu TV3 chưa push, bạn chưa build được. Hãy nhắn TV3 push `Database.cs` trước.

---

## 3. Mỗi file làm gì

### Tầng dữ liệu
- **`TaiKhoanDTO`**: lớp chỉ có thuộc tính (`MaTK`, `TenDangNhap`, `MatKhau`, `VaiTro`, `TrangThai`). Kèm lớp `VaiTro` chứa hai hằng `"QuanLy"` và `"NhanVien"` để khỏi gõ chuỗi tay.
- **`TaiKhoanDAL`**: ba hàm SQL: `LayTheoTenDangNhap`, `TonTai`, `Them`. Dùng `SqlParameter` cho mọi giá trị, nên người dùng gõ `' OR 1=1 --` cũng không phá được.
- **`TaiKhoanBLL`**: chứa quy tắc nghiệp vụ.
  - `DangNhap`: tìm tài khoản, so mật khẩu băm SHA-256, kiểm tra tài khoản bị khóa.
  - **Chống dò mật khẩu:** sai 5 lần thì khóa tạm 60 giây. Đây là test case số 4 của bộ test ("đăng nhập sai nhiều lần").
  - Sai tên hay sai mật khẩu đều báo cùng một câu, để người ngoài không biết tên nào tồn tại.
  - `DangKy`: kiểm tra định dạng, độ dài, mật khẩu khớp, trùng tên. Tài khoản tự đăng ký luôn là `NhanVien`.
  - BLL chỉ trả **mã kết quả** (enum), không chứa chữ tiếng Việt, nên GUI tự dịch sang VN/EN.
- **`PhienDangNhap`**: nơi lưu người đang đăng nhập. Form nào cũng gọi được:
  ```csharp
  int maTK = PhienDangNhap.TaiKhoan.MaTK;
  bool laQuanLy = PhienDangNhap.LaQuanLy;
  ```

### Giao diện
- **`Fx.cs`**: bảng màu, hàm vẽ hình bo góc, hàm easing (`EaseOutBounce`, `Spring`) và các icon vẽ bằng GDI+ (user, khóa, mắt, cờ...), nên không cần file ảnh.
- **`Lang.cs`**: từ điển 2 ngôn ngữ. Gọi `Lang.T("khoa")` để lấy chữ, `Lang.Set(Ngon.EN)` để đổi. Mọi form đang mở tự cập nhật nhờ sự kiện `Lang.Changed`. Muốn thêm chữ mới, thêm một dòng vào từ điển.
- **`FxControls.cs`**: bộ control tự vẽ: `FxButton`, `RoundedTextBox`, `FxLink`, `FxCheck`, `LangChip`, `CardPanel`.
- **`FrmDangNhap.cs`**: một form chứa cả đăng nhập và đăng ký (chuyển qua lại bằng link).
- **`FrmMain.cs`**: form MDI. `MoFormCon<T>()` đúng theo hợp đồng kỹ thuật mục 7.5.
- **`Program.cs`**: vòng lặp Đăng nhập → Form chính → Đăng xuất → quay lại Đăng nhập.

### Các hiệu ứng làm bằng cách nào
| Yêu cầu | Cách làm |
|---|---|
| **Hover effect** | Mỗi control có biến `Hover` chạy mượt từ 0 đến 1 khi rê chuột vào (timer 15ms, xong tự dừng). Nút nổi lên 2px và có quầng sáng. Link đổi màu và gạch chân. Ô nhập đậm nền lên. |
| **Background-color hover** | `Fx.Lerp(màuThường, màuHover, Hover)` pha màu theo `Hover`, nên màu đổi từ từ chứ không giật. |
| **Bounce animation** | (1) Thẻ đăng nhập rơi xuống và nảy khi mở form hoặc chuyển Đăng nhập ↔ Đăng ký (`EaseOutBounce`). (2) Nút nảy kiểu lò xo khi thả chuột (`Spring`). (3) Dấu tick nảy khi tick vào ô. (4) Thẻ lắc khi nhập sai. |
| **Đổi ngôn ngữ** | Nút VN/EN góc phải mở menu chọn, gọi `Lang.Set(...)`. Form chính cũng đổi theo (menu Hệ thống → Ngôn ngữ). |

---

## 4. Những chỗ mình làm KHÁC ảnh mẫu (và lý do)

| Ảnh mẫu | Bản của bạn | Lý do |
|---|---|---|
| Ô **email** | Ô **tên đăng nhập** | Bảng `TaiKhoan` dùng `TenDangNhap` (`admin`, `staff`), không có cột email. |
| Nút **Tiếp tục với Google** | **Không có** | Đăng nhập Google cần OAuth + tài khoản Google Cloud, ngoài phạm vi đồ án WinForms. Làm nút giả sẽ bị thầy hỏi. |
| **Tự động đăng nhập** | **Ghi nhớ tên đăng nhập** | Tự đăng nhập phải lưu mật khẩu vào máy, là lỗi bảo mật. Bản này chỉ nhớ tên (file `%AppData%\QuanLySuKienVaBanVe\remember.txt`), mật khẩu vẫn phải gõ. |
| Quên mật khẩu → gửi email | Hiện thông báo "liên hệ Quản lý" | Chưa có hệ thống gửi email. |

---

## 5. Kiểm tra nghiệm thu (DoD của kế hoạch)

| # | Cách thử | Kết quả mong đợi |
|---|---|---|
| 1 | Đăng nhập `admin / 123456` | Mở form chính, thấy đủ menu: Hệ thống, Danh mục, Bán vé, Báo cáo, Trợ giúp |
| 2 | Đăng nhập `staff / 123456` | Chỉ thấy Hệ thống, Danh mục (chỉ **Khách hàng**), Bán vé, Trợ giúp. Không thấy Báo cáo |
| 3 | Menu Hệ thống → Đăng xuất → Yes | Quay lại màn hình đăng nhập |
| 4 | Gõ sai mật khẩu | Thẻ lắc, ô mật khẩu viền đỏ, báo "Sai tên đăng nhập hoặc mật khẩu" |
| 5 | Sai liên tiếp 5 lần | Báo "Sai quá nhiều lần. Thử lại sau 60 giây" |
| 6 | Đăng ký tên mới, mật khẩu ≥ 6 ký tự | Quay về đăng nhập, tên đã điền sẵn, có dòng xanh "Tạo tài khoản thành công" |
| 7 | Đăng ký tên đã có (VD `admin`) | Báo "Tên đăng nhập đã tồn tại" |
| 8 | Bấm VN → English | Toàn bộ chữ đổi sang tiếng Anh ngay, không cần mở lại form |
| 9 | Tắt SQL Server rồi đăng nhập | Báo "Không kết nối được CSDL". Rê chuột vào dòng lỗi để xem lỗi gốc |

---

## 6. Việc phải phối hợp với người khác

**Với TV2 (script SQL), quan trọng:**
Mật khẩu trong CSDL nên là chuỗi băm SHA-256. Code vẫn chấp nhận mật khẩu dạng chữ thường (`123456`) để demo chạy ngay, nhưng báo cáo nên dùng bản băm. Nhờ TV2 chạy:

```sql
UPDATE TaiKhoan
SET MatKhau = '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92'  -- SHA-256 của 123456
WHERE TenDangNhap IN ('admin', 'staff');
```
Sau đó trong `TaiKhoanBLL.KiemTraMatKhau`, xóa dòng `return luu == nhap;` để chỉ còn chấp nhận mật khẩu đã băm. Người ta sẽ không đăng nhập được bằng chuỗi băm đó, vì hệ thống băm lại cái họ gõ rồi mới so.

**Với TV3:**
- Cần `Database.GetConnection()` (xem mục 2).
- Bán vé sẽ lấy `PhienDangNhap.TaiKhoan.MaTK` để ghi vào `DonVe.MaTK`.

**Khi TV2/TV3 merge form xong:** mở `FrmMain.cs`, tìm các hàm `MoSuKien()`, `MoBanVe()`... Mỗi hàm có sẵn comment `TODO`. Thay đúng 1 dòng, ví dụ:
```csharp
private void MoSuKien() { MoFormCon<FrmQuanLySuKien>(); }
```

---

## 7. Lưu ý quan trọng

1. **Tự đăng ký ai cũng được làm Nhân viên bán vé.** Với đồ án thì ổn và tiện demo. Trong thực tế phải chờ Quản lý duyệt. Nếu thầy hỏi, bạn trả lời được và có thể đề xuất cải tiến: tạo tài khoản với `TrangThai = 0` và để Quản lý kích hoạt.
2. **Nếu build báo warning về nullable** (project .NET 6 trở lên), mở file `.csproj` và đặt `<Nullable>disable</Nullable>`.
3. **Chữ gợi ý mờ trong ô nhập** (như "Nhập tên đăng nhập") dùng chức năng có sẵn của Windows nên chỉ hiện trên Windows. Đây không phải lỗi.
4. Giao diện dựng hoàn toàn bằng code, **không có file Designer**, nên bạn sẽ không kéo thả được trong VS. Đổi màu, vị trí thì sửa trong code (các hằng ở đầu `FrmDangNhap.cs` và bảng màu trong `Fx.cs`).

## 8. Những việc TV1 còn lại sau mốc này (theo kế hoạch)

- `UcSoDoGhe` (26/10 – 29/10): việc khó nhất của bạn. Nên đẩy sớm một bản **stub** (đúng tên hàm và event theo hợp đồng, dữ liệu giả) để TV3 làm `FrmBanVe` song song.
- `FrmInVe`, `FrmNhatKy`, `NhatKyDAL` (tuần 3).
- `FrmDoiMatKhau`: đề bài (PDF) có yêu cầu đổi mật khẩu nhưng hai file kế hoạch chưa có. Menu "Đổi mật khẩu" đã có sẵn chỗ chờ.
- README, ảnh chụp giao diện, báo cáo (08/11 – 09/11).
