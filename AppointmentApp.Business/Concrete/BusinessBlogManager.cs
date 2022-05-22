using AppointmentApp.Business.Abstract;
using AppointmentApp.Data.Abstract;
using AppointmentApp.Data.Models;
using AppointmentApp.Data.Tool;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppointmentApp.Business.Concrete
{
    public class BusinessBlogManager : IBlogService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BusinessBlogManager(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }


        public async Task<ProjectResult<List<Blog>>> Create(Blog entity)
        {
            return await _unitOfWork.Blogs.Create(entity);
        }

        public async Task<ProjectResult<List<Blog>>> Delete(int id)
        {
            return await _unitOfWork.Blogs.Delete(id);
        }

        public async Task<ProjectResult<List<Blog>>> GetAll()
        {
            return await _unitOfWork.Blogs.GetAll();
        }

        public async Task<ProjectResult<List<Blog>>> GetBlogByUserId(int userId)
        {
            return await _unitOfWork.Blogs.GetBlogByUserId(userId);
        }

        public async Task<ProjectResult<List<Blog>>> GetById(int id)
        {
            return await _unitOfWork.Blogs.GetById(id);
        }

        public async Task<ProjectResult<List<Blog>>> Update(Blog entity)
        {
            return await _unitOfWork.Blogs.Update(entity);
        }
    }
}
