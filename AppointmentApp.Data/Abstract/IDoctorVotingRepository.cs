using AppointmentApp.Data.Models;
using AppointmentApp.Data.Tool;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppointmentApp.Data.Abstract
{
    public interface IDoctorVotingRepository : IRepository<DoctorVoting>
    {
    Task<ProjectResult<List<DoctorVoting>>> GetDoctorVotingByUserId(int id);
    }
}
