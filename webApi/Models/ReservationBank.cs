namespace BookShopWebApi.Models
{
    public class ReservationBank
    {
        public int ReservationId { get; set; }
        public Reservation? Reservation { get; set; }

        public int BookId { get; set; }
        public Book? Book { get; set; }


    }
}
