# NHẬN XÉT HỒ SƠ ĐỀ TÀI 20 – QUẢN LÝ SỰ KIỆN VÀ BÁN VÉ (NHÓM 3)

> Đối chiếu `KeHoach_TienDo_Deadline.md` và `BaoCaoBanGiao_Nhom3.md` với PDF đề tài của ThS. Lê Thanh Thoại (COMP1019).

---

## A. Thiếu sót so với đề bài (PDF)

| # | Yêu cầu trong PDF | Hiện trạng trong hồ sơ | Cần bổ sung |
|:-:|---|---|---|
| 1 | *"Transaction khi **giữ** và bán ghế"* | Chỉ có transaction khi bán. Trạng thái `DangGiu` có trong CSDL nhưng không có luồng nào dùng; màu "đang chọn" chỉ xử lý phía giao diện | Hàm giữ ghế có transaction + `UPDLOCK`, quy tắc nhả ghế khi hết hạn hoặc đóng form |
| 2 | *"Hủy hoặc **đổi** vé theo quy định"* | Chỉ mô tả luồng hủy. Trạng thái `DaDoi` có nhưng không có luồng, không có cột liên kết đơn cũ – đơn mới, không xử lý chênh lệch giá, không có test case | Luồng đổi vé trong 1 transaction, cột `MaDonGoc`, test case riêng |
| 3 | *"In danh sách khách tham dự"* | Báo cáo bàn giao đặt trong `FrmKhachHang`; file tiến độ không có ở đâu (không task, không DoD, không test). Danh sách phải lọc theo **suất diễn**, không phải toàn bộ khách hàng | Chức năng in danh sách theo suất (có Print Preview), đưa vào cả hai file |
| 4 | Phần mở rộng: vẽ vé GDI+, mã vé, thống kê lấp đầy **theo khu vực** | GDI+ ghi "nếu làm mở rộng"; chưa nói mã vé là QR/mã vạch hay chỉ chuỗi; thống kê đang theo sự kiện/suất | Chốt làm hay không; nếu làm thì ghi rõ thư viện QR/mã vạch và thống kê theo khu vực |
| 5 | Tài khoản: *đăng nhập, **đổi mật khẩu**, phân quyền* | Không có form đổi mật khẩu; mật khẩu chỉ ghi "nên hash" | `FrmDoiMatKhau`, chốt thuật toán hash ngay trong script SQL |
| 6 | Màn hình danh mục dùng `ErrorProvider`; lập phiếu có xem trước khi in; ưu tiên stored procedure | `ErrorProvider` không được nhắc; Print Preview chỉ là 1 dòng DoD; stored procedure ghi "nếu dùng" | Chốt dùng `ErrorProvider` cho mọi form CRUD; dùng stored procedure ít nhất cho bán vé và báo cáo |
| 7 | Tài liệu bàn giao có mô tả CSDL, giao diện, các trường hợp kiểm tra | CSDL chỉ có sơ đồ ASCII; bộ test case chưa có các trường hợp biên mà PDF mục 7 (bước 8) nêu | ERD chuẩn; test dữ liệu rỗng, trùng, giá trị biên, thao tác sai thứ tự, lỗi kết nối |

---

## B. Mâu thuẫn giữa hai file (cần thống nhất trước khi code)

| # | Nội dung | File tiến độ | File bàn giao |
|:-:|---|---|---|
| 1 | Script SQL | `database/create_database.sql`, `sample_data.sql` | `Scripts/01_TaoBang.sql`, `02_DuLieuMau.sql` |
| 2 | Tên nhánh Git | `feature/seat-matrix-uc`, `feature/seat-category-gen`, `feature/ticket-print-audit`... | `feature/seat-map`, `feature/zone-generator`, `feature/print-ticket-gdi`... |
| 3 | Event của `UcSoDoGhe` | `OnSelectedSeatsChanged` | `OnSeatSelectionChanged` |
| 4 | Lớp xử lý bán vé | `BanVeBLL.XuLyThanhToan()` | `DonVeBLL` (không có `BanVeBLL`) |
| 5 | Hàm sinh ghế | `KhuVucBLL.TaoKhuVucVaSinhGhe()` | `KhuVucBLL.ThemKhuVuc()` |
| 6 | Tên DTO | `GheDTO`, `KhachHangDTO`... | `Ghe`, `KhachHang`... (không hậu tố) |
| 7 | `FrmNhatKy` | Có, giao cho TV1 | Không có trong cây thư mục và bảng Form |
| 8 | Người review PR | Xoay vòng TV1 → TV2 → TV3 → TV1 | Ghi "chờ TV2 review" cho mọi PR |
| 9 | Bộ 6 test case | Phân quyền, bán vé, rollback, hủy vé, sinh ghế, báo cáo | Trùng ghế, hủy khi suất đã qua, mất kết nối, đăng nhập sai, sự kiện đã kết thúc, sinh ghế |
| 10 | Định dạng báo cáo | Word/PDF (có chỗ ghi PDF/LaTeX) | LaTeX trong `docs/report/` |
| 11 | Thư mục tài liệu | `docs/BaoCao_DoAn_Nhom3.pdf`, `docs/Slide_...pptx` | `docs/report/`, `docs/slides/` |

