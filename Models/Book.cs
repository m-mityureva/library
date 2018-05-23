using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    [Table("Books")]
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a book title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter an author")]
        public string Author { get; set; }

        [ForeignKey("GenreId")]
        public Genre Genre { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a positive count")]
        public int Count { get; set; }
        
        [BindNever]
        public int Available { get; set; }
    }
}
