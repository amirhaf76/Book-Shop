using BookShopWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BookShopWebApi.Data
{
    public class BookShopDbContext: DbContext
    {

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public BookShopDbContext(DbContextOptions<BookShopDbContext> options) : base(options)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ReservationBank>().HasKey(rb => new { rb.ReservationId, rb.BookId  });

            modelBuilder.Entity<ReservationBank>()
                .HasOne<Reservation>(rb => rb.Reservation )
                .WithMany(r => r.Reservations)
                .HasForeignKey(rb => rb.ReservationId);

            modelBuilder.Entity<ReservationBank>()
                .HasOne<Book>(rb => rb.Book)
                .WithMany(b => b.Reservations)
                .HasForeignKey(rb => rb.BookId);

        }
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Reservation> Reservations { get; set; }    


    }
}
