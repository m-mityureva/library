using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    [Table("ReturnRequest")]
    public class ReturnRequest
    {
        [BindNever]
        public int Id { get; set; }
        public UserBook UserBook { get; set; }
        public int Quantity { get; set; }
        public bool Approved { get; set; }
    }
}
