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
    public class DoctorVotingController : ControllerBase
    {
        private IDoctorVotingService _doctorVotingService;
        public DoctorVotingController(IDoctorVotingService doctorVotingService)
        {
            this._doctorVotingService = doctorVotingService;
        }


        /// <summary>
        /// Get All DoctorVoting
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _doctorVotingService.GetAll();
            return Ok(ResultData.Get(result.IsSuccess, result.Message, result.Data));
        }

        /// <summary>
        /// Create DoctorVoting
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DoctorVoting doctorVoting)
        {
            var result = await _doctorVotingService.Create(doctorVoting);
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
            var result = await _doctorVotingService.GetById(id);
            if (result.Data.Count() != 0)
            {
                return Ok(ResultData.Get(result.IsSuccess, result.Message, result.Data));
            }
            return NotFound();
        }

        /// <summary>
        /// Delete DoctorVoting By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _doctorVotingService.Delete(id);
            return Ok(ResultData.Get(result.IsSuccess, result.Message, result.Data));
        }

        /// <summary>
        /// Update DoctorVoting
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] DoctorVoting doctorVoting)
        {
            var result = await _doctorVotingService.Update(doctorVoting);
            return Ok(ResultData.Get(result.IsSuccess, result.Message, result.Data));
        }

        /// <summary>
        /// GetDoctorVotingByUserId
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpGet("GetDoctorVotingByUserId")]
        public async Task<IActionResult> GetDoctorVotingByUserId(int id)
        {
            var result = await _doctorVotingService.GetDoctorVotingByUserId(id);
            return Ok(ResultData.Get(result.IsSuccess, result.Message, result.Data));
        }
    }
}