**Khuyến nghị:** gộp thành một nguồn duy nhất; bộ test case của file bàn giao bám đề bài hơn, nên hợp nhất cả hai bộ.

---

## C. Lỗi thiết kế CSDL và logic

1. **Chống bán trùng ghế chỉ dựa vào `UPDLOCK`.** Nên thêm ràng buộc ở CSDL: unique index có điều kiện trên `ChiTietVe(MaGhe)` với `TrangThai = 'HopLe'`. Lưu ý `UNIQUE (MaDon, MaGhe)` chỉ ngăn trùng trong cùng một đơn.
2. **Sinh tên hàng bằng `(char)('A' + h)`** sẽ sai khi số hàng lớn hơn 26. Cần `CHECK (SoHang <= 26)` hoặc cách đặt tên AA, AB...
3. **Code mẫu `KhuVucBLL.ThemKhuVuc` nuốt lỗi:** `catch { Rollback; return false; }` không báo nguyên nhân. Nên trả thông điệp lỗi hoặc ghi log.
4. **Sinh ghế bằng từng câu `INSERT` trong vòng lặp** (hàng trăm lần gọi). Nên gộp INSERT nhiều dòng hoặc dùng `SqlBulkCopy`.
5. **`KhuVuc` gắn với `SuatDien`:** mỗi suất phải tạo lại toàn bộ khu vực và ghế (dữ liệu lặp). Chấp nhận được cho đồ án nhưng phải nêu rõ lý do thiết kế trong báo cáo.
6. **Dư thừa tiền:** `DonVe.TongTien`, `ThanhToan.SoTien`, `ChiTietVe.GiaVe` cùng lưu tiền. Cần ghi quy tắc đối chiếu (test báo cáo đã có kiểm tra khớp).
7. **Chưa chốt quy tắc nghiệp vụ:** thời hạn hủy "trước X giờ" chưa có giá trị; trạng thái `SuKien`/`SuatDien` chưa nói do hệ thống tự cập nhật hay thủ công (test "sự kiện đã kết thúc thì từ chối bán" phụ thuộc điều này).
8. **Sơ đồ quan hệ ASCII** khó đọc và dễ sai; thay bằng ERD chuẩn.

---

## D. Kế hoạch và phân công

1. **Khối lượng lệch sang TV3.** TV3 vừa làm `Database.cs` (nền tảng cả nhóm cần từ ngày đầu), khách hàng, bán vé + transaction, quản lý đơn + hủy/đổi, vừa soạn slide, chủ trì kiểm thử và điều phối. Nên chuyển bớt (ví dụ `FrmQuanLyDonVe` hoặc in danh sách tham dự) sang TV2, người có tuần 3 nhẹ hơn.
2. **`FrmBanVe` (TV3) phụ thuộc `UcSoDoGhe` (TV1).** `UcSoDoGhe` xong 29/10 và chỉ merge 01/11, trong khi TV3 bắt đầu `FrmBanVe` từ 02/11. TV1 nên đẩy sớm một bản stub (đúng chữ ký hàm và event, dữ liệu giả) để TV3 làm song song.
3. **Kiểm thử chỉ có 1 ngày (07/11) sau code freeze 06/11.** Nếu lỗi transaction xuất hiện thì không còn thời gian sửa. Nên đổi thành "feature freeze 06/11, vẫn được sửa lỗi đến 09/11", và mô tả rõ cách giả lập "mất kết nối" ở test rollback.
