using BookShopWebApi.Helper;
using System.ComponentModel.DataAnnotations;

namespace BookShopWebApi.Models
{
    public class User
    {
        [Key]
        [Required]
        [MaxLength(50, ErrorMessageResourceName = "اندازه نام کاربری باید کمتر از ۵۰  کاراکتر باشد")]
        public string Username { get; set; } 
        [Required]
        public string Password { get; set; }
        [RegularExpression(".+@.+$")]
        public string? Email { get; set; }
        public string Role { get; set; } = UserRole.USER;
        public ICollection<Reservation>? Reservations { get; set; }

        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }

    }
}
