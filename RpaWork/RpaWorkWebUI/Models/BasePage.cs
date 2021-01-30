using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RpaWorkWebUI.Models
{
    public class BasePage
    {
        //Toplam item
        public int TotalItems { get; set; }

        //Sayfa Başına düşen item
        public int ItemsPerPage { get; set; }

        //bulunduğumuz sayfa
        public int CurrentPage { get; set; }

        //Bulunduğumuz Kategori.
        public string CurrentCategory { get; set; }

        public int TotalPages()
        {
            return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
        }
    }
}
