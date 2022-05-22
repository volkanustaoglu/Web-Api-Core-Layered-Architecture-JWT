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
    public class BlogCommentsController : ControllerBase
    {
        private IBlogCommentsService _blogCommentsService;
        public BlogCommentsController(IBlogCommentsService blogCommentsService)
        {
            this._blogCommentsService = blogCommentsService;
        }


        /// <summary>
        /// Get All BlogComment
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _blogCommentsService.GetAll();
            return Ok(ResultData.Get(result.IsSuccess, result.Message, result.Data));
        }

        /// <summary>
        /// Create BlogComment
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BlogComment blogComment)
        {
            var result = await _blogCommentsService.Create(blogComment);
            return Ok(ResultData.Get(result.IsSuccess, result.Message, result.Data));
        }

        /// <summary>
        /// GetById BlogComment
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _blogCommentsService.GetById(id);
            if (result.Data.Count() != 0)
            {
                return Ok(ResultData.Get(result.IsSuccess, result.Message, result.Data));
            }
            return NotFound();
        }

        /// <summary>
        /// Delete BlogComment
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _blogCommentsService.Delete(id);
            return Ok(ResultData.Get(result.IsSuccess, result.Message, result.Data));
        }

        /// <summary>
        /// Update BlogComment
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] BlogComment blogComment)
        {
            var result = await _blogCommentsService.Update(blogComment);
            return Ok(ResultData.Get(result.IsSuccess, result.Message, result.Data));
        }

        /// <summary>
        /// GetBlogCommentsByBlogId
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpGet("GetBlogCommentsByBlogId")]
        public async Task<IActionResult> GetBlogCommentsByBlogId(int blogId)
        {
            var result = await _blogCommentsService.GetBlogCommentsByBlogId(blogId);
            return Ok(ResultData.Get(result.IsSuccess, result.Message, result.Data));
        }
    }
}
