# Lab 02 - Quản lý mảng số nguyên bằng Console (C#)

2611COMP101904 - Lập trình Windows

MSSV: 51.01.104.087

Họ và Tên: Lê Minh Tài

Lớp: 51.01.CNTT.C

# 1. Mô tả bài toán

Chương trình Console quản lý một mảng số nguyên. Chương trình hiển thị menu để người dùng lựa chọn chức năng. Sau khi thực hiện xong một chức năng, chương trình quay lại menu cho đến khi người dùng chọn thoát.

# 2. Chức năng chương trình
   
Lựa chọn	Chức năng	Mô tả
1	Nhập mảng	Nhập số lượng phần tử n (n phải là số nguyên dương), sau đó nhập n phần tử của mảng
2	Xuất mảng	In toàn bộ phần tử của mảng ra màn hình
3	Tính tổng	Tính và in tổng các phần tử trong mảng
4	Tìm lớn nhất và nhỏ nhất	In giá trị lớn nhất và giá trị nhỏ nhất trong mảng
5	Đếm chẵn/lẻ	Đếm số lượng phần tử chẵn và số lượng phần tử lẻ
6	Sắp xếp tăng dần	Sắp xếp mảng theo thứ tự tăng dần và in kết quả sau khi sắp xếp
7	Tìm kiếm	Nhập giá trị x, kiểm tra x có xuất hiện trong mảng hay không, nếu có thì in vị trí xuất hiện đầu tiên
0	Thoát	Kết thúc chương trình

# 3. Công cụ sử dụng:
   
Visual Studio, 
Console App (C#)

# 4. Cấu trúc chương trình

Chương trình được tách thành các phương thức nhỏ, không viết toàn bộ logic trong Main:

NhapSoNguyen(string message) — nhập một số nguyên bất kỳ, kiểm tra định dạng

NhapSoNguyenDuong(string message) — nhập một số nguyên dương, kiểm tra định dạng và giá trị

NhapMang() — nhập số lượng phần tử và giá trị của mảng

XuatMang(int[] a) — in mảng ra màn hình

TinhTong(int[] a) — tính tổng các phần tử

TimMax(int[] a) — tìm giá trị lớn nhất

TimMin(int[] a) — tìm giá trị nhỏ nhất

DemChan(int[] a) — đếm số phần tử chẵn

DemLe(int[] a) — đếm số phần tử lẻ

SapXepTangDan(int[] a) — sắp xếp mảng tăng dần (thuật toán Selection Sort)

TimKiem(int[] a, int x) — tìm kiếm tuần tự, trả về vị trí đầu tiên hoặc -1 nếu không có

Chương trình có kiểm tra dữ liệu nhập (số lượng phần tử, lựa chọn menu) và không cho thực hiện các chức năng xử lý nếu người dùng chưa nhập mảng.
# 5. Dữ liệu kiểm thử
   
STT	Dữ liệu nhập	Kết quả cần kiểm tra

1)	5 phần tử: 4 1 9 2 7	Tổng = 23, max = 9, min = 1, chẵn = 2, lẻ = 3

2)	4 phần tử: -3 0 8 -1	Tổng = 4, max = 8, min = -3, chẵn = 2, lẻ = 2

3)	Tìm x = 9 trong mảng 4 1 9 2 7	Có tìm thấy, vị trí đầu tiên là 2 (tính từ 0)

4)	Tìm x = 5 trong mảng 4 1 9 2 7	Không tìm thấy

5)	Nhập n = 0 hoặc n âm	Chương trình yêu cầu nhập lại

# 6. Hình ảnh minh chứng
<img width="742" height="698" alt="Screenshot 2026-09-15 234305" src="https://github.com/user-attachments/assets/ea6eea9b-ff8d-4a4f-a02b-30f48b91774f" />
<img width="648" height="460" alt="Screenshot 2026-09-15 234341" src="https://github.com/user-attachments/assets/aceb94fd-1b4b-48ca-91ef-e1c7f35954be" />
<img width="750" height="727" alt="Screenshot 2026-09-15 234420" src="https://github.com/user-attachments/assets/6d1ee702-c1d3-4fb4-884f-80816f5a419f" />
<img width="596" height="601" alt="Screenshot 2026-09-15 234512" src="https://github.com/user-attachments/assets/1accb86c-28ad-4c8f-b9b4-a68c526ddb7f" />
<img width="845" height="316" alt="Screenshot 2026-09-15 234551" src="https://github.com/user-attachments/assets/b752a97a-8a30-4879-b8e3-51f205a99b0a" />
<img width="591" height="307" alt="Screenshot 2026-09-15 234604" src="https://github.com/user-attachments/assets/ad8efaee-7746-48d2-a0ab-1fcf120790f9" />
<img width="642" height="292" alt="Screenshot 2026-09-15 234618" src="https://github.com/user-attachments/assets/72e4eea0-35f3-4348-8108-74678e4b6dab" />
<img width="1907" height="410" alt="Screenshot 2026-09-15 234643" src="https://github.com/user-attachments/assets/fd44c558-37ac-41c4-ad35-a451e0b09308" />
