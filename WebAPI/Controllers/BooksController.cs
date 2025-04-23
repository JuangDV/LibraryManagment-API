using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Repositories;
using DataAccessLayer.Entities;
using ApplicationLayer.Interfaces;
using Newtonsoft;
using ApplicationLayer.DTOs;
using System.Xml;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : Controller
    {
        private readonly IBookRepository _repo;
        public BooksController(IBookRepository repoBooks)
        {
            _repo = repoBooks;
        }

        [HttpGet]
        public async Task<ActionResult<(string, IEnumerable<Books>)>> GetAll(int page = 1) // se muestran 3 libros por pagina
        {

            var bookList = await _repo.GetAllAsync(page);
            return Ok(bookList.Page == -1 ? "NO se ha agregado ningún libro aún." : bookList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Books>> GetById(int id)
        {
            if(await _repo.GetByIdAsync(id) != null)
            {
                var book = await _repo.GetByIdAsync(id);
                return Ok(book);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] WithoutIdBookRequest bookDto)
        {
            // Evitar que el frontend manipule el ID
            var book = new Books(0, bookDto.Title, bookDto.Author, bookDto.YearPublication, bookDto.ISBN, bookDto.Genre, bookDto.Available);
            var errorMessage = await _repo.ValidateAsync(book);

            if (errorMessage.Equals(string.Empty))
            {
                await _repo.CreateAsync(book);
                return CreatedAtAction(nameof(GetById), new { id = book.Id }, Created().Location);
            }
            else
            {
                return BadRequest(errorMessage);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if(await _repo.GetByIdAsync(id) != null)
            {
                await _repo.DeleteAsync(id);
                return Ok();
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] WithoutIdBookRequest bookDto)
        {
            var book = await _repo.GetByIdAsync(id);
            if (book != null)
            {
                // Evitar que el frontend manipule el ID
                // Se edita la misma entidad obtenida para no provocar una excepción de doble trackeo
                book.Title = bookDto.Title;
                book.Author = bookDto.Author;
                book.YearPublication = bookDto.YearPublication;
                book.ISBN = bookDto.ISBN;
                book.Genre = bookDto.Genre;
                book.Available = bookDto.Available;

                var errorMessage = await _repo.ValidateAsync(book);

                if (errorMessage.Equals(string.Empty))
                {
                    await _repo.UpdateAsync(book);
                    return Ok();
                }
                else
                {
                    return BadRequest(errorMessage);
                }
            }
            return NotFound();
        }
    }
}
