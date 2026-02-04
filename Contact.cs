using System;

namespace PhoneBook
{
    internal class Contact : Entity
    {
        private string _name;
        private string _phone;
        private string _email;
        private string[] _tags; // Mảng lưu các tag
        private int _tagCount;  // Số lượng tag thực tế đang có
        private int _maxTags;   // Giới hạn số tag (do mảng cố định)

        public Contact(int id, int maxTag) : base(id)
        {
            _maxTags = maxTag;
            _tags = new string[_maxTags];
            _tagCount = 0;
            _name = "";
            _email = "";
            _phone = "";
        }

        // --- Getters ---
        public string GetName() => _name;
        public string GetPhone() => _phone;
        public string GetEmail() => _email;
        public int GetTagCount() => _tagCount;

        // Trả về chuỗi các tag để hiển thị (ví dụ: "gia-dinh, ban-be")
        public string GetTagsString()
        {
            string result = "";
            for (int i = 0; i < _tagCount; i++)
            {
                result += _tags[i];
                if (i < _tagCount - 1)
                    result += ", ";
            }
            return result;
        }

        // --- Setters & Validation ---

        public void SetName(string name)
        {
            string cleanName = TrimSafe(name);
            if (cleanName.Length == 0)
                throw new Exception("Ten khong duoc de trong.");
            _name = cleanName;
        }

        public void SetPhone(string phone)
        {
            string cleanPhone = TrimSafe(phone);
            if (!ValidatePhone(cleanPhone))
                throw new Exception("So dien thoai khong hop le (10-12 so, khong chua chu, co the bat dau bang +).");
            _phone = cleanPhone;
        }

        public void SetEmail(string email)
        {
            string cleanEmail = TrimSafe(email);
            // Email cho phép rỗng
            if (cleanEmail.Length > 0)
            {
                // Validate cơ bản: Có @ và có dấu . sau @
                int atIndex = -1;
                int dotIndex = -1;

                // Tìm vị trí @
                for (int i = 0; i < cleanEmail.Length; i++)
                {
                    if (cleanEmail[i] == '@')
                    {
                        atIndex = i;
                        break;
                    }
                }

                // Tìm vị trí . (phải nằm sau @)
                if (atIndex != -1)
                {
                    for (int i = atIndex + 1; i < cleanEmail.Length; i++)
                    {
                        if (cleanEmail[i] == '.')
                        {
                            dotIndex = i;
                            break;
                        }
                    }
                }

                if (atIndex == -1 || dotIndex == -1)
                    throw new Exception("Email khong hop le (phai co @ va dau cham sau @).");
            }
            _email = cleanEmail;
        }

        public void SetTags(string tagsInput)
        {
            if (string.IsNullOrEmpty(tagsInput))
            {
                _tagCount = 0;
                return;
            }

            // Reset tags
            _tagCount = 0;

            // Tách chuỗi thủ công hoặc dùng Split
            string[] rawTags = tagsInput.Split(',');

            for (int i = 0; i < rawTags.Length; i++)
            {
                if (_tagCount >= _maxTags) break; // Đầy bộ nhớ tag

                string t = TrimSafe(rawTags[i]);
                if (t.Length > 0 && !HasTag(t)) // Không thêm rỗng, không thêm trùng
                {
                    _tags[_tagCount] = t;
                    _tagCount++;
                }
            }
        }

        // --- Helpers ---

        // Kiểm tra xem contact này đã có tag này chưa (để tránh trùng lặp nội bộ)
        public bool HasTag(string tag)
        {
            tag = TrimSafe(tag).ToLower();
            for (int i = 0; i < _tagCount; i++)
            {
                if (_tags[i].ToLower() == tag)
                    return true;
            }
            return false;
        }

        private string TrimSafe(string s)
        {
            if (s == null) return "";
            return s.Trim();
        }

        private bool ValidatePhone(string s)
        {
            if (string.IsNullOrEmpty(s)) return false;
            if (s.Length < 10 || s.Length > 12) return false;

            int start = 0;
            if (s[0] == '+') start = 1;

            for (int i = start; i < s.Length; i++)
            {
                if (s[i] < '0' || s[i] > '9') return false;
            }
            return true;
        }

        // --- Polymorphism Implementation ---

        // Ghi đè phương thức Matches của lớp Entity
        public override bool Matches(string keyword)
        {
            if (string.IsNullOrEmpty(keyword)) return true;

            keyword = keyword.ToLower();

            // Kiểm tra Name
            if (_name.ToLower().Contains(keyword)) return true;

            // Kiểm tra Phone
            if (_phone.Contains(keyword)) return true; // Phone thường không cần lower

            // Kiểm tra Email
            if (_email.ToLower().Contains(keyword)) return true;

            // Kiểm tra Tags
            for (int i = 0; i < _tagCount; i++)
            {
                if (_tags[i].ToLower().Contains(keyword)) return true;
            }

            return false;
        }

        public void DisplayString()
        {
            Console.WriteLine($"Id={GetId()} | {_name} | {_phone} | {_email} | Tags: {GetTagsString()}");
        }
    }
}