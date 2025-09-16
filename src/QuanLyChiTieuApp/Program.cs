using System;

namespace QuanLyChiTieu
{
    class Program
    {
        //MENU APP
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
                    Console.WriteLine("Chuc nang Quan ly chi tieu: ");
                    Console.ReadKey();
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
