using LibraryProyect.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;


namespace LibraryProyect.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LibraryController : ControllerBase
        {
        private readonly ExcelDatabaseHandler _excelDbHandler;
        private readonly LibraryService _libraryService;
        public LibraryController(LibraryService libraryService, ExcelDatabaseHandler excelDbHandler)
        {
            _libraryService = libraryService;
            _excelDbHandler = excelDbHandler;
        }

        //Controladores para manejar los datos de los libros--------------------------------------------------------
        // POST: api/library/add-book
        [HttpPost("add-book")]
        public IActionResult AddBook([FromBody] Book book)
        {
            if (book == null || string.IsNullOrWhiteSpace(book.Title) || string.IsNullOrWhiteSpace(book.Author))
            {
                return BadRequest("Datos del libro inválidos.");
            }

            _libraryService.AddBook(book);
           
            return Ok(new { message = "Libro agregado exitosamente." });
        }


        // POST: api/library/register-user
       

        [HttpGet("all-books")]
            public IActionResult GetAllBooks()
            {
                var books = _libraryService.ViewAllBooks();
                return Ok(books);
            }


        //DELETE: 
        [HttpDelete("delete-book/{isbn}")]
            public IActionResult DeleteBook(int isbn)
            {
                try
                {
                    _excelDbHandler.DeleteBook(isbn);
                    return Ok(new { message = "Book deleted succesfully" });
                
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Error: {ex.Message}");
                }
            }
     


 //// GET: api/library/borrowed-books/{id}
        //[HttpGet("borrowed-books/{id}")]
        //public IActionResult GetBorrowedBooks(int id)
        //{
        //    var books = _libraryService.ViewBorrowedBooks(id);

        //    if (books == null || books.Count == 0)
        //    {
        //        return NotFound("No se encontraron libros prestados para este usuario.");
        //    }

        //    return Ok(books);
        //}

    //TODO hacer la logica de mostrar los libros desde la bd
    
     //Controlladores para hacer los manejos de los datos de los usuarios-------------------------------------------------------

       
       [HttpPost("register-user")]
            public IActionResult RegisterUser([FromBody] User user)
            {
                if (user == null || string.IsNullOrWhiteSpace(user.Name))
                {
                    return BadRequest("Datos del usuario inválidos.");
                }

                _libraryService.AddUser(user);
                _excelDbHandler.AddUser(user);

                return Ok(new { message = "Usuario registrado exitosamente." });
            }


        //Manejo de logica de borrar el usuario en caso de ser necesario. Recibe el id del usuario a borrar 
        [HttpDelete("delete-user/{id}")]

        public IActionResult DeleteUser(int id)
        {

            try
            {

                _excelDbHandler.DeleteUser(id);
                return Ok(new { message = "User deleted succesfully" });
            }
            catch (Exception ex)

            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        
        }



        [HttpGet("get-users")]

        public IActionResult GetUsers() 
        {

            var users = _excelDbHandler.GetUsers();
            return string.Join(", ", users.Select(user => user.ToString()));


        }
      
    }
}