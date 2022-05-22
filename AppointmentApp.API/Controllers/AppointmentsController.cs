using AppointmentApp.Business.Abstract;
using AppointmentApp.Data.Abstract;
using AppointmentApp.Data.Models;
using AppointmentApp.Data.ResponseModel;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private IAppointmentsService _appointmentsService;
        public AppointmentsController(IAppointmentsService appointmentsService)
        {
            this._appointmentsService = appointmentsService;
        }


        /// <summary>
        /// Get All Appointment
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _appointmentsService.GetAll();
            return Ok(ResultData.Get(result.IsSuccess, result.Message, result.Data));
        }

        /// <summary>
        /// Create Appointment
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Appointment appointment)
        {
            var result = await _appointmentsService.Create(appointment);
            return Ok(ResultData.Get(result.IsSuccess, result.Message, result.Data));
        }

        /// <summary>
        /// Get Appointment By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _appointmentsService.GetById(id);
            if (result.Data.Count() != 0)
            {
                return Ok(ResultData.Get(result.IsSuccess, result.Message, result.Data));
            }
            return NotFound();
        }

        /// <summary>
        /// Delete Appointment By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _appointmentsService.Delete(id);
            return Ok(ResultData.Get(result.IsSuccess, result.Message, result.Data));
        }

        /// <summary>
        /// Update Appointment
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Appointment appointment)
        {
            var result = await _appointmentsService.Update(appointment);
            return Ok(ResultData.Get(result.IsSuccess, result.Message, result.Data));
        }

        ///// <summary>
        /// GetAppointmentByDoctorId
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpGet("GetAppointmentByDoctorId")]
        public async Task<IActionResult> GetAppointmentByDoctorId(int docturId)
        {
            var result = await _appointmentsService.GetAppointmentByDoctorId(docturId);
            return Ok(ResultData.Get(result.IsSuccess, result.Message, result.Data));
        }

        /// <summary>
        /// GetAppointmentByUserId
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpGet("GetAppointmentByUserId")]
        public async Task<IActionResult> GetAppointmentByUserId(int userId)
        {
            var result = await _appointmentsService.GetAppointmentByUserId(userId);
            return Ok(ResultData.Get(result.IsSuccess, result.Message, result.Data));
        }
    }
}
