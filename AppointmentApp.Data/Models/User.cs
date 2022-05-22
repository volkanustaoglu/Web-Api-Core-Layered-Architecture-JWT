using AppointmentApp.Data.Enums;

namespace AppointmentApp.Data.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public UserTypes UserType { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Img { get; set; }
        public string EmailConfirmToken { get; set; }

    }
}
