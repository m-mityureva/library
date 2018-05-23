using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    [Table("UserBook")]
    public class UserBook
    {
        public int Id { get; set; }
        public Book Book { get; set; }
        public int Quantity { get; set; }
        public int Returned { get; set; }
        public string UserName { get; set; }
        public DateTime IssuanceDate { get; set; } 
    }
}
