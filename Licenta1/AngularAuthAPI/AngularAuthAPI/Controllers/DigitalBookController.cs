using AngularAuthAPI.Context;
using AngularAuthAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AngularAuthAPI.Controllers
{ 
    [Route("api/[controller]")]
    [ApiController]
    public class DigitalBookController : Controller
    {
        private readonly AppDbContext _context;
        public DigitalBookController(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        [HttpPost ("add_digitalBook")]

        public IActionResult AddDigitalBook([FromBody] DigitalBook digitalBookObj)
        {
            try {  
                if(digitalBookObj == null)
                {
                    return BadRequest();
                }
                else 
                {
                   /* byte[] b = File.ReadAllBytes(digitalBookObj.ImageBook);
                    digitalBookObj.ImageBook = b;*/
                    _context.DigitalBooks.Add(digitalBookObj);
                    _context.SaveChanges();
                    return Ok(new
                    {
                        StatusCode=200,
                        Message="Book added Successfuly"
                    });
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        [HttpPut("update_digitalBook")]
        public IActionResult UpdateDigitalBook([FromBody] DigitalBook digitalBookObj)
        {
            if(digitalBookObj == null) {
                return BadRequest();
            }
            var book = _context.DigitalBooks.AsNoTracking().FirstOrDefault(x => x.Id_digitalBook == digitalBookObj.Id_digitalBook);
            if(book == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "User Not Found"
                });
            }
            else
            {
                _context.Entry(digitalBookObj).State=EntityState.Modified;
                _context.SaveChanges();
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Book Updated Successfuly"
                });
            }
        }

        [HttpDelete("delteDigitalBook/{id}")]
        public IActionResult DeleteDigitalBook(int id)
        {
            var book = _context.DigitalBooks.Find(id);
            if(book == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "User Not Found"
                });
            }
            else
            {
                _context.Remove(book);
                _context.SaveChanges();
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Book Deleted Successfuly"
                });
            }
        }

        [HttpGet("get_all_digitalBooks")]
        public IActionResult GetAllBooks()
        {
            var book = _context.DigitalBooks.AsQueryable();
            return Ok(new
            {
                StatusCode = 200,
                BooksDetail = book
            }); 
        }

        [HttpGet("get_digitalBooks/id")]
        public IActionResult GetDigitalBook(int id)
        {
            var book = _context.DigitalBooks.Find(id);
            if (book == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "Book Not Found"
                });
            }
            else
            {
                return Ok(new
                {
                    StatusCode = 200,
                    DigitalBook = book
                });
            }
        }


        [HttpGet("get_digitalBooks_byUser")]
        public IActionResult GetDigitalBooksByUser(string userEmail)
        {
            var books = _context.DigitalBooks.Where(book => book.Email == userEmail).ToList();
            if (books.Count == 0)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "No books found"
                });
            }
            else
            {
                return Ok(new
                {
                    StatusCode = 200,
                    DigitalBooks = books
                });
            }
        }

    }
}
