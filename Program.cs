using System;
using PhoneBook;

// Cấu hình hiển thị tiếng Việt có dấu cho Console
Console.OutputEncoding = System.Text.Encoding.UTF8;

// Khởi tạo Repository: Max 100 contact, mỗi contact max 5 tags
ContactRepository repo = new ContactRepository(100, 5);

while (true)
{
    // --- MENU CHÍNH ---
    Console.WriteLine("\n=== DANH BẠ ĐIỆN THOẠI THÔNG MINH ===");
    Console.WriteLine("1. Thêm liên hệ mới");
    Console.WriteLine("2. Hiển thị danh sách");
    Console.WriteLine("3. Tìm kiếm (Tên, SĐT, Email, Tag)");
    Console.WriteLine("4. Sửa liên hệ (theo ID)");
    Console.WriteLine("5. Xóa liên hệ (theo ID)");
    Console.WriteLine("6. Lọc danh bạ theo Tag");
    Console.WriteLine("0. Thoát chương trình");
    Console.Write(">> Mời chọn chức năng: ");

    string choice = Console.ReadLine();

    try
    {
        switch (choice)
        {
            case "1":
                AddNewContact(repo);
                break;
            case "2":
                Console.WriteLine("\n--- DANH SÁCH LIÊN HỆ ---");
                repo.PrintAllContacts();
                break;
            case "3":
                Console.Write("Nhập từ khóa cần tìm: ");
                string kw = Console.ReadLine();
                Console.WriteLine($"\n--- KẾT QUẢ TÌM KIẾM CHO '{kw}' ---");
                repo.Search(kw);
                break;
            case "4":
                EditContact(repo);
                break;
            case "5":
                DeleteContact(repo);
                break;
            case "6":
                Console.Write("Nhập tên Tag cần lọc: ");
                string tag = Console.ReadLine();
                Console.WriteLine($"\n--- DANH SÁCH TAG '{tag}' ---");
                repo.FilterByTag(tag);
                break;
            case "0":
                Console.WriteLine("Đã thoát chương trình. Tạm biệt!");
                return;
            default:
                Console.WriteLine("[!] Lựa chọn không hợp lệ, vui lòng thử lại.");
                break;
        }
    }
    catch (Exception ex)
    {
        // Bắt mọi lỗi logic (ví dụ: trùng SĐT, bộ nhớ đầy...)
        Console.WriteLine($"\n[LỖI HỆ THỐNG]: {ex.Message}");
    }
}

// --- CÁC HÀM HỖ TRỢ XỬ LÝ GIAO DIỆN (HELPER FUNCTIONS) ---

static void AddNewContact(ContactRepository repo)
{
    Console.WriteLine("\n--- THÊM LIÊN HỆ MỚI ---");

    Contact c = repo.CreateEmptyContact();

    Console.Write("1. Nhập Họ Tên: ");
    c.SetName(Console.ReadLine());

    Console.Write("2. Nhập Số Điện Thoại: ");
    c.SetPhone(Console.ReadLine());

    Console.Write("3. Nhập Email (Enter để bỏ qua): ");
    c.SetEmail(Console.ReadLine());

    Console.Write("4. Nhập Tags (cách nhau bởi dấu phẩy): ");
    c.SetTags(Console.ReadLine());

    repo.Add(c);
    Console.WriteLine(">> Thêm mới thành công!");
}

static void EditContact(ContactRepository repo)
{
    Console.WriteLine("\n--- SỬA THÔNG TIN LIÊN HỆ ---");
    Console.Write("Nhập ID cần sửa: ");

    if (!int.TryParse(Console.ReadLine(), out int id))
    {
        Console.WriteLine("[!] ID phải là số nguyên.");
        return;
    }

    Contact c = repo.GetContactById(id);
    if (c == null)
    {
        Console.WriteLine("[!] Không tìm thấy liên hệ với ID này.");
        return;
    }

    Console.WriteLine($"-> Đang sửa thông tin cho: {c.GetName()}");

    // 1. Sửa Tên
    Console.Write($"Tên mới (Hiện tại: {c.GetName()} - Enter để giữ nguyên): ");
    string newName = Console.ReadLine().Trim();
    if (newName.Length > 0) c.SetName(newName);

    // 2. Sửa Email
    Console.Write($"Email mới (Hiện tại: {c.GetEmail()} - Enter để giữ nguyên): ");
    string newEmail = Console.ReadLine().Trim();
    if (newEmail.Length > 0) c.SetEmail(newEmail);

    // 3. Sửa Tags
    Console.Write($"Bạn có muốn sửa Tags không? (y/n): ");
    if (Console.ReadLine().ToLower() == "y")
    {
        Console.Write($"Nhập Tags mới (Hiện tại: {c.GetTagsString()}): ");
        c.SetTags(Console.ReadLine());
    }

    // 4. Sửa Phone (Có kiểm tra trùng lặp)
    Console.Write($"Bạn có muốn sửa SĐT không? (y/n): ");
    if (Console.ReadLine().ToLower() == "y")
    {
        Console.Write($"Nhập SĐT mới (Hiện tại: {c.GetPhone()}): ");
        string newPhone = Console.ReadLine();

        // Kiểm tra logic trùng số trước khi set
        if (repo.IsPhoneExist(newPhone, id))
        {
            throw new Exception("Số điện thoại này đã tồn tại trong danh bạ!");
        }
        c.SetPhone(newPhone);
    }

    Console.WriteLine(">> Cập nhật thông tin thành công!");
}

static void DeleteContact(ContactRepository repo)
{
    Console.WriteLine("\n--- XÓA LIÊN HỆ ---");
    Console.Write("Nhập ID cần xóa: ");

    if (int.TryParse(Console.ReadLine(), out int id))
    {
        repo.Delete(id);
        // Lưu ý: Hàm Delete trong Repository có thể đã in ra thông báo xóa thành công hoặc lỗi.
        // Nếu muốn đồng bộ, nên để Repository chỉ xử lý dữ liệu và Program xử lý in ấn, 
        // nhưng theo kiến trúc hiện tại thì repo.Delete() sẽ thực hiện in hoặc throw exception.
    }
    else
    {
        Console.WriteLine("[!] ID không hợp lệ.");
    }
}