using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Repositories;
using DataAccessLayer.Entities;
using ApplicationLayer.Interfaces;
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

        /// <summary>
        /// Obtener los datos de todos los libros de una pagina especifica
        /// </summary>
        /// <param name="page"></param>
        /// <returns>Lista de <see cref="Books"/></returns>
        [HttpGet]
        public async Task<ActionResult<(string, IEnumerable<Books>)>> GetAll(int page = 1, int quantityBooks = 3)
        {

            var bookList = await _repo.GetAllAsync(page, quantityBooks);
            return Ok(bookList.Page == -1 ? "NO se ha agregado ningún libro aún." : bookList);
        }

        /// <summary>
        /// Obtener los datos de un libro especifico a partir de su identificador
        /// </summary>
        /// <param name="id"></param>
        /// <returns><see cref="Books"/> si existe, de lo contrario <see cref="NotFoundResult"/></returns>
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

        /// <summary>
        /// Crear un libro nuevo
        /// </summary>
        /// <param name="bookDto"></param>
        /// <returns>Url location si fue creado, de lo contrario <see cref="BadRequestResult"/> con el error</returns>
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

        /// <summary>
        /// Elimina un libro a partir de su identificador
        /// </summary>
        /// <param name="id"></param>
        /// <returns><see cref="OkResult"/> si fue eliminado, <see cref="NotFoundResult"/> si no fue encontrado</returns>
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

        /// <summary>
        /// Edita un libro a partir de su identificador
        /// </summary>
        /// <param name="id"></param>
        /// <param name="bookDto"></param>
        /// <returns><see cref="OkResult"/> si fue editado exitosamente, de lo contrario <see cref="BadRequestResult"/> con el error</returns>
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
