# Ứng dụng Quản Lý Bán Cà Phê (QuanLyBanCafe)

## Giới thiệu
Đây là **ứng dụng quản lý bán cà phê** được xây dựng bằng **C# (WinForms)**, phục vụ cho việc quản lý hoạt động bán hàng tại quán cà phê, bao gồm:

- Quản lý sản phẩm (đồ uống)
- Quản lý hóa đơn
- Quản lý chi tiết hóa đơn
- Tính toán tổng tiền
- Lưu trữ dữ liệu bằng **file nhị phân**
- Giao diện trực quan, dễ sử dụng

Ứng dụng phù hợp cho **đồ án môn học / học phần C#**.

---

## Công nghệ sử dụng
- **Ngôn ngữ:** C#
- **Framework:** .NET (WinForms)
- **IDE:** Visual Studio
- **Cơ sở dữ liệu:** **File nhị phân (Binary File)**

---

## Yêu cầu hệ thống
- Windows
- Visual Studio 2019 trở lên
- .NET Framework phù hợp với project
- Git

---

## Hướng dẫn cài đặt & chạy chương trình

### 1️. Clone source code
```bash
git clone https://github.com/khanhan0603/ungdungquanlybancafe.git
```
### 2. Mở project
- Mở file QuanLyBanCoffee.sln bằng Visual Studio
- Chờ Visual Studio load project
### 3. Dữ liệu lưu trữ
- Ứng dụng không sử dụng hệ quản trị CSDL
- Dữ liệu được lưu bằng file nhị phân (.dat, .bin, …)
- File dữ liệu sẽ được:
Tự động tạo khi chạy chương trình lần đầu
Lưu trong thư mục của ứng dụng (hoặc thư mục cấu hình sẵn)
Không cần cấu hình database
