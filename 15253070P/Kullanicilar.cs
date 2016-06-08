using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15253070P
{
    class Kullanicilar
    {
        public int id;
        public string username;

        public Kullanicilar(int _id, string _username)
        {
            id = _id;
            username = _username;
        }
        public override string ToString()
        {
            return username;
        }
    }
}
