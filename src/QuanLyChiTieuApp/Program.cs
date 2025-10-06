using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace QuanLyChiTieu
{
    // ==== ENUM DANH MỤC ====
    public enum Category
    {
        AnUong = 1,
        DiChuyen,
        HocTap,
        GiaiTri,
        SucKhoe,
        Khac
    }

    // ==== GIAO DỊCH ====
    public class Transaction
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public bool IsIncome { get; set; } // true = thu, false = chi
        public Category Category { get; set; }
        public decimal Amount { get; set; }
        public string Note { get; set; } = "";
    }

    // ==== NGƯỜI DÙNG ====
    public struct User
    {
        public string Username;
        public string Password;

        public User(string u, string p)
        {
            Username = u ?? throw new ArgumentNullException(nameof(u)); // Thêm kiểm tra null
            Password = p ?? throw new ArgumentNullException(nameof(p));
        }
    }

    // ==== DỊCH VỤ LƯU FILE GIAO DỊCH ====
    public static class FileService
    {
        // Chỗ này thay đổi code để lưu dữ liệu giao dịch thành từng file riêng/người thay vì lưu chung vào file transactions.txt
        private static readonly string baseDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "QuanLyChiTieu");
        private static readonly string logFile = Path.Combine(baseDataFolder, "error.log");

        private static string GetTransactionFilePath(string username)
        {
            string userFolder = Path.Combine(baseDataFolder, username);
            Directory.CreateDirectory(userFolder); // Tạo thư mục riêng cho user nếu chưa có
            return Path.Combine(userFolder, "transactions.txt");
        }

        //đoạn dưới đây sẽ sửa phần try-catch để chi tiết hơn 
        public static void Save(List<Transaction> list, string username)
        {
            try
            {
                string filePath = GetTransactionFilePath(username);
                if (File.Exists(filePath))
                {
                    string backupPath = $"{filePath}.{DateTime.Now:yyyyMMddHHmmss}.bak";
                    File.Copy(filePath, backupPath, overwrite: true); // Tạo file backup khi ghi đè
                }
                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    foreach (var t in list)
                    {
                        sw.WriteLine($"{t.Id}|{t.Date:yyyy-MM-dd}|{t.IsIncome}|{t.Category}|{t.Amount}|{t.Note}");
                    }
                }
            }
            catch (IOException ex)
            {
                LogError($"Lỗi I/O khi lưu file cho {username}: {ex.Message}");
            }
            catch (UnauthorizedAccessException ex)
            {
                LogError($"Không có quyền truy cập để lưu file cho {username}: {ex.Message}");
            }
            catch (Exception ex)
            {
                LogError($"Lỗi không xác định khi lưu file cho {username}: {ex.Message}");
            }
        }


        public static List<Transaction> Load(string username)
        {
            try
            {
                string filePath = GetTransactionFilePath(username);
                var list = new List<Transaction>();
                if (!File.Exists(filePath)) return list;

                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    var parts = line.Split('|');
                    if (parts.Length == 6)
                    {
                        list.Add(new Transaction
                        {
                            Id = int.Parse(parts[0]),
                            Date = DateTime.ParseExact(parts[1], "yyyy-MM-dd", CultureInfo.InvariantCulture),
                            IsIncome = bool.Parse(parts[2]),
                            Category = Enum.Parse<Category>(parts[3]),
                            Amount = decimal.Parse(parts[4]),
                            Note = parts[5]
                        });
                    }
                }
                return list;
            }
            catch (IOException ex)
            {
                LogError($"Lỗi I/O khi tải file cho {username}: {ex.Message}");
                return new List<Transaction>();
            }
            catch (FormatException ex)
            {
                LogError($"Lỗi định dạng dữ liệu trong file cho {username}: {ex.Message}");
                return new List<Transaction>();
            }
            catch (Exception ex)
            {
                LogError($"Lỗi không xác định khi tải file cho {username}: {ex.Message}");
                return new List<Transaction>();
            }
        }

        // Thêm phương thức log lỗi ra file
        private static void LogError(string message)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(logFile));
                File.AppendAllText(logFile, $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}{Environment.NewLine}");
            }
            catch (Exception)
            {
                // Nếu log lỗi thất bại, chỉ in ra console để tránh vòng lặp vô hạn
                Console.WriteLine($"[Lỗi log] {message}");
            }
        }
    }
    // ==== DỊCH VỤ LƯU FILE NGƯỜI DÙNG ====
        public static class UserService
        {
            private static readonly string userFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "QuanLyChiTieu", "users.txt"); //bổ sung đường dẫn

            public static void Save(List<User> users)
            {
                try
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(userFile)); //Tạo thư mục nếu chưa có
                    using (StreamWriter sw = new StreamWriter(userFile))
                    {
                        foreach (var u in users)
                        {
                            // Lưu dạng: username|password
                            sw.WriteLine($"{u.Username}|{u.Password}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Lỗi khi lưu file users: {ex.Message}"); // Thêm xử lý ngoại lệ~~
                }
            }

            public static List<User> Load()
            {
                var list = new List<User>();
                try
                {
                    if (!File.Exists(userFile)) return list;

                    string[] lines = File.ReadAllLines(userFile);
                    foreach (string line in lines)
                    {
                        var parts = line.Split('|');
                        if (parts.Length == 2)
                        {
                            list.Add(new User(parts[0], parts[1]));
                        }
                    }
                    return list;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Lỗi khi tải file users: {ex.Message}");
                    return list;
                }
            }
        }

    // ==== THỐNG KÊ ====
    public static class Statistics
    {
        public static void PrintSummary(List<Transaction> list)
        {
            if (!list.Any())
            {
                Console.WriteLine("Chưa có dữ liệu.");
                return;
            }

            decimal income = list.Where(t => t.IsIncome).Sum(t => t.Amount);
            decimal expense = list.Where(t => !t.IsIncome).Sum(t => t.Amount);
            Console.WriteLine($"Tổng thu: {income:N0}");
            Console.WriteLine($"Tổng chi: {expense:N0}");
            Console.WriteLine($"Số dư: {(income - expense):N0}");
        }

        public static void PrintByCategory(List<Transaction> list)
        {
            var groups = list.Where(t => !t.IsIncome)
                             .GroupBy(t => t.Category)
                             .Select(g => new { Cat = g.Key, Total = g.Sum(x => x.Amount) });

            Console.WriteLine("\nChi tiêu theo loại:");
            foreach (var g in groups)
                Console.WriteLine($"{g.Cat}: {g.Total:N0}");
        }
    }

    // ==== SẮP XẾP ====
    public static class Sorting
    {
        // Selection sort theo số tiền
        public static void SelectionSortByAmount(List<Transaction> list, bool ascending = true)
        {
            for (int i = 0; i < list.Count - 1; i++)
            {
                int minIdx = i;
                for (int j = i + 1; j < list.Count; j++)
                {
                    if (ascending ? list[j].Amount < list[minIdx].Amount : list[j].Amount > list[minIdx].Amount)
                        minIdx = j;
                }
                (list[i], list[minIdx]) = (list[minIdx], list[i]);
            }
        }
        

        //Thêm phương thức Bỉnary search theo ID
        public static int BinarySearchById(List<Transaction> list, int id)
        {
            int left = 0, right = list.Count - 1;
            while (left <= right)
            {
                int mid = left + (right - left) / 2;
                if (list[mid].Id == id) return mid;
                if (list[mid].Id < id) left = mid + 1;
                else right = mid - 1;
            }
            return -1; // Không tìm thấy
        }
        
        // QuickSort theo ngày
        public static void QuickSortByDate(List<Transaction> list, int left, int right, bool ascending = true)
        {
            int i = left, j = right;
            DateTime pivot = list[(left + right) / 2].Date;

            while (i <= j)
            {
                if (ascending)
                {
                    while (list[i].Date < pivot) i++;
                    while (list[j].Date > pivot) j--;
                }
                else
                {
                    while (list[i].Date > pivot) i++;
                    while (list[j].Date < pivot) j--;
                }

                if (i <= j)
                {
                    (list[i], list[j]) = (list[j], list[i]);
                    i++; j--;
                }
            }
            if (left < j) QuickSortByDate(list, left, j, ascending);
            if (i < right) QuickSortByDate(list, i, right, ascending);
        }
    }

    // ==== CHƯƠNG TRÌNH CHÍNH ====
    class Program
    {
        static List<Transaction> transactions = new List<Transaction>();
        static int nextId = 1;
        static User currentUser;

        // Danh sách người dùng (lưu toàn bộ tài khoản)
        static List<User> users = new List<User>();

        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Load users
            users = UserService.Load();

            // Đăng nhập / Đăng ký
            DangNhap();

            //đoạn này đưa xuống thay vì ở trên chỗ console.outputEncoding để tải giao dịch sau khi đăng nhập xong, chứ để trên là sẽ load toàn bộ giao dịch
            transactions = FileService.Load(currentUser.Username);
            if (transactions.Any())
                nextId = transactions.Max(t => t.Id) + 1;
                
            // Menu chính
            int chon;
            do
            {
                Console.WriteLine("\n===== MENU CHÍNH =====");
                Console.WriteLine("1. Thêm giao dịch");
                Console.WriteLine("2. Xem / Sửa / Xoá giao dịch");
                Console.WriteLine("3. Tìm kiếm giao dịch");
                Console.WriteLine("4. Sắp xếp giao dịch");
                Console.WriteLine("5. Thống kê chi tiêu");
                Console.WriteLine("0. Thoát");
                Console.Write("Chọn: ");
                int.TryParse(Console.ReadLine(), out chon);

                switch (chon)
                {
                    case 1: ThemGiaoDich(); break;
                    case 2: QuanLyCRUD(); break;
                    case 3: TimKiem(); break;
                    case 4: SapXep(); break;
                    case 5: ThongKe(); break;
                    case 0:
                        UserService.Save(users); // lưu lại toàn bộ tài khoản trước khi thoát
                        FileService.Save(transactions, currentUser.Username);//đoạn này xóa cái fileservice ở trên và code lại dưới này để lưu cho user hiện tại
                        Console.WriteLine("Đã lưu và thoát!");
                        break;
                    default: Console.WriteLine("Sai lựa chọn!"); break;
                }
            } while (chon != 0);
        }

        // ==== ĐĂNG NHẬP / ĐĂNG KÝ ====
        static void DangNhap()
        {
            while (true)
            {
                Console.Write("Bạn đã có tài khoản chưa? (Y/N): ");
                string ans = Console.ReadLine()?.Trim().ToUpper();

                if (ans == "Y")
                {
                    Console.Write("Tên đăng nhập: ");
                    string u = Console.ReadLine();
                    Console.Write("Mật khẩu: ");
                    string p = Console.ReadLine();

                    if (string.IsNullOrEmpty(u) || string.IsNullOrEmpty(p))
                    {
                        Console.WriteLine("Tên đăng nhập hoặc mật khẩu không được để trống!");// Thêm kiểm tra nếu người dùng quên nhập tên hoặc pass
                        continue;
                    }

                    // Kiểm tra trong danh sách users đã load
                    var found = users.FirstOrDefault(x => x.Username == u && x.Password == p);
                    if (found.Username != null)
                    {
                        currentUser = found;
                        Console.WriteLine("Đăng nhập thành công!");
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Sai tài khoản hoặc mật khẩu!");
                    }
                }
                else if (ans == "N")
                {
                    Console.Write("Tạo tên đăng nhập: ");
                    string u = Console.ReadLine();
                    Console.Write("Tạo mật khẩu: ");
                    string p = Console.ReadLine();
                    currentUser = new User(u, p);

                    // Thêm vào danh sách users và lưu
                    users.Add(currentUser);
                    UserService.Save(users);

                    Console.WriteLine("Đăng ký thành công!");
                    return;
                }
                else
                {
                    Console.WriteLine("Chỉ nhập Y hoặc N!");
                }
            }
        }

        // ==== CRUD ====
        static void ThemGiaoDich()
        {
            Console.WriteLine("\n== Thêm giao dịch ==");
            Transaction t = new Transaction();
            t.Id = nextId++;

            // Nhập ngày bằng ReadDate
            t.Date = ReadDate("Ngày (dd-MM-yyyy): ");

            Console.Write("Là thu nhập? (y/n): ");
            t.IsIncome = Console.ReadLine()?.Trim().ToLower() == "y";

            Console.WriteLine("Chọn loại: 1=Ăn uống, 2=Di chuyển, 3=Học tập, 4=Giải trí, 5=Sức khoẻ, 6=Khác");
            int cat; int.TryParse(Console.ReadLine(), out cat);
            t.Category = (Category)cat;

            // Nhập số tiền bằng ReadMoney
            t.Amount = ReadMoney("Số tiền: ");

            Console.Write("Ghi chú: ");
            t.Note = Console.ReadLine();

            transactions.Add(t);

            // Lưu ngay sau khi thêm, **thay đổi: thêm currentuser để lưu cho người hiện tại
            FileService.Save(transactions, currentUser.Username);

            Console.WriteLine("Đã thêm!");
        }

        static void QuanLyCRUD()
        {
            Console.WriteLine("\n== Danh sách giao dịch ==");
            foreach (var t in transactions)
                Console.WriteLine($"{t.Id} | {t.Date:yyyy-MM-dd} | {(t.IsIncome ? "Thu" : "Chi")} | {t.Category} | {t.Amount:N0} | {t.Note}");

            Console.Write("Nhập ID để sửa/xoá (0 để quay lại): ");
            int id; int.TryParse(Console.ReadLine(), out id);
            var tran = transactions.FirstOrDefault(x => x.Id == id);
            if (tran.UsernameIsNull()) { /* helper to avoid warning */ } // <- NOOP (keeps dev's original logic untouched)

            // Note: because we use struct for User and class for Transaction,
            // retrieving by FirstOrDefault might return default(Transaction) if not found.
            if (tran == null || tran.Id == 0) return;

            Console.Write("Sửa (S) hay Xoá (X)? ");
            string op = Console.ReadLine()?.Trim().ToUpper();
            if (op == "S")
            {
                // Dùng ReadMoney để chuẩn hoá nhập tiền sửa
                decimal m = ReadMoney("Nhập số tiền mới: ");
                tran.Amount = m;

                // Vì tran là bản sao, cần cập nhật lại trong list
                int idx = transactions.FindIndex(x => x.Id == tran.Id);
                if (idx >= 0) transactions[idx] = tran;

                FileService.Save(transactions, currentUser.Username); // Lưu sau khi sửa,**thay đổi: tương tự ở trên
                Console.WriteLine("Đã sửa!");
            }
            else if (op == "X")
            {
                transactions.RemoveAll(x => x.Id == tran.Id);
                FileService.Save(transactions, currentUser.Username); // Lưu sau khi xoá,**thay đổi: tương tự ở trên
                Console.WriteLine("Đã xoá!");
            }
        }

        // ==== TÌM KIẾM ====
        static void TimKiem()
        {
            Console.WriteLine("\n1. Tìm theo ID");
            Console.WriteLine("2. Tìm theo loại");
            Console.WriteLine("3. Tìm theo khoảng tiền");
            Console.WriteLine("4. Tìm theo ngày");
            Console.Write("Chọn: ");
            int ch; int.TryParse(Console.ReadLine(), out ch);

            List<Transaction> kq = new List<Transaction>();
            switch (ch)
            {
                case 1:
                    Console.Write("ID: ");
                    int id; int.TryParse(Console.ReadLine(), out id); //Đoạn dưới này dùng Binary search để tìm theo ID người dùng
                    int index = Sorting.BinarySearchById(transactions, id);
                    if (index != -1) kq.Add(transactions[index]);
                    break;
                case 2:
                    Console.Write("Nhập loại (1..6): ");
                    int c; int.TryParse(Console.ReadLine(), out c);
                    kq = transactions.Where(t => t.Category == (Category)c).ToList();
                    break;
                case 3:
                    Console.Write("Từ: ");
                    decimal.TryParse(Console.ReadLine(), out decimal min);
                    Console.Write("Đến: ");
                    decimal.TryParse(Console.ReadLine(), out decimal max);
                    kq = transactions.Where(t => t.Amount >= min && t.Amount <= max).ToList();
                    break;
                case 4:
                    // Sử dụng ReadDate
                    DateTime d1 = ReadDate("Từ (dd-MM-yyyy): ");
                    DateTime d2 = ReadDate("Đến (dd-MM-yyyy): ");
                    kq = transactions.Where(t => t.Date >= d1 && t.Date <= d2).ToList();
                    break;
            }

            Console.WriteLine("\n== Kết quả ==");
            foreach (var t in kq)
                Console.WriteLine($"{t.Id} | {t.Date:yyyy-MM-dd} | {(t.IsIncome ? "Thu" : "Chi")} | {t.Category} | {t.Amount:N0} | {t.Note}");
        }

        // ==== SẮP XẾP ====
        static void SapXep()
        {
            Console.WriteLine("\n1. Sắp xếp theo số tiền (Selection Sort)");
            Console.WriteLine("2. Sắp xếp theo ngày (QuickSort)");
            Console.Write("Chọn: ");
            int ch; int.TryParse(Console.ReadLine(), out ch);

            if (ch == 1) Sorting.SelectionSortByAmount(transactions, true);
            else if (ch == 2) Sorting.QuickSortByDate(transactions, 0, transactions.Count - 1, true);

            Console.WriteLine("== Sau khi sắp xếp ==");
            foreach (var t in transactions)
                Console.WriteLine($"{t.Id} | {t.Date:yyyy-MM-dd} | {t.Amount:N0}");
        }

        // ==== THỐNG KÊ ====
        static void ThongKe()
        {
            Statistics.PrintSummary(transactions);
            Statistics.PrintByCategory(transactions);
        }

        // ==== HÀM TIỆN ÍCH ====
        // Nhập ngày dd-MM-yyyy
        static DateTime ReadDate(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                if (DateTime.TryParseExact(input, "dd-MM-yyyy",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out DateTime date))
                {
                    return date;
                }
                Console.WriteLine("❌ Sai định dạng! Vui lòng nhập theo dạng dd-MM-yyyy (VD: 14-09-2025).");
            }
        }

        // Nhập số tiền
        static decimal ReadMoney(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine().Replace(".", "").Replace("VND", "").Trim();

                if (decimal.TryParse(input, out decimal amount) && amount > 0)
                {
                    return amount;
                }
                Console.WriteLine("❌ Sai định dạng! Vui lòng nhập số tiền dương (VD: 30000 hoặc 30.000).");
            }
        }
    }

    // ==== Extension helper để tránh cảnh báo (không ảnh hưởng logic) ====
    public static class TransactionExtensions
    {
        // Hàm rỗng chỉ để tránh tham chiếu null struct khi dùng FirstOrDefault
        public static bool UsernameIsNull(this Transaction t)
        {
            return false;
        }
    }
}