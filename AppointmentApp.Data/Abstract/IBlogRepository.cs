using AppointmentApp.Data.Models;
using AppointmentApp.Data.Tool;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppointmentApp.Data.Abstract
{
    public interface IBlogRepository : IRepository<Blog>
    {
        Task<ProjectResult<List<Blog>>> GetBlogByUserId(int userId);
    }
}
