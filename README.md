# QUAN-LY-CHI-TIEU
Ứng dụng Quản lý Chi tiêu Cá nhân
1. Giới thiệu
Ứng dụng Quản lý Chi tiêu Cá nhân được phát triển bằng ngôn ngữ lập trình C# trên nền tảng Console Application. Mục tiêu của chương trình là hỗ trợ người dùng ghi chép, quản lý và phân tích các khoản thu – chi hằng ngày một cách có hệ thống và khoa học. Bên cạnh ý nghĩa thực tiễn, dự án còn giúp nhóm sinh viên vận dụng tổng hợp các kiến thức về lập trình hướng đối tượng, quản lý tệp dữ liệu, xử lý thuật toán và thao tác với ngôn ngữ truy vấn LINQ trong môi trường C#.
Ứng dụng được thiết kế hướng đến sự đơn giản, trực quan và tiện lợi cho người dùng. Mỗi cá nhân có thể tạo tài khoản riêng, đăng nhập và quản lý toàn bộ thông tin chi tiêu của mình. Các giao dịch được lưu trữ độc lập theo từng người dùng nhằm bảo đảm tính bảo mật và riêng tư. Thông qua đó, người dùng có thể dễ dàng nắm bắt tình hình tài chính cá nhân, điều chỉnh thói quen chi tiêu và lập kế hoạch tiết kiệm hợp lý hơn.
2. Tính năng của chương trình
Ứng dụng Quản lý Chi tiêu Cá nhân được phát triển nhằm hỗ trợ người dùng ghi chép, quản lý và phân tích các khoản thu – chi một cách có hệ thống, đồng thời thể hiện khả năng vận dụng lập trình hướng đối tượng, thao tác tệp và thuật toán trong C#.
Đăng nhập / Đăng ký người dùng:
Mỗi người dùng có tài khoản riêng, với thông tin đăng nhập được lưu trong tệp users.txt. Dữ liệu của từng người được quản lý tách biệt nhằm đảm bảo tính bảo mật và độc lập.
Thêm, sửa, xoá giao dịch:
Cho phép người dùng tạo mới, chỉnh sửa hoặc xoá các giao dịch thu – chi. Mỗi giao dịch bao gồm ngày, loại, số tiền và ghi chú cụ thể, giúp theo dõi tài chính chi tiết và khoa học.
Tìm kiếm giao dịch:
Chương trình hỗ trợ nhiều phương thức tìm kiếm, bao gồm tìm theo ID bằng Binary Search, hoặc tìm theo loại, khoảng tiền và ngày giao dịch thông qua LINQ, giúp truy xuất dữ liệu nhanh và chính xác.
Sắp xếp dữ liệu:
Ứng dụng triển khai hai thuật toán sắp xếp cơ bản: Selection Sort để sắp xếp theo số tiền, và Quick Sort để sắp xếp theo ngày giao dịch. Việc áp dụng đồng thời hai thuật toán thể hiện khả năng kết hợp lý thuyết thuật toán với thực tiễn lập trình.
Thống kê chi tiêu:
Hệ thống tự động tính toán tổng thu, tổng chi, số dư hiện tại, đồng thời thống kê chi tiêu theo từng danh mục. Các thông tin này giúp người dùng đánh giá và điều chỉnh thói quen chi tiêu hợp lý.
Lưu trữ và truy xuất dữ liệu (Data Persistence):
Tất cả thông tin tài khoản và giao dịch được lưu dưới dạng tệp văn bản .txt. Khi người dùng thoát khỏi chương trình, hệ thống sẽ tự động cập nhật và lưu lại toàn bộ dữ liệu hiện có, bảo đảm tính liên tục và toàn vẹn thông tin giữa các lần sử dụng.
3. Cách chạy chương trình 
3.1. Yêu cầu hệ thống
-	.NET SDK 6.0 trở lên.
-	Hệ điều hành tương thích: Windows, macOS hoặc Linux.
-	Môi trường phát triển khuyến nghị: Visual Studio hoặc Visual Studio Code.
3.2. Các bước thực hiện
1.	Tải toàn bộ mã nguồn về máy (bao gồm Program.cs, users.txt, transactions.txt, và các tệp liên quan).
2.	Mở thư mục dự án bằng Visual Studio hoặc VS Code.
3.	Chạy chương trình bằng một trong hai cách:
o	Nhấn F5 trong Visual Studio.
o	Hoặc dùng lệnh dòng: dotnet run
4.	Giao diện console sẽ hiển thị menu chính, cho phép bạn đăng nhập hoặc đăng ký tài khoản mới.
4. Hướng dẫn sử dụng 
Khi khởi động chương trình, bạn sẽ thấy hai tùy chọn: Đăng nhập hoặc Đăng ký. Nếu bạn chưa có tài khoản, hãy chọn Đăng ký và nhập tên đăng nhập cùng mật khẩu mới. Nếu đã có tài khoản, chọn Đăng nhập và nhập thông tin đã đăng ký để truy cập dữ liệu cá nhân. Mỗi tài khoản quản lý dữ liệu riêng, đảm bảo thông tin của bạn được bảo mật.
Sau khi đăng nhập, bạn có thể sử dụng các chức năng chính sau:
-	Thêm giao dịch: Nhập ngày, loại giao dịch (thu nhập hoặc chi tiêu), danh mục, số tiền và ghi chú. Giao dịch sẽ được lưu tự động vào tài khoản của bạn.
-	Xem, sửa và xoá giao dịch: Xem danh sách giao dịch, sửa thông tin hoặc xoá những giao dịch không cần thiết. Thay đổi được lưu ngay lập tức.
-	Tìm kiếm giao dịch: Tìm nhanh theo ID, loại giao dịch, khoảng tiền hoặc khoảng ngày. Tính năng này giúp bạn tra cứu dữ liệu dễ dàng và nhanh chóng.
-	Sắp xếp giao dịch: Chọn “1” để sắp xếp theo số tiền hoặc “2” để sắp xếp theo ngày giao dịch. Bạn sẽ dễ dàng quan sát các giao dịch theo thứ tự mong muốn.
-	Thống kê chi tiêu: Xem tổng thu, tổng chi, số dư và chi tiết chi theo từng danh mục. Thống kê này giúp bạn đánh giá tình hình tài chính tổng thể.
-	Lưu và thoát: Khi thoát chương trình, toàn bộ dữ liệu của bạn sẽ được lưu tự động và có bản sao lưu để phòng trường hợp cần khôi phục.
Giao diện và các bước sử dụng được thiết kế trực quan, giúp bạn thao tác dễ dàng và quản lý tài chính cá nhân một cách hiệu quả.
5. Lời kết
Ứng dụng Quản lý Chi tiêu Cá nhân cung cấp một công cụ đơn giản và trực quan để theo dõi, quản lý và phân tích các khoản thu – chi hàng ngày. Người dùng có thể thêm, sửa, xoá, tìm kiếm, sắp xếp và thống kê giao dịch, từ đó dễ dàng nắm bắt tình hình tài chính, điều chỉnh chi tiêu và lập kế hoạch tiết kiệm hợp lý. Dữ liệu được lưu riêng cho từng tài khoản, đảm bảo an toàn và bảo mật, đồng thời chương trình cũng minh họa cách vận dụng lập trình hướng đối tượng, thao tác tệp dữ liệu và thuật toán cơ bản trong C#.
Trong tương lai, ứng dụng có thể được mở rộng với các tính năng như thống kê chi tiêu theo tháng hoặc quý, xuất dữ liệu ra định dạng .CSV hoặc .XLSX, và trực quan hóa chi tiêu bằng biểu đồ. Mục tiêu của dự án là giúp người dùng hình thành thói quen quản lý tài chính cá nhân một cách khoa học, đồng thời tạo cơ hội cho sinh viên rèn luyện kỹ năng lập trình và xử lý dữ liệu. 
Chúng tôi hy vọng ứng dụng sẽ trở thành công cụ hữu ích, giúp bạn quản lý chi tiêu hiệu quả và hình thành thói quen tài chính thông minh. 
