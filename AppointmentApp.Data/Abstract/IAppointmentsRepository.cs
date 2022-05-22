using AppointmentApp.Data.Models;
using AppointmentApp.Data.Tool;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppointmentApp.Data.Abstract
{
    public interface IAppointmentsRepository : IRepository<Appointment>
    {
        Task<ProjectResult<List<Appointment>>> GetAppointmentByUserId(int userId);
        Task<ProjectResult<List<Appointment>>> GetAppointmentByDoctorId(int doctorId);
    }
}
