using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using LibraryProyect.Models;

public class ExcelDatabaseHandler
{
    private readonly string _filePath = "C:\\Users\\Nico\\Desktop\\ProyectoApiCrombie\\.vs\\LibraryProyect\\LibraryProyect\\LibraryData\\LibraryDb.xlsx";
    private string? _sheetName;


    //Manejo de los usuarios dentro de la bd -------------------------------------------------
    // Método para agregar un nuevo usuario fila al final de la hoja.
    //Recive un objeto tipo usuario y lo agrega al final de la bd.
    public void AddUser(User user)
    {

        _sheetName = "Users";

        using (XLWorkbook workbook = new XLWorkbook(_filePath))
        {
            var worksheet = workbook.Worksheet(_sheetName);

           
            // Encuentra la última fila usada
            int lastRow = worksheet.LastRowUsed()?.RowNumber() ?? 0;

            // Determina la nueva fila donde se agregarán los datos
            int newRow = lastRow + 1;

            // Agrega los valores a las celdas de la nueva fila


            worksheet.Cell(newRow, 1).Value = user.Id;
            worksheet.Cell(newRow, 2).Value = user.Name;
            worksheet.Cell(newRow, 3).Value = user.GetType().ToString();


            // Guarda el archivo actualizado
            workbook.SaveAs(_filePath);
        }
    }

    //Delete user. Recive el id del usuario a borrar y lo elimina de la base de datos
    public bool DeleteUser(int userId)
    {
        using (var workbook = new XLWorkbook(_filePath))
        {
            _sheetName = "Users";
            var worksheet = workbook.Worksheet(_sheetName);

            if (worksheet == null)
            {
                throw new Exception($"La hoja '{_sheetName}' no existe en el archivo Excel.");
            }

            
            var userRow = worksheet.RowsUsed().FirstOrDefault(row =>
            {
                var cellValue = row.Cell(1).GetValue<string>().Trim();// Encuentra la fila que contiene el ID del usuario

                
                return int.TryParse(cellValue, out int id) && id == userId;// Intentar convertir el valor a un entero, y compararlo con el userId
            });

            if (userRow != null)
            {
                
                userRow.Delete();// Elimina la fila encontrada
                workbook.SaveAs(_filePath); // Guarda el archivo con los cambios
                return true; // Usuario eliminado exitosamente
            }

                throw new Exception($"El usuario '{userId}' no existe en la base de datos."); // Usuario no encontrado
        }
    }

    public List<User> GetUsers() 
    {

        _sheetName = "Users";
        List<User> users = new List<User>();

        using (XLWorkbook workbook = new XLWorkbook(_filePath))
        {
            var worksheet = workbook.Worksheet(_sheetName);

            foreach (var row in worksheet.RowsUsed().Skip(1)) // Skip(1) omite la primera fila
            {

                users.Add(new User(row.Cell(1).GetValue<int>(), row.Cell(2).GetValue<string>())) ;                
                
            }
            return users;

        }
    }







    //Manejo de los datos de los libros dentro de la bd----------------------------------------
    //Add books. Recibe un objeto de tipo libro y lo guarda en la base de datos, lo agrega al final de la lista
    public void AddBook(Book book)
    {

        using (var workbook = new XLWorkbook(_filePath))
        {
            var worksheet = workbook.Worksheet("Books");
            int lastRow = worksheet.LastRowUsed()?.RowNumber() ?? 0;

            // Encuentra la última fila usada
            int newRow = lastRow + 1;
                        
            // Agrega los valores a las celdas de la nueva fila
            worksheet.Cell(newRow, 1).Value = book.ISBN;
            worksheet.Cell(newRow, 2).Value = book.Title;
            worksheet.Cell(newRow, 3).Value = book.Author;
            worksheet.Cell(newRow, 4).Value = book.Available ? "Available" : "On loan";


            workbook.Save();
        }
    }


    public void DeleteBook(int _isbn) 
    {

        using (XLWorkbook workbook = new XLWorkbook(_filePath))
        {
            _sheetName = "Books";
            var worksheet = workbook.Worksheet(_sheetName);

            if (worksheet == null)
            {
                throw new Exception($"La hoja '{_sheetName}' no existe en el archivo Excel.");
            }

            // Encuentra la fila que contiene el ID del usuario
            var userRow = worksheet.Rows().FirstOrDefault(row =>
            {
                var cellValue = row.Cell(1).Value.ToString(); // Obtener el valor de la celda como string

                // Intentar convertir el valor a un entero, y compararlo con el userId
                if (int.TryParse(cellValue, out int ISBN))
                {
                    return ISBN == _isbn;
                }
                return false;
            });

            if (userRow != null)
            {
                
                userRow.Delete();// Elimina la fila encontrada
                workbook.SaveAs(_filePath); // Guarda el archivo con los cambios
            }
            else
            {
                throw new Exception($"No se encontró un libro con el ID {_isbn}.");
            }
        }

    }



    //MAnejar los datos de prestar y devolver un libro-----------------------------------------

}