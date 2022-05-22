using AppointmentApp.Business.Abstract;
using AppointmentApp.Data.Abstract;
using AppointmentApp.Data.Models;
using AppointmentApp.Data.Tool;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppointmentApp.Business.Concrete
{
    public class BusinessDoctorDetailManager : IDoctorDetailService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BusinessDoctorDetailManager(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }


        public async Task<ProjectResult<List<DoctorDetail>>> Create(DoctorDetail doctorDetail)
        {
            return await _unitOfWork.DoctorDetails.Create(doctorDetail);
        }

        public async Task<ProjectResult<List<DoctorDetail>>> Delete(int id)
        {
            var result = _unitOfWork.DoctorDetails.Delete(id);
            return await result;
        }

        public async Task<ProjectResult<List<DoctorDetail>>> GetAll()
        {

            var result = _unitOfWork.DoctorDetails.GetAll();
            return await result;
        }

        public async Task<ProjectResult<List<DoctorDetail>>> GetById(int id)
        {
            var result = _unitOfWork.DoctorDetails.GetById(id);
            return await result;
        }

        public async Task<ProjectResult<List<DoctorDetail>>> Update(DoctorDetail entity)
        {
            var result = _unitOfWork.DoctorDetails.Update(entity);
            return await result;
        }
    }
}
