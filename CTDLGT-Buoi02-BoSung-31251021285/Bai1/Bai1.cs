using System;
using System.Collections.Generic;
using System.Globalization;

namespace Bai1
{

    internal class Bai1
    {
        
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            // 1. Đọc số n ở dòng đầu tiên
            Console.WriteLine($"Nhập số lượng phần tử n:");
            int n = int.Parse(Console.ReadLine());
            //tạo mảng:
            int[] a = new int[n];
            for(int i = 0; i < n; i++)
            {
                Console.WriteLine($"Nhập phần tử thứ :{i+1}");
                a[i] = int.Parse(Console.ReadLine());
            }
            int max = a[0];
            int min = a[0];
            long sum = 0;
            int am = 0;
            int duong = 0;
            int zero = 0;
            // 3. Duyệt qua từng số vừa tách
            for (int i = 0; i < n; i++)
            {

                sum += a[i];
                if (a[i] < 0)
                {
                    am++;
                }
                else if (a[i] > 0)
                {
                    duong++;
                }
                else
                {
                    zero++;
                }
            }
            //Thống kê:
            // 4. Tính trung bình bằng decimal và làm tròn ra xa 0 theo gợi ý của đề
            decimal ave = (decimal)sum / n;
            decimal roundedAve = Math.Round(ave, 2);
            Console.WriteLine(max);
            Console.WriteLine(min);
            Console.WriteLine(sum);
            Console.WriteLine(roundedAve.ToString("0.00", CultureInfo.InvariantCulture));
            Console.WriteLine($"Số phần tử âm:{am}");
            Console.WriteLine($"Số phần tử dương:{duong}");
            Console.WriteLine($"Số phần tử bằng 0:{zero}");

            //Đảo mảng:
            // Chọn giá trị đầu tiên để bắt đầu so sánh
            int left = 0;
            //Chọn giá trị cuối cùng của chuỗi đặt là right
            int right = n - 1;
            while(left < right)//Điều kiện để dừng lại là left lớn hơn right
            {
                // đặt hàm phụ để giữ giá trị bên left
                int temp = a[left];
                //tiến hành đảo vị trí left = right, right = temp
                a[left] = a[right]; 
                a[right] = temp;
                left++; //left tăng dần và right giảm dần đến khi left > right thì kết thúc
                right--;
            }
            Console.WriteLine(string.Join(", ", a)); //in mảng sau khi đảo
          

        }
    }
}
