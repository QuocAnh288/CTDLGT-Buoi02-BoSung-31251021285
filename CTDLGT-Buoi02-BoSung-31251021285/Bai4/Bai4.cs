using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CTDLGT_Buoi02_BoSung_31251022334
{
    // Lớp mảng động DynamicArray<T> bản nhập môn cơ bản
    public class DynamicArray<T>
    {
        private T[] items;
        public int Count { get; private set; }
        public int Capacity => items.Length;

        // Khởi tạo mảng động (dung lượng mặc định là 4)
        public DynamicArray(int capacity = 4)
        {
            if (capacity < 0)
            {
                Console.WriteLine("Loi: Capacity khong duoc am! Tu dong gan mac dinh = 4.");
                capacity = 4;
            }
            items = new T[capacity];
            Count = 0;
        }

        // Tự động gấp đôi dung lượng khi mảng đầy
        private void EnsureCapacity()
        {
            if (Count == Capacity)
            {
                int newCapacity = Capacity == 0 ? 4 : Capacity * 2;
                T[] newItems = new T[newCapacity];

                // Sao chép thủ công đúng Count phần tử từ mảng cũ sang mảng mới
                for (int i = 0; i < Count; i++)
                {
                    newItems[i] = items[i];
                }

                Console.WriteLine($"   [MỞ RỘNG] Capacity tang tu {Capacity} -> {newCapacity} (Da sao chep {Count} phan tu)");
                items = newItems;
            }
        }

        // Thêm phần tử vào mảng
        public void Add(T item)
        {
            EnsureCapacity();
            items[Count] = item;
            Count++;
        }

        // Indexer đọc / ghi phần tử (dùng if đơn giản)
        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                {
                    Console.WriteLine($"Loi: Index {index} khong hop le (phai tu 0 den {Count - 1})!");
                    return default(T); // Trả về giá trị mặc định (ví dụ kiểu int sẽ trả về 0)
                }
                return items[index];
            }
            set
            {
                if (index < 0 || index >= Count)
                {
                    Console.WriteLine($"Loi: Index {index} khong hop le (phai tu 0 den {Count - 1})!");
                    return;
                }
                items[index] = value;
            }
        }
    }

    class Program1
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            DynamicArray<int> arr = new DynamicArray<int>(4);

            Console.WriteLine("=== 1. THEM CAC SO TU 1 DEN 10 VA THEO DOI CAPACITY ===");
            for (int i = 1; i <= 10; i++)
            {
                arr.Add(i);
                Console.WriteLine($"Add({i}) -> Count = {arr.Count}, Capacity = {arr.Capacity}");
            }

            Console.WriteLine("\n=== 2. KIEM TRA DU LIEU TRONG MANG ===");
            Console.Write("Cac phan tu: ");
            for (int i = 0; i < arr.Count; i++)
            {
                Console.Write(arr[i] + " ");
            }
            Console.WriteLine();

            Console.WriteLine("\n=== 3. DOC VA CAP NHAT QUA INDEXER ===");
            Console.WriteLine($"Gia tri tai index 2: {arr[2]}");
            arr[2] = 99;
            Console.WriteLine($"Gia tri tai index 2 sau khi sua: {arr[2]}");

            Console.WriteLine("\n=== 4. THU NHAP INDEX SAI ===");
            Console.Write("Thu doc index -1: ");
            int test1 = arr[-1];

            Console.Write("Thu doc index bang Count (10): ");
            int test2 = arr[arr.Count];
        }
    }
}
