# KẾ HOẠCH TIẾN ĐỘ & PHÂN CÔNG DEADLINE CHI TIẾT

## ĐỀ TÀI 20: QUẢN LÝ SỰ KIỆN VÀ BÁN VÉ — NHÓM 3

> **Học phần:** COMP1019 — Lập trình Windows  
> **Giảng viên hướng dẫn:** ThS. Lê Thanh Thoại — Khoa CNTT, Trường ĐH Sư phạm TP.HCM (HCMUE)  
> **Thời gian thực hiện:** 19/10/2026 – 11/11/2026 (Tổng cộng: 24 ngày)  
> **Ngày báo cáo chính thức:** 11/11/2026  
> **Nguyên tắc tiến độ:** **Đóng băng mã nguồn (Code Freeze) vào 23:59 ngày 06/11/2026**. Dành trọn 5 ngày cuối (07/11 – 11/11) cho kiểm thử, viết báo cáo, làm slide và diễn tập thuyết trình.

---

## 1. BẢNG PHÂN VAI & MA TRẬN TRÁCH NHIỆM

| Thành viên | Vai trò | Phân hệ phụ trách chính | Nhánh Git làm việc | Nhiệm vụ tài liệu bàn giao |
|:---:|---|---|---|---|
| **TV1** | Thành viên kỹ thuật | • Phân quyền & Đăng nhập (`FrmDangNhap`)<br>• Giao diện chính MDI (`FrmMain`)<br>• **User Control Sơ đồ ghế ma trận (`UcSoDoGhe`)**<br>• In vé cho khách (`FrmInVe`)<br>• Nhật ký thao tác (`FrmNhatKy`) | `feature/auth-main`<br>`feature/seat-matrix-uc`<br>`feature/ticket-print-audit` | • Biên tập chính **Báo cáo đồ án** (bản Word/PDF)<br>• Cập nhật hoàn thiện `README.md`<br>• Chụp ảnh giao diện vào `docs/screenshots/` |
| **TV2** | Thành viên kỹ thuật | • Quản lý Sự kiện (`FrmQuanLySuKien`)<br>• Quản lý Suất diễn (`FrmQuanLySuatDien`)<br>• Quản lý Khu vực & Giá vé (`FrmQuanLyKhuVuc`)<br>• **Thuật toán tự sinh ma trận ghế (`KhuVucBLL`)**<br>• Báo cáo thống kê & Biểu đồ doanh thu (`FrmBaoCao`) | `feature/event-showtime`<br>`feature/seat-category-gen`<br>`feature/analytics-reporting` | • Quản lý cấu trúc CSDL SQL Server (`database/`)<br>• Đóng gói source code dự phòng `.zip`<br>• Phối hợp Review & Merge PR |
| **TV3** | Nhóm trưởng | • Hạ tầng dữ liệu chung (`Database.cs`)<br>• Quản lý Khách hàng (`FrmKhachHang`)<br>• **Nghiệp vụ Bán vé tại quầy (`FrmBanVe`)**<br>• **Bảo toàn giao dịch an toàn (`SqlTransaction`)**<br>• Quản lý đơn vé & Hủy/Đổi vé (`FrmQuanLyDonVe`) | `feature/customer-management`<br>`feature/order-management`<br>`feature/ticket-booking-core` | • **Soạn Slide thuyết trình PowerPoint**<br>• Chủ trì kiểm thử (Test execution report)<br>• Điều phối phân chia công việc & Review PR |

---

## 2. TỔNG QUAN LỘ TRÌNH 4 GIAI ĐOẠN

