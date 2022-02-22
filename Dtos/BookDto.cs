using System.ComponentModel.DataAnnotations;

namespace LibApp.Dtos
{
    public class BookDto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public GenreDto Genre { get; set; }
    }

    public class GenreDto
    {
        public byte Id { get; set; }
        public string Name { get; set; }
    }
}