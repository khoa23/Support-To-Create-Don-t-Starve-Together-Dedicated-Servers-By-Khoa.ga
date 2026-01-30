
## Hướng dẫn tạo Server Dedicated của game Don't starve together bằng tool hỗ trợ của Khoa
(Yêu cầu hệ điều hành Windows và đã tải Don't Starve Together Dedicated Server trên Steam)
### 1. Tại Library -> TOOLS, tải về Don't Starve Together Dedicated Server.
Lưu ý: để cùng ổ đĩa với game Don't starve together
![DSTDedicatedServerSteam](https://user-images.githubusercontent.com/48979664/132850417-6d782686-4fd7-4f4d-902d-761b707dd11e.png)
### 2. Tạo World và bật Mods
- Vào game tạo 1 host. Chỉnh world, add caves, enabled mod, chọn chế độ... tất tần tật sau đó vào game rồi thoát ra. Nhớ chọn 'Save Type: Local Save' nhé.
![20220607195204_1](https://user-images.githubusercontent.com/48979664/172384055-deb92249-65b4-4992-ae6a-75b63e0dc7d5.jpg)
### 3. Lấy và chỉnh sửa Token
- Tại màn hình game ấn vào “Account” 
![DSTManHinhChinh](https://user-images.githubusercontent.com/48979664/132851064-e2a71ad9-fcb0-490f-a8d5-59c6e71ce84b.png)
- Tại “Games” ấn vào “Game Servers” 
![DSTKleiAccount](https://user-images.githubusercontent.com/48979664/132851150-6b22e16b-9974-4391-9296-94678175c010.png)
- Điền tên bất kì, sau đó ấn “ADD NEW SERVER” rồi copy token lại 
![DSTTenServer](https://user-images.githubusercontent.com/48979664/132851255-72352254-3fdb-4a59-990a-7c51ea751c61.png)
![DSTTokenServer](https://user-images.githubusercontent.com/48979664/132851282-82da9a73-0b20-4404-b8b4-b0f9bffc991b.png)
### 4. Tải tool về
- Các bạn tải **[tool này](https://github.com/khoa23/Support-To-Create-Don-t-Starve-Together-Dedicated-Servers-By-Khoa.ga/raw/main/Support%20to%20create%20DST%20dedicated%20server.zip)** về và chạy Support to create Don't Starve Together dedicated server.exe lên rồi nhập các thông số.
![ToolDedi](https://user-images.githubusercontent.com/48979664/132851588-594004d4-e41b-4fd0-a814-7faf88382b78.png)
(1)'Select steamapp' Mặc định sẽ chọn đường dẫn folder DST Dedicated Server bạn tải ở Steam về, nếu chưa đúng thì bạn chọn lại đường dẫn Don't Starve Together Dedicated Server nhé</br>
(2) Bạn chọn đúng thư mục chứa save game, nếu ko biết thì coi ảnh hướng ở dưới</br>
(3) Là token đã tạo ở bước 3</br>
- Nếu bạn không nhớ save game của mình ở cluster mấy thì vào phần host game và rê con chuột lại biểu tượng thư mục của host đó
![Tim Thu Muc Cluster](https://github.com/khoa23/Support-To-Create-Don-t-Starve-Together-Dedicated-Servers-By-Khoa.ga/assets/48979664/caf2e7dc-046b-4f8c-8288-727b0bdb572b)
![Tim Thu Muc Cluster 2](https://github.com/khoa23/Support-To-Create-Don-t-Starve-Together-Dedicated-Servers-By-Khoa.ga/assets/48979664/c3b7a27e-fede-466a-9c37-1819f33bc8ab)


- Nếu sửa thông tin server bên phải thì bạn nhớ nhấn nút Save nhé :3 
- Xong thì ấn Create, tool sẽ copy mod và tạo file .bat. Tới đây sẽ có file tên MyDedicatedServer ngoài Destop, khi cần mở server bạn chỉ cần chạy nó lên là được.
Chúc các bạn thành công :v

### Q&A
##### Lỗi không tìm thấy server
- Bạn hãy kiểm tra Don't Starve Together Dedicated Server ở Steam có update không nhé.
#### Thêm/bớt mod cho server thế nào?
- Bạn bật lại host đó trong game rồi thêm/bớt mod xong thì ấn Resume world như bình thường, rồi thoát ra thôi.
#### Cập nhật mod ở Dedicated server thế nào?
- Bạn update mod trong game rồi làm lại hết :))
#### Thêm admin vào host?
- [Bình luận của Akio](https://steamcommunity.com/app/322330/discussions/0/521643320346586412/)
- Tạo file adminlist.txt trong documents/klei/don't starve together/folder lưu game (Nếu biết folder lưu game ở đâu thì xem bước 4), rồi bạn bỏ ID của người muốn trao quyền admin vào (VD: KU_ad39dik), không phải steam id nhé. Lấy Klei User ID ở https://accounts.klei.com/account/info

### 📫 Liên hệ [facebook](https://www.facebook.com/www.khoa.ga) của mình nếu có thắc mắc.
Vì mình lười bắt lỗi nên sẽ có 'n' lỗi, mọi người cứ chụp gửi qua facebook để mình fix nhé 💞️