| Giai đoạn | Mốc thời gian | Trọng tâm công việc | Kết quả nghiệm thu (Milestone) |
|:---|:---:|:---|:---|
| **Tuần 1** | **19/10 – 25/10** | Dựng DB, Đăng nhập (`FrmDangNhap`), Form MDI (`FrmMain`), Quản lý Sự kiện & Khách hàng | 🏁 **Milestone 1:** App đăng nhập được, mở đủ Menu, quản lý danh mục mượt mà |
| **Tuần 2** | **26/10 – 01/11** | UserControl Sơ đồ ghế (`UcSoDoGhe`), Thuật toán tự sinh ghế (`KhuVucBLL`), Quản lý Đơn vé | 🏁 **Milestone 2:** Sơ đồ ghế đổi màu động, sinh đủ ghế theo hàng x cột |
| **Tuần 3** | **02/11 – 06/11** | Màn hình Bán vé (`FrmBanVe`), Giao dịch an toàn (`SqlTransaction`), In vé & Báo cáo doanh thu | 🔒 **23:59 ngày 06/11: CODE FREEZE (Đóng băng code, không sửa tính năng)** |
| **Tuần 4** | **07/11 – 10/11** | Chạy 6 Test case kiểm thử, Soạn Báo cáo Word/PDF, Thiết kế Slide PowerPoint, Diễn tập demo | 🎯 **10/11: Tổng duyệt thuyết trình thử 15 phút (Rehearsal)** |
| 🎓 **BÁO CÁO** | **11/11/2026** | **Báo cáo và vấn đáp bảo vệ đồ án chính thức trước ThS. Lê Thanh Thoại (HCMUE)** | 🏆 **Mục tiêu: Đạt điểm tối đa (A+)** |

### 🧭 Sơ đồ dòng chảy tiến độ (Timeline Flow)

```text
┌────────────────────────────────────────────────────────┐
│ 📅 TUẦN 1 (19/10 – 25/10): KHỞI TẠO & DANH MỤC         │
├────────────────────────────────────────────────────────┤
│  • Khởi tạo Solution 3 lớp & cấu hình Git              │
│  • Tạo CSDL 10 bảng & nạp dữ liệu mẫu                  │
│  • Xây dựng FrmDangNhap & FrmMain (MDI)                │
│  • Xây dựng FrmSuKien & FrmKhachHang                   │
└───────────────────────────┬────────────────────────────┘
                            │
                            ▼
┌────────────────────────────────────────────────────────┐
│ 📅 TUẦN 2 (26/10 – 01/11): XÂY DỰNG LÕI SƠ ĐỒ GHẾ      │
├────────────────────────────────────────────────────────┤
│  • Viết UcSoDoGhe sinh ghế động theo trạng thái        │
│  • Thuật toán sinh ma trận ghế tự động                 │
│  • Xây dựng FrmQuanLyDonVe (Nghiệp vụ hủy/đổi vé)      │
│  • Review & Merge Pull Request Tuần 2                  │
└───────────────────────────┬────────────────────────────┘
                            │
                            ▼
┌────────────────────────────────────────────────────────┐
│ 📅 TUẦN 3 (02/11 – 06/11): TÍCH HỢP & TRANSACTION      │
├────────────────────────────────────────────────────────┤
│  • Ráp FrmBanVe trung tâm (chọn ghế & tính tiền)       │
│  • Xử lý đồng thời bằng SqlTransaction                 │
│  • FrmInVe (vẽ vé GDI+) & FrmBaoCao (biểu đồ)          │
│  • 🔒 23:59 ngày 06/11: CODE FREEZE (Chốt code)        │
└───────────────────────────┬────────────────────────────┘
                            │
                            ▼
┌────────────────────────────────────────────────────────┐
│ 📅 TUẦN 4 (07/11 – 10/11): KIỂM THỬ & HOÀN THIỆN       │
├────────────────────────────────────────────────────────┤
│  • Chạy nghiệm thu 6 kịch bản Test Cases               │
│  • Hoàn thiện Báo cáo tổng kết (PDF/LaTeX)             │
│  • Thiết kế Slide thuyết trình đồ án                   │
│  • Ngày 10/11: Diễn tập thuyết trình toàn nhóm         │
└───────────────────────────┬────────────────────────────┘
                            │
                            ▼
╔════════════════════════════════════════════════════════╗
║   🎓 11/11/2026: BÁO CÁO VỚI GIẢNG VIÊN (HCMUE)        ║
╚════════════════════════════════════════════════════════╝
```

