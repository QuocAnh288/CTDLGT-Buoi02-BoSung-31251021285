# CTDLGT-Buoi02-BoSung-31251021285

**Bài tập thực hành 02**
Học phần: Cấu trúc dữ liệu và Giải thuật – Khoa Công nghệ thông tin kinh doanh, UEH
Giảng viên: TS. Nguyễn Thành Huy

| Thông tin | Nội dung |
|---|---|
| Họ và tên | Võ Quốc Anh |
| Mã số sinh viên | 31251021285 |


## Cấu trúc repository

```
CTDLGT-Buoi02-BoSung-MSSV/
├── Bai1/   # Thống kê và đảo mảng
├── Bai2/   # Chèn và xóa trên mảng
├── Bai3/   # Quản lý List<Student>
├── Bai4/   # Mảng động DynamicArray<T> và Capacity
└── README.md
```

## Cách chạy

Yêu cầu: đã cài [.NET SDK](https://dotnet.microsoft.com/download).

```bash
git clone https://github.com/<username>/CTDLGT-Buoi02-BoSung-MSSV.git
cd CTDLGT-Buoi02-BoSung-MSSV

# Bài 1
cd Bai1 && dotnet run && cd ..
# Bài 2
cd Bai2 && dotnet run && cd ..
# Bài 3
cd Bai3 && dotnet run && cd ..
# Bài 4
cd Bai4 && dotnet run && cd ..
```

Bài 1 nhập từ bàn phím: dòng 1 là `n`, dòng 2 là `n` số nguyên cách nhau bởi dấu cách.
Bài 3 chạy theo menu, nhập số tương ứng chức năng, chọn "Thoát" để kết thúc.

---

## Bài 1 – Thống kê và đảo mảng

### Mô tả thuật toán
1. Đọc `n` và mảng `a`.
2. **Thống kê (một lần duyệt):** khởi tạo `min = max = a[0]`, `sum = 0` (kiểu `long`), các bộ đếm âm, dương, bằng 0 = 0. Duyệt `i = 0..n-1`: cộng `a[i]` vào `sum`, cập nhật `min`, `max`, tăng đúng một bộ đếm theo dấu của `a[i]`.
3. **Trung bình:** `avg = (double)sum / n`, in với định dạng `F2`.
4. **Đảo tại chỗ:** `left = 0`, `right = n - 1`. Khi `left < right`: hoán đổi `a[left]` và `a[right]` bằng một biến tạm, rồi `left++`, `right--`.
5. In mảng sau khi đảo.

### Giải thích kỹ thuật
- **Min/max khởi tạo từ `a[0]`:** nếu khởi tạo bằng 0 thì mảng toàn số âm sẽ cho `max = 0` (sai) và mảng toàn số dương sẽ cho `min = 0` (sai). Dùng phần tử đầu luôn là giá trị có thật trong mảng.
- **`long` cho tổng:** 100000 phần tử `int` có thể cho tổng vượt `int.MaxValue` (tràn số).
- **Ép kiểu trước khi chia:** `(double)sum / n`. Nếu không ép, phép chia `long / int` là chia nguyên và mất phần thập phân.
- **`left`, `right`:** trước hoán đổi `left` trỏ phần tử đầu đoạn chưa xử lý, `right` trỏ phần tử cuối đoạn đó. Sau hoán đổi hai phần tử ở hai đầu đã đúng vị trí, `left` tăng 1 và `right` giảm 1 để thu hẹp đoạn. Dừng khi `left >= right`: `left == right` là phần tử giữa (không cần đổi), `left > right` là đã đổi hết các cặp.
- **Hai vòng lặp liên tiếp:** n + n/2 phép hoán đổi = Θ(n), vì hằng số bị bỏ qua nên tổng vẫn là Θ(n).

### Độ phức tạp
| Phần | Thời gian | Bộ nhớ phụ |
|---|---|---|
| Thống kê | Θ(n) | Θ(1) |
| Đảo mảng | Θ(n) | Θ(1) |
| Toàn chương trình (sau khi lưu mảng) | Θ(n) | Θ(1) |

Chỉ dùng vài biến đơn (`sum`, `min`, `max`, bộ đếm, `left`, `right`, `temp`), không dùng mảng phụ, `Array.Reverse` hay LINQ.

### Ca kiểm thử
| STT | Đầu vào | Kết quả mong đợi | Thực tế | Nhận xét |
|---|---|---|---|---|
| 1 (thường) | `3 7 2 9 5` | tổng 26; min 2; max 9; TB 5,20; âm 0, dương 5, bằng 0 là 0; đảo `5 9 2 7 3` | Đúng như mong đợi | Đạt |
| 2 (âm và 0) | `-5 0 5 0` | tổng 0; min -5; max 5; TB 0,00; âm 1, dương 1, bằng 0 là 2; đảo `0 5 0 -5` | Đúng như mong đợi | Đạt |
| 3 (biên: 1 phần tử) | `8` | min = max = 8; TB 8,00; đảo `8` | Đúng như mong đợi | Đạt, vòng đảo không chạy vì `left == right` |
| 4 (toàn số âm) | `-4 -2 -9` | tổng -15; min -9; max -2; TB -5,00; đảo `-9 -2 -4` | Đúng như mong đợi | Đạt, chứng minh min/max khởi tạo từ phần tử đầu |

---

## Bài 2 – Chèn và xóa trên mảng

### Mô tả thuật toán
Mảng `data` có `data.Length` ô đã cấp phát, `count` ô đầu là dữ liệu hợp lệ.

**`Insert(data, ref count, index, value)`**
1. `index` ngoài `[0, count]` thì ném `ArgumentOutOfRangeException`.
2. `count == data.Length` (đầy) thì ném `InvalidOperationException`.
3. Dịch sang phải từ cuối về `index`: `for i = count; i > index; i--` thì `data[i] = data[i-1]`.
4. `data[index] = value`, sau đó `count++`.

**`RemoveAt(data, ref count, index)`**
1. `index` ngoài `[0, count-1]` thì ném `ArgumentOutOfRangeException`.
2. Dịch sang trái từ `index`: `for i = index; i < count-1; i++` thì `data[i] = data[i+1]`.
3. `count--` (có thể xóa giá trị rác ở ô cuối bằng `data[count] = default`).

### Giải thích kỹ thuật
- **Hướng dịch:** Insert dịch **từ phải sang trái** (bắt đầu từ phần tử cuối) để mỗi phần tử được chép sang ô trống phía sau trước khi ô đó bị ghi đè. Nếu dịch từ trái sang phải thì `data[index]` sẽ ghi đè `data[index+1]` làm mất dữ liệu. RemoveAt ngược lại, dịch **từ trái sang phải** để phần tử phía sau lấp vào chỗ vừa bị xóa mà không ghi đè phần tử chưa được chép.
- **`Length` và `count`:** `data.Length` là sức chứa cố định, không đổi trước, trong và sau thao tác. `count` là số phần tử hợp lệ; chỉ thay đổi ở bước cuối.
- **Vì sao `count++`/`count--` đặt sau khi dịch:** vòng dịch dùng `count` làm cận. Nếu đổi trước thì cận sai (Insert truy cập `data[count]` có thể vượt vùng, RemoveAt dịch thiếu một phần tử).

### Độ phức tạp
| Thao tác | Số phần tử dịch | Thời gian | Bộ nhớ phụ |
|---|---|---|---|
| Insert tại `index` | `count - index` | Θ(count - index + 1), xấu nhất O(n) | Θ(1) |
| RemoveAt tại `index` | `count - index - 1` | Θ(count - index), xấu nhất O(n) | Θ(1) |

- **Tốt nhất:** thao tác ở cuối (Insert tại `count`: dịch 0; RemoveAt tại `count-1`: dịch 0) thì O(1).
- **Xấu nhất:** thao tác ở đầu (`index = 0`) thì dịch gần toàn bộ `count` phần tử, O(n).

### Ca kiểm thử
Dữ liệu ban đầu `[4, 7, 2, 9, 5]`, `count = 5`, mảng cấp phát 10 ô (trừ ca "đầy").

| Ca | Thao tác | Kết quả mong đợi | Số phần tử dịch | Nhận xét |
|---|---|---|---|---|
| Thường | Insert 99 tại 2 | `[4, 7, 99, 2, 9, 5]`, count = 6 | 3 (2, 9, 5) | Đạt |
| Thường | Sau đó RemoveAt 1 | `[4, 99, 2, 9, 5]`, count = 5 | 4 (99, 2, 9, 5) | Đạt |
| Biên | Insert tại đầu (index 0) | giá trị mới ở đầu | `count` | Trường hợp xấu nhất |
| Biên | Insert tại cuối (index = count) | giá trị mới ở cuối | 0 | Trường hợp tốt nhất |
| Biên | RemoveAt đầu (index 0) | bỏ phần tử đầu | `count - 1` | Trường hợp xấu nhất |
| Biên | RemoveAt cuối (index = count-1) | bỏ phần tử cuối | 0 | Trường hợp tốt nhất |
| Không hợp lệ | Insert/RemoveAt index âm | `ArgumentOutOfRangeException` | – | Bắt bằng `try/catch` và in thông báo |
| Không hợp lệ | RemoveAt index = count | `ArgumentOutOfRangeException` | – | Chỉ hợp lệ đến `count - 1` |
| Không hợp lệ | Insert khi `count == data.Length` | `InvalidOperationException` | – | Mảng đầy |

---

## Bài 3 – Quản lý List\<Student\>

### Mô tả chương trình
`Student` gồm `Id`, `FullName`, `Score`. Dữ liệu lưu trong `List<Student>`, chương trình chạy theo menu cho đến khi chọn thoát. Mỗi chức năng là một phương thức riêng, `Main` chỉ hiển thị menu và gọi phương thức; không dùng LINQ.

| Chức năng | Mô tả thuật toán |
|---|---|
| Thêm | Kiểm tra điểm trong `[0, 10]`; duyệt List kiểm tra trùng `Id`; hợp lệ thì `Add`. |
| Cập nhật | Tìm theo `Id` bằng duyệt tuần tự; thấy thì sửa họ tên hoặc điểm (kiểm tra điểm), không thấy thì báo lỗi. |
| Xóa | Tìm chỉ số theo `Id`; thấy thì `RemoveAt(i)`, không thấy thì thông báo. |
| Tìm kiếm | Duyệt List, so khớp `FullName` chứa chuỗi cần tìm không phân biệt hoa thường (`IndexOf(..., StringComparison.OrdinalIgnoreCase)`); in trực tiếp từng kết quả. |
| Hiển thị / thống kê | Danh sách rỗng thì báo "chưa có sinh viên", không chia cho 0; ngược lại một lần duyệt tính tổng điểm (suy ra điểm trung bình) và đếm sinh viên có điểm >= 5. |

### Giải thích kỹ thuật
- **Trạng thái List:** `Add` ghi vào cuối, `Count` tăng 1 (nếu hết `Capacity` thì List tự cấp mảng lớn hơn). Cập nhật chỉ đổi giá trị thuộc tính, `Count` giữ nguyên. `RemoveAt(i)` dời các phần tử sau `i` lên một vị trí và `Count` giảm 1.
- **Truy cập theo chỉ số O(1) nhưng tìm theo mã O(n):** `list[i]` tính thẳng địa chỉ từ chỉ số. Tìm theo `Id` không biết chỉ số trước nên phải duyệt từng phần tử đến khi gặp, xấu nhất duyệt hết n.
- **Ngăn mã trùng:** duyệt List trước khi `Add`; trùng thì từ chối. Điểm kiểm tra `0 <= Score <= 10` trước khi lưu. Danh sách rỗng khi tính trung bình thì kiểm tra `Count == 0` trước phép chia.
- **Xóa giữa danh sách:** các phần tử phía sau phải dịch lên một ô để không để lại khoảng trống, tốn O(n).

### Độ phức tạp
| Thao tác | Thời gian | Bộ nhớ phụ |
|---|---|---|
| Thêm (có kiểm tra trùng) | O(n) (`Add` O(1) khấu hao, nhưng kiểm tra trùng O(n)) | O(1) |
| Tìm theo mã / theo họ tên | O(n) xấu nhất | O(1) (in trực tiếp) |
| Cập nhật theo mã | O(n) | O(1) |
| Xóa theo mã | tìm O(n) + dịch O(n) = O(n) | O(1) |
| Điểm trung bình + đếm sinh viên đạt | Θ(n) | Θ(1) |

### Ca kiểm thử
Dữ liệu mẫu (ít nhất 5 sinh viên):

| Id | Họ tên | Điểm |
|---|---|---|
| SV01 | Nguyễn Văn An | 8.5 |
| SV02 | Trần Thị Bình | 6.0 |
| SV03 | Lê Hoàng Cường | 4.5 |
| SV04 | Phạm Minh Dũng | 9.0 |
| SV05 | Võ Thị Em | 5.0 |

| Ca | Thao tác | Kết quả mong đợi | Nhận xét |
|---|---|---|---|
| Thường | Thêm SV01 đến SV05 | Thêm thành công, `Count = 5` | Đạt |
| Không hợp lệ | Thêm `SV01` lần nữa | Từ chối, báo mã trùng | Đạt |
| Không hợp lệ | Thêm điểm `11` hoặc `-1` | Từ chối, báo điểm ngoài `[0, 10]` | Đạt |
| Thường | Cập nhật SV03 điểm 4.5 thành 6.5 | Đổi điểm thành công | Đạt |
| Thường | Tìm `bình` (viết thường) | Ra Trần Thị Bình | Không phân biệt hoa thường |
| Biên | Tìm `xyz` | Thông báo không tìm thấy | Đạt |
| Thường | Xóa SV02 | Xóa thành công, `Count = 4` | Các phần tử sau dịch lên |
| Không hợp lệ | Xóa `SV99` | Thông báo không tìm thấy | Đạt |
| Biên | Thống kê khi danh sách rỗng | Thông báo rỗng, không lỗi chia 0 | Đạt |

---

## Bài 4 – Mảng động DynamicArray\<T\> và Capacity

### Mô tả thuật toán
Lớp `DynamicArray<T>` lưu dữ liệu trong `T[] items`, thuộc tính `Count` (số phần tử) và `Capacity => items.Length`.
- **Constructor:** `capacity` mặc định 4; `capacity < 0` thì ném `ArgumentOutOfRangeException`.
- **`Add(item)`:** gọi `EnsureCapacity()`, gán `items[Count] = item`, rồi `Count++`.
- **`EnsureCapacity()`:** nếu `Count == Capacity`, tạo mảng mới dung lượng `Capacity * 2` (nếu `Capacity == 0` thì dùng 4), sao chép đúng `Count` phần tử bằng vòng `for`, rồi gán `items = mảng mới`.
- **Indexer:** `index < 0` hoặc `index >= Count` thì ném `ArgumentOutOfRangeException`; ngược lại get/set `items[index]`.

### Giải thích kỹ thuật
- Khi mở rộng chỉ sao chép `Count` phần tử đầu theo đúng thứ tự `0..Count-1`, nên dữ liệu cũ giữ nguyên thứ tự; các ô còn lại là giá trị mặc định.
- Indexer kiểm tra theo `Count`, không theo `Capacity`, vì các ô từ `Count` đến `Capacity-1` chưa chứa dữ liệu hợp lệ.
- **Tăng capacity thêm 1 mỗi lần thì n lần Add là O(n²):** mỗi lần Add đều đầy nên phải sao chép toàn bộ: 1 + 2 + ... + (n-1) = n(n-1)/2 = O(n²).
- **Gấp đôi dung lượng thì tổng là O(n):** các lần mở rộng sao chép 4 + 8 + 16 + ... < 2n phần tử, cộng n lần ghi thì tổng < 3n, trung bình O(1) mỗi lần Add (khấu hao).

### Độ phức tạp
| Thao tác | Thời gian | Ghi chú |
|---|---|---|
| Indexer get/set | O(1) | |
| Add khi còn chỗ | O(1) | |
| Add khi phải mở rộng | O(n) | Sao chép `Count` phần tử |
| Add khấu hao | O(1) | |
| n lần Add từ mảng rỗng | O(n) | Bộ nhớ lưu trữ O(n); lần mở rộng cần O(n) bộ nhớ tạm cho mảng mới |

### Ca kiểm thử
Thêm lần lượt 1 đến 10 (capacity khởi tạo 4):

| Lần Add | Giá trị | Count | Capacity | Số phần tử sao chép |
|---|---|---|---|---|
| 1 | 1 | 1 | 4 | 0 |
| 2 | 2 | 2 | 4 | 0 |
| 3 | 3 | 3 | 4 | 0 |
| 4 | 4 | 4 | 4 | 0 |
| 5 | 5 | 5 | 8 | **4** (mở rộng 4 thành 8) |
| 6 | 6 | 6 | 8 | 0 |
| 7 | 7 | 7 | 8 | 0 |
| 8 | 8 | 8 | 8 | 0 |
| 9 | 9 | 9 | 16 | **8** (mở rộng 8 thành 16) |
| 10 | 10 | 10 | 16 | 0 |

Tổng số phần tử sao chép: 4 + 8 = 12, nhỏ hơn 2n = 20, phù hợp phân tích khấu hao.

| Ca | Thao tác | Kết quả mong đợi | Nhận xét |
|---|---|---|---|
| Thường | Đọc `arr[0]`, `arr[9]`; gán `arr[2] = 30` | Đọc 1 và 10; phần tử index 2 thành 30 | Đạt |
| Chứng minh thứ tự | In mảng trước và sau lần mở rộng 4 thành 8 | `1 2 3 4` thành `1 2 3 4 5` | Dữ liệu cũ giữ nguyên thứ tự |
| Không hợp lệ | `arr[-1]` | `ArgumentOutOfRangeException` | Đạt |
| Biên | `arr[Count]` (index = 10) | `ArgumentOutOfRangeException` | Chỉ hợp lệ đến `Count - 1` |
| Không hợp lệ | `new DynamicArray<int>(-1)` | `ArgumentOutOfRangeException` | Từ chối capacity âm |

---
