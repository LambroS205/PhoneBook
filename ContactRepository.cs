using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook
{
    internal class ContactRepository
    {
        Contact[] _contactArr; //Danh sách contact
        int _count; //Đếm số contact
        int _nextId; //Id Contact tiếp theo
        int _maxTagsPerContact;
        public ContactRepository(int numberOfContact, int maxTagsPerContact)
        {
            _contactArr = new Contact[numberOfContact];
            _count = 0;
            _nextId = 1; //Id đầu tiên bằng 1
            _maxTagsPerContact = maxTagsPerContact;
        }
        public int GetCount() //Trả về số Contact
        {
            return _count;
        }
        public Contact CreateNewContact()
        {
            Contact contact = new Contact(_nextId, _maxTagsPerContact);
            _nextId++; //Sau khi tạo mới xong thì tăng id lên 1. Có thể dùng getCount nhưng không hay
            return contact;
        }

        public void Add(Contact contact) //Thêm 1 contact mới
        {
            if (_count >= _contactArr.Length)
            {
                throw new Exception("Danh ba da day!");
            }
            for (int i = 0; i < _count; i++)
            {
                if (_contactArr[i].GetPhone() == contact.GetPhone())
                    throw new Exception("Phone da ton tai!");
            }
            _contactArr[_count] = contact;
            _count++; //Tăng lên 1 (contact tiếp theo)
        }

        public void PrintAllContacts()
        {
            if (_count == 0)
                Console.WriteLine("Danh sach rong.");
            for (int i = 0; i < _count; i++)
            {
                _contactArr[i].DisplayString();
            }
        }

    }
}
