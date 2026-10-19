using System;
using System.Collections.Generic;
using System.Threading.Tasks.Dataflow;

namespace Bai2
{

    internal class Bai2
    {
        static void Insert(int[] data, ref int count, int index, int value)
        {
            for (int i = count; i > index; i--)
            {
                data[i] = data[i - 1]; // Sao chép giá trị ô trước sang ô sau
            }

            // Đặt giá trị cần chèn vào đúng vị trí trống đã dọn sẵn
            data[index] = value;

            // Tăng số lượng phần tử hợp lệ trong mảng lên 1
            count++;
        }
        static void RemoveAt(int[] data, ref int count, int index)
        {

            // Dịch chuyển các phần tử đứng sau index tiến sang trái 1 vị trí để lấp chỗ trống
            for (int i = index; i < count - 1; i++)
            {
                data[i] = data[i + 1]; // Đè phần tử phía sau lên phần tử phía trước
            }

            // Giảm số lượng phần tử hợp lệ trong mảng đi 1
            count--;
        }
        static void PrintArray(int[] data, int count)
        {
            if (count == 0)
            {
                Console.WriteLine("Mảng rỗng.");
                return;
            }

            for (int i = 0; i < count; i++)
            {
                Console.Write(data[i] + " ");
            }
            Console.WriteLine();
        }
        static void Main2(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            int count = 5;
            int[] data = new int[10] { 4, 7, 2, 9, 5, 0, 0, 0, 0, 0 };
            Console.WriteLine("Mảng ban đầu:");
            PrintArray(data, count);


            Insert(data,ref count,2, 99);
            Console.WriteLine("Sau khi thêm số 99 vào vị trí 2");
            PrintArray(data, count);
            RemoveAt(data, ref count, 1);
            Console.WriteLine("Sau khi xóa giá trị ở vị trí 1:");
            PrintArray(data, count);
        }
    }
}