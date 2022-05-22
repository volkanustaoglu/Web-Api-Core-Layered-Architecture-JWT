using AppointmentApp.Data.Abstract;
using AppointmentApp.Data.Methods;
using AppointmentApp.Data.Models;
using AppointmentApp.Data.Tool;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppointmentApp.Data.Concrete.Ado
{
    public class AdoBlogCommentsRepository : IBlogCommentsRepository
    {
        public async Task<ProjectResult<List<BlogComment>>> Create(BlogComment entity)
        {
            return await BlogCommentsManager.Instance.Create(entity);
        }

        public async Task<ProjectResult<List<BlogComment>>> Delete(int id)
        {
            return await BlogCommentsManager.Instance.Delete(id);
        }

        public async Task<ProjectResult<List<BlogComment>>> GetAll()
        {
            return await BlogCommentsManager.Instance.GetAll();
        }

        public async Task<ProjectResult<List<BlogComment>>> GetBlogCommentsByBlogId(int blogId)
        {
            return await BlogCommentsManager.Instance.GetBlogCommentsByBlogId(blogId);
        }

        public async Task<ProjectResult<List<BlogComment>>> GetById(int id)
        {
            return await BlogCommentsManager.Instance.GetById(id);
        }

        public async Task<ProjectResult<List<BlogComment>>> Update(BlogComment entity)
        {
            return await BlogCommentsManager.Instance.Update(entity);
        }
    }
}
