# BÁO CÁO LAB 06 – WINDOWS FORMS NÂNG CAO VÀ DATAGRIDVIEW

**2611COMP101904 - Lập trình Windows
-**MSSV:** 51.01.104.087
-**Họ và Tên:** Lê Minh Tài
-**Lớp:** 51.01.CNTT.C


---

## 1. Mục tiêu

- Thiết kế màn hình quản lý dữ liệu có bố cục rõ ràng.
- Hiển thị danh sách sản phẩm bằng `DataGridView`.
- Thực hiện thêm, sửa, xóa, tìm kiếm, làm mới dữ liệu trong bộ nhớ.
- Kiểm tra dữ liệu nhập bằng `ErrorProvider`.
- Dùng `MenuStrip`, `ToolStrip`, `StatusStrip`, `ContextMenuStrip` để thao tác nhanh.

## 2. Mô tả bài toán

Xây dựng ứng dụng Windows Forms **ProductManager** quản lý sản phẩm của một cửa hàng. Dữ liệu chỉ lưu trong bộ nhớ (`BindingList<Product>` + `BindingSource`), không kết nối SQL Server.

## 3. Cấu trúc project

| File | Nội dung |
|---|---|
| `Product.cs` | Class `Product` lưu dữ liệu sản phẩm (có thuộc tính tính toán `ThanhTien`) |
| `FrmProduct.cs` | Xử lý sự kiện và nghiệp vụ của form chính |
| `FrmProduct.Designer.cs` | Thiết kế giao diện |
| `Program.cs` | Điểm khởi chạy ứng dụng |

**Các trường dữ liệu:** `MaSP`, `TenSP`, `LoaiSP`, `DonGia`, `SoLuong`, `NgayNhap`, `ConKinhDoanh`, `DuongDanAnh`.

## 4. Giao diện (`FrmProduct` – "Quản lý sản phẩm")

| Thành phần | Mô tả |
|---|---|
| MenuStrip | File (Chọn ảnh, Thoát), Quản lý (Thêm, Sửa, Xóa, Tìm kiếm, Làm mới), Trợ giúp (Giới thiệu) |
| ToolStrip | Thêm, Sửa, Xóa, Làm mới, Tìm kiếm |
| GroupBox "Thông tin sản phẩm" | `txtMaSP`, `txtTenSP`, `cboLoaiSP`, `numDonGia`, `numSoLuong`, `dtpNgayNhap`, `chkConKinhDoanh`, các nút Thêm/Sửa/Xóa/Làm mới |
| PictureBox + nút Chọn ảnh | `picSanPham`, `btnChonAnh` (dùng `OpenFileDialog`) – hiển thị ảnh minh họa của sản phẩm |
| Ô tìm kiếm | `txtTimKiem`, `btnTimKiem` – tìm theo mã hoặc tên |
| DataGridView | `dgvProducts` – `ReadOnly`, `FullRowSelect`, `MultiSelect = false` |
| ContextMenuStrip | Chuột phải trên lưới: Sửa, Xóa, Xem chi tiết |
| StatusStrip | `lblStatus` – trạng thái thao tác và số lượng sản phẩm |

## 5. Chức năng đã thực hiện

1. **Load form:** nạp danh sách loại sản phẩm (Điện tử, Văn phòng, Gia dụng, Thời trang), tạo 4 sản phẩm mẫu và hiển thị lên lưới.
2. **Thêm:** kiểm tra dữ liệu và mã không trùng, thêm vào danh sách, cập nhật lưới.
3. **Sửa:** chọn một dòng, dữ liệu đưa lên form, chỉnh sửa rồi cập nhật lại.
4. **Xóa:** chọn một dòng, hiện `MessageBox` xác nhận trước khi xóa.
5. **Tìm kiếm:** theo mã hoặc tên, không phân biệt hoa/thường.
6. **Làm mới:** xóa nội dung nhập, bỏ lỗi `ErrorProvider`, hiển thị lại toàn bộ dữ liệu.
7. **Chọn ảnh:** chọn file jpg/png/bmp bằng `OpenFileDialog`, hiển thị trong `PictureBox`, lưu đường dẫn vào `DuongDanAnh`; chọn lại dòng thì ảnh hiện lại.
8. **StatusStrip:** sau mỗi thao tác cập nhật trạng thái và số lượng sản phẩm.

