using System;
using System.ComponentModel.DataAnnotations;

namespace LibApp.Models
{
    public class Book
    {
        public int Id { get; set; }
		[Required(ErrorMessage = "Name is required")]
		[StringLength(255)]
		public string Name { get; set; }
		[Required(ErrorMessage = "Author name is required")]
		public string AuthorName { get; set; }
		public Genre Genre { get; set; }
		[Required(ErrorMessage = "Genre is required")]
		public byte GenreId { get; set; }
		public DateTime DateAdded { get; set; }
		[Required(ErrorMessage = "Release date is required")]
		public DateTime ReleaseDate { get; set; }
		[Range(1, 20, ErrorMessage = "Number in stock must be between 1 and 20")]
		[Required(ErrorMessage = "Number in stock is required")]
		public int NumberInStock { get; set; }
		public int NumberAvailable { get; set; }
	}
      
}
