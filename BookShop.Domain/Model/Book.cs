using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Domain.Model
{
    [Table("Book")]
    public class Book
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string? BookName { get; set; }

        [Required]
        [MaxLength(50)]
        public string? AutorName { get; set; }
        public double Price { get; set; }
        public string? Image { get; set; }
        public int? GenreId { get; set; }
        public Genre Genre { get; set; }
        public List<OrderDetails> OrderDetail { get; set; }
        public List<CartDetail> CartDetail { get; set; }
        
        [NotMapped]
        public string GenreName { get; set; }

    }
}
