using LibraryProyect.Models;
using LibraryProyect.Service;

public class LibraryService
{
    private readonly Library _library;
    private readonly ExcelDatabaseHandler _excelDatabaseHandler;
    private readonly UserService _userService;

    public LibraryService(Library library, ExcelDatabaseHandler excelDatabaseHandler, UserService userService)
    {
        _library = library;
        _excelDatabaseHandler = excelDatabaseHandler;
        _userService = userService;
        }

    public void AddBook(Book book)
    {
        if (_library.books.Any(b => b.ISBN == book.ISBN))
        {
            throw new InvalidOperationException("El libro ya existe en la biblioteca.");
        }



        _library.books.Add(book);
        _excelDatabaseHandler.AddBook(book);

    }

    public void AddUser(User user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user), "El usuario no puede ser nulo.");
        }

        if (_library.users.Any(u => u.Id == user.Id))
        {
            throw new InvalidOperationException("El usuario ya está registrado.");
        }

        _library.users.Add(user);
        _excelDatabaseHandler.AddUser(user);
    }

    public List<Book> ViewAllBooks()
    {
        return _library.books;
    }

    public List<User> ViewAllUsers()
    {
        return _library.users;
    }
}
