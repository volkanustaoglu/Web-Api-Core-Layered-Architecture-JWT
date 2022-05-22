using AppointmentApp.Data.Models;
using AppointmentApp.Data.Tool;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppointmentApp.Data.Abstract
{
    public interface IBlogCommentsRepository : IRepository<BlogComment>
    {
        Task<ProjectResult<List<BlogComment>>> GetBlogCommentsByBlogId(int blogId);
    }
}
