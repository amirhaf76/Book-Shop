using BookShopWebApi.Data;
using BookShopWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace BookShopWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController : Controller
    {
        private readonly BookShopDbContext _dbContext;

        public BooksController(BookShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet(Name ="GetAll")]
        [AllowAnonymous]
        public IActionResult Get()
        {
            return Ok(_dbContext.Books);
        }

        /// <summary>
        /// just a summary.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Ok</response>
        /// <response code="404">The book is not found</response>
        /// <response code="400">Bad Request</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetById(int id)
        {
            Book? book = _dbContext.Books.Find(id);

            if (book is null)
                return NotFound($"There is not book like this id:{id}");

            return Ok(book);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Book book)
        {
            try
            {
                _dbContext.Books.Add(book);
                _dbContext.SaveChanges();

                return CreatedAtAction(nameof(Post), "The book is created successfully.");
                // return StatusCode(StatusCodes.Status201Created, "The book is created successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("ByImage")]
        public IActionResult PostByImage([FromForm] Book book)
        {
            try
            {
                string? path = null; 
                if (book.Image is not null)
                {
                    Guid guid = Guid.NewGuid();
                    
                    path = Path.Combine("wwwroot", book.Title + "." + guid + ".jpg");

                    using FileStream fs = new FileStream(path, FileMode.CreateNew, FileAccess.Write);

                    book.Image.CopyTo(fs);
                }
                
                book.ImageUrl = path?.Remove(0,7);

                _dbContext.Books.Add(book);
                _dbContext.SaveChanges();

                return CreatedAtAction(nameof(PostByImage), "The book is created successfully.");
                // return StatusCode(StatusCodes.Status201Created, "The book is created successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Book newBook)
        {
            try
            {
                Book? book = _dbContext.Books.Find(id);

                if (book is null)
                    return NotFound($"There is not book like this id:{id}");

                book.Title = newBook.Title ;
                book.PublishedDate = newBook.PublishedDate ;
                book.Pages = newBook.Pages ;
                book.Author = newBook.Author ;

                _dbContext.SaveChanges();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Book? book = _dbContext.Books.Find(id);

            if (book is null)
                return NotFound($"There is not book like this id:{id}");

            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();

            return Ok();
        }
    }
}
