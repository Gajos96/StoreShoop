using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Domain.Model.DTO
{
    public class DisplayModelBooks
    {
        public IEnumerable<Book> Books { get; set;}
        public IEnumerable<Genre> Genres { get; set; }

        public string STerm { get; set; } = "";
        public int GenreId { get; set; }


    }
}
