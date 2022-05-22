using AppointmentApp.Data.Abstract;
using AppointmentApp.Data.MainParameters;
using AppointmentApp.Data.Methods;
using AppointmentApp.Data.Models;
using AppointmentApp.Data.Tool;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppointmentApp.Data.Concrete.Ado
{
    public class AdoUserRepository : IUserRepository
    {
        public async Task<ProjectResult<List<User>>> Create(User entity)
        {
            // ToDo : Bakılacak
            //return await UserManager.Instance.CreateUser(entity);

            return await UserManager.Instance.Create(entity);
        }

        public async Task<ProjectResult<List<User>>> Delete(int id)
        {
            return await UserManager.Instance.Delete(id);
        }

        public async Task<ProjectResult<List<User>>> GetAll()
        {
            return await UserManager.Instance.GetAll();
        }

        public async Task<ProjectResult<List<User>>> GetByEmail(string email)
        {
            return await UserManager.Instance.GetByEmail(email);
        }

        public async Task<ProjectResult<List<User>>> GetById(int id)
        {
            return await UserManager.Instance.GetById(id); 
        }

        public async Task<ProjectResult<List<User>>> GetUser(User entity)
        {
            return await UserManager.Instance.GetUser(entity);
        }

        public async Task<ProjectResult<List<UserParameter>>> LoginUserCheck(UserParameter entity)
        {
            return await UserManager.Instance.LoginUserCheck(entity);
        }

        public async Task<ProjectResult<List<User>>> RegisterUser(User entity)
        {
            return await UserManager.Instance.RegisterUser(entity);
        }

        public async Task<ProjectResult<List<User>>> Update(User entity)
        {
            return await UserManager.Instance.Update(entity);
        }

        public async Task<ProjectResult<List<User>>> UpdateEmailConfirm(string userId, string token)
        {
            return await UserManager.Instance.UpdateConfirmEmail(userId, token);
        }

        public async Task<ProjectResult<List<User>>> UpdateEmailConfirmToken(string userId, string token, string newToken)
        {
            return await UserManager.Instance.UpdateEmailConfirmToken(userId, token, newToken);
        }

        public async Task<ProjectResult<List<User>>> UpdatePassword(UpdatePasswordModel entity)
        {
            return await UserManager.Instance.UpdatePassword(entity);
        }
    }
}
