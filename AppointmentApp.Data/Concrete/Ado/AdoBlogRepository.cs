using AppointmentApp.Data.Abstract;
using AppointmentApp.Data.Methods;
using AppointmentApp.Data.Models;
using AppointmentApp.Data.Tool;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppointmentApp.Data.Concrete.Ado
{
    public class AdoBlogRepository : IBlogRepository
    {
        public async Task<ProjectResult<List<Blog>>> Create(Blog blog)
        {
            return await BlogManager.Instance.Create(blog);
        }

        public async Task<ProjectResult<List<Blog>>> Delete(int id)
        {
            return await BlogManager.Instance.Delete(id);
        }

        public async Task<ProjectResult<List<Blog>>> GetAll()
        {
            return await BlogManager.Instance.GetAll();
        }

        public async Task<ProjectResult<List<Blog>>> GetBlogByUserId(int userId)
        {
            return await BlogManager.Instance.GetBlogByUserId(userId);
        }

        public async Task<ProjectResult<List<Blog>>> GetById(int id)
        {
            return await BlogManager.Instance.GetById(id);
        }

        public async Task<ProjectResult<List<Blog>>> Update(Blog blog)
        {
            return await BlogManager.Instance.Update(blog);
        }
    }
}
