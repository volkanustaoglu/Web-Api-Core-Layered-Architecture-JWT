using AppointmentApp.Business.Abstract;
using AppointmentApp.Data.Abstract;
using AppointmentApp.Data.Models;
using AppointmentApp.Data.Tool;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppointmentApp.Business.Concrete
{
    public class BusinessDoctorManager : IDoctorService
    {

        private readonly IUnitOfWork _unitOfWork;

        public BusinessDoctorManager(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        
        public async Task<ProjectResult<List<Doctor>>> Create(Doctor entity)
        {
            return await _unitOfWork.Doctors.Create(entity);
        }

        public async Task<ProjectResult<List<Doctor>>> Delete(int id)
        {
            var result = _unitOfWork.Doctors.Delete(id);
            return await result;
        }

        public async Task<ProjectResult<List<Doctor>>> GetAll()
        {
           
            var result = _unitOfWork.Doctors.GetAll();
            return await result;
        }

        public async Task<ProjectResult<List<Doctor>>> GetById(int id)
        {
            var result = _unitOfWork.Doctors.GetById(id);
            return await result;
        }

        public async Task<ProjectResult<List<Doctor>>> Update(Doctor entity)
        {
            var result = _unitOfWork.Doctors.Update(entity);
            return await result;
        }
    }
}
