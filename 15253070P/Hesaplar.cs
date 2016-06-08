using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace _15253070P
{
    class Hesaplar
    {
        public int id, musteriNo;
        public decimal ekHesap, hesap, bakiye;


        public Hesaplar(int _id, int _musteriNo, decimal _ekHesap, decimal _hesap)
        {
            id = _id;
            musteriNo = _musteriNo;
            ekHesap = _ekHesap;
            hesap = _hesap;
            bakiye = _ekHesap + _hesap;
        }
        public override string ToString()
        {
            return "HesapNo:"+id + " Hesap:" + hesap + " EkHesap:" + ekHesap;
        }
        
    }
}
