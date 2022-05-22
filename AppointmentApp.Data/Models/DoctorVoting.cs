
namespace AppointmentApp.Data.Models
{
    public class DoctorVoting
    {
        public int Id{ get; set; }
        public int UserId{ get; set; }
        public int DoctorId { get; set; }
        public double Vote{ get; set; }
    }
}
