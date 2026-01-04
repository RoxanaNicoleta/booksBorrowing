using AngularAuthAPI.Context;
using AngularAuthAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AngularAuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : Controller
    {
        private readonly AppDbContext _context;
        public PostController(AppDbContext appDbContext)
        {
            _context= appDbContext;
        }

        [HttpPost("add_post")]
        public IActionResult AddPost([FromBody] PostModel postObj)
        {
            try
            {
                if(postObj == null)
                {
                    return BadRequest();
                }
                else
                {
                    _context.Posts.Add(postObj);
                    _context.SaveChanges();
                    return Ok(new
                    {
                        StatusCode=200,
                        Message="Post added Successfully"
                    });
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        [HttpPut("update_post")]
        public IActionResult UpdatePost([FromBody] PostModel postObj)
        {
            if (postObj == null)
            {
                return BadRequest();
            }
            var post = _context.Posts.AsNoTracking().FirstOrDefault(x => x.Id_post == postObj.Id_post);
            if(post==null)
            {
                return NotFound(new
                {
                    statusCode = 404,
                    Message = "Post Not found"
                });
            }
            else
            {
                _context.Entry(postObj).State=EntityState.Modified;
                _context.SaveChanges();
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Post updated Successfully"
                });
            }
        }

        [HttpDelete ("delete_post/{id}")]
        public IActionResult DeletePost(int id)
        {
            var post = _context.Posts.Find(id);
            if (post == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "post not Found"
                });
            }
            else
            {
                _context.Remove(post);
                _context.SaveChanges();
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "PostAPI Deleted"
                });
            }
        }

        [HttpGet("get_all_posts")]
        public IActionResult GetAllPost()
        {
            var post = _context.Posts.AsQueryable();
            return Ok(new
            {
                StatusCode = 200,
                PostDetails = post
            });
        }

        [HttpGet("get_post/id")]
        public IActionResult Getemployee(int id)
        {
            var post = _context.Posts.Find(id);
            if (post == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "Post Not Found"
                });
            }
            else
            {
                return Ok(new
                {
                    StatusCode = 200,
                    PostDetails = post
                });
            }
        }

        [HttpGet("get_data_post")]
        public async Task<ActionResult<IEnumerable<PostModel>>> Get()
        {
            var data = await _context.Posts.ToListAsync();
            return Ok(data);
        }


        [HttpGet("get_data_post_email")]
        public IActionResult GetDataPostByEmail(string email)
        {
            // Logica de preluare a datelor din baza de date
            var filteredData = _context.Posts.Where(d => d.Email == email).ToList();

            return Ok(filteredData);
        }


    }
}
