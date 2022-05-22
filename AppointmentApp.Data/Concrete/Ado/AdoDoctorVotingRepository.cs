using AppointmentApp.Data.Abstract;
using AppointmentApp.Data.Methods;
using AppointmentApp.Data.Models;
using AppointmentApp.Data.Tool;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppointmentApp.Data.Concrete.Ado
{
    public class AdoDoctorVotingRepository : IDoctorVotingRepository
    {
        public async Task<ProjectResult<List<DoctorVoting>>> Create(DoctorVoting entity)
        {
            return await DoctorVotingManager.Instance.Create(entity);
        }

        public async Task<ProjectResult<List<DoctorVoting>>> Delete(int id)
        {
            return await DoctorVotingManager.Instance.Delete(id);
        }

        public async Task<ProjectResult<List<DoctorVoting>>> GetAll()
        {
            return await DoctorVotingManager.Instance.GetAll();
        }

        public async Task<ProjectResult<List<DoctorVoting>>> GetById(int id)
        {
            return await DoctorVotingManager.Instance.GetById(id); // ToDo : GetById mevcut değil
        }

        public async Task<ProjectResult<List<DoctorVoting>>> GetDoctorVotingByUserId(int id)
        {
            return await DoctorVotingManager.Instance.GetDoctorVotingByUserId(id);
        }

        public async Task<ProjectResult<List<DoctorVoting>>> Update(DoctorVoting entity)
        {
            return await DoctorVotingManager.Instance.Update(entity); // ToDo : Update mevcut değil
        }
    }
}
