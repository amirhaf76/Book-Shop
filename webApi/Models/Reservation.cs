using System.ComponentModel.DataAnnotations;

namespace BookShopWebApi.Models
{
    public class Reservation
    {
        public int Id { get; set; } 
        
        public User? User { get; set; }

        public DateTimeOffset CreatedDateTime { get; set; }

        public DateTimeOffset ExpireDateTime { get; set; }

        public ICollection<ReservationBank>? Reservations { get; set; }

        

    }
}
