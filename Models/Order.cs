using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using static Library.Models.Cart;

namespace Library.Models
{
    [Table("Orders")]
    public class Order
    {
        [BindNever]
        public int OrderId { get; set; }

        [BindNever]
        public ICollection<CartLine> Lines { get; set; }

        [BindNever]
        public bool Approved { get; set; }

        [BindNever]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter an address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please enter a city name")]
        public string City { get; set; }
        
        public string Zip { get; set; }

        [Required(ErrorMessage = "Please enter a country name")]
        public string Country { get; set; }
    }
}
