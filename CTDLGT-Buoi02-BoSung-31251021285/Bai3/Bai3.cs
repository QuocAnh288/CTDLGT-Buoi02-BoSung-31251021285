using System;
using System.Collections.Generic;
using System.Globalization;

namespace CTDLGT_Buoi02_BoSung_31251022334
{
    /*
    CA KIEM THU BAN DAU (nap san khi chay chuong trinh):
    - SV101: Nguyen Van Hoa  - Diem: 0     (bien duoi)
    - SV102: Tran Thi Mai    - Diem: 4.5   (truot)
    - SV103: Le Quoc Bao     - Diem: 5     (dung moc dat)
    - SV104: Pham Minh Chau  - Diem: 7.25  (diem le)
    - SV105: Hoang Anh Tuan  - Diem: 10    (bien tren)
    - SV106: Nguyen Thi Hoa  - Diem: 4.99  (sat moc dat, trung ten "Hoa")

    KET QUA MONG DOI KHI CHON CHUC NANG 5:
    - Tong diem = 31.74, so SV = 6  ->  Diem trung binh = 5.29
    - So SV dat (>= 5) = 3/6  (SV103, SV104, SV105)

    CAC TINH HUONG NEN THU THEM:
    - Tim "hoa" hoac "HOA"  -> ra SV101 va SV106 (khong phan biet hoa thuong)
    - Tim "nguyen"          -> ra SV101 va SV106
    - Them ma "sv103"       -> bao trung ma (khong phan biet hoa thuong)
    - Them diem 10.5, -1, "abc" -> bi tu choi
    - Nhap diem "8,5" va "8.5"  -> deu duoc chap nhan
    - Xoa het 6 SV roi chon 5   -> bao danh sach rong, khong loi chia cho 0
    - Nhap chu o menu           -> bao loi, khong thoat chuong trinh
    */

    // Lop Student chua thong tin sinh vien
    public class Student
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public double Score { get; set; }

        public Student(string id, string fullName, double score)
        {
            Id = id;
            FullName = fullName;
            Score = score;
        }