---

## 3. KẾ HOẠCH CHI TIẾT TỪNG NGÀY & TỪNG TUẦN

### 🔹 TUẦN 1: NỀN TẢNG, ĐĂNG NHẬP & DANH MỤC CƠ SỞ (19/10 – 25/10/2026)
> **Mục tiêu:** Cả 3 thành viên kéo code nền tảng về máy, kết nối CSDL thành công. Ứng dụng chạy được màn hình Đăng nhập, Giao diện chính MDI và 2 danh mục nền tảng.

| Mốc thời gian | Thành viên | Công việc chi tiết | Nhánh Git | Tiêu chí nghiệm thu (DoD) |
|---|:---:|---|---|---|
| **19/10 (Thứ 2)** | **Cả nhóm** | • TV3 push toàn bộ khung dự án (Solution `.sln`, `.gitignore`, `App.config`, scripts DB) lên nhánh `main`.<br>• TV1 và TV2 clone/pull về máy, thực thi script `create_database.sql` và `sample_data.sql` trên SSMS.<br>• Mở Solution bằng Visual Studio, nhấn Rebuild Solution. | `main` | Cả 3 máy đều build thành công **(0 Warning, 0 Error)**, kết nối thông suốt CSDL. |
| **20/10 – 22/10** | **TV1** | • Viết `TaiKhoanDTO`, `TaiKhoanDAL`, `TaiKhoanBLL`.<br>• Thiết kế form `FrmDangNhap`: Kiểm tra thông tin, hiển thị lỗi nếu sai pass, lưu thông tin phiên đăng nhập.<br>• Thiết kế `FrmMain` dạng MDI Container: Menu Strip, ToolStrip, StatusStrip, phân quyền ẩn/hiện menu theo vai trò (`QuanLy` / `NhanVien`). | `feature/auth-main` | • Đăng nhập với `admin/123456` mở đủ menu Admin.<br>• Đăng nhập với `staff/123456` chỉ mở menu bán vé/tra cứu.<br>• Đăng xuất quay lại màn hình đăng nhập. |
| **20/10 – 23/10** | **TV2** | • Viết DTO/DAL/BLL cho `SuKien` và `SuatDien`.<br>• Thiết kế `FrmQuanLySuKien`: Danh sách sự kiện, Thêm, Sửa, Xóa (ràng buộc không xóa sự kiện đã có suất diễn), Tìm kiếm.<br>• Thiết kế `FrmQuanLySuatDien`: Chọn sự kiện từ ComboBox, quản lý các suất diễn theo ngày giờ và địa điểm. | `feature/event-showtime` | Thao tác Thêm/Sửa/Xóa lưu chính xác vào bảng `SuKien` và `SuatDien`. Dữ liệu hiển thị chuẩn trên DataGridView. |
| **20/10 – 23/10** | **TV3** | • Hoàn thiện lớp trợ giúp dữ liệu `Database.cs` (phương thức `ExecuteQuery`, `ExecuteNonQuery`, `ExecuteScalar` và hỗ trợ overload nhận `SqlTransaction`).<br>• Viết `KhachHangDTO`, `KhachHangDAL`, `KhachHangBLL`.<br>• Thiết kế `FrmKhachHang`: CRUD khách hàng, tìm kiếm nhanh theo SĐT (kiểm tra UNIQUE SĐT). | `feature/customer-management` | • `Database.cs` chạy ổn định cho cả nhóm.<br>• Form khách hàng kiểm tra chặt chẽ định dạng số điện thoại, bắt lỗi trùng SĐT. |
| **24/10 – 25/10** | **Cả nhóm** | **HỌP NHÓM REVIEW TUẦN 1 & MERGE PR:**<br>• TV1 mở PR `feature/auth-main` $\rightarrow$ TV2 review & merge.<br>• TV2 mở PR `feature/event-showtime` $\rightarrow$ TV3 review & merge.<br>• TV3 mở PR `feature/customer-management` $\rightarrow$ TV1 review & merge. | `main` | **Milestone 1 hoàn thành:** Nhánh `main` có đăng nhập, MDI chính và 2 màn hình danh mục hoàn chỉnh. |

