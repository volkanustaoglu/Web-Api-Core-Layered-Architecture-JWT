using AppointmentApp.Business.Abstract;
using AppointmentApp.Data.Abstract;
using AppointmentApp.Data.Models;
using AppointmentApp.Data.Tool;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppointmentApp.Business.Concrete
{
    public class BusinessBlogCommentsManager : IBlogCommentsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BusinessBlogCommentsManager(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }


        public async Task<ProjectResult<List<BlogComment>>> Create(BlogComment entity)
        {
            return await _unitOfWork.BlogComments.Create(entity);
        }

        public async Task<ProjectResult<List<BlogComment>>> Delete(int id)
        {
            return await _unitOfWork.BlogComments.Delete(id);
        }

        public async Task<ProjectResult<List<BlogComment>>> GetAll()
        {
            return await _unitOfWork.BlogComments.GetAll();
        }

        public async Task<ProjectResult<List<BlogComment>>> GetBlogCommentsByBlogId(int blogId)
        {
            return await _unitOfWork.BlogComments.GetBlogCommentsByBlogId(blogId);
        }

        public async Task<ProjectResult<List<BlogComment>>> GetById(int id)
        {
            return await _unitOfWork.BlogComments.GetById(id);
        }

        public async Task<ProjectResult<List<BlogComment>>> Update(BlogComment entity)
        {
            return await _unitOfWork.BlogComments.Update(entity);
        }
    }
}
