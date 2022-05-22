using AppointmentApp.Business.Abstract;
using AppointmentApp.Data.Abstract;
using AppointmentApp.Data.Models;
using AppointmentApp.Data.Tool;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppointmentApp.Business.Concrete
{
    public class BusinessDoctorVotingManager : IDoctorVotingService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BusinessDoctorVotingManager(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }


        public async Task<ProjectResult<List<DoctorVoting>>> Create(DoctorVoting entity)
        {
            return await _unitOfWork.DoctorVotings.Create(entity);
        }

        public async Task<ProjectResult<List<DoctorVoting>>> Delete(int id)
        {
            return await _unitOfWork.DoctorVotings.Delete(id);
        }

        public async Task<ProjectResult<List<DoctorVoting>>> GetAll()
        {
            return await _unitOfWork.DoctorVotings.GetAll();
        }

        public async Task<ProjectResult<List<DoctorVoting>>> GetById(int id)
        {
            return await _unitOfWork.DoctorVotings.GetById(id);
        }

        public async Task<ProjectResult<List<DoctorVoting>>> GetDoctorVotingByUserId(int id)
        {
            return await _unitOfWork.DoctorVotings.GetDoctorVotingByUserId(id);
        }

        public async Task<ProjectResult<List<DoctorVoting>>> Update(DoctorVoting entity)
        {
            return await _unitOfWork.DoctorVotings.Update(entity);
        }
    }
}
