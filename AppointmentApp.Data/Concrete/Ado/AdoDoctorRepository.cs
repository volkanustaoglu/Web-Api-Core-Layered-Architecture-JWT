using AppointmentApp.Data.Abstract;
using AppointmentApp.Data.Methods;
using AppointmentApp.Data.Models;
using AppointmentApp.Data.Tool;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppointmentApp.Data.Concrete.Ado
{
    public class AdoDoctorRepository : IDoctorRepository
    {
        public async Task<ProjectResult<List<Doctor>>> Create(Doctor entity)
        {
            return await DoctorManager.Instance.Create(entity);
        }

        public async Task<ProjectResult<List<Doctor>>> Delete(int id)
        {
            return await DoctorManager.Instance.Delete(id);
        }

        public async Task<ProjectResult<List<Doctor>>> GetAll()
        {
            return await DoctorManager.Instance.GetAll();
        }

        public async Task<ProjectResult<List<Doctor>>> GetById(int id)
        {
            return await DoctorManager.Instance.GetById(id);
        }

        public async Task<ProjectResult<List<Doctor>>> Update(Doctor entity)
        {
            return await DoctorManager.Instance.Update(entity);
        }
    }
}
