
namespace AppointmentApp.Data.Models
{
    public class BlogComment
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public int UserId { get; set; }
        public int BlogId { get; set; }
        public string Description { get; set; }
    }
}
