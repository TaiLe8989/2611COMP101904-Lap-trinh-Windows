# BÁO CÁO BÀN GIAO DỰ ÁN — NHÓM 3

## ĐỀ TÀI 20: QUẢN LÝ SỰ KIỆN VÀ BÁN VÉ

> **Học phần:** COMP1019 — Lập trình Windows  
> **Giảng viên:** ThS. Lê Thanh Thoại — Khoa CNTT, HCMUE  
> **Ngày lập:** 18/10/2026  
> **Repository:** https://github.com/lekhoanohope-blip/QuanLySuKienVaBanVe

---

## MỤC LỤC

1. [Tổng quan đề tài](#1-tổng-quan-đề-tài)
2. [Kiến trúc hệ thống](#2-kiến-trúc-hệ-thống)
3. [Cấu trúc thư mục](#3-cấu-trúc-thư-mục)
4. [Thiết kế cơ sở dữ liệu](#4-thiết-kế-cơ-sở-dữ-liệu)
5. [Danh sách chức năng đầy đủ](#5-danh-sách-chức-năng-đầy-đủ)
6. [Phân công công việc chi tiết](#6-phân-công-công-việc-chi-tiết)
7. [Hợp đồng kỹ thuật giữa các thành viên](#7-hợp-đồng-kỹ-thuật-giữa-các-thành-viên)
8. [Quy trình Git bắt buộc](#8-quy-trình-git-bắt-buộc)
9. [Giai đoạn 0 — Khởi tạo nền tảng](#9-giai-đoạn-0--khởi-tạo-nền-tảng)
10. [Checklist nghiệm thu & bàn giao](#10-checklist-nghiệm-thu--bàn-giao)

---

## 1. Tổng quan đề tài

| Mục | Nội dung |
|-----|----------|
| **Tên đề tài** | Quản lý sự kiện và bán vé |
| **Nhóm bài toán** | Quản trị và vận hành |
| **Mô tả** | Quản lý sự kiện, khu vực ghế, lịch tổ chức (suất diễn), khách hàng, đặt vé và thanh toán trong ứng dụng desktop |
| **Vai trò sử dụng** | (1) Quản lý sự kiện — (2) Nhân viên bán vé |
| **Dữ liệu chính** | Sự kiện, Suất diễn, Khu vực, Ghế, Khách hàng, Đơn vé, Chi tiết vé |

### Chức năng bắt buộc (theo đề bài)

1. **Quản lý sự kiện và suất tổ chức** — Khai báo, chỉnh sửa thông tin sự kiện, thời gian và địa điểm từng suất diễn.
2. **Hiển thị sơ đồ ghế** — Trực quan hóa danh sách ghế/vị trí theo từng khu vực và suất diễn.
3. **Chọn ghế và lập đơn vé** — Chọn ghế trực tiếp trên giao diện sơ đồ, ghi nhận khách hàng và tiến hành thanh toán.
4. **Hủy hoặc đổi vé** — Xử lý việc hủy vé hoặc chuyển đổi suất/ghế theo đúng quy định nghiệp vụ.
5. **In vé và danh sách** — Hỗ trợ in vé cho khách **và** in danh sách khách tham dự của từng sự kiện/suất diễn.

### Phần mở rộng (khuyến khích)

- Vẽ phôi vé bằng đồ họa **GDI+**.
- Sinh và hiển thị **mã vé** (mã vạch hoặc QR Code).
- Thống kê báo cáo **tỷ lệ lấp đầy ghế** theo từng khu vực.

---

## 2. Kiến trúc hệ thống

```
┌──────────────────────────────────────────────────────────┐
│                      GUI (WinForms)                      │
│   FrmDangNhap │ FrmMain (MDI) │ FrmBanVe │ UcSoDoGhe    │
│   FrmQuanLySuKien │ FrmQuanLySuatDien │ FrmBaoCao │ ... │
├──────────────────────────────────────────────────────────┤
│                     BLL (Nghiệp vụ)                      │
│   Validate dữ liệu │ Tính tiền │ Kiểm tra trạng thái    │
│   Quy tắc hủy/đổi vé │ Phân quyền vai trò               │
├──────────────────────────────────────────────────────────┤
│                 DAL (Truy cập dữ liệu)                   │
│   Database.cs (SqlConnection + SqlTransaction dùng chung)│
│   TaiKhoanDAL │ SuKienDAL │ GheDAL │ DonVeDAL │ ...     │
├──────────────────────────────────────────────────────────┤
│              DTO (Đối tượng truyền dữ liệu)              │
│   TaiKhoan │ SuKien │ SuatDien │ KhuVuc │ Ghe │ ...     │
├──────────────────────────────────────────────────────────┤
│                   Microsoft SQL Server                    │
└──────────────────────────────────────────────────────────┘
```

**Nguyên tắc:**
- GUI chỉ gọi BLL (không bao giờ gọi thẳng DAL).
- BLL xử lý logic, validate, rồi gọi DAL để đọc/ghi CSDL.
- DAL chỉ chứa câu lệnh SQL / Stored Procedure, trả về `DataTable` hoặc `bool`/`int`.
- DTO là lớp đơn giản chỉ có properties, dùng để truyền dữ liệu giữa các tầng.

---

## 3. Cấu trúc thư mục

### 3.1. Cấu trúc trên máy cá nhân

```
📁 Project_QuanLySuKienVaBanVe/          ← CHỈ LƯU Ở MÁY CÁ NHÂN (Git không theo dõi)
│
├── 📄 nhap_code.txt                     ← File nháp, ghi chú cá nhân
├── 📁 backup_zip/                       ← Các bản nén dự phòng khi code thử
├── 📁 tai_lieu_tham_khao/               ← Đề bài PDF, slide môn học, nháp LaTeX
│
└── 📁 QuanLySuKienVaBanVe/              ← REPOSITORY GIT (xem mục 3.2)
```

> **Lưu ý cho cả nhóm:** Khi mở terminal làm việc với Git, phải `cd` vào `QuanLySuKienVaBanVe/` (nơi chứa `.git/`). KHÔNG gõ lệnh Git ở thư mục cha.

### 3.2. Cấu trúc Repository trên GitHub

```
📁 QuanLySuKienVaBanVe/                  ← GỐC REPO (.git/ nằm tại đây)
│
├── 📄 .gitignore                        ← Chặn bin/, obj/, .vs/, *.exe, *.user
├── 📄 README.md                         ← Hướng dẫn theo mẫu 7 mục của thầy
├── 📄 QuanLySuKienVaBanVe.sln           ← File Solution — BẮT BUỘC ở gốc repo
│
├── 📁 Scripts/                          ← Script cơ sở dữ liệu SQL Server
│   ├── 📄 01_TaoBang.sql                ← CREATE DATABASE + CREATE TABLE + FK + UNIQUE
│   ├── 📄 02_DuLieuMau.sql              ← INSERT dữ liệu mẫu + tài khoản demo
│   └── 📄 03_StoredProcedures.sql       ← Stored Procedures (nếu dùng)
│
├── 📁 docs/
│   ├── 📁 screenshots/                  ← Ảnh chụp màn hình gắn vào README.md
│   ├── 📁 report/                       ← Báo cáo LaTeX + file PDF xuất ra
│   └── 📁 slides/                       ← Slide thuyết trình
│
└── 📁 QuanLySuKienVaBanVe/              ← Project C# WinForms (.csproj nằm trong này)
    ├── 📄 QuanLySuKienVaBanVe.csproj
    ├── 📄 Program.cs
    ├── 📄 App.config                    ← ConnectionString (mỗi người tự sửa cục bộ)
    │
    ├── 📁 DTO/
    │   ├── TaiKhoan.cs
    │   ├── SuKien.cs
    │   ├── SuatDien.cs
    │   ├── KhuVuc.cs
    │   ├── Ghe.cs
    │   ├── KhachHang.cs
    │   ├── DonVe.cs
    │   ├── ChiTietVe.cs
    │   ├── ThanhToan.cs                 ← BỔ SUNG
    │   └── NhatKy.cs                    ← BỔ SUNG
    │
    ├── 📁 DAL/
    │   ├── Database.cs                  ← Lớp nền tảng: hỗ trợ query thường VÀ Transaction
    │   ├── TaiKhoanDAL.cs
    │   ├── SuKienDAL.cs
    │   ├── SuatDienDAL.cs
    │   ├── KhuVucDAL.cs
    │   ├── GheDAL.cs
    │   ├── KhachHangDAL.cs
    │   ├── DonVeDAL.cs
    │   ├── ChiTietVeDAL.cs
    │   ├── ThanhToanDAL.cs              ← BỔ SUNG
    │   └── NhatKyDAL.cs                 ← BỔ SUNG
    │
    ├── 📁 BLL/
    │   ├── TaiKhoanBLL.cs
    │   ├── SuKienBLL.cs
    │   ├── SuatDienBLL.cs
    │   ├── KhuVucBLL.cs                 ← Có logic: Thêm KhuVuc → tự động sinh Ghế
    │   ├── GheBLL.cs
    │   ├── KhachHangBLL.cs
    │   ├── DonVeBLL.cs                  ← Có logic: Bán vé (Transaction) + Hủy/Đổi vé
    │   ├── ChiTietVeBLL.cs
    │   ├── ThanhToanBLL.cs              ← BỔ SUNG
    │   └── NhatKyBLL.cs                 ← BỔ SUNG
    │
    ├── 📁 GUI/
    │   ├── 📁 Forms/
    │   │   ├── FrmDangNhap.cs           ← Đăng nhập + phân quyền vai trò
    │   │   ├── FrmMain.cs               ← MDI Parent + Menu theo quyền
    │   │   ├── FrmQuanLySuKien.cs       ← CRUD Sự kiện
    │   │   ├── FrmQuanLySuatDien.cs     ← CRUD Suất diễn (theo sự kiện)
    │   │   ├── FrmQuanLyKhuVuc.cs       ← BỔ SUNG — CRUD Khu vực + sinh ghế tự động
    │   │   ├── FrmBanVe.cs              ← Tích hợp UcSoDoGhe + lập đơn + thanh toán
    │   │   ├── FrmQuanLyDonVe.cs        ← BỔ SUNG — Tra cứu đơn + Hủy/Đổi vé
    │   │   ├── FrmKhachHang.cs          ← CRUD Khách hàng + In danh sách tham dự
    │   │   ├── FrmBaoCao.cs             ← Thống kê doanh thu + tỷ lệ lấp đầy
    │   │   └── FrmInVe.cs               ← In vé (GDI+ nếu làm mở rộng)
    │   │
    │   └── 📁 Controls/
    │       └── UcSoDoGhe.cs             ← BỔ SUNG — UserControl sơ đồ ghế dùng chung
    │
    ├── 📁 Resources/
    │   ├── icons/                       ← Icon cho menu, nút bấm
    │   └── images/                      ← Logo, hình nền
    │
    ├── 📁 bin/                          ← ❌ Bị .gitignore chặn
    └── 📁 obj/                          ← ❌ Bị .gitignore chặn
```

### 3.3. Tổng hợp những gì BỔ SUNG so với bản kế hoạch ban đầu

| Hạng mục | Lý do bổ sung |
|----------|---------------|
| `ThanhToan.cs` (DTO/DAL/BLL) | CSDL có bảng `ThanhToan` nhưng code gốc không có lớp xử lý |
| `NhatKy.cs` (DTO/DAL/BLL) | CSDL có bảng `NhatKy` nhưng code gốc không có lớp xử lý |
| `FrmQuanLyKhuVuc.cs` | TV2 có `KhuVucBLL` nhưng không có Form nào CRUD khu vực |
| `FrmQuanLyDonVe.cs` | Đề bài yêu cầu "Hủy hoặc đổi vé" nhưng không có Form nào xử lý |
| `UcSoDoGhe.cs` (UserControl) | Giải quyết điểm nghẽn ghép code giữa TV1 (Sơ đồ ghế) và TV3 (Bán vé) |
| `GUI/Forms/` + `GUI/Controls/` | Tách Form và UserControl vào thư mục con cho rõ ràng |
| `docs/report/` + `docs/slides/` | Đề bài yêu cầu nộp Báo cáo + Slide nhưng chưa có thư mục chứa |

---

## 4. Thiết kế cơ sở dữ liệu

### 4.1. Sơ đồ quan hệ tổng quát

```
TaiKhoan
    │
    ▼ (1 TK → nhiều log)
NhatKy

SuKien ──1:N──► SuatDien ──1:N──► KhuVuc ──1:N──► Ghe
                    │                                │
                    │                                │
                    ▼ (1 Suất → nhiều Đơn)           │
               DonVe ──1:N──► ChiTietVe ◄────────────┘
                 │                         (1 Ghế → 1 ChiTietVe)
                 │
                 ▼ (1 Đơn → 1 ThanhToan)
              ThanhToan

KhachHang ──1:N──► DonVe
```

### 4.2. Chi tiết các bảng

#### Bảng `TaiKhoan`
| Cột | Kiểu | Ràng buộc | Ghi chú |
|-----|------|-----------|---------|
| MaTK | INT | PK, IDENTITY | Khóa chính tự tăng |
| TenDangNhap | NVARCHAR(50) | **UNIQUE**, NOT NULL | Không cho phép trùng |
| MatKhau | NVARCHAR(255) | NOT NULL | Nên hash (SHA256 hoặc bcrypt) |
| VaiTro | NVARCHAR(30) | NOT NULL | `'QuanLy'` hoặc `'NhanVien'` |
| TrangThai | BIT | DEFAULT 1 | 1 = Hoạt động, 0 = Khóa |

#### Bảng `SuKien`
| Cột | Kiểu | Ràng buộc | Ghi chú |
|-----|------|-----------|---------|
| MaSK | INT | PK, IDENTITY | |
| TenSK | NVARCHAR(200) | NOT NULL | |
| MoTa | NVARCHAR(MAX) | | |
| NgayBatDau | DATETIME | NOT NULL | |
| NgayKetThuc | DATETIME | NOT NULL | CHECK >= NgayBatDau |
| DiaDiem | NVARCHAR(200) | NOT NULL | |
| TrangThai | NVARCHAR(30) | DEFAULT N'SapDien' | `SapDien` / `DangDien` / `DaKetThuc` / `DaHuy` |

#### Bảng `SuatDien`
| Cột | Kiểu | Ràng buộc | Ghi chú |
|-----|------|-----------|---------|
| MaSuat | INT | PK, IDENTITY | |
| MaSK | INT | FK → SuKien | |
| ThoiGianBatDau | DATETIME | NOT NULL | |
| ThoiGianKetThuc | DATETIME | NOT NULL | CHECK > ThoiGianBatDau |
| TrangThai | NVARCHAR(30) | DEFAULT N'MoCuaBanVe' | `MoCuaBanVe` / `DongBanVe` / `DangDien` / `DaKetThuc` |

#### Bảng `KhuVuc`
| Cột | Kiểu | Ràng buộc | Ghi chú |
|-----|------|-----------|---------|
| MaKV | INT | PK, IDENTITY | |
| TenKV | NVARCHAR(50) | NOT NULL | VD: "VIP", "Thường", "Balcony" |
| MaSuat | INT | FK → SuatDien | Mỗi suất diễn có danh sách khu vực riêng |
| SoHang | INT | NOT NULL, CHECK > 0 | ⚡ Thay `SoGhe` bằng `SoHang` + `SoCot` |
| SoCot | INT | NOT NULL, CHECK > 0 | Tổng ghế = SoHang × SoCot |
| GiaVe | DECIMAL(18,0) | NOT NULL, CHECK >= 0 | Giá vé đồng nhất theo khu vực |

#### Bảng `Ghe`
| Cột | Kiểu | Ràng buộc | Ghi chú |
|-----|------|-----------|---------|
| MaGhe | INT | PK, IDENTITY | |
| MaKV | INT | FK → KhuVuc | |
| Hang | NVARCHAR(5) | NOT NULL | VD: "A", "B", "C" |
| Cot | INT | NOT NULL | VD: 1, 2, 3... |
| TrangThai | NVARCHAR(20) | DEFAULT N'Trong' | `Trong` / `DangGiu` / `DaBan` |
| | | **UNIQUE (MaKV, Hang, Cot)** | Không cho 2 ghế cùng vị trí trong 1 khu vực |

#### Bảng `KhachHang`
| Cột | Kiểu | Ràng buộc | Ghi chú |
|-----|------|-----------|---------|
| MaKH | INT | PK, IDENTITY | |
| TenKH | NVARCHAR(100) | NOT NULL | |
| SDT | VARCHAR(15) | **UNIQUE** | Tránh trùng khách hàng |
| Email | NVARCHAR(100) | | Có thể NULL |

#### Bảng `DonVe`
| Cột | Kiểu | Ràng buộc | Ghi chú |
|-----|------|-----------|---------|
| MaDon | INT | PK, IDENTITY | |
| MaKH | INT | FK → KhachHang | |
| MaSuat | INT | FK → SuatDien | |
| MaTK | INT | FK → TaiKhoan | Nhân viên thực hiện giao dịch |
| NgayDat | DATETIME | DEFAULT GETDATE() | |
| TongTien | DECIMAL(18,0) | NOT NULL | |
| TrangThai | NVARCHAR(30) | DEFAULT N'DaThanhToan' | `DaThanhToan` / `DaHuy` / `DaDoi` |

#### Bảng `ChiTietVe`
| Cột | Kiểu | Ràng buộc | Ghi chú |
|-----|------|-----------|---------|
| MaCT | INT | PK, IDENTITY | |
| MaDon | INT | FK → DonVe | |
| MaGhe | INT | FK → Ghe | |
| GiaVe | DECIMAL(18,0) | NOT NULL | Giá tại thời điểm mua |
| MaVeCode | VARCHAR(50) | **UNIQUE** | ⚡ Mã vé duy nhất (VD: `SK01-SD02-A05-8921`) dùng cho in vé / QR |
| TrangThai | NVARCHAR(20) | DEFAULT N'HopLe' | `HopLe` / `DaHuy` |
| | | **UNIQUE (MaDon, MaGhe)** | 1 đơn không chứa 2 lần cùng 1 ghế |

#### Bảng `ThanhToan`
| Cột | Kiểu | Ràng buộc | Ghi chú |
|-----|------|-----------|---------|
| MaTT | INT | PK, IDENTITY | |
| MaDon | INT | FK → DonVe | |
| NgayThanhToan | DATETIME | DEFAULT GETDATE() | |
| SoTien | DECIMAL(18,0) | NOT NULL | |
| PhuongThuc | NVARCHAR(30) | NOT NULL | `TienMat` / `ChuyenKhoan` / `TheNganHang` |

#### Bảng `NhatKy`
| Cột | Kiểu | Ràng buộc | Ghi chú |
|-----|------|-----------|---------|
| MaLog | INT | PK, IDENTITY | |
| MaTK | INT | FK → TaiKhoan | Ai thực hiện |
| HanhDong | NVARCHAR(500) | NOT NULL | Mô tả hành động |
| ThoiGian | DATETIME | DEFAULT GETDATE() | |

### 4.3. Ràng buộc UNIQUE bắt buộc

```sql
ALTER TABLE TaiKhoan  ADD CONSTRAINT UQ_TenDangNhap     UNIQUE (TenDangNhap);
ALTER TABLE KhachHang  ADD CONSTRAINT UQ_SDT             UNIQUE (SDT);
ALTER TABLE Ghe        ADD CONSTRAINT UQ_Ghe_ViTri       UNIQUE (MaKV, Hang, Cot);
ALTER TABLE ChiTietVe  ADD CONSTRAINT UQ_ChiTietVe_DonGhe UNIQUE (MaDon, MaGhe);
ALTER TABLE ChiTietVe  ADD CONSTRAINT UQ_MaVeCode        UNIQUE (MaVeCode);
```

### 4.4. Quy ước giá trị TrangThai

| Bảng | Giá trị | Ý nghĩa |
|------|---------|---------|
| **Ghe** | `Trong` | Chưa có ai đặt |
| | `DangGiu` | Đang được chọn trên giao diện (chưa thanh toán) |
| | `DaBan` | Đã thanh toán thành công |
| **DonVe** | `DaThanhToan` | Đơn vé hợp lệ |
| | `DaHuy` | Đơn đã bị hủy → Ghế trở về `Trong` |
| | `DaDoi` | Đơn gốc đã đổi → Tạo đơn mới thay thế |
| **SuKien** | `SapDien` / `DangDien` / `DaKetThuc` / `DaHuy` | |
| **SuatDien** | `MoCuaBanVe` / `DongBanVe` / `DangDien` / `DaKetThuc` | |

---

## 5. Danh sách chức năng đầy đủ

### 5.1. Đối chiếu đề bài → Form → Người phụ trách

| # | Chức năng (theo đề bài) | Form/Control | Người |
|---|------------------------|-------------|-------|
| 1 | Đăng nhập + Phân quyền | `FrmDangNhap` + `FrmMain` (MDI) | TV1 |
| 2 | Quản lý sự kiện | `FrmQuanLySuKien` | TV2 |
| 3 | Quản lý suất diễn | `FrmQuanLySuatDien` | TV2 |
| 4 | Quản lý khu vực + Sinh ghế | `FrmQuanLyKhuVuc` ← **BỔ SUNG** | TV2 |
| 5 | Hiển thị sơ đồ ghế | `UcSoDoGhe` (UserControl) ← **BỔ SUNG** | TV1 |
| 6 | Chọn ghế + Lập đơn + Thanh toán | `FrmBanVe` (tích hợp UcSoDoGhe) | TV3 |
| 7 | **Hủy hoặc đổi vé** | `FrmQuanLyDonVe` ← **BỔ SUNG** | TV3 |
| 8 | Quản lý khách hàng | `FrmKhachHang` | TV3 |
| 9 | In vé cho khách | `FrmInVe` (GDI+ nếu mở rộng) | TV1 |
| 10 | **In danh sách khách tham dự** | Tích hợp trong `FrmKhachHang` | TV3 |
| 11 | Thống kê + Tỷ lệ lấp đầy | `FrmBaoCao` | TV2 |

### 5.2. Luồng nghiệp vụ chính: Bán vé

```
Nhân viên đăng nhập
  → Mở FrmBanVe
    → Chọn Sự kiện → Chọn Suất diễn
      → UcSoDoGhe hiển thị sơ đồ ghế (Trắng = Trống, Đỏ = Đã bán)
        → Click chọn ghế (Trắng → Vàng = Đang chọn)
          → Nhập/Tìm thông tin khách hàng
            → Xác nhận thanh toán
              → ┌─ TRANSACTION BẮT ĐẦU ─────────────┐
                │  INSERT DonVe                       │
                │  INSERT ChiTietVe (từng ghế)        │
                │  UPDATE Ghe.TrangThai → 'DaBan'     │
                │  INSERT ThanhToan                   │
                └─ TRANSACTION COMMIT ───────────────┘
                  → In vé (FrmInVe)
```

### 5.3. Luồng nghiệp vụ: Hủy vé

```
Nhân viên mở FrmQuanLyDonVe
  → Tìm đơn vé theo MaDon hoặc SDT khách hàng
    → Kiểm tra quy tắc nghiệp vụ:
        ✓ Suất diễn chưa diễn ra
        ✓ Thời gian hủy trước suất diễn ít nhất X giờ (nhóm tự quy định)
    → ┌─ TRANSACTION BẮT ĐẦU ─────────────────┐
      │  UPDATE DonVe.TrangThai → 'DaHuy'       │
      │  UPDATE ChiTietVe.TrangThai → 'DaHuy'   │
      │  UPDATE Ghe.TrangThai → 'Trong'          │
      └─ TRANSACTION COMMIT ───────────────────┘
```

---

## 6. Phân công công việc chi tiết

### 6.1. Bảng phân công tổng hợp

| | **TV1** | **TV2** | **TV3** |
|---|---------|------------------------|---------|
| **DTO** | `TaiKhoan`, `Ghe`, `NhatKy` | `SuKien`, `SuatDien`, `KhuVuc` | `KhachHang`, `DonVe`, `ChiTietVe`, `ThanhToan` |
| **DAL** | `TaiKhoanDAL`, `GheDAL` *(có overload Transaction)*, `NhatKyDAL` | `SuKienDAL`, `SuatDienDAL`, `KhuVucDAL` | `Database.cs`, `KhachHangDAL`, `DonVeDAL`, `ChiTietVeDAL`, `ThanhToanDAL` |
| **BLL** | `TaiKhoanBLL`, `GheBLL`, `NhatKyBLL` | `SuKienBLL`, `SuatDienBLL`, `KhuVucBLL` *(sinh ghế tự động)* | `KhachHangBLL`, `DonVeBLL` *(Transaction bán + hủy/đổi)*, `ChiTietVeBLL`, `ThanhToanBLL` |
| **GUI** | `FrmDangNhap`, `FrmMain` *(MDI)*, `UcSoDoGhe`, `FrmInVe` | `FrmQuanLySuKien`, `FrmQuanLySuatDien`, `FrmQuanLyKhuVuc`, `FrmBaoCao` | `FrmKhachHang` *(+ In DS)*, `FrmBanVe` *(tích hợp UcSoDoGhe)*, `FrmQuanLyDonVe` |
| **Tài liệu** | `README.md`, Biên tập chính Báo cáo | Script SQL (`Scripts/`), Review & Merge PR | `Resources/`, Kiểm thử + Test case, **Slide thuyết trình** |
| **Số Form** | 4 | 4 | 3 |

### 6.2. Nhánh Git quy ước

| Thành viên | Nhánh Git |
|------------|-----------|
| **TV1** | `feature/auth-main`, `feature/seat-map`, `feature/print-ticket-gdi`, `docs/readme-report` |
| **TV2** | `feature/event-showtime`, `feature/zone-generator`, `feature/report-statistics`, `fix/database-scripts` |
| **TV3** | `feature/project-init`, `feature/customer-attendees`, `feature/ticket-booking`, `feature/ticket-cancel-exchange`, `fix/validation-testing` |

---

## 7. Hợp đồng kỹ thuật giữa các thành viên

> ⚠️ **Mục này là thỏa thuận bắt buộc** — phải thống nhất signature các hàm dùng chung **TRƯỚC** khi ai viết code nấy.

### 7.1. `Database.cs` — TV3 chịu trách nhiệm viết

```csharp
public class Database
{
    // === Mở kết nối mới ===
    public static SqlConnection GetConnection();

    // === Query KHÔNG Transaction (đọc dữ liệu thông thường) ===
    public static DataTable ExecuteQuery(string query, object[] parameters = null);
    public static int ExecuteNonQuery(string query, object[] parameters = null);
    public static object ExecuteScalar(string query, object[] parameters = null);

    // === Query CÓ Transaction (ghi dữ liệu liên bảng) ===
    // TV1, TV2, TV3 đều gọi được, dùng chung 1 connection + 1 transaction
    public static int ExecuteNonQuery(string query, SqlConnection conn,
                                       SqlTransaction tran, object[] parameters = null);
    public static object ExecuteScalar(string query, SqlConnection conn,
                                        SqlTransaction tran, object[] parameters = null);
}
```

**Tại sao cần overload Transaction?**  
Khi bán vé, phải `INSERT DonVe` + `INSERT ChiTietVe` + `UPDATE Ghe.TrangThai` + `INSERT ThanhToan` trong **CÙNG 1 transaction**. Mà `GheDAL` (TV1) và `DonVeDAL` (TV3) là 2 class khác nhau → cần truyền chung `SqlConnection` + `SqlTransaction` qua tham số.

### 7.2. `GheDAL.CapNhatTrangThai()` — TV1 viết, TV3 gọi

```csharp
// TV1 BẮT BUỘC phải viết cả 2 phiên bản:

// Phiên bản 1: Cập nhật đơn lẻ (VD: giữ ghế tạm)
public static bool CapNhatTrangThai(int maGhe, string trangThai);

// Phiên bản 2: Trong Transaction bán vé (TV3 sẽ gọi)
public static bool CapNhatTrangThai(int maGhe, string trangThai,
                                     SqlConnection conn, SqlTransaction tran);
```

### 7.3. `UcSoDoGhe` — TV1 xây dựng, TV3 nhúng vào `FrmBanVe`

```csharp
public partial class UcSoDoGhe : UserControl
{
    // --- EVENT: TV3 bắt sự kiện này trong FrmBanVe ---
    public event EventHandler<List<Ghe>> OnSeatSelectionChanged;

    // --- PHƯƠNG THỨC PUBLIC ---
    public void LoadSoDoGhe(int maSuat);       // Nạp sơ đồ theo suất diễn
    public List<Ghe> GetSelectedSeats();        // Lấy DS ghế đang chọn
    public void ResetSelection();               // Reset sau khi bán vé xong
}
```

TV3 tích hợp vào `FrmBanVe`:
```csharp
// FrmBanVe.cs
private void ucSoDoGhe1_OnSeatSelectionChanged(object sender, List<Ghe> dsGheChon)
{
    decimal tongTien = dsGheChon.Sum(g => g.GiaVe);
    lblTongTien.Text = tongTien.ToString("N0") + " VNĐ";
    lblSoGheDaChon.Text = dsGheChon.Count.ToString();
}
```

### 7.4. `KhuVucBLL.ThemKhuVuc()` — TV2 viết, tự động sinh Ghế

```csharp
public static bool ThemKhuVuc(KhuVuc kv)
{
    using (SqlConnection conn = Database.GetConnection())
    {
        conn.Open();
        SqlTransaction tran = conn.BeginTransaction();
        try
        {
            // 1. Insert KhuVuc → lấy MaKV vừa tạo
            int maKV = KhuVucDAL.ThemKhuVuc(kv, conn, tran);

            // 2. Sinh ghế theo SoHang × SoCot
            for (int h = 0; h < kv.SoHang; h++)
            {
                string hang = ((char)('A' + h)).ToString(); // A, B, C...
                for (int c = 1; c <= kv.SoCot; c++)
                {
                    GheDAL.ThemGhe(new Ghe
                    {
                        MaKV = maKV, Hang = hang,
                        Cot = c, TrangThai = "Trong"
                    }, conn, tran);
                }
            }

            tran.Commit();
            return true;
        }
        catch { tran.Rollback(); return false; }
    }
}
```

> **Lưu ý cho TV1:** `GheDAL.ThemGhe()` cũng cần overload nhận `SqlConnection` + `SqlTransaction` để TV2 gọi được.

### 7.5. `FrmMain` MDI — TV1 viết, cả nhóm tuân thủ

```csharp
// FrmMain.cs — MDI Parent
public void MoFormCon<T>() where T : Form, new()
{
    // Kiểm tra Form đã mở chưa (tránh mở trùng)
    foreach (Form f in this.MdiChildren)
    {
        if (f is T) { f.Activate(); return; }
    }
    T frm = new T();
    frm.MdiParent = this;
    frm.Show();
}

// TV2, TV3: KHÔNG tự new Form rồi Show()
// TV1 sẽ gắn sự kiện menu gọi MoFormCon<FrmQuanLySuKien>() v.v.
```

### 7.6. Xử lý `App.config` — Tránh merge conflict

Mỗi thành viên có tên SQL Server Instance khác nhau.

**Quy ước:**
1. Commit `App.config` lần đầu với giá trị chuẩn:
   ```xml
   <connectionStrings>
     <add name="QuanLySuKienDB"
          connectionString="Data Source=.\SQLEXPRESS;Initial Catalog=QuanLySuKienVaBanVe;
                            Integrated Security=True"
          providerName="System.Data.SqlClient" />
   </connectionStrings>
   ```
2. Mỗi người tự sửa `App.config` cục bộ cho khớp SQL Instance của mình.
3. Khi commit, **bỏ `App.config` ra khỏi staging:**
   ```bash
   git add .
   git reset HEAD QuanLySuKienVaBanVe/App.config
   ```

### 7.7. Xử lý đồng thời — Chống bán trùng ghế

```sql
-- Trong Stored Procedure hoặc DAL, dùng UPDLOCK để khóa ghế khi kiểm tra:
BEGIN TRAN
  SELECT TrangThai FROM Ghe WITH (UPDLOCK, ROWLOCK) WHERE MaGhe = @MaGhe
  -- Nếu TrangThai = 'Trong' mới cho UPDATE
  UPDATE Ghe SET TrangThai = 'DaBan' WHERE MaGhe = @MaGhe AND TrangThai = 'Trong'
COMMIT
```

---

## 8. Quy trình Git bắt buộc

### 8.1. Khẩu hiệu vàng

> *"Trước khi code: `git pull`. Sau khi code chạy được: `git commit`. Khi muốn nộp lên GitHub: `git push`."*

### 8.2. Vòng lặp phát triển chuẩn

```bash
# 1. Luôn bắt đầu từ main và cập nhật mới nhất
git checkout main
git pull

# 2. Tạo nhánh riêng
git checkout -b feature/<ten-tinh-nang>

# 3. Code + Test trên máy cá nhân

# 4. Đưa vào staging và commit
git status
git add .
git commit -m "Add seat booking form with transaction"

# 5. Đẩy nhánh lên GitHub
git push -u origin feature/<ten-tinh-nang>

# 6. Lên GitHub → Mở Pull Request → Chờ TV2 review → Merge
```

### 8.3. Quy tắc đặt tên

| Loại | Quy tắc | Ví dụ |
|------|---------|-------|
| Nhánh tính năng | `feature/<ten>` | `feature/login`, `feature/seat-map` |
| Nhánh sửa lỗi | `fix/<ten>` | `fix/database-connection` |
| Nhánh tài liệu | `docs/<ten>` | `docs/readme`, `docs/report` |
| Commit Message | `[Hành động] + [Đối tượng]` | `Add login form`, `Fix seat transaction` |
| **CẤM** | ❌ Commit vô nghĩa | ~~`update`~~, ~~`sua bai`~~, ~~`fix bug`~~, ~~`final`~~ |

### 8.4. Quy tắc chống Merge Conflict (WinForms)

| Quy tắc | Chi tiết |
|---------|---------|
| Phân chia theo Form | Mỗi người chỉ sửa Form/Control của mình |
| File nhạy cảm | Không sửa chung `*.Designer.cs`, `*.resx`, `App.config`, `Program.cs` |
| `Program.cs` + `FrmMain.cs` | Chỉ **TV1** được sửa — gắn kết menu sau khi TV2, TV3 merge Form con |
| Thay đổi SQL | Phải báo **TV2** cập nhật `Scripts/` và thông báo cả nhóm chạy lại script |
| Kéo thả giao diện | Không ai được kéo thả control trên Form của người khác |

### 8.5. Yêu cầu Pull Request

Mỗi PR mở trên GitHub **bắt buộc** phải có:
1. ✅ Mô tả chi tiết tính năng đã làm
2. ✅ Hướng dẫn các bước kiểm tra (test flow)
3. ✅ Ảnh chụp màn hình nếu có thay đổi giao diện
4. ✅ Cảnh báo nếu có thay đổi cấu trúc bảng hoặc script SQL

---

## 9. Giai đoạn 0 — Khởi tạo nền tảng

> **Cả nhóm phải hoàn thành Giai đoạn 0 TRƯỚC khi tách nhánh làm tính năng riêng.**

### Ngày 1: TV2 + TV3 làm song song

| Người | Công việc | Nhánh | Output |
|-------|-----------|-------|--------|
| **TV2** | Viết `01_TaoBang.sql` (10 bảng + FK + UNIQUE) + `02_DuLieuMau.sql` (dữ liệu mẫu + 2 tài khoản demo) | Push `main` (chỉ lần này) | File SQL chạy được trên SSMS |
| **TV3** | Tạo Solution `.sln`, cấu trúc thư mục `DTO/DAL/BLL/GUI`, viết `Database.cs` + `App.config` | `feature/project-init` → PR | Solution mở được bằng VS |

### Ngày 2: Merge + Kiểm tra chéo

| Người | Công việc |
|-------|-----------|
| **TV2** | Review PR `feature/project-init` → Merge vào `main` |
| **Cả nhóm** | Pull `main` → Mở Solution → Chạy script SQL → Kiểm tra kết nối DB |
| **TV1** | Bắt đầu `feature/auth-main` (FrmDangNhap + FrmMain MDI) |
| **TV2** | Bắt đầu `feature/event-showtime` (FrmQuanLySuKien + FrmQuanLySuatDien) |
| **TV3** | Bắt đầu `feature/customer-attendees` (FrmKhachHang) |

### Thứ tự ưu tiên phát triển

```
Ưu tiên 1 (Nền tảng):    FrmDangNhap → FrmMain (MDI) → Database script
    ↓
Ưu tiên 2 (Danh mục):    FrmQuanLySuKien → FrmQuanLySuatDien → FrmQuanLyKhuVuc → FrmKhachHang
    ↓
Ưu tiên 3 (Nghiệp vụ):   UcSoDoGhe → FrmBanVe (Transaction) → FrmQuanLyDonVe (Hủy/Đổi)
    ↓
Ưu tiên 4 (Báo cáo):     FrmBaoCao → FrmInVe (GDI+)
    ↓
Ưu tiên 5 (Bàn giao):    README.md → Báo cáo LaTeX → Slide → Screenshots
```

---

## 10. Checklist nghiệm thu & bàn giao

### 10.1. Thành phần bàn giao (theo đề bài)

| # | Thành phần | Người phụ trách | ☐ |
|---|-----------|----------------|---|
| 1 | Mã nguồn C# WinForms đầy đủ (nén `.zip`) | TV2 (nén) | ☐ |
| 2 | Script SQL tạo bảng + dữ liệu mẫu | TV2 | ☐ |
| 3 | Danh sách tài khoản demo (Admin + NV) | TV2 (SQL) + TV1 (README) | ☐ |
| 4 | Hướng dẫn cài đặt + ConnectionString | TV1 (README.md) | ☐ |
| 5 | Báo cáo đồ án + bảng % công việc | TV1 (biên tập chính) | ☐ |
| 6 | GitHub có lịch sử commit rõ ràng | Cả nhóm | ☐ |
| 7 | README.md theo mẫu 7 mục của thầy | TV1 | ☐ |
| 8 | **Slide thuyết trình** | **TV3** | ☐ |

### 10.2. Tiêu chí GitHub (theo quy_git.md)

| # | Tiêu chí | ☐ |
|---|----------|---|
| 1 | Lịch sử commit trải dài, thể hiện đóng góp từng người | ☐ |
| 2 | Nhánh `main` build thành công, không lỗi biên dịch | ☐ |
| 3 | `.gitignore` đầy đủ (không tồn tại `bin/`, `obj/`, `.vs/`) | ☐ |
| 4 | `README.md` có hướng dẫn DB + tài khoản mẫu | ☐ |
| 5 | Script SQL có ràng buộc toàn vẹn + dữ liệu chạy thử | ☐ |

### 10.3. Chức năng chính (theo đề bài)

| # | Chức năng | Form | ☐ |
|---|----------|------|---|
| 1 | Quản lý sự kiện + suất tổ chức | FrmQuanLySuKien + FrmQuanLySuatDien | ☐ |
| 2 | Hiển thị sơ đồ ghế trực quan | UcSoDoGhe | ☐ |
| 3 | Chọn ghế + lập đơn vé + thanh toán | FrmBanVe | ☐ |
| 4 | Hủy hoặc đổi vé | FrmQuanLyDonVe | ☐ |
| 5 | In vé cho khách | FrmInVe | ☐ |
| 6 | In danh sách khách tham dự | FrmKhachHang | ☐ |

### 10.4. Test case quan trọng (đề bài bước 8)

| # | Kịch bản | Kết quả mong đợi | ☐ |
|---|---------|-------------------|---|
| 1 | Chọn trùng ghế (2 đơn cùng 1 ghế) | Chỉ 1 đơn thành công | ☐ |
| 2 | Hủy vé khi suất diễn đã qua | Từ chối hủy | ☐ |
| 3 | Mất kết nối giữa chừng bán vé | Transaction rollback | ☐ |
| 4 | Đăng nhập sai mật khẩu nhiều lần | Cảnh báo / khóa tạm | ☐ |
| 5 | Sự kiện đã kết thúc → mở bán vé | Từ chối | ☐ |
| 6 | Thêm KhuVuc (3 hàng × 5 cột) | Tự sinh 15 ghế (A1→C5) | ☐ |

---

## PHỤ LỤC: Mẫu README.md theo chuẩn thầy

```markdown
# QUẢN LÝ SỰ KIỆN VÀ BÁN VÉ

## 1. Thông tin thành viên nhóm
| STT | MSSV | Họ và tên | Vai trò / Phân công | Tỷ lệ (%) |
|:---:|:----:|:----------|:--------------------|:----------:|
|  1  | ...  | ...       | Nhóm trưởng / ...   |    ...%    |
|  2  | ...  | ...       | Thành viên / ...    |    ...%    |
|  3  | ...  | ...       | Thành viên / ...    |    ...%    |

## 2. Mô tả đề tài
Ứng dụng desktop quản lý toàn bộ quy trình tổ chức sự kiện: từ khai báo sự kiện,
thiết lập suất diễn, phân bổ khu vực ghế ngồi, đến bán vé, in vé và thống kê doanh thu.

## 3. Công nghệ và Kiến trúc
- Ngôn ngữ: C# (.NET WinForms)
- Cơ sở dữ liệu: Microsoft SQL Server
- Truy xuất dữ liệu: ADO.NET (DataTable, SqlCommand, SqlTransaction)
- Kiến trúc: 3 lớp (GUI - BLL - DAL) kết hợp DTO

## 4. Các chức năng chính
- Phân hệ Quản trị & Xác thực (Đăng nhập, phân quyền)
- Phân hệ Danh mục (CRUD Sự kiện, Suất diễn, Khu vực, Khách hàng)
- Phân hệ Nghiệp vụ chính (Bán vé với Transaction, Sơ đồ ghế trực quan, Hủy/Đổi vé)
- Phân hệ Báo cáo & In ấn (Thống kê, In vé GDI+, In danh sách khách tham dự)

## 5. Hướng dẫn cài đặt & Phục hồi Database
1. Mở SQL Server Management Studio (SSMS).
2. Mở và thực thi file: `Scripts/01_TaoBang.sql`
3. Mở và thực thi file: `Scripts/02_DuLieuMau.sql`
4. Mở `QuanLySuKienVaBanVe/App.config`, sửa `connectionString` cho khớp SQL Instance.
5. Mở `QuanLySuKienVaBanVe.sln` bằng Visual Studio → Clean & Rebuild → Start.

## 6. Tài khoản Demo
| Tài khoản | Mật khẩu | Vai trò |
|:----------|:---------|:--------|
| admin     | 123456   | Quản lý sự kiện |
| staff     | 123456   | Nhân viên bán vé |

## 7. Hình ảnh minh họa giao diện
### Màn hình đăng nhập
![Login](docs/screenshots/login.png)

### Sơ đồ ghế trực quan
![SeatMap](docs/screenshots/seat-map.png)

### Bán vé và thanh toán
![Booking](docs/screenshots/booking.png)
```

---

> **Tài liệu này được lập bởi Nhóm 3 — Đề tài 20**  
> **Cập nhật lần cuối:** 18/10/2026
