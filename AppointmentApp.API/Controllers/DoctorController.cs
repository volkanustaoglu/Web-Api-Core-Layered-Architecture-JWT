using AppointmentApp.Business.Abstract;
using AppointmentApp.Data.Models;
using AppointmentApp.Data.ResponseModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentApp.API.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private IDoctorService _doctorService;
        public DoctorController(IDoctorService doctorService)
        {
            this._doctorService = doctorService;
        }


        /// <summary>
        /// Get All Doctors
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _doctorService.GetAll();
            return Ok(ResultData.Get(result.IsSuccess, result.Message, result.Data));

        }

        /// <summary>
        /// Create Doctor
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Doctor entity)
        {
            var result = await _doctorService.Create(entity);
            return Ok(ResultData.Get(result.IsSuccess, result.Message, result.Data));
        }

        /// <summary>
        /// Get Doctor By Id
        /// </summary> 
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _doctorService.GetById(id);
            if (result.Data.Count() != 0)
            {
                return Ok(ResultData.Get(result.IsSuccess, result.Message, result.Data));
            }
            return NotFound();

        }

        /// <summary>
        /// Delete Doctor By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _doctorService.Delete(id);
            return Ok(ResultData.Get(result.IsSuccess, result.Message, result.Data));

        }

        /// <summary>
        /// Update Doctor
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Doctor entity)
        {
            var result = await _doctorService.Update(entity);
            return Ok(ResultData.Get(result.IsSuccess, result.Message, result.Data));
        }


    }
}