**Thao tác nhanh:** Ctrl+N (Thêm), Ctrl+E (Sửa), Ctrl+D (Xóa), Ctrl+F (Tìm kiếm), F5 (Làm mới), Enter trong ô tìm kiếm, di chuyển dòng bằng phím mũi tên, chuột phải mở menu ngữ cảnh.

## 6. Quy tắc kiểm tra dữ liệu (Validation)

| Trường | Quy định | Thông báo lỗi |
|---|---|---|
| MaSP | Không rỗng, không trùng | "Mã sản phẩm không được rỗng" / "Mã sản phẩm đã tồn tại" |
| TenSP | Không rỗng | "Tên sản phẩm không được rỗng" |
| DonGia | Lớn hơn 0 | "Đơn giá phải lớn hơn 0" |
| SoLuong | Lớn hơn hoặc bằng 0 | "Số lượng phải lớn hơn hoặc bằng 0" |
| NgayNhap | Không lớn hơn ngày hiện tại | "Ngày nhập không được lớn hơn ngày hiện tại" |

Lỗi hiển thị bằng `ErrorProvider` tại control sai, kèm một `MessageBox` tổng hợp. Lỗi tự biến mất khi người dùng sửa lại ô nhập.

## 7. Kỹ thuật áp dụng

- Tách hàm rõ ràng: `ValidateInput`, `ClearInput`, `LoadProductToForm`, `GetSelectedProduct`, `UpdateStatus`, `ApplyFilter`, `ShowAll`, `ShowImage`.
- `ToolStrip`, menu và `ContextMenuStrip` dùng lại logic của các nút chính qua `PerformClick`, tránh viết trùng code.
- Chưa chọn dòng mà nhấn Sửa/Xóa: chỉ hiện thông báo, không gây lỗi.
- Ảnh được đọc qua `FileStream` nên không khóa file trên đĩa.
- Tìm kiếm không làm hỏng dữ liệu gốc: Sửa/Xóa vẫn đúng khi đang lọc.

## 8. Kiểm thử

| Mã | Thao tác | Kết quả mong đợi |
|---|---|---|
| TC01 | Để trống mã SP, nhấn Thêm | Báo lỗi tại `txtMaSP` |
| TC02 | Nhập mã đã tồn tại, nhấn Thêm | Báo lỗi mã trùng |
| TC03 | Đơn giá bằng 0 | Báo lỗi đơn giá |
| TC04 | Chọn ngày nhập lớn hơn hôm nay | Báo lỗi ngày nhập |
| TC05 | Chọn một dòng, nhấn Xóa | Hiện hộp thoại xác nhận |
| TC06 | Tìm theo một phần tên sản phẩm | Lưới chỉ hiện các dòng phù hợp |
| TC07 | Nhấn Sửa/Xóa khi chưa chọn dòng | Chỉ hiện thông báo, không lỗi |
| TC08 | Chọn ảnh rồi chọn dòng khác và quay lại | Ảnh hiển thị đúng theo từng sản phẩm |

## 9. Hình ảnh minh họa
<img width="1161" height="897" alt="Screenshot 2026-10-07 233403" src="https://github.com/user-attachments/assets/49c14cdd-7226-404c-afdb-516d557264ba" />
<img width="1167" height="912" alt="Screenshot 2026-10-07 233524" src="https://github.com/user-attachments/assets/c6ae68ef-6a0c-41c1-8197-55f42c588169" />
<img width="1163" height="907" alt="Screenshot 2026-10-07 233547" src="https://github.com/user-attachments/assets/c05579d2-7f19-4458-acd1-b55f68a261ca" />
<img width="1160" height="902" alt="Screenshot 2026-10-07 233608" src="https://github.com/user-attachments/assets/0ab565b7-08ce-402a-9c98-0925d0cafc55" />
<img width="1166" height="905" alt="Screenshot 2026-10-07 233627" src="https://github.com/user-attachments/assets/085f5f23-0d90-471e-bb0d-0783da76752d" />
<img width="1172" height="898" alt="Screenshot 2026-10-07 233713" src="https://github.com/user-attachments/assets/9e80f2d5-4b47-4e45-a0d2-0233a0ebb639" />

