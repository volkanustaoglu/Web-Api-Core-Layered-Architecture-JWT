using AppointmentApp.API.EmailServices;
using AppointmentApp.API.JwtToken;
using AppointmentApp.Business.Abstract;
using AppointmentApp.Business.AdditionalClasses;
using AppointmentApp.Data.MainParameters;
using AppointmentApp.Data.Models;
using AppointmentApp.Data.ResponseModel;
using AppointmentApp.Data.Tool;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        private IEmailSender _emailSender;
        public UserController(IUserService userService, IEmailSender emailSender)
        {
            this._userService = userService;
            this._emailSender = emailSender;
        }

        /// <summary>
        /// Get All User
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userService.GetAll();
            return Ok(ResultData.Get(result.IsSuccess, result.Message, result.Data));
        }

        /// <summary>
        /// Create User
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User entity)
        {
         
            var result = await _userService.Create(entity);
            return Ok(ResultData.Get(result.IsSuccess, result.Message, result.Data));
          
        }

        /// <summary>
        /// Post username-password or email-password
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserParameter entity)
        {
            var result = await _userService.LoginUserCheck(entity);
            if (result.IsSuccess == true)
            {
                result.Message = GenerateToken.GetToken();
                return Ok(ResultData.Get(result.IsSuccess, result.Message, result.Data));
            }
            return BadRequest();
        }

        /// <summary>
        /// Get User By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _userService.GetById(id);
            if (result.Data.Count() != 0)
            {
                return Ok(ResultData.Get(result.IsSuccess, result.Message, result.Data));
            }
            return NotFound();
        }

        /// <summary>
        /// Delete User By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _userService.Delete(id);
            return Ok(ResultData.Get(result.IsSuccess, result.Message, result.Data));
        }

        /// <summary>
        /// Update User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] User user)
        {
            var result = await _userService.Update(user);
            return Ok(ResultData.Get(result.IsSuccess, result.Message, result.Data));
        }

        /// <summary>
        /// GetUser
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUser([FromBody] User user)
        {
            var result = await _userService.GetUser(user);
            return Ok(ResultData.Get(result.IsSuccess, result.Message, result.Data));
        }

        /// <summary>
        /// UpdatePassword
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("UpdatePassword")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> UpdatePassword(string userId, string token) 
        {
            if (userId ==null || token == null)
            {
                return BadRequest();
            }

            var model = new UpdatePasswordModel { Token = token };

            return null;

        }


        /// <summary>
        /// UpdatePassword
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut("UpdatePassword")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordModel entity)
        {
            var user = await _userService.GetByEmail(entity.Email);
            if (user.Data.Count ==0)
            {
                return BadRequest();
            }
            var result = await _userService.UpdatePassword(entity);

            return Ok(ResultData.Get(result.IsSuccess, result.Message, result.Data));

        }

        /// <summary>
        /// User Register
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost("RegisterUser")]
        public async Task<IActionResult> RegisterUser([FromBody] User entity)
        {
            entity.EmailConfirmToken = ConfirmEmailToken.RandomStringConfirmEmail();

            var result = await _userService.RegisterUser(entity);


            var url = Url.Action("ConfirmEmail", "User", new
            {

                userId = result.ReturnId,
                token = entity.EmailConfirmToken
            });
            try
            {
                await _emailSender.SendEmailAsync(entity.Email, "Hesabınızı Onaylayınız.", $"Lütfen email hesabınızı onaylamak için linke <a href='http://localhost:20763{url}'>tıklayınız.</a>");
            }
            catch (Exception ex)
            {

                WebLogger.ApiError.Error(ex);
                result.IsSuccess = false;
            }
           

            return Ok(ResultData.Get(result.IsSuccess, result.Message, result.Data));

        }


        /// <summary>
        /// Post Confirm Email
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("ConfirmEmail")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return BadRequest();
            }
            
            var user = await _userService.GetById(Convert.ToInt32(userId));

            if (user.Data.Count ==0)
            {
                return BadRequest();
            }
            else
            {
                if (user.Data[0].EmailConfirmToken == token)
                {
                    var result = await _userService.UpdateEmailConfirm(userId, token);

                    return Ok(ResultData.Get(result.IsSuccess, result.Message, result.Data));
                }
                return BadRequest();
            }

        }

        /// <summary>
        /// Forget Password Post Email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
       [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword(string email)  
        {
            var newCode = ConfirmEmailToken.RandomStringConfirmEmail();
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest();
            }
            var result = await _userService.GetByEmail(email);
            var code = result.Data[0].EmailConfirmToken;
            var userId = result.Data[0].Id;

            await _userService.UpdateEmailConfirmToken(userId.ToString(), code, newCode);
            var user = await _userService.GetById(userId);
            var getNewCode = user.Data[0].EmailConfirmToken;

            if (result.Data.Count ==0)
            {
                return BadRequest();
            }
            
            var url = Url.Action("UpdatePassword", "User", new
            {

                userId = userId,
                token = getNewCode
            });

            try
            {
                await _emailSender.SendEmailAsync(email, "Reset Password.", $"Parolanızı yenilemek için linke <a href='http://localhost:20763{url}'>tıklayınız.</a>");
            }
            catch (Exception ex)
            {

                WebLogger.ApiError.Error(ex);
                result.IsSuccess = false;
            }
          
          return Ok(ResultData.Get(result.IsSuccess, result.Message, result.Data));




        }

    }
}
