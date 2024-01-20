using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Domain.Model
{
    [Table("CartDetail")]
    public class CartDetail
    {
        public int Id { get; set; }
        [Required]
        public int ShoppingCartId { get; set; }
        public int BookId { get; set; }
        [Required]
        public int Quantity { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
        public Book Book { get; set; }
    }
}
