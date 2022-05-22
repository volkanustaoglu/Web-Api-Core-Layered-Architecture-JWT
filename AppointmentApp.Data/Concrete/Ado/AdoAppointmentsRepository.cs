using AppointmentApp.Data.Abstract;
using AppointmentApp.Data.Methods;
using AppointmentApp.Data.Models;
using AppointmentApp.Data.Tool;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppointmentApp.Data.Concrete.Ado
{
    public class AdoAppointmentsRepository : IAppointmentsRepository
    {
        public async Task<ProjectResult<List<Appointment>>> Create(Appointment entity)
        {
            return await AppointmentsManager.Instance.Create(entity);
        }

        public async Task<ProjectResult<List<Appointment>>> Delete(int id)
        {
            return await AppointmentsManager.Instance.Delete(id);
        }

        public async Task<ProjectResult<List<Appointment>>> GetAll()
        {
            return await AppointmentsManager.Instance.GetAll();
        }

        public async Task<ProjectResult<List<Appointment>>> GetAppointmentByDoctorId(int doctorId)
        {
            return await AppointmentsManager.Instance.GetAppointmentByDoctorId(doctorId);
        }

        public async Task<ProjectResult<List<Appointment>>> GetAppointmentByUserId(int userId)
        {
            return await AppointmentsManager.Instance.GetAppointmentByUserId(userId);
        }

        public async Task<ProjectResult<List<Appointment>>> GetById(int id)
        {
            return await AppointmentsManager.Instance.GetById(id);
        }

        public async Task<ProjectResult<List<Appointment>>> Update(Appointment entity)
        {
            return await AppointmentsManager.Instance.Update(entity);
        }
    }
}
