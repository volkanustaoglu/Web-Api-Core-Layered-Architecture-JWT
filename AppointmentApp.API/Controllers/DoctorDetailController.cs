using AppointmentApp.Business.Abstract;
using AppointmentApp.Data.Models;
using AppointmentApp.Data.ResponseModel;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorDetailController : ControllerBase
    {
        private IDoctorDetailService _doctorDetailService;
        public DoctorDetailController(IDoctorDetailService doctorDetailService)
        {
            this._doctorDetailService = doctorDetailService; 
        }


        /// <summary>
        /// Get All DoctorDetail
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _doctorDetailService.GetAll();
            return Ok(ResultData.Get(result.IsSuccess, result.Message, result.Data));
        }

        /// <summary>
        /// Create DoctorDetail
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DoctorDetail doctorDetail)
        {
            var result = await _doctorDetailService.Create(doctorDetail);
            return Ok(ResultData.Get(result.IsSuccess, result.Message, result.Data));
        }

        /// <summary>
        /// Get Doctor By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _doctorDetailService.GetById(id);
            if (result.Data.Count() != 0)
            {
                return Ok(ResultData.Get(result.IsSuccess, result.Message, result.Data));
            }
            return NotFound();
        }

        /// <summary>
        /// Delete DoctorDetail By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _doctorDetailService.Delete(id);
            return Ok(ResultData.Get(result.IsSuccess, result.Message, result.Data));
        }

        /// <summary>
        /// Update DoctorDetail
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] DoctorDetail doctorDetail)
        {
            var result = await _doctorDetailService.Update(doctorDetail);
            return Ok(ResultData.Get(result.IsSuccess, result.Message, result.Data));
        }
    }
}
