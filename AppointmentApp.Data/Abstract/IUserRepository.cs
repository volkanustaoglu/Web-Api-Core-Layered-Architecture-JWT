using AppointmentApp.Data.MainParameters;
using AppointmentApp.Data.Models;
using AppointmentApp.Data.Tool;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppointmentApp.Data.Abstract
{
    public interface IUserRepository :IRepository<User>
    {
        Task<ProjectResult<List<UserParameter>>> LoginUserCheck(UserParameter entity);
        Task<ProjectResult<List<User>>> UpdatePassword(UpdatePasswordModel entity);
        Task<ProjectResult<List<User>>> GetUser(User entity);
        Task<ProjectResult<List<User>>> RegisterUser(User entity);
        Task<ProjectResult<List<User>>> UpdateEmailConfirm(string userId, string token);
        Task<ProjectResult<List<User>>> GetByEmail(string email);
        Task<ProjectResult<List<User>>> UpdateEmailConfirmToken(string userId,string token, string newToken);
    }
}