---

### 🔹 TUẦN 2: SƠ ĐỒ GHẾ ĐỘNG & SINH MA TRẬN GHẾ (26/10 – 01/11/2026)
> **Mục tiêu:** Xử lý xong thành phần kỹ thuật phức tạp nhất của WinForms (`UcSoDoGhe`), cơ chế sinh ghế hàng x cột tự động và quản lý đơn vé.

| Mốc thời gian | Thành viên | Công việc chi tiết | Nhánh Git | Tiêu chí nghiệm thu (DoD) |
|---|:---:|---|---|---|
| **26/10 – 29/10** | **TV1** *(Trọng điểm)* | • **Xây dựng `UcSoDoGhe.cs` (UserControl):**<br>  - Nhận vào `MaSuat` hoặc `MaKV`, load danh sách ghế từ `GheDAL`.<br>  - Dùng code C# vẽ động ma trận `Button` theo hàng (A, B, C...) và cột (1, 2, 3...).<br>  - Đổi màu ghế theo trạng thái (Trắng/Xanh = Trống, Vàng = Đang chọn, Đỏ = Đã bán/Vô hiệu hóa).<br>  - Tối ưu hiệu năng: Dùng `SuspendLayout()` / `ResumeLayout()` tránh lag.<br>  - Tạo Custom Event `OnSelectedSeatsChanged` bắn danh sách `List<GheDTO>` đang chọn ra ngoài. | `feature/seat-matrix-uc` | • Load 200 - 300 ghế trong dưới 0.5 giây.<br>• Click chọn/bỏ chọn ghế đổi màu tức thì.<br>• Ghế đã bán không cho click.<br>• Form cha nhận được đúng danh sách ghế đang chọn. |
| **26/10 – 29/10** | **TV2** | • Viết `KhuVucDTO`, `KhuVucDAL`, `KhuVucBLL`.<br>• Thiết kế form `FrmQuanLyKhuVuc` (Cấu hình tên khu vực, hạng vé VIP/Thường, đơn giá vé).<br>• **Viết logic `KhuVucBLL.TaoKhuVucVaSinhGhe()`:** Tự động sinh hàng loạt ghế theo số hàng (ký tự A..Z) và số cột (1..N) vào bảng `Ghe` trong CSDL. | `feature/seat-category-gen` | Khi người dùng nhập "Hàng: 5, Cột: 10", hệ thống tự động sinh chính xác 50 bản ghi ghế (từ A1 đến E10) vào bảng `Ghe`. |
| **26/10 – 30/10** | **TV3** | • Viết `DonVeDTO`, `ChiTietVeDTO`, `ThanhToanDTO`.<br>• Viết `DonVeDAL`, `ChiTietVeDAL`, `ThanhToanDAL`.<br>• Thiết kế `FrmQuanLyDonVe`:<br>  - Tra cứu đơn đặt vé theo Mã đơn hoặc SĐT khách hàng.<br>  - Hiển thị danh sách chi tiết các ghế trong đơn.<br>  - Chức năng Hủy vé / Đổi vé: Kiểm tra nếu suất diễn chưa bắt đầu thì cho phép hủy, chuyển trạng thái đơn sang `DaHuy` và trả ghế về trạng thái `Trong`. | `feature/order-management` | Tìm kiếm đơn vé nhanh chóng. Hủy đơn thành công và giải phóng trạng thái ghế trên CSDL. |
| **31/10 – 01/11** | **Cả nhóm** | **HỌP NHÓM REVIEW TUẦN 2 & MERGE PR:**<br>• Test tích hợp UserControl `UcSoDoGhe` với dữ liệu sinh tự động của TV2.<br>• Kiểm tra và giải quyết conflict nếu có.<br>• Merge toàn bộ code tuần 2 vào nhánh `main`. | `main` | **Milestone 2 hoàn thành:** Sơ đồ ghế động chạy trơn tru, cơ chế sinh ghế và quản lý đơn vé hoạt động chuẩn xác. |

