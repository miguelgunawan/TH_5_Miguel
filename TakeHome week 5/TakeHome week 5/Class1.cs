using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TakeHome_week_5
{
    internal class Product
    {
        public string Idproduct { get; set; }
        public string namaproduct { get; set; }
        public string hargaproduct { get; set; }
        public string stockproduct { get; set; }
        public string idcatagory { get; set; }
        public string namacatagory { get; set; }

        public Product(string namaproduct,string hargaproduct,string stockproduct,string namacatagory)
        {
            this.namaproduct = namaproduct;
            this.hargaproduct = hargaproduct;
            this.stockproduct = stockproduct;
            this.namacatagory = namacatagory;
        }
    }
}
