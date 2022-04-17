using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookShopWebApi.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="کتاب باید عنوان داشته باشد.")]
        public string? Title { get; set; }
        [Required]
        public string? Author { get; set; }
        public DateTime PublishedDate { get; set; }
       
        public int Pages { get; set; }

        [NotMapped]
        public IFormFile? Image { get; set; }

        public string? ImageUrl { get; set; }    
       
        public ICollection<ReservationBank>? Reservations { get; set; }
    }
}
