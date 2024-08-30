# CharityHub
Hệ thống quản lý gây quỹ cho các chiến dịch từ thiện 

## Tổng quan
Là một hệ thống có giao diện thân thiện với người dùng, với các chức năng quản lý người dùng, chiến dịch từ thiện dành cho người quản lý và các chức năng quyên góp có liên kết với các ví điện tử phổ biến hiện nay dành cho người dùng

## Tính năng

- **Xác thực người dùng**: Xác thực và phân quyền với các vai trò Admin/User
  
- **Quản lý chiến dịch ( Dành cho admin )**: Cho phép thêm chiến dịch, chỉnh sửa thời gian bắt đầu, kết thúc cũng như các thông tin về tên, nhà đồng hành và số tiền mục tiêu 

- **Quản lý người dùng ( Dành cho admin )**: Xem được thông tin của các người dùng trên hệ thống, cấm người dùng nếu như đặt tên hiển thị không phù hợp 

- **Xem các thông tin về chiến dịch ( Toàn bộ người dùng )**: Xem tiêu đề, mô tả chiến dịch, danh sách các người quyên góp mới nhất / quyên góp nhiều nhất của từng chiến dịch

- **Quyên góp thông qua ví điện tử ( Người dùng )**: Người dùng có thể chọn các hình thức thanh toán khác nhau, quyên góp một cách nhanh chóng

- **Xem thông tin cá nhân ( Dành cho người dùng đã đăng nhập )**: Xem và sửa được tên hiển thị, số điện thoại và xen được lịch sử quyên góp của bản thân
  
## Các công nghệ sử dụng
* Backend: ASP.NET Core, Entity Framework Core
* Frontend: Angular
* Xác thực : JWT
* Database: SQL Server

## Các hình ảnh về trang web có thể tham khảo trong file báo cáo

## Hướng dẫn cài đặt: 
* Yêu cầu cần cài đặt 1 IDE hỗ trợ C# ( Khuyến khích Visual Studio 2022), SQL Server, SSMS, Angular(18), NodeJS
* Mở terminal, clone repository này: git clone https://github.com/vux-66a5/CharityHub.git
* Sửa connection string dựa trên máy tính của bạn trong 2 file CharityHub.Data/Data/CharityHubDbContext.cs (dòng 23) và CharityHub.WebAPI/appsettings.json (dòng 9)
* Chạy lệnh ```dotnet ef migrations add InitialCreate``` ( với .NET Core Cli) hoặc ```Add-Migration InitialCreate``` ( với Terminal thường ) - Tham khảo chi tiết tại https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=vs
* Mở Terminal trong /CharityHub.WebAPI, chạy ```dotnet run```, có thể truy cập giao diện Swagger thông qua https://localhost:7244/swagger/index.html
* Mở Terminal trong /FE, chạy ```npm install```
* Sau khi npm install, chạy ```ng serve -o``` và chờ 1 chút, localhost:4200 sẽ hiện lên giao diện của trang web

## Hướng dẫn sử dụng:
* Tài khoản mặc có sẵn của Admin là datdq@gmail.com/datdq@123. Sau khi đăng nhập có thể có các giao diện thân thiện với người dùng để thực hiện các tính năng quản lý cơ bản ( không thể quyên góp )
* Người dùng có thể chọn quyên góp ngay khi vào trang web, hoặc cũng có thể đăng kí rồi đăng nhập.
* Nếu đăng nhập thì người dùng có thể vào các tab sửa thông tin cũng như xem lịch sử
