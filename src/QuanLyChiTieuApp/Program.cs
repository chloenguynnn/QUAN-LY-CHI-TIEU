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
                Console.WriteLine("3. Xem và Tìm kiếm chi tiêu");
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
                else if (luaChon == 3)
                {
                    Console.WriteLine("Chuc nang Xem va Tim kiem chi tieu: ");
                    MenuTimKiem();
                }
                else
                {
                    Console.WriteLine("Lua chon khong hop le.");
                    Console.ReadKey();
                }

            } while (luaChon != 2);
        }
        static class TimKiem
        {
            public static void MenuTimKiem()
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("==== XEM & TIM KIEM CHI TIEU ====");
                    Console.WriteLine("1. Xem tat ca");
                    Console.WriteLine("2. Tim theo ID");
                    Console.WriteLine("3. Tim theo khoang so tien");
                    Console.WriteLine("4. Tim theo loai");
                    Console.WriteLine("5. Tim theo khoang ngay");
                    Console.WriteLine("0. Thoat ve menu chinh");
                    Console.Write("Chon chuc nang: ");

                    string luaChon = Console.ReadLine();
                    List<ChiTieu> ketQua = null;

                    switch (luaChon)
                    {
                        case "1":
                            QuanLyCRUD.XemDanhSach(QuanLyCRUD.expenses);
                            break;

                        case "2": // Tim theo ID
                            Console.Write("Nhap ID: ");
                            int id;
                            while (!int.TryParse(Console.ReadLine(), out id) || id < 0)
                            {
                                Console.Write("Sai! Nhap lai ID (so nguyen >= 0): ");
                            }
                            ketQua = QuanLyCRUD.expenses.Where(e => e.Id == id).ToList();
                            break;

                        case "3": // Tim theo khoang so tien
                            Console.WriteLine("Nhap khoang so tien:");

                            double min, max;
                            Console.Write("  So tien tu: ");
                            while (!double.TryParse(Console.ReadLine(), out min) || min < 0)
                            {
                                Console.Write("  Sai! Nhap lai so tien >= 0: ");
                            }

                            Console.Write("  Den: ");
                            while (!double.TryParse(Console.ReadLine(), out max) || max < min)
                            {
                                Console.Write($"  Sai! Nhap lai so tien >= {min}: ");
                            }

                            ketQua = QuanLyCRUD.expenses.Where(e => e.SoTien >= min && e.SoTien <= max).ToList();
                            break;

                        case "4": // Tim theo loai
                            Console.WriteLine("Chon loai chi tieu:");
                            Console.WriteLine("1. An uong");
                            Console.WriteLine("2. Di lai");
                            Console.WriteLine("3. Hoc tap");
                            Console.WriteLine("4. Giai tri");
                            Console.WriteLine("5. YTe");
                            Console.WriteLine("6. Khac");

                            int chonLoai;
                            while (!int.TryParse(Console.ReadLine(), out chonLoai) || chonLoai < 1 || chonLoai > 6)
                            {
                                Console.Write("Sai! Vui long nhap tu 1-6: ");
                            }

                            LoaiChiTieu loaiTim = (LoaiChiTieu)chonLoai;
                            ketQua = QuanLyCRUD.expenses.Where(e => e.Loai == loaiTim).ToList();
                            break;

                        case "5": // Tim theo khoang ngay
                            Console.WriteLine("Nhap khoang thoi gian:");

                            DateTime start;
                            while (true)
                            {
                                Console.WriteLine("Ngay bat dau:");

                                Console.Write("  Ngay: ");
                                int ngayBD;
                                while (!int.TryParse(Console.ReadLine(), out ngayBD) || ngayBD < 1 || ngayBD > 31)
                                {
                                    Console.Write("  Sai! Nhap lai ngay (1-31): ");
                                }

                                Console.Write("  Thang: ");
                                int thangBD;
                                while (!int.TryParse(Console.ReadLine(), out thangBD) || thangBD < 1 || thangBD > 12)
                                {
                                    Console.Write("  Sai! Nhap lai thang (1-12): ");
                                }

                                Console.Write("  Nam: ");
                                int namBD;
                                while (!int.TryParse(Console.ReadLine(), out namBD) || namBD < 1)
                                {
                                    Console.Write("  Sai! Nhap lai nam (>0): ");
                                }

                                try
                                {
                                    start = new DateTime(namBD, thangBD, ngayBD);
                                    break;
                                }
                                catch
                                {
                                    Console.WriteLine($"  Ngay {ngayBD}/{thangBD}/{namBD} khong hop le, vui long nhap lai!\n");
                                }
                            }

                            DateTime end;
                            while (true)
                            {
                                Console.WriteLine("Ngay ket thuc:");

                                Console.Write("  Ngay: ");
                                int ngayKT;
                                while (!int.TryParse(Console.ReadLine(), out ngayKT) || ngayKT < 1 || ngayKT > 31)
                                {
                                    Console.Write("  Sai! Nhap lai ngay (1-31): ");
                                }

                                Console.Write("  Thang: ");
                                int thangKT;
                                while (!int.TryParse(Console.ReadLine(), out thangKT) || thangKT < 1 || thangKT > 12)
                                {
                                    Console.Write("  Sai! Nhap lai thang (1-12): ");
                                }

                                Console.Write("  Nam: ");
                                int namKT;
                                while (!int.TryParse(Console.ReadLine(), out namKT) || namKT < 1)
                                {
                                    Console.Write("  Sai! Nhap lai nam (>0): ");
                                }

                                try
                                {
                                    end = new DateTime(namKT, thangKT, ngayKT);
                                    break;
                                }
                                catch
                                {
                                    Console.WriteLine($"  Ngay {ngayKT}/{thangKT}/{namKT} khong hop le, vui long nhap lai!\n");
                                }
                            }

                            ketQua = QuanLyCRUD.expenses.Where(e => e.Ngay >= start && e.Ngay <= end).ToList();
                            break;

                        case "0":
                            return;

                        default:
                            Console.WriteLine("Lua chon khong hop le!");
                            break;
                    }

                    if (ketQua != null) QuanLyCRUD.XemDanhSach(ketQua);

                    Console.WriteLine("\nNhan ENTER de tiep tuc trong menu nay, nhap 0 de thoat ve menu chinh");
                    string tiep = Console.ReadLine();
                    if (tiep == "0") return;
                }
            }
        }

    }
}
