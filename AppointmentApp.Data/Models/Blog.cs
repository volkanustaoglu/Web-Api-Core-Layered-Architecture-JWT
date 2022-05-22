
using System;

namespace AppointmentApp.Data.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public string Img { get; set; }
    }
}
