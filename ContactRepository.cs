using System;

namespace PhoneBook
{
    internal class ContactRepository
    {
        private Contact[] _contactArr; // Mảng cố định
        private int _count;            // Số lượng phần tử hiện tại
        private int _nextId;           // Auto-increment ID
        private int _maxTagsPerContact;

        public ContactRepository(int maxContacts, int maxTagsPerContact)
        {
            _contactArr = new Contact[maxContacts];
            _count = 0;
            _nextId = 1;
            _maxTagsPerContact = maxTagsPerContact;
        }

        public int GetCount() => _count;

        // Tạo object nhưng chưa lưu vào mảng
        public Contact CreateEmptyContact()
        {
            return new Contact(_nextId, _maxTagsPerContact);
        }

        // (F1) Thêm Contact vào mảng
        public void Add(Contact contact)
        {
            if (_count >= _contactArr.Length)
                throw new Exception("Danh ba da day, khong the them moi!");

            // Kiểm tra trùng SĐT trên toàn bộ danh bạ
            if (IsPhoneExist(contact.GetPhone(), -1)) // -1 nghĩa là không loại trừ ID nào
                throw new Exception($"So dien thoai {contact.GetPhone()} da ton tai!");

            // Thêm vào mảng
            _contactArr[_count] = contact;
            _count++;

            // Tăng ID cho lần sau (chỉ tăng khi thêm thành công)
            _nextId++;
        }

        // (F2) In tất cả
        public void PrintAllContacts()
        {
            if (_count == 0)
            {
                Console.WriteLine("(Trong)");
                return;
            }

            for (int i = 0; i < _count; i++)
            {
                _contactArr[i].DisplayString();
            }
        }

        // (F3) Tìm kiếm đa hình
        public void Search(string keyword)
        {
            bool found = false;
            for (int i = 0; i < _count; i++)
            {
                // Gọi hàm Matches đã override ở Contact
                if (_contactArr[i].Matches(keyword))
                {
                    _contactArr[i].DisplayString();
                    found = true;
                }
            }

            if (!found) Console.WriteLine("(Khong tim thay)");
        }

        // (F4) & (F5) Tìm contact theo ID để Sửa/Xóa
        public Contact GetContactById(int id)
        {
            for (int i = 0; i < _count; i++)
            {
                if (_contactArr[i].GetId() == id)
                    return _contactArr[i];
            }
            return null;
        }

        // (F5) Xóa contact
        public void Delete(int id)
        {
            int index = -1;
            // 1. Tìm vị trí
            for (int i = 0; i < _count; i++)
            {
                if (_contactArr[i].GetId() == id)
                {
                    index = i;
                    break;
                }
            }

            if (index == -1)
                throw new Exception("Khong tim thay ID de xoa.");

            // 2. Dồn mảng (Shift Left)
            // Di chuyển tất cả phần tử phía sau index lên trước 1 bước
            for (int i = index; i < _count - 1; i++)
            {
                _contactArr[i] = _contactArr[i + 1];
            }

            // 3. Xóa phần tử cuối (optional, để sạch sẽ) và giảm count
            _contactArr[_count - 1] = null;
            _count--;

            Console.WriteLine("Da xoa thanh cong.");
        }

        // (F6) Lọc theo tag
        public void FilterByTag(string tagKeyword)
        {
            if (string.IsNullOrEmpty(tagKeyword)) return;

            bool found = false;
            for (int i = 0; i < _count; i++)
            {
                if (_contactArr[i].HasTag(tagKeyword))
                {
                    _contactArr[i].DisplayString();
                    found = true;
                }
            }
            if (!found) Console.WriteLine("(Khong co ket qua)");
        }

        // Helper: Kiểm tra trùng phone
        // excludeId: Dùng khi update (nếu update chính số của mình thì ko báo lỗi)
        public bool IsPhoneExist(string phone, int excludeId)
        {
            for (int i = 0; i < _count; i++)
            {
                if (_contactArr[i].GetId() != excludeId &&
                    _contactArr[i].GetPhone() == phone)
                {
                    return true;
                }
            }
            return false;
        }
    }
}