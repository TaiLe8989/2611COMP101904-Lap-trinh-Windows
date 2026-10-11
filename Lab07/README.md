# BÁO CÁO BÀI LAB 07 – SDI, MDI VÀ ỨNG DỤNG NHIỀU FORM TRONG C# WINFORMS

**Môn học:** COMP1019 – Lập trình trên Windows
**Đề tài:** Xây dựng ứng dụng quản lý cửa hàng mini nhiều Form – **ShopManagerMini**
**Sinh viên:** _(điền họ và tên)_  **Lớp:** _(điền lớp)_
**Hình thức:** Cá nhân  **Công nghệ:** C# WinForms, .NET 8, dữ liệu lưu trong bộ nhớ (`BindingList<T>`)

---

## 1. Mục tiêu

- Tạo ứng dụng WinForms gồm nhiều Form.
- Tạo Form đăng nhập và truyền thông tin người dùng sang Form chính.
- Tạo Form chính dạng MDI Parent có `MenuStrip` và `StatusStrip`.
- Mở Form con MDI từ menu và không mở trùng Form cùng loại.
- Xây dựng màn hình quản lý dữ liệu có thêm, sửa, xóa, tìm kiếm trong bộ nhớ.

## 2. Danh sách Form

| Form | Vai trò | Nội dung chính |
|---|---|---|
| `FrmLogin` | Đăng nhập | Nhập username/password, kiểm tra tài khoản mẫu, trả `DialogResult.OK` khi thành công |
| `FrmMain` | Màn hình chính | MDI Parent, `MenuStrip`, `StatusStrip` (người dùng, vai trò, giờ hiện tại) |
| `FrmProduct` | Quản lý sản phẩm | Thêm, Sửa, Xóa, Làm mới, Tìm kiếm; hiển thị bằng `DataGridView` |
| `FrmCustomer` | Quản lý khách hàng | Thêm, Sửa, Xóa, Làm mới, Tìm kiếm; hiển thị bằng `DataGridView` |
| `FrmAbout` | Giới thiệu | Thông tin ứng dụng, tên sinh viên, lớp |

**Tài khoản đăng nhập mẫu**

| Username | Password | Họ tên | Vai trò |
|---|---|---|---|
| `admin` | `123` | Quản trị viên | Admin |
| `user` | `123` | Nhân viên bán hàng | User |

## 3. Cấu trúc project

```
ShopManagerMini
|-- Models
|   |-- User.cs         (Username, Password, FullName, Role)
|   |-- Product.cs      (Id, Name, Category, Price, Quantity)
|   |-- Customer.cs     (Id, FullName, Phone, CustomerType)
|
|-- Forms
|   |-- FrmLogin.cs
|   |-- FrmMain.cs
|   |-- FrmProduct.cs
|   |-- FrmCustomer.cs
|   |-- FrmAbout.cs
|
|-- Program.cs
|-- ShopManagerMini.csproj
```

## 4. Tóm tắt chức năng

### 4.1. Đăng nhập (`FrmLogin`)
- Bỏ trống username hoặc password → `MessageBox` cảnh báo.
- Sai tài khoản → `MessageBox` báo lỗi, xóa ô mật khẩu.
- Đúng → gán `CurrentUser`, `DialogResult = OK`, đóng form và mở `FrmMain`.
- Nút **Thoát** trả `DialogResult.Cancel`; phím Enter = Đăng nhập, Esc = Thoát.

### 4.2. Form chính MDI (`FrmMain`)
- `IsMdiContainer = true`.
- Menu **Hệ thống** (Đăng xuất, Thoát), **Danh mục** (Quản lý sản phẩm, Quản lý khách hàng), **Trợ giúp** (Giới thiệu).
- `StatusStrip` hiển thị tên người dùng, vai trò và đồng hồ cập nhật mỗi giây.
- Form con mở bên trong `FrmMain`; nếu Form cùng loại đang mở thì chỉ kích hoạt lại (hàm `OpenChild<T>`).
- Thoát chương trình và Đăng xuất đều hỏi xác nhận bằng `MessageBox`.

### 4.3. Quản lý sản phẩm (`FrmProduct`)
- Thông tin: mã sản phẩm, tên, loại (ComboBox), đơn giá, số lượng.
- Không cho thêm trùng mã; không cho đơn giá hoặc số lượng âm hoặc sai định dạng.
- Xóa phải hỏi xác nhận; chọn dòng trên lưới sẽ đổ dữ liệu lên các ô nhập.
- Tìm kiếm theo mã hoặc tên (không phân biệt hoa thường).