        // In thong tin mot sinh vien, dung chung cho moi chuc nang
        public void Print()
        {
            Console.WriteLine($"Ma: {Id} | Ten: {FullName} | Diem: {Score:0.##}");
        }
    }

    public class Program
    {
        //CAC HAM HO TRO NHAP LIEU 

        // Doc mot dong, tranh null va cat khoang trang hai dau
        static string DocChuoi(string thongBao)
        {
            Console.Write(thongBao);
            return (Console.ReadLine() ?? "").Trim();
        }

        // Chuyen chuoi thanh diem, chap nhan ca "7.5" va "7,5", phai nam trong [0, 10]
        static bool TryDocDiem(string s, out double diem)
        {
            bool ok = double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out diem)
                   || double.TryParse(s, NumberStyles.Float, CultureInfo.CurrentCulture, out diem)
                   || double.TryParse(s.Replace(',', '.'), NumberStyles.Float, CultureInfo.InvariantCulture, out diem);

            return ok && !double.IsNaN(diem) && diem >= 0 && diem <= 10;
        }

        // Tim vi tri sinh vien theo ID O(n)
        static int TimViTriTheoId(List<Student> list, string id)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Id.Equals(id, StringComparison.OrdinalIgnoreCase))
                {
                    return i;
                }
            }
            return -1;
        }

        // ====== CAC CHUC NANG ======

        // 1. Them sinh vien
        static void ThemSinhVien(List<Student> list)
        {
            string id = DocChuoi("Nhap ma sinh vien: ");
            if (id == "")
            {
                Console.WriteLine("Loi: Ma sinh vien khong duoc de trong!");
                return;
            }

            // Chan ma trung: duyet danh sach O(n)
            if (TimViTriTheoId(list, id) != -1)
            {
                Console.WriteLine("Loi: Ma sinh vien da ton tai!");
                return;
            }

            string fullName = DocChuoi("Nhap ho va ten: ");
            if (fullName == "")
            {
                Console.WriteLine("Loi: Ho ten khong duoc de trong!");
                return;
            }

            if (!TryDocDiem(DocChuoi("Nhap diem tong ket (0 - 10): "), out double score))
            {
                Console.WriteLine("Loi: Diem phai la so trong doan tu 0 den 10!");
                return;
            }

            list.Add(new Student(id, fullName, score));
            Console.WriteLine("Them sinh vien thanh cong!");
        }

        // 2. Cap nhat ho ten hoac diem theo ma
        static void CapNhatSinhVien(List<Student> list)
        {
            string id = DocChuoi("Nhap ma sinh vien can cap nhat: ");
            int index = TimViTriTheoId(list, id);
            if (index == -1)
            {
                Console.WriteLine("Loi: Khong tim thay sinh vien voi ma nay!");
                return;
            }

            string newName = DocChuoi("Nhap ho ten moi (de trong neu khong doi): ");
            string newScoreText = DocChuoi("Nhap diem moi (de trong neu khong doi): ");

            // Kiem tra diem truoc, neu sai thi khong thay doi gi ca
            double newScore = 0;
            bool doiDiem = newScoreText != "";
            if (doiDiem && !TryDocDiem(newScoreText, out newScore))
            {
                Console.WriteLine("Loi: Diem phai la so trong doan tu 0 den 10. Khong cap nhat!");
                return;
            }

            if (newName != "")
            {
                list[index].FullName = newName;
            }
            if (doiDiem)
            {
                list[index].Score = newScore;
            }

            Console.WriteLine("Cap nhat thong tin thanh cong!");
        }

        // 3. Xoa sinh vien theo ma
        static void XoaSinhVien(List<Student> list)
        {
            string id = DocChuoi("Nhap ma sinh vien can xoa: ");
            int index = TimViTriTheoId(list, id);
            if (index == -1)
            {
                Console.WriteLine("Loi: Khong tim thay sinh vien voi ma nay!");
                return;
            }

            // RemoveAt xoa phan tu va don cac phan tu sau sang trai: O(n)
            list.RemoveAt(index);
            Console.WriteLine("Xoa sinh vien thanh cong!");
        }

        // 4. Tim kiem theo mot phan ho ten (khong phan biet hoa thuong, khong LINQ)
        static void TimKiemTheoTen(List<Student> list)
        {
            string keyword = DocChuoi("Nhap tu khoa ho ten can tim: ");
            if (keyword == "")
            {
                Console.WriteLine("Loi: Tu khoa khong duoc de trong!");
                return;
            }

            bool timThay = false;
            Console.WriteLine("\n--- KET QUA TIM KIEM ---");
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].FullName.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    list[i].Print();
                    timThay = true;
                }
            }

            if (!timThay)
            {
                Console.WriteLine("Khong tim thay sinh vien nao phu hop!");
            }
        }

        // 5a. Hien thi danh sach
        static void HienThiDanhSach(List<Student> list)
        {
            Console.WriteLine("\n--- DANH SACH SINH VIEN ---");
            for (int i = 0; i < list.Count; i++)
            {
                list[i].Print();
            }
        }

        // 5b. Thong ke: diem trung binh va so sinh vien dat (>= 5), duyet 1 lan O(n)
        static void ThongKe(List<Student> list)
        {
            double tongDiem = 0;
            int soSvDat = 0;

            for (int i = 0; i < list.Count; i++)
            {
                tongDiem += list[i].Score;
                if (list[i].Score >= 5.0)
                {
                    soSvDat++;
                }
            }

            double diemTrungBinh = tongDiem / list.Count; // list.Count > 0 da duoc kiem tra o noi goi
            Console.WriteLine($"\n-> Diem trung binh: {diemTrungBinh:0.##}");
            Console.WriteLine($"-> So sinh vien dat (diem >= 5): {soSvDat}/{list.Count}");
        }

        static void HienThiVaThongKe(List<Student> list)
        {
            // Danh sach rong thi thoat som, tranh chia cho 0
            if (list.Count == 0)
            {
                Console.WriteLine("Danh sach sinh vien dang rong!");
                return;
            }

            HienThiDanhSach(list);
            ThongKe(list);
        }

        // Nap san 6 sinh vien theo ca kiem thu (gom diem bien, diem sat moc dat, ten trung nhau)
        static void NapDuLieuMau(List<Student> list)
        {
            list.Add(new Student("SV101", "Nguyen Van Hoa", 0));
            list.Add(new Student("SV102", "Tran Thi Mai", 4.5));
            list.Add(new Student("SV103", "Le Quoc Bao", 5));
            list.Add(new Student("SV104", "Pham Minh Chau", 7.25));
            list.Add(new Student("SV105", "Hoang Anh Tuan", 10));
            list.Add(new Student("SV106", "Nguyen Thi Hoa", 4.99));
        }

        static void Main(string[] augs)
        {
            List<Student> danhSach = new List<Student>();
            NapDuLieuMau(danhSach);

            int chon = -1; // khac 0 de vao vong lap

            do
            {
                Console.WriteLine("\n===== MENU QUAN LY SINH VIEN =====");
                Console.WriteLine("1. Them sinh vien");
                Console.WriteLine("2. Cap nhat sinh vien theo ma");
                Console.WriteLine("3. Xoa sinh vien theo ma");
                Console.WriteLine("4. Tim kiem sinh vien theo ten");
                Console.WriteLine("5. Hien thi danh sach va thong ke");
                Console.WriteLine("0. Thoat");

                // Nhap sai (chu, de trong) thi bao loi va hoi lai, khong thoat chuong trinh
                if (!int.TryParse(DocChuoi("Chon chuc nang (0-5): "), out chon))
                {
                    Console.WriteLine("Loi: Vui long nhap mot so tu 0 den 5!");
                    chon = -1;
                    continue;
                }

                switch (chon)
                {
                    case 1: ThemSinhVien(danhSach); break;
                    case 2: CapNhatSinhVien(danhSach); break;
                    case 3: XoaSinhVien(danhSach); break;
                    case 4: TimKiemTheoTen(danhSach); break;
                    case 5: HienThiVaThongKe(danhSach); break;
                    case 0: Console.WriteLine("Da thoat chuong trinh."); break;
                    default: Console.WriteLine("Lua chon khong hop le!"); break;
                }
            } while (chon != 0);
        }
    }
}