
namespace AppointmentApp.Data.Models
{
    public class DoctorDetail
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public double Vote { get; set; }
        public string Subtitle { get; set; }
        public string Description { get; set; }
    }
}
