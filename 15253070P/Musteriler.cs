using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15253070P
{
    class Musteriler
    {
        public string ad, soyad;
        public int id;

        public Musteriler(int _id, string _ad, string _soyad)
        {
            id = _id;
            ad = _ad;
            soyad = _soyad;
        }

        public override string ToString()
        {
            return id + " - " + ad + " " + soyad;
        }
    }
}
