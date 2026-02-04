using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook
{
    //Lớp Contact kế thừa lớp Entity
    //Lớp Contact là lớp con (lớp kế thừa)
    //Lớp Entity là lớp cha (lớp được kế thừa)
    //Lớp con được sử dụng lại toàn bộ mã nguồn của lớp cha
    internal class Contact : Entity
    {
        private string _name;
        private string _phone;
        private string _email;        
        private string[] _tags;//tag: gia-dinh, ban-be, cong-viec,..
        private int _tagCount;//for (int i=0; i< _tagCount; i++)
                                // print _tags[i]
        
        
        public Contact(int id, int maxTag) //maxTag: số tag tối đa của 1 contact (fixed luôn)
            : base(id)    //Khởi tạo id cho contact. base(id) gọi cấu tử lớp cha
        {
            _tagCount = 0;
            _name = "";
            _email = "";
            _phone = "";
            _tags = new string[maxTag]; //Số tag tối đa của 1 contact

        }

        public string GetName()
        {
            return _name;
        }
        public string GetPhone()
        {
            return _phone;
        }
        public string GetEmail()
        {
            return _email;
        }
        public int GetTagCount()
        {
            return _tagCount;
        }

        public void SetName(string name)
        {
            name = TrimSafe(name);
            if (name.Length == 0)
                throw new Exception("Ten khong duoc de trong");
            _name = name;
        }

        public void SetPhone(string phone)
        {
            phone = TrimSafe(phone);
            if (ValidatePhone(phone))
                _phone = phone;
        }
        //public 
        private string TrimSafe(string name)
        {
            if (name == null)
                return "";
            return name.Trim(); //Ten theo đúng chuẩn (không có dấu cách thừa)
        }
        private bool ValidatePhone(string s)
        {
            int start = 0;
            if (s == null) return false;
            if (s.Length < 10 || s.Length > 12) return false;
            if (s[0] == '+') //Nếu ký tự đầu là dấu +
                start = 1;
            for (int i = start; i < s.Length; i++)
            {
                if (s[i] < '0' || s[i] > '9') return false; //Chứa ký tự
            }
            return true;
        }

        public void DisplayString()
        {
            string tags = string.Empty; // cong-viec, ban-be
            for (int i = 0; i < _tagCount; i++)
            {
                tags += _tags[i];
                if (i < _tagCount - 1)
                    tags += ", ";
            }

            Console.Write("Id = " + GetId());
            Console.Write(" | Name = " + _name);
            Console.Write(" | Phone = " + _phone);
            Console.WriteLine(" | Tags: " + tags);
        }
    }
}