### 4.4. Quản lý khách hàng (`FrmCustomer`)
- Thông tin: mã khách hàng, họ tên, số điện thoại (9–11 chữ số), loại khách hàng.
- Có đủ thêm, sửa, xóa, làm mới, tìm kiếm (theo mã, tên hoặc số điện thoại).

## 5. Cách chạy

1. Cài **Visual Studio 2022** với workload *.NET desktop development* (hoặc .NET 8 SDK).
2. Mở file `ShopManagerMini.csproj`, nhấn **F5**.
3. Đăng nhập bằng `admin / 123` hoặc `user / 123`.

> Dữ liệu dùng chung được giữ trong `FrmMain`, nên đóng rồi mở lại Form con vẫn còn dữ liệu; thoát chương trình thì dữ liệu mất (chưa dùng cơ sở dữ liệu ở buổi này).

## 6. Đối chiếu tiêu chí chấm điểm

| Tiêu chí | Điểm | Thực hiện |
|---|---|---|
| Thiết kế đủ Form, giao diện rõ ràng, đặt tên control hợp lý | 2.0 | Đủ 5 Form; control đặt tên theo tiền tố `txt`, `btn`, `lbl`, `cbo`, `dgv`, `mnu` |
| Đăng nhập đúng, truyền user sang Form chính | 2.0 | Qua thuộc tính `CurrentUser` và constructor của `FrmMain` |
| `FrmMain` dạng MDI, mở Form con đúng, không mở trùng | 2.0 | `IsMdiContainer`, `MdiParent`, hàm `OpenChild<T>` |
| `FrmProduct` có CRUD và `DataGridView` | 2.0 | Đủ Thêm, Sửa, Xóa, Tìm kiếm, Làm mới |
| Validation, MessageBox, xác nhận xóa/thoát | 1.0 | Kiểm tra dữ liệu, xác nhận xóa, thoát, đăng xuất |
| Code rõ ràng, có class Model, có comment | 1.0 | 3 class Model, comment ở các đoạn chính |

## 7. Trả lời câu hỏi nộp kèm

**1. SDI và MDI khác nhau như thế nào?**
SDI (Single Document Interface) mỗi Form là một cửa sổ độc lập, không nằm trong cửa sổ nào khác. MDI (Multiple Document Interface) có một Form cha (`IsMdiContainer = true`) chứa nhiều Form con bên trong, dùng chung menu và thanh trạng thái của Form cha.

**2. Vì sao Form đăng nhập nên dùng `ShowDialog()`?**
`ShowDialog()` hiển thị form dạng modal: chương trình dừng lại chờ người dùng xử lý xong và nhận về `DialogResult`. Nhờ vậy chỉ mở Form chính khi kết quả là `OK`; nếu bấm Thoát hoặc đóng form thì chương trình kết thúc.

**3. Dữ liệu user được truyền từ `FrmLogin` sang `FrmMain` bằng cách nào?**
`FrmLogin` lưu người dùng hợp lệ vào thuộc tính `CurrentUser`. Sau khi `ShowDialog()` trả `OK`, `Program.cs` lấy `login.CurrentUser` và truyền vào constructor `new FrmMain(user)`. `FrmMain` dùng đối tượng này để hiển thị tên và vai trò lên `StatusStrip`.

**4. Nếu bấm menu sản phẩm nhiều lần, chương trình xử lý thế nào để không mở trùng?**
Hàm `OpenChild<T>` duyệt `MdiChildren`. Nếu đã có Form kiểu `T` thì chỉ khôi phục (nếu đang thu nhỏ) và gọi `Activate()`; nếu chưa có mới tạo Form mới, gán `MdiParent = this` rồi `Show()`.

**5. Khi nào nên dùng constructor, khi nào nên dùng property để truyền dữ liệu giữa các Form?**
Dùng **constructor** cho dữ liệu bắt buộc, cần có ngay khi Form được tạo (ví dụ `FrmMain(User)`, `FrmProduct(BindingList<Product>)`). Dùng **property** cho dữ liệu tùy chọn, hoặc cần lấy kết quả trả về sau khi Form xử lý xong (ví dụ `FrmLogin.CurrentUser`).
