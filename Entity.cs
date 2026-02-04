using System;

namespace PhoneBook
{
    internal class Entity
    {
        private int _id;

        // Constructor protected: Chỉ lớp con mới được gọi
        protected Entity(int id)
        {
            _id = id;
        }

        // Đổi thành public hoặc internal để Repository có thể dùng ID để tìm kiếm/xóa
        public int GetId()
        {
            return _id;
        }

        // Yêu cầu Đa hình (Polymorphism): Phương thức ảo để lớp con ghi đè
        // Mục đích: Kiểm tra xem đối tượng này có khớp với từ khóa tìm kiếm không
        public virtual bool Matches(string keyword)
        {
            // Mặc định trả về false, lớp con sẽ tự định nghĩa cách tìm
            return false;
        }
    }
}