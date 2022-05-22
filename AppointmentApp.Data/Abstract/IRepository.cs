using AppointmentApp.Data.Models;
using AppointmentApp.Data.Tool;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentApp.Data.Abstract
{
    public interface IRepository<T>
    {
        Task<ProjectResult<List<T>>> GetById(int id);
        Task<ProjectResult<List<T>>> GetAll();
        Task<ProjectResult<List<T>>> Create(T entity);
        Task<ProjectResult<List<T>>> Update(T entity);
        Task<ProjectResult<List<T>>> Delete(int id);
    }
}
