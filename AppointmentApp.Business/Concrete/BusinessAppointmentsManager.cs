using AppointmentApp.Business.Abstract;
using AppointmentApp.Data.Abstract;
using AppointmentApp.Data.Models;
using AppointmentApp.Data.Tool;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppointmentApp.Business.Concrete
{
    public class BusinessAppointmentsManager : IAppointmentsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BusinessAppointmentsManager(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }


        public async Task<ProjectResult<List<Appointment>>> Create(Appointment entity)
        {
            return await _unitOfWork.Appointments.Create(entity);
        }

        public async Task<ProjectResult<List<Appointment>>> Delete(int id)
        {
            return await _unitOfWork.Appointments.Delete(id);
        }

        public async Task<ProjectResult<List<Appointment>>> GetAll()
        {
            return await _unitOfWork.Appointments.GetAll();
        }

        public async Task<ProjectResult<List<Appointment>>> GetAppointmentByDoctorId(int doctorId)
        {
            return await _unitOfWork.Appointments.GetAppointmentByDoctorId(doctorId);
        }

        public async Task<ProjectResult<List<Appointment>>> GetAppointmentByUserId(int userId)
        {
            return await _unitOfWork.Appointments.GetAppointmentByUserId(userId);
        }

        public async Task<ProjectResult<List<Appointment>>> GetById(int id)
        {
            return await _unitOfWork.Appointments.GetById(id);
        }

        public async Task<ProjectResult<List<Appointment>>> Update(Appointment entity)
        {
            return await _unitOfWork.Appointments.Update(entity);
        }
    }
}
