# BÁO CÁO LAB 08 – ỨNG DỤNG PAINT ĐƠN GIẢN BẰNG WINFORMS VÀ GDI+

| Thông tin  | Chi tiết                          |
| ---------- | --------------------------------- |
| Môn học    | COMP1019 – Lập trình trên Windows |
| Giảng viên | ThS. Lê Thanh Thoại               |
| Buổi học   | Buổi 8 – Lab 08                   |
| Project    | `COMP1019_Lab08_SimplePaint`      |
| Công nghệ  | C#, Windows Forms, GDI+ (.NET 8)  |

---

## 1. Mục tiêu

- Sử dụng `Graphics`, `Pen`, `Brush` và sự kiện `Paint` trong WinForms.
- Xử lý `MouseDown`, `MouseMove`, `MouseUp` để vẽ tương tác bằng chuột.
- Lưu danh sách đối tượng cần vẽ và vẽ lại khi Form/Panel được refresh.
- Hoàn thiện ứng dụng Paint đơn giản có chọn màu, độ dày, Undo và Clear.

## 2. Giao diện

| Thành phần          | Tên control | Chức năng                                              |
| ------------------- | ----------- | ------------------------------------------------------ |
| ComboBox            | `cboMode`   | Chọn chế độ: Pen, Line, Rectangle, Ellipse             |
| NumericUpDown       | `nudWidth`  | Độ dày nét từ 1 đến 20                                 |
| Button              | `btnColor`  | Mở `ColorDialog` chọn màu (nút hiển thị màu đang chọn) |
| Button              | `btnUndo`   | Xóa nét vẽ gần nhất                                    |
| Button              | `btnClear`  | Xóa toàn bộ vùng vẽ                                    |
| DrawSurface (Panel) | `canvas`    | Vùng vẽ chính, nền trắng, bật DoubleBuffered           |
| Label               | `lblStatus` | Hiển thị tọa độ chuột hiện tại                         |

## 3. Cấu trúc project

```
COMP1019_Lab08_SimplePaint/
├── Program.cs     # Điểm bắt đầu chương trình
├── FrmPaint.cs    # Form chính: giao diện + xử lý sự kiện + vẽ
├── DrawMode.cs    # enum chế độ vẽ (Pen, Line, Rectangle, Ellipse)
├── DrawItem.cs    # Dữ liệu gốc của một nét vẽ
├── DrawSurface.cs # Panel tùy chỉnh bật DoubleBuffered
└── README.md
```

## 4. Ý tưởng giải quyết

1. `MouseDown`: tạo `DrawItem` tạm (`currentItem`).
2. `MouseMove`: cập nhật điểm cuối (hoặc thêm điểm với Pen), rồi gọi `canvas.Invalidate()`.
3. `MouseUp`: đưa `currentItem` vào `List<DrawItem> items`.
4. `Paint`: vẽ lại toàn bộ `items` và `currentItem` (nếu có).

Chỉ gọi `Invalidate()` để yêu cầu vẽ lại, không gọi trực tiếp `Paint`.

## 5. Đối chiếu yêu cầu

| Yêu cầu                          | Cách đáp ứng                                                                  |
| -------------------------------- | ----------------------------------------------------------------------------- |
| Pen / Line / Rectangle / Ellipse | `DrawFreePen`, `DrawLine`, `DrawRectangle`, `DrawEllipse` trong `DrawOneItem` |
| Chọn màu, độ dày                 | `ColorDialog`, `NumericUpDown` (1–20)                                         |
| Undo, Clear                      | Xóa phần tử cuối / xóa cả danh sách rồi `Invalidate()`                        |
| Hiển thị tọa độ chuột            | `lblStatus` cập nhật trong `MouseMove`                                        |
| Vẽ trong sự kiện `Paint`         | `Canvas_Paint`                                                                |
| Có danh sách lưu đối tượng       | `List<DrawItem> items`                                                        |
| Không mất nét khi resize         | Vẽ lại từ danh sách dữ liệu gốc                                               |
| Giải phóng `Pen`/`Brush`         | Dùng `using`                                                                  |
| Kéo chuột mọi hướng              | `GetRectangle()` dùng `Math.Min` / `Math.Abs`                                 |
| Chuột phải không tạo nét         | Chỉ xử lý `MouseButtons.Left`                                                 |
| Kiểm tra lỗi                     | Undo khi rỗng không làm gì; `Enum.TryParse` an toàn với chế độ vẽ             |

**Chức năng nâng cao đã làm:** phím tắt `Ctrl+Z` để Undo.

## 6. Giải thích nhanh

- **Vì sao không vẽ trực tiếp trong `MouseMove`?** Hình vẽ bằng `CreateGraphics()` sẽ biến mất khi resize hoặc bị che; lưu dữ liệu gốc rồi vẽ lại trong `Paint` thì ổn định.
- **`Invalidate()`** chỉ đánh dấu vùng cần cập nhật, Windows sẽ gọi `Paint` vào thời điểm phù hợp.
- **`GetRectangle()`** tránh chiều rộng/cao âm khi kéo từ phải sang trái hoặc từ dưới lên.
- **`using` với `Pen`** tự gọi `Dispose()`, tránh rò rỉ tài nguyên.
- **`DrawSurface`** bật `DoubleBuffered` để giảm nhấp nháy.

## 7. Checklist kiểm thử

- [ ] Vẽ tự do (Pen), Line, Rectangle, Ellipse
- [ ] Kéo Rectangle/Ellipse theo cả hai hướng trái–phải và phải–trái
- [ ] Đổi màu, đổi độ dày: nét mới áp dụng đúng
- [ ] Undo khi có và khi chưa có nét vẽ; `Ctrl+Z`
- [ ] Clear và xác nhận xóa
- [ ] Resize Form: hình không bị mất
- [ ] Chuột phải trên canvas: không tạo nét vẽ

## 8. Ảnh minh họa
