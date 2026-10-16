# BÁO CÁO TÓM TẮT ĐỒ ÁN
## ỨNG DỤNG THIẾT KẾ VÀ IN THẺ SINH VIÊN (LAB 09 - WINFORMS)

---

### 1. TỔNG QUAN ĐỒ ÁN
- **Tên dự án:** StudentCardPrinter
- **Nền tảng phát triển:** C# .NET 8.0 (Windows Forms)
- **Môi trường phát triển đề xuất:** Visual Studio 2022
- **Mục tiêu:** Xây dựng ứng dụng desktop cho phép nhập thông tin sinh viên, tùy chỉnh ảnh đại diện, tự động sinh lớp học tương ứng theo Khoa & Khóa, trực quan hóa thẻ sinh viên bằng GDI+ và hỗ trợ xem trước/in ấn trực tiếp.

---

### 2. CÁC CẢI TIẾN VÀ XỬ LÝ THEO YÊU CẦU

#### 2.1. Cải tiến giao diện (UI/UX)
1. **Khắc phục lỗi lệch ô Mã sinh viên:**
   - Điều chỉnh tọa độ (`Location`) và kích thước (`Size`) của `txtStudentId` giúp hiển thị đầy đủ văn bản nhập vào, không còn hiện tượng bị cấn hay lẹm mất chữ.
2. **Căn chỉnh nhóm "Xem trước thẻ sinh viên":**
   - Đồng bộ tọa độ trục $Y$ (`y = 12`) cho cả 2 `GroupBox` (Thông tin sinh viên bên trái và Xem trước bên phải), loại bỏ tình trạng khối preview bị dâng lên cao so với các thành phần kế bên.
3. **Thứ tự sắp xếp lại các trường nhập liệu:**
   - **Vị trí 1:** Mã sinh viên (`TextBox`)
   - **Vị trí 2:** Họ tên (`TextBox`)
   - **Vị trí 3:** Khoa (`ComboBox`)
   - **Vị trí 4:** Khóa học (`TextBox`)
   - **Vị trí 5:** Lớp (`ComboBox` tự động cập nhật)
   - **Vị trí 6:** Ngày sinh (`DateTimePicker`)
   - **Vị trí 7:** Giới tính (`ComboBox`)

#### 2.2. Xử lý nghiệp vụ & Logic dữ liệu
1. **Danh mục Khoa mẫu:**
   - Cung cấp sẵn 2 khoa lựa chọn: `"Công nghệ thông tin"` và `"Ngôn ngữ Pháp"`.
2. **Logic tự động gợi ý Lớp học theo Khoa & Khóa:**
   - **Xử lý nhập liệu linh hoạt:** Chấp nhận người dùng nhập chữ `K` hoa, `k` thường, hoặc chỉ nhập số (Ví dụ: `k51`, `K51`, `51`).
   - **Lọc danh sách Lớp tương ứng:**
     - Nếu chọn **Khoa CNTT** + nhập khóa **K51** $\rightarrow$ Danh sách lớp tự động hiển thị: `K51.CNTT.A`, `K51.CNTT.B`, `K51.CNTT.C`, `K51.CNTT.D`.
     - Nếu chọn **Khoa Ngôn ngữ Pháp** + nhập khóa **K51** $\rightarrow$ Danh sách lớp tự động hiển thị: `K51.NNP.A`, `K51.NNP.B`, `K51.NNP.C`.
3. **Chuẩn hóa dữ liệu xuất ra Thẻ:**
   - Dù người dùng gõ `k51` hay `51`, khi xuất ra thẻ xem trước hoặc bản in thực tế, ký tự **K** luôn bắt buộc được chuyển thành **K in hoa** (`K51`).

---

### 3. CẤU TRÚC MÃ NGUỒN (SOURCE CODE)

| File | Chức năng chính |
| :--- | :--- |
| **`Program.cs`** | Điểm khởi chạy ứng dụng (Entry Point). |
| **`StudentCardInfo.cs`** | Model lưu trữ toàn bộ dữ liệu của thẻ sinh viên (Mã, Tên, Khoa, Khóa, Lớp, Ảnh,...). |
| **`FrmStudentCard.cs`** | Form chính chứa giao diện, xử lý sự kiện, ràng buộc logic lớp/khóa và hàm vẽ GDI+ (`DrawStudentCard`). |

---

### 4. QUY TRÌNH HOẠT ĐỘNG VÀ VẼ GDI+

1. **Vẽ Thẻ Trực quan (Real-time Preview):**
   - Khi có thay đổi ở bất kỳ trường dữ liệu nào, sự kiện `Invalidate()` trên `Panel` sẽ được kích hoạt để vẽ lại thẻ ngay lập tức bằng đồ họa GDI+.
   - Ảnh thẻ được tính toán tỷ lệ tỉ mỉ (`DrawImageZoom`) giúp hình ảnh hiển thị vuông vắn, không bị co giãn hay méo hình.
2. **Xử lý In ấn (Print Engine):**
   - Sử dụng `PrintDocument` phối hợp với `PrintPreviewDialog` và `PrintDialog`.
   - Sử dụng chung hàm đồ họa `DrawStudentCard` cho cả việc Xem trước trên Form và In thực tế ra file PDF/máy in thật, đảm bảo tính nhất quán tuyệt đối giữa bản preview và bản in.

---

### 5. HƯỚNG DẪN CHẠY DỰ ÁN
1. Mở file solution `StudentCardPrinter.sln` bằng **Visual Studio 2022**.
2. Nhấn nút **Start** hoặc phím **F5** để biên dịch và chạy ứng dụng.
3. Nhập mã sinh viên, tên sinh viên, chọn Khoa, Khóa học và chọn file Ảnh đại diện (`.jpg`, `.png`).
4. Quan sát khung **Xem trước** bên phải tự động cập nhật.
5. Nhấn **Xem trước** để mở cửa sổ in hoặc nhấn **In** để xuất file PDF/In thẻ.