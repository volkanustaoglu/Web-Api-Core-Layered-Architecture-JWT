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
    public class BlogController : ControllerBase
    {
        private IBlogService _blogService;
        public BlogController(IBlogService blogService)
        {
            this._blogService = blogService;
        }


        /// <summary>
        /// Get All Blog
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _blogService.GetAll();
            return Ok(ResultData.Get(result.IsSuccess, result.Message, result.Data));
        }

        /// <summary>
        /// Create Blog
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Blog blog)
        {
            var result = await _blogService.Create(blog);
            return Ok(ResultData.Get(result.IsSuccess, result.Message, result.Data));
        }

        /// <summary>
        /// Get Blog By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _blogService.GetById(id);
            if (result.Data.Count() != 0)
            {
                return Ok(ResultData.Get(result.IsSuccess, result.Message, result.Data));
            }
            return NotFound();
        }

        /// <summary>
        /// Delete Blog By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _blogService.Delete(id);
            return Ok(ResultData.Get(result.IsSuccess, result.Message, result.Data));
        }

        /// <summary>
        /// Update Blog
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Blog blog)
        {
            var result = await _blogService.Update(blog);
            return Ok(ResultData.Get(result.IsSuccess, result.Message, result.Data));
        }

        /// <summary>
        /// GetBlogByUserId
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpGet("GetBlogByUserId")]
        public async Task<IActionResult> GetBlogByUserId(int userId)
        {
            var result = await _blogService.GetBlogByUserId(userId);
            return Ok(ResultData.Get(result.IsSuccess, result.Message, result.Data));
        }
    }
}
