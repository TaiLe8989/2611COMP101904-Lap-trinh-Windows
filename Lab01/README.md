# BÁO CÁO BÀI LAB 01 - ỨNG DỤNG THÔNG TIN CÁ NHÂN

**Học phần:** COMP1019 - Lập trình trên Windows
**Họ tên sinh viên:** Lê Minh Tài
**Mã số sinh viên:** 51.01.104.087
**Lớp:** 51.01.CNTT.C

---

## 1. Mục tiêu bài làm

Bài lab yêu cầu xây dựng một ứng dụng Windows Forms đơn giản, cho phép nhập thông tin cá nhân của sinh viên (họ tên, năm sinh, email, giới tính, khoa/lớp) và hiển thị lại thông tin đó sau khi kiểm tra dữ liệu hợp lệ. Đây là bài đầu tiên của học phần nên mục đích chính là làm quen với việc tạo project WinForms, kéo control lên giao diện và viết xử lý sự kiện cho Button.

## 2. Công cụ sử dụng

- Visual Studio 2022
- Ngôn ngữ C#, nền tảng .NET (Windows Forms App)

## 3. Mô tả giao diện đã xây dựng

Giao diện gồm các thành phần chính sau:

- Label tiêu đề ở trên cùng.
- 3 ô TextBox để nhập họ tên, năm sinh và email.
- 2 RadioButton (Nam/Nữ) được đặt chung trong một GroupBox để nhóm lại cho gọn.
- 1 ComboBox liệt kê sẵn các khoa/lớp để người dùng chọn
- 3 Button ở giữa form: Hiển thị, Xóa, Thoát.
- Một TextBox nhiều dòng (readonly) phía dưới cùng dùng để xuất kết quả sau khi bấm Hiển thị.

Cách đặt tên control được tuân theo tiền tố quen thuộc (lbl, txt, rad, cbo, btn) để dễ đọc code và đúng theo gợi ý trong đề bài.

## 4. Xử lý chức năng

### 4.1. Nút Hiển thị

Khi bấm nút này, chương trình sẽ kiểm tra lần lượt từng trường dữ liệu trước khi xử lý, cụ thể:

- Họ tên không được để trống.
- Năm sinh không được để trống, phải nhập đúng dạng số nguyên, và giá trị phải nằm trong khoảng từ 1900 đến năm hiện tại (lấy theo `DateTime.Now.Year` để không bị cứng số liệu).
- Email không được để trống, đồng thời phải đúng định dạng (không chứa khoảng trắng, không ký tự đặc biệt ngoài dấu `@`, và bắt buộc phải có đuôi `@gmail.com`). Phần này dùng biểu thức chính quy (Regex) để kiểm tra cho gọn thay vì viết nhiều dòng if lồng nhau.
- Phải chọn 1 trong 2 giới tính.
- Phải chọn khoa/lớp trong ComboBox, nếu chưa chọn gì thì báo lỗi.

Nếu có bất kỳ lỗi nào ở trên, chương trình dừng lại ngay và hiện MessageBox cảnh báo tương ứng, đồng thời đưa con trỏ về đúng ô bị sai để người dùng sửa cho nhanh. Nếu toàn bộ dữ liệu hợp lệ, chương trình tính tuổi bằng cách lấy năm hiện tại trừ năm sinh, rồi ghép toàn bộ thông tin lại và in ra ô kết quả bên dưới form.

### 4.2. Nút Xóa

Đưa toàn bộ form về trạng thái ban đầu: xóa nội dung 3 ô TextBox nhập liệu, bỏ chọn cả 2 RadioButton, đưa ComboBox về trạng thái chưa chọn, và xóa luôn nội dung ở ô kết quả.

### 4.3. Nút Thoát

Trước khi đóng chương trình, hệ thống hiện hộp thoại hỏi xác nhận (Yes/No). Nếu người dùng chọn Yes thì chương trình mới thoát, chọn No thì quay lại làm việc bình thường, tránh trường hợp bấm nhầm bị mất dữ liệu đang nhập.

## 5. Kết quả chạy thử

Ví dụ nhập dữ liệu:

| Trường    | Giá trị                 |
| --------- | ----------------------- |
| Họ tên    | Lê Minh Tài             |
| Năm sinh  | 2007                    |
| Email     | Taile22032007@gmail.com |
| Giới tính | Nam                     |
| Khoa      | Công nghệ thông tin     |

Sau khi bấm **Hiển thị**, kết quả xuất ra:

```
THÔNG TIN SINH VIÊN
Họ tên: Lê Minh Tài
Tuổi: 19
Email: Taile22032007@gmail.com
Giới tính: Nam
Khoa/Lớp: Công nghệ thông tin
```

## 6. Tự đánh giá

Chương trình đã đáp ứng đầy đủ các yêu cầu về giao diện, đặt tên control, xử lý sự kiện cho 3 nút, kiểm tra dữ liệu đầu vào và xử lý thoát chương trình có xác nhận. Code được tổ chức thành các hàm xử lý riêng cho từng nút, có comment giải thích ở những đoạn quan trọng để dễ theo dõi.
