using System;
using System.Collections.Generic;
using System.Globalization;

namespace QuanLyChiTieu
{
    class Transaction
    {
        public int Id { get; set; }
        public string Loai { get; set; }
        public double SoTien { get; set; }
        public string MoTa { get; set; }
        public DateTime Ngay { get; set; }
    }

    class Program
    {
        static List<Transaction> transactions = new List<Transaction>();
        static int nextId = 1;

        // Thêm giao dịch
        static void ThemGiaoDich()
        {
            Console.WriteLine("\n=== THEM GIAO DICH ===");

            Console.WriteLine("Chon loai giao dich:");
            Console.WriteLine("1. An uong");
            Console.WriteLine("2. Chi tieu hang ngay");
            Console.WriteLine("3. Di lai");
            Console.WriteLine("4. Hoc tap");
            Console.WriteLine("5. Giai tri");
            Console.WriteLine("6. Y te");
            int loaiChon;
            if (!int.TryParse(Console.ReadLine(), out loaiChon) || loaiChon < 1 || loaiChon > 6)
            {
                Console.WriteLine("Lua chon khong hop le!");
                return;
            }

            string[] loaiGD = { "An uong", "Chi tieu hang ngay", "Di lai", "Hoc tap", "Giai tri", "Y te" };

            Console.Write("Nhap so tien: ");
            double soTien;
            if (!double.TryParse(Console.ReadLine(), out soTien))
            {
                Console.WriteLine("So tien khong hop le!");
                return;
            }

            Console.Write("Nhap mo ta: ");
            string moTa = Console.ReadLine();

            Console.Write("Nhap ngay giao dich (dd/mm/yyyy): ");
            string ngayNhap = Console.ReadLine();
            DateTime ngay;
            if (!DateTime.TryParseExact(ngayNhap, "dd/mm/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out ngay))
            {
                Console.WriteLine("Ngay khong hop le!");
                return;
            }

            transactions.Add(new Transaction
            {
                Id = nextId++,
                Loai = loaiGD[loaiChon - 1],
                SoTien = soTien,
                MoTa = moTa,
                Ngay = ngay
            });

            Console.WriteLine("Them giao dich thanh cong!");
        }

        // Xem danh sách
        static void XemDanhSach()
        {
            Console.WriteLine("\n=== DANH SACH GIAO DICH ===");
            if (transactions.Count == 0)
            {
                Console.WriteLine("Chua co giao dich nao.");
                return;
            }

            foreach (var t in transactions)
            {
                Console.WriteLine($"ID: {t.Id}, Loai: {t.Loai}, So tien: {t.SoTien}, Mo ta: {t.MoTa}, Ngay: {t.Ngay:dd/mm/yyyy}");
            }
        }

        // Sửa giao dịch
        static void SuaGiaoDich()
        {
            Console.Write("\nNhap ID giao dich can sua: ");
            int id;
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("ID khong hop le!");
                return;
            }

            var t = transactions.Find(x => x.Id == id);
            if (t == null)
            {
                Console.WriteLine("Khong tim thay giao dich.");
                return;
            }

            Console.WriteLine("Chon loai giao dich moi:");
            Console.WriteLine("1. An uong");
            Console.WriteLine("2. Chi tieu hang ngay");
            Console.WriteLine("3. Di lai");
            Console.WriteLine("4. Hoc tap");
            Console.WriteLine("5. Giai tri");
            Console.WriteLine("6. Y te");
            int loaiChon;
            if (int.TryParse(Console.ReadLine(), out loaiChon) && loaiChon >= 1 && loaiChon <= 6)
            {
                string[] loaiGD = { "An uong", "Chi tieu hang ngay", "Di lai", "Hoc tap", "Giai tri", "Y te" };
                t.Loai = loaiGD[loaiChon - 1];
            }

            Console.Write("Nhap so tien moi: ");
            if (double.TryParse(Console.ReadLine(), out double soTien)) t.SoTien = soTien;

            Console.Write("Nhap mo ta moi: ");
            t.MoTa = Console.ReadLine();

            Console.Write("Nhap ngay moi (dd/mm/yyyy): ");
            string ngayNhap = Console.ReadLine();
            if (DateTime.TryParseExact(ngayNhap, "dd/mm/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime ngay))
            {
                t.Ngay = ngay;
            }

            Console.WriteLine("Sua giao dich thanh cong!");
        }

        // Xóa giao dịch
        static void XoaGiaoDich()
        {
            Console.Write("\nNhap ID giao dich can xoa: ");
            int id;
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("ID khong hop le!");
                return;
            }

            var t = transactions.Find(x => x.Id == id);
            if (t == null)
            {
                Console.WriteLine("Khong tim thay giao dich.");
                return;
            }

            transactions.Remove(t);
            Console.WriteLine("Xoa giao dich thanh cong!");
        }

        // Menu quản lý chi tiêu
        static void MenuQuanLyChiTieu()
        {
            int chon;
            do
            {
                Console.WriteLine("\n===== QUAN LY CHI TIEU =====");
                Console.WriteLine("1. Them giao dich");
                Console.WriteLine("2. Xem danh sach giao dich");
                Console.WriteLine("3. Sua giao dich");
                Console.WriteLine("4. Xoa giao dich");
                Console.WriteLine("5. Quay lai menu chinh");
                Console.Write("Moi ban chon: ");

                if (!int.TryParse(Console.ReadLine(), out chon)) continue;

                switch (chon)
                {
                    case 1: ThemGiaoDich(); break;
                    case 2: XemDanhSach(); break;
                    case 3: SuaGiaoDich(); break;
                    case 4: XoaGiaoDich(); break;
                    case 5: Console.WriteLine("Quay lai menu chinh..."); break;
                    default: Console.WriteLine("Lua chon khong hop le."); break;
                }

            } while (chon != 5);
        }

        static void Main(string[] args)
        {
     //Chỗ này khi nào có dữ liệu tên người dùng + Pass thì bỏ vào "" cho nó đọc nè!!!

            string username = "";
            string password = "";

            // Hỏi xem đã có tài khoản chưa
            while (true)
            {
                Console.Write("Ban da co tai khoan chua? (Y/N): ");
                string coTaiKhoan = Console.ReadLine().ToUpper();

                if (coTaiKhoan == "Y")
                {
                    // Dang nhap
                    Console.Write("Nhap ten dang nhap: ");
                    string userNhap = Console.ReadLine();
                    Console.Write("Nhap mat khau: ");
                    string passNhap = Console.ReadLine();

                    if (userNhap == username && passNhap == password)
                    {
                        Console.WriteLine("Dang nhap thanh cong!");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Sai tai khoan hoac mat khau!");
                    }
                }
                else if (coTaiKhoan == "N")
                {
                    // Dang ky
                    Console.Write("Nhap ten dang ky: ");
                    username = Console.ReadLine();
                    Console.Write("Nhap mat khau: ");
                    password = Console.ReadLine();
                    Console.WriteLine("Dang ky thanh cong!");
                    break;
                }
                else
                {
                    Console.WriteLine("Nhap Y hoac N thoi nha!");
                }
            }

            // Menu chinh
            int luaChon;
            do
            {
                Console.Clear();
                Console.WriteLine($"\nXin chao {username}!");
                Console.WriteLine("===== MENU CHINH =====");
                Console.WriteLine("1. Quan ly chi tieu");
                Console.WriteLine("2. Thoat");
                Console.Write("Moi ban chon: ");

                if (!int.TryParse(Console.ReadLine(), out luaChon)) continue;

                if (luaChon == 1)
                {
                    MenuQuanLyChiTieu();
                }
                else if (luaChon == 2)
                {
                    Console.WriteLine("Thoat chuong trinh...");
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine("Lua chon khong hop le.");
                    Console.ReadKey();
                }

            } while (luaChon != 2);
        }
    }
}
