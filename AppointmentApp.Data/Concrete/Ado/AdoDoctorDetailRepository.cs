using AppointmentApp.Data.Abstract;
using AppointmentApp.Data.Methods;
using AppointmentApp.Data.Models;
using AppointmentApp.Data.Tool;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppointmentApp.Data.Concrete.Ado
{
    public class AdoDoctorDetailRepository : IDoctorDetailRepository
    {
        public async Task<ProjectResult<List<DoctorDetail>>> Create(DoctorDetail doctorDetail)
        {
            return await DoctorDetailManager.Instance.Create(doctorDetail);
        }

        public async Task<ProjectResult<List<DoctorDetail>>> Delete(int id)
        {
            return await DoctorDetailManager.Instance.Delete(id);
        }

        public async Task<ProjectResult<List<DoctorDetail>>> GetAll()
        {
            return await DoctorDetailManager.Instance.GetAll();
        }

        public async Task<ProjectResult<List<DoctorDetail>>> GetById(int id)
        {
            return await DoctorDetailManager.Instance.GetById(id);
        }

        public async Task<ProjectResult<List<DoctorDetail>>> Update(DoctorDetail doctorDetail)
        {
            return await DoctorDetailManager.Instance.Update(doctorDetail);
        }
    }
}
