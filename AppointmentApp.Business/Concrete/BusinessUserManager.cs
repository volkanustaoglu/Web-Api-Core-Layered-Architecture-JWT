using AppointmentApp.Business.Abstract;
using AppointmentApp.Business.AdditionalClasses;
using AppointmentApp.Data.Abstract;
using AppointmentApp.Data.MainParameters;
using AppointmentApp.Data.Models;
using AppointmentApp.Data.Tool;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppointmentApp.Business.Concrete
{
    public class BusinessUserManager : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BusinessUserManager(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }


        public async Task<ProjectResult<List<User>>> Create(User entity)
        {
            entity.Password = PasswordHash.GetPasswordHash(entity);
            return await this._unitOfWork.Users.Create(entity);
        }

        public async Task<ProjectResult<List<User>>> Delete(int id)
        {
            return await _unitOfWork.Users.Delete(id);
        }

        public async Task<ProjectResult<List<User>>> GetAll()
        {
            return await _unitOfWork.Users.GetAll();
        }

        public async Task<ProjectResult<List<User>>> GetByEmail(string email)
        {
            return await _unitOfWork.Users.GetByEmail(email);
        }

        public async Task<ProjectResult<List<User>>> GetById(int id)
        {
            return await _unitOfWork.Users.GetById(id);
        }

        public async Task<ProjectResult<List<User>>> GetUser(User entity)
        {
            return await _unitOfWork.Users.GetUser(entity);
        }

        public async Task<ProjectResult<List<UserParameter>>> LoginUserCheck(UserParameter entity)
        {
            User user = new User();
            user.Password = entity.Password;
            entity.Password = PasswordHash.GetPasswordHash(user); // ToDo : Buradaki işlem hatayı önlemek için yapıldı. UserParameter iptali karar alındı.

            return await _unitOfWork.Users.LoginUserCheck(entity);
        }

        public async Task<ProjectResult<List<User>>> RegisterUser(User entity)
        {
            entity.Password = PasswordHash.GetPasswordHash(entity);
            return await this._unitOfWork.Users.RegisterUser(entity);
            
        }

        public async Task<ProjectResult<List<User>>> Update(User entity)
        {
            return await _unitOfWork.Users.Update(entity);
        }

        public async Task<ProjectResult<List<User>>> UpdateEmailConfirm(string userId, string token)
        {
            return await _unitOfWork.Users.UpdateEmailConfirm(userId, token);
        }

        public async Task<ProjectResult<List<User>>> UpdateEmailConfirmToken(string userId, string token, string newToken)
        {
            return await _unitOfWork.Users.UpdateEmailConfirmToken(userId, token, newToken);
        }

      

        public async Task<ProjectResult<List<User>>> UpdatePassword(UpdatePasswordModel entity)
        {
            return await _unitOfWork.Users.UpdatePassword(entity);
        }
    }
}