---

### 🔹 TUẦN 3: NGHIỆP VỤ BÁN VÉ, GIAO DỊCH AN TOÀN & BÁO CÁO (02/11 – 06/11/2026)
> **Mục tiêu:** Ráp nối toàn bộ hệ thống vào màn hình Bán vé trung tâm (`FrmBanVe`), áp dụng `SqlTransaction`, in vé và biểu đồ báo cáo. **ĐÓNG BĂNG MÃ NGUỒN.**

| Mốc thời gian | Thành viên | Công việc chi tiết | Nhánh Git | Tiêu chí nghiệm thu (DoD) |
|---|:---:|---|---|---|
| **02/11 – 04/11** | **TV3** *(Trọng điểm)* | • **Dựng màn hình Bán vé trung tâm (`FrmBanVe`):**<br>  - Chọn Sự kiện $\rightarrow$ load Suất diễn $\rightarrow$ load `UcSoDoGhe` tương ứng.<br>  - Nhận event từ `UcSoDoGhe` để hiển thị danh sách ghế chọn và tự động tính tổng tiền tức thời.<br>  - Nhập/chọn khách hàng, chọn phương thức thanh toán.<br>• **Xây dựng `BanVeBLL.XuLyThanhToan()` với `SqlTransaction`:**<br>  - `INSERT DonVe` $\rightarrow$ duyệt danh sách ghế để `INSERT ChiTietVe` $\rightarrow$ `UPDATE Ghe.TrangThai = 'DaBan'` $\rightarrow$ `INSERT ThanhToan`.<br>  - Đảm bảo tính nguyên tử (Atomicity): Lỗi bất kỳ bước nào lập tức Rollback toàn bộ. | `feature/ticket-booking-core` | • Luồng bán vé hoàn chỉnh từ chọn ghế đến thanh toán.<br>• Sau khi bấm Thanh toán, ghế lập tức chuyển sang màu đỏ.<br>• Không bao giờ bị trừ tiền mà không có vé. |
| **02/11 – 04/11** | **TV2** | • Viết `BaoCaoDAL` và `BaoCaoBLL`.<br>• Dựng giao diện `FrmBaoCao`:<br>  - Thống kê doanh thu theo sự kiện, theo suất diễn, theo ngày/tháng/năm.<br>  - Tính tỷ lệ lấp đầy ghế (Số ghế đã bán / Tổng số ghế).<br>  - Tích hợp biểu đồ cột/tròn (`MS Chart Control`) thể hiện trực quan doanh thu từng sự kiện. | `feature/analytics-reporting` | Biểu đồ trực quan, số liệu thống kê khớp 100% với dữ liệu đơn hàng trong CSDL. |
| **02/11 – 04/11** | **TV1** | • Xây dựng form in vé `FrmInVe` (sử dụng `PrintDocument` hoặc GDI+ để xuất mẫu vé có đầy đủ: Tên sự kiện, Suất diễn, Hàng ghế, Mã vé Code, Giá vé).<br>• Dựng form `FrmNhatKy` và viết `NhatKyDAL` (ghi lại lịch sử đăng nhập, bán vé, hủy vé để Admin theo dõi). | `feature/ticket-print-audit` | • Bấm "In vé" hiển thị hộp thoại xem trước (Print Preview) chuẩn đẹp.<br>• Nhật ký hệ thống ghi nhận đúng người dùng và thời gian thực hiện. |
| **05/11 – 06/11** | **Cả nhóm** | **TÍCH HỢP TOÀN HỆ THỐNG & CODE FREEZE:**<br>• Ghép nối `FrmInVe` tự động mở sau khi `FrmBanVe` thanh toán thành công.<br>• Review chéo lần cuối và merge toàn bộ nhánh tính năng vào `main`.<br>• **23:59 ngày 06/11/2026: CHÍNH THỨC ĐÓNG BĂNG MÃ NGUỒN.** | `main` | **100% chức năng hoàn tất.** Nhánh `main` ổn định, không chỉnh sửa code tính năng mới nữa. |

