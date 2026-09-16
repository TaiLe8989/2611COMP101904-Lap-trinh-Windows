# Bài tập: Quản lý nhân viên (C# Console)

**Ngày thực hiện:** 16/9/2026

**Ngôn ngữ:** C# (.NET 8, Console App)

2611COMP101904 - Lập trình Windows

MSSV: 51.01.104.087

Họ và Tên: Lê Minh Tài

Lớp: 51.01.CNTT.C

---

## 1. Mục tiêu bài tập

Bài này yêu cầu xây dựng một chương trình quản lý nhân viên đơn giản, mục đích chính là ôn lại 5 kiến thức nền tảng của OOP: Class, Property, Constructor, Encapsulation, và đặc biệt là Kế thừa với Đa hình. Đề bài không chỉ dừng ở việc viết cho chạy được, mà còn yêu cầu khi xử lý (hiển thị thông tin, tính lương, tìm lương cao nhất...) phải dùng đúng cơ chế đa hình chứ không được "ăn gian" bằng if/switch để check xem nhân viên đang thuộc loại nào.

## 2. Cách tổ chức chương trình

Em chia chương trình thành các lớp riêng biệt cho dễ quản lý, thay vì gộp hết vào 1 file:

- `NhanVien.cs` – lớp cha, chứa các thuộc tính chung: mã NV, họ tên, lương cơ bản.
- `NhanVienVanPhong.cs` – kế thừa từ NhanVien, thêm số ngày làm việc.
- `NhanVienKinhDoanh.cs` – kế thừa từ NhanVien, thêm doanh số.
- `NhanVienThoiVu.cs` – phần bonus, thêm số giờ làm và lương theo giờ.
- `Program.cs` – chứa menu và toàn bộ logic xử lý của chương trình.

### Lớp NhanVien (lớp cha)

Ba thuộc tính Mã NV, Họ tên, Lương cơ bản đều được viết dưới dạng property (get/set) thay vì để field public, phần set có kiểm tra điều kiện hợp lệ (ví dụ lương cơ bản phải > 0, nếu không sẽ ném lỗi). Đây chính là phần Encapsulation mà đề yêu cầu.

Hai phương thức `TinhLuong()` và `HienThiThongTin()` được khai báo `virtual` để các lớp con override lại theo công thức riêng của từng loại.

### Hai lớp con chính

- NhanVienVanPhong: Lương = Lương cơ bản + Số ngày làm việc × 200.000. Số ngày làm việc giới hạn 0–31 (set có validate).
- NhanVienKinhDoanh: Lương = Lương cơ bản + 5% × Doanh số.

Cả hai constructor đều gọi `base(...)` để tái sử dụng phần khởi tạo của lớp cha, tránh viết lại code trùng lặp.

### Phần bonus – NhanVienThoiVu

Thêm loại nhân viên thời vụ, tính lương theo công thức khác hẳn (Số giờ làm × Lương theo giờ), hoàn toàn không dùng lương cơ bản để tính. Mục đích của phần này là để chứng minh: nếu áp dụng đa hình đúng cách thì thêm 1 loại nhân viên mới không cần đụng vào phần thuật toán tìm lương cao nhất hay tính tổng lương công ty ở Program.cs – chỉ cần thêm 1 lớp mới override lại 2 hàm là xong.

## 3. Về việc không dùng if/switch để phân loại

Đây là phần em thấy quan trọng nhất và cũng là chỗ dễ làm sai nhất. Trong các hàm xử lý chính (xuất danh sách, tìm lương cao nhất, tính tổng lương), chương trình chỉ thao tác trên biến kiểu `NhanVien` và gọi `TinhLuong()` / `HienThiThongTin()`. Nhờ cơ chế đa hình của C#, dù object thực chất là NhanVienVanPhong hay NhanVienKinhDoanh, chương trình vẫn tự động gọi đúng bản override tương ứng, không cần code phải "biết" trước nó là loại gì.

Chỗ duy nhất em có dùng switch là trong hàm nhập liệu (`ThemNhanVien`), để hỏi người dùng muốn tạo loại nhân viên nào rồi mới `new` ra đúng lớp. Cái này khác với việc "kiểm tra nhân viên thuộc lớp nào để xử lý" mà đề cấm – đây chỉ là bước khởi tạo object, sau đó toàn bộ xử lý phía sau vẫn chạy hoàn toàn qua đa hình.

## 4. Chương trình chính (menu)

Chương trình bắt đầu bằng việc yêu cầu nhập tối thiểu 5 nhân viên (người dùng chọn loại, sau đó nhập thông tin tương ứng). Sau khi nhập xong sẽ vào menu:

```
========== MENU ==========
1. Xuất danh sách nhân viên
2. Tìm nhân viên theo mã
3. Tìm nhân viên có lương cao nhất
4. Tính tổng lương công ty phải trả
5. Thêm nhân viên mới
0. Thoát
```

Em có thêm mục số 5 (thêm nhân viên mới) ngoài yêu cầu đề bài một chút, để tiện test chương trình mà không cần thoát ra nhập lại từ đầu.

## 5. Một vài lưu ý / hạn chế

- Chương trình chưa có chức năng lưu file, mỗi lần chạy lại phải nhập lại từ đầu (đề không yêu cầu nên em chưa làm thêm phần này).
- Validate dữ liệu còn ở mức cơ bản (chặn số âm, số ngày ngoài khoảng...), chưa xử lý hết các trường hợp nhập sai định dạng phức tạp.
- Mã nhân viên hiện tại chưa kiểm tra trùng lặp khi thêm mới.
  
## 6. Ảnh ví dụ
<img width="670" height="567" alt="Screenshot 2026-09-16 231037" src="https://github.com/user-attachments/assets/d89cda74-46f4-436a-9280-546ff8e97f38" />
<img width="657" height="387" alt="Screenshot 2026-09-16 231131" src="https://github.com/user-attachments/assets/31022c9c-fe12-4d3e-aa78-69cb638a909c" />
<img width="527" height="282" alt="Screenshot 2026-09-16 231238" src="https://github.com/user-attachments/assets/cb985d5f-0ec2-4ffc-9016-dd46fc4029d0" />
<img width="486" height="307" alt="Screenshot 2026-09-16 231336" src="https://github.com/user-attachments/assets/eac31f8f-e778-4a84-b3c3-0d5d3cb83369" />
<img width="607" height="976" alt="Screenshot 2026-09-16 231536" src="https://github.com/user-attachments/assets/b6fde0b9-bf54-492e-bb1d-facfefa5f382" />
<img width="1312" height="432" alt="Screenshot 2026-09-16 231554" src="https://github.com/user-attachments/assets/774b92b3-cbb8-4b64-bed5-c45d745da3ac" />
<img width="1226" height="307" alt="Screenshot 2026-09-16 231614" src="https://github.com/user-attachments/assets/90f52179-71fb-45d9-bbb0-06bc884ecc30" />
<img width="1186" height="287" alt="Screenshot 2026-09-16 231628" src="https://github.com/user-attachments/assets/ddc6f44c-f91d-4b5d-80b3-6ce415b36110" />
<img width="623" height="252" alt="Screenshot 2026-09-16 231642" src="https://github.com/user-attachments/assets/5573932b-3b92-4506-bb05-2f507a4c143e" />
<img width="502" height="323" alt="Screenshot 2026-09-16 231655" src="https://github.com/user-attachments/assets/98b23751-2a0e-4c3e-9744-51ccc1266b39" />

