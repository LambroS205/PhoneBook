using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook
{
    internal class Entity // Lớp cha
    {
        private int _id;//Biến cục bộ
        //public/protected
        protected Entity(int id) //protected: chỉ cho phép truy cập trong lớp kế thừa (tính đóng gói)
        {
            _id = id;
        }
        protected int GetId()
        {
            return _id;
        }
    }
}