---

### 🔹 TUẦN 4: KIỂM THỬ HỆ THỐNG, BÁO CÁO, SLIDE & TỔNG DUYỆT (07/11 – 11/11/2026)
> **Mục tiêu:** Kiểm thử 6 kịch bản nghiêm ngặt, hoàn tất tài liệu, slide thuyết trình và diễn tập để đạt điểm tối đa khi báo cáo.

| Ngày | Công việc cụ thể | Phụ trách chính | Tiêu chuẩn chất lượng bàn giao |
|---|---|:---:|---|
| **07/11 (Thứ 7)** | **Kiểm thử toàn diện 6 Test Cases chuẩn:**<br>1. *TC1 - Phân quyền:* Đăng nhập tài khoản Staff, kiểm tra các nút quản lý Admin có bị khóa không.<br>2. *TC2 - Bán vé thành công:* Mua 3 ghế cùng lúc $\rightarrow$ kiểm tra bảng `DonVe`, `ChiTietVe`, `Ghe`, `ThanhToan` trong SQL có tăng đúng 4 bảng không.<br>3. *TC3 - Rollback Transaction:* Giả lập mất kết nối giữa chừng xem ghế có bị trừ oan không.<br>4. *TC4 - Hủy vé:* Hủy 1 đơn vé $\rightarrow$ kiểm tra ghế có trả về màu trắng/xanh (`Trong`) ngay không.<br>5. *TC5 - Sinh ghế tự động:* Tạo khu vực mới xem có sinh đúng số lượng ghế không.<br>6. *TC6 - Báo cáo thống kê:* Kiểm tra tổng tiền doanh thu có khớp tổng tiền các đơn vé không. | **TV3** (chủ trì)<br>+ TV1, TV2 | Lập file biên bản kiểm thử **`Test_Execution_Report.md`** ghi rõ Pass/Fail của từng test case. Sửa triệt để các lỗi giao diện nhỏ nếu phát sinh. |
| **08/11 (Chủ nhật)** | • **Biên tập hoàn thiện Báo cáo đồ án:** Soạn thảo tài liệu báo cáo theo đúng chuẩn khoa CNTT - HCMUE (bìa, mục lục, cơ sở lý thuyết 3 lớp, thiết kế CSDL, hướng dẫn sử dụng, bảng % đóng góp của từng thành viên).<br>• **Thiết kế Slide thuyết trình:** Soạn slide PowerPoint (15 – 20 slide) với bố cục chuyên nghiệp (Vấn đề $\rightarrow$ Kiến trúc 3 lớp $\rightarrow$ Giải pháp kỹ thuật Transaction & Sơ đồ ghế $\rightarrow$ Kịch bản Demo). | **TV1** (Báo cáo Word/PDF)<br><br>**TV3** (Slide PPT) | • File báo cáo `BaoCao_DoAn_Nhom3.pdf` chuẩn chỉnh, không lỗi chính tả.<br>• File slide `Slide_ThuyetTrinh_Nhom3.pptx` hình ảnh rõ nét, cô đọng. |
| **09/11 (Thứ 2)** | • Chụp ảnh màn hình toàn bộ các form đưa vào thư mục `docs/screenshots/`.<br>• Cập nhật hoàn thiện file `README.md` trên GitHub (gắn link ảnh chụp màn hình, kiểm tra hướng dẫn cài đặt).<br>• Xuất bản nén mã nguồn dự phòng `.zip` (đã dọn sạch `bin/`, `obj/`, `.vs/`). | **TV1** (README & Ảnh)<br><br>**TV2** (Đóng gói zip) | • `README.md` hiển thị đầy đủ ảnh minh họa trên GitHub.<br>• File nén `QuanLySuKienVaBanVe_Nhom3.zip` nhẹ, mở ra chạy được ngay. |
| **10/11 (Thứ 3)** | **TỔNG DUYỆT (REHEARSAL) TRƯỚC BÁO CÁO:**<br>• Cả nhóm họp trực tiếp/online thuyết trình thử bấm giờ (khống chế 15 – 20 phút).<br>• Phân chia người trình bày rõ ràng:<br>  - *TV1:* Giới thiệu đề tài, phân quyền, sơ đồ ghế động.<br>  - *TV2:* Danh mục sự kiện, cơ chế sinh ghế và báo cáo doanh thu.<br>  - *TV3:* Demo luồng bán vé trung tâm, giải thích cơ chế `SqlTransaction` và kết luận.<br>• Chuẩn bị thiết bị: Laptop chạy demo, cáp HDMI, sạc dự phòng, chuột trình chiếu. | **Cả nhóm** | Mọi thành viên nắm vững toàn bộ mã nguồn và tự tin trả lời câu hỏi phản biện của thầy Thoại. |
| **11/11 (Thứ 4)** | 🎓 **NGÀY BÁO CÁO CHÍNH THỨC VỚI THS. LÊ THANH THOẠI!** | **Cả nhóm** | **Báo cáo thành công xuất sắc — Mục tiêu đạt điểm A+** |

