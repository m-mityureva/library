using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    [Table("CartLine")]
    public class CartLine
    {
        public int CartLineID { get; set; }
        public Book Book { get; set; }
        public int Quantity { get; set; }
    }
}
