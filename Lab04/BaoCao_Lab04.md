# BÁO CÁO LAB 04 — QUẢN LÝ SẢN PHẨM

**Học phần:** COMP1019 – Lập trình trên Windows
**Buổi học:** Buổi 4 – Exception, Delegate/Event, Func/Action, Generic
**Đề tài:** Chương trình Console C# quản lý sản phẩm bằng `Repository<T>`

---

## 1. Mục tiêu bài lab

- Vận dụng `try-catch`, `throw` và exception tự tạo để xử lý lỗi.
- Sử dụng delegate/event (`Action`) để thông báo khi dữ liệu thay đổi.
- Sử dụng `Func<T, bool>` để lọc và tìm kiếm dữ liệu theo điều kiện.
- Xây dựng generic class `Repository<T>` để quản lý danh sách đối tượng.
- Tổ chức code rõ ràng, tách lớp, không viết toàn bộ xử lý trong `Main`.

## 2. Mô tả bài toán

Chương trình Console quản lý sản phẩm, lưu dữ liệu trong bộ nhớ bằng `Repository<T>`.
Chương trình xử lý ba loại lỗi thường gặp: nhập liệu sai định dạng, mã sản phẩm bị
trùng khi thêm mới, và sản phẩm không tồn tại khi xóa.

## 3. Cơ sở lý thuyết áp dụng

### 3.1. Exception tự tạo

Exception tự tạo là lớp kế thừa từ `Exception`, dùng để mô tả một lỗi nghiệp vụ cụ
thể thay vì dùng exception chung chung. Trong bài, hai exception được tạo:

- `DuplicateProductException`: phát sinh khi thêm sản phẩm có mã đã tồn tại.
- `ProductNotFoundException`: phát sinh khi xóa sản phẩm không tồn tại.

Mỗi exception lưu lại mã sản phẩm liên quan và có thông điệp lỗi bằng tiếng Việt,
giúp nơi gọi (`Program`) bắt lỗi và hiển thị thông báo phù hợp bằng `try-catch`.

### 3.2. Delegate/Event (Action)

Event dựa trên delegate `Action<T>` cho phép một lớp "phát tín hiệu" khi có việc
xảy ra, mà không cần biết ai sẽ xử lý tín hiệu đó. Trong bài, `ProductService`
khai báo hai event:

```csharp
public event Action<Product>? ProductAdded;
public event Action<Product>? ProductRemoved;
```

`Program` đăng ký (subscribe) hai hàm xử lý cho hai event này để in thông báo
"Đã thêm/xóa sản phẩm thành công" ngay sau khi thao tác hoàn tất. Cách làm này
tách rời phần xử lý nghiệp vụ (Service) khỏi phần hiển thị (Program).

### 3.3. Func/Action trong tìm kiếm và lọc

`Func<Product, bool>` là một delegate nhận vào một `Product` và trả về `bool`,
dùng làm điều kiện lọc tùy biến. Nhờ đó, một hàm `Filter` duy nhất trong
`Repository<T>` có thể phục vụ nhiều loại tìm kiếm khác nhau (theo tên, theo
khoảng giá...) chỉ bằng cách truyền vào điều kiện khác nhau, mà không cần viết
lại vòng lặp mỗi lần.

### 3.4. Generic class Repository<T>

Generic cho phép viết một lớp dùng chung cho nhiều kiểu dữ liệu mà vẫn đảm bảo
kiểm tra kiểu lúc biên dịch. `Repository<T> where T : IEntity` yêu cầu mọi kiểu
`T` được quản lý phải có thuộc tính `Id`, nhờ đó các thao tác `Add`, `Remove`,
`FindById` có thể viết một lần và dùng lại cho bất kỳ thực thể nào implement
`IEntity` (ví dụ `Product`), không chỉ riêng cho sản phẩm.

## 4. Kiến trúc chương trình

| Lớp / File | Vai trò |
|---|---|
| `IEntity` | Interface quy định thuộc tính `Id`, dùng làm ràng buộc generic |
| `Product` | Thực thể sản phẩm: `MaSP`, `TenSP`, `Price`, `Quantity`; tự kiểm tra dữ liệu hợp lệ |
| `DuplicateProductException` | Exception khi thêm trùng mã sản phẩm |
| `ProductNotFoundException` | Exception khi không tìm thấy sản phẩm |
| `Repository<T>` | Lưu trữ generic trong bộ nhớ: `Add`, `Remove`, `FindById`, `Find`, `GetAll` |
| `ProductService` | Xử lý nghiệp vụ: kiểm tra trùng/không tồn tại, gọi `Repository`, phát event |
| `Program` | Hiển thị menu, nhận nhập liệu, gọi `ProductService`, bắt exception |

Luồng xử lý một thao tác (ví dụ thêm sản phẩm):

```
Program (nhận nhập liệu)
   → ProductService.AddProduct (kiểm tra trùng mã)
        → Repository<Product>.Add (lưu vào danh sách)
        → phát event ProductAdded
   → Program (hàm xử lý event) in thông báo thành công
```

Nếu có lỗi ở bất kỳ bước nào (nhập sai định dạng, trùng mã, giá trị âm...),
exception được ném lên và `Program` bắt lại bằng `try-catch`, chương trình
không bị dừng đột ngột.

## 5. Các chức năng đã hiện thực

| STT | Chức năng | Cơ chế sử dụng |
|---|---|---|
| 1 | Thêm sản phẩm | Kiểm tra dữ liệu trong `Product`, kiểm tra trùng mã bằng `DuplicateProductException`, phát event `ProductAdded` |
| 2 | Xuất danh sách | `Repository.GetAll()`, báo riêng khi danh sách rỗng |
| 3 | Tìm theo mã | `Repository.FindById` |
| 4 | Tìm theo tên | `Func<Product, bool>` kiểm tra tên chứa từ khóa |
| 5 | Lọc theo khoảng giá | `Func<Product, bool>` kiểm tra giá nằm trong khoảng |
| 6 | Xóa sản phẩm | Kiểm tra tồn tại bằng `ProductNotFoundException`, phát event `ProductRemoved` |
| 7 | Tính tổng giá trị kho | Tổng `Price * Quantity` của mọi sản phẩm |
| 0 | Thoát | Kết thúc vòng lặp chính |

## 6. Kết quả kiểm thử

Chương trình đã được build và chạy thử với các tình huống:

- Nhập đơn giá/số lượng không phải số → báo lỗi, yêu cầu nhập lại, không thoát chương trình.
- Nhập số lượng âm → `ArgumentException` được bắt và hiển thị.
- Thêm mã sản phẩm đã tồn tại (kể cả khác hoa/thường) → `DuplicateProductException`.
- Xóa hoặc tìm mã không tồn tại → `ProductNotFoundException` hoặc thông báo không tìm thấy.
- Lọc với giá nhỏ nhất lớn hơn giá lớn nhất → báo lỗi hợp lệ hóa đầu vào.
- Thêm/xóa thành công → hiển thị đúng thông báo qua event.

Tất cả các trường hợp trên đều được xử lý êm, chương trình không bị crash.

## 7. Kết luận

Bài lab đã đáp ứng đầy đủ các yêu cầu kỹ thuật: có tối thiểu 2 exception tự tạo,
có generic class `Repository<T>`, có event khi thêm/xóa thành công, có sử dụng
`Func<Product, bool>` trong tìm kiếm/lọc, và tách xử lý ra khỏi `Main` thông qua
`ProductService` và `Repository<T>`. Cấu trúc phân lớp giúp code dễ đọc, dễ mở
rộng thêm chức năng hoặc thêm loại thực thể khác trong tương lai.