---

## 4. BẢNG CHECKLIST SẢN PHẨM BÀN GIAO NGÀY 11/11/2026

| STT | Thành phần bàn giao | Người chịu trách nhiệm | Định dạng / Nơi lưu | Trạng thái |
|:---:|---|:---:|---|:---:|
| 1 | Mã nguồn WinForms chạy ổn định (0 Warning, 0 Error) | **TV3** | Nhánh `main` GitHub | ⏳ Đang làm |
| 2 | Kịch bản CSDL đầy đủ (`create_database.sql`, `sample_data.sql`) | **TV2** | Thư mục `database/` | ✅ Đã có sẵn |
| 3 | File `README.md` chuẩn 7 mục theo mẫu của thầy | **TV1** | Gốc repo GitHub | ✅ Đã có sẵn |
| 4 | File Báo cáo đồ án chi tiết (kèm bảng % đóng góp) | **TV1** | `docs/BaoCao_DoAn_Nhom3.pdf` | ⏳ Deadline 08/11 |
| 5 | Slide thuyết trình đồ án | **TV3** | `docs/Slide_ThuyetTrinh_Nhom3.pptx` | ⏳ Deadline 08/11 |
| 6 | File nén source code dự phòng (không chứa rác build) | **TV2** | `QuanLySuKienVaBanVe_Nhom3.zip` | ⏳ Deadline 09/11 |
| 7 | Ảnh chụp minh họa các màn hình phần mềm | **TV1** | Thư mục `docs/screenshots/` | ⏳ Deadline 09/11 |

---

## 5. NGUYÊN TẮC LÀM VIỆC ĐẢM BẢO TIẾN ĐỘ

1. **Tuân thủ quy trình Git hàng ngày:**
   - *"Trước khi code: `git pull origin main`"*.
   - *"Code xong chạy được: `git commit` với thông điệp rõ ràng"*.
   - *"Muốn merge vào main: Mở Pull Request $\rightarrow$ nhờ thành viên khác Review & Approve $\rightarrow$ mới bấm Merge"*.
2. **Quy tắc "Không ôm việc tắc nghẽn":**
   - Nếu gặp lỗi hoặc bị kẹt quá **24 giờ** (đặc biệt ở phần sơ đồ ghế hoặc Transaction), phải nhắn ngay lên nhóm chat để cả 3 cùng vào debug hỗ trợ.
3. **Commit đều đặn suốt 3 tuần:**
   - Không được để trống GitHub rồi đến ngày 05/11 mới dồn một đống code lên. Thầy Thoại sẽ xem biểu đồ đóng góp (Commit Insights) để đánh giá thái độ học tập của từng người.