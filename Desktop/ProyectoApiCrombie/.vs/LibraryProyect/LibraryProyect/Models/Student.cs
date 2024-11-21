namespace LibraryProyect.Models
{
    public class Student : User
    {
        public const string  UserType = "Student";

        public const int MaxBooksOnLoan = 3;

        

        public Student(int id, string name) : base(id, name) { }


        public override void WithdrawBook(int id, int isbn, Library library)
        {

            if (library.books[isbn].Available == true) {  

            if (MaxBooksOnLoan > library.users[id].books.Count)
                {

                    library.users[id].books.Add(isbn, library.books[isbn]);
                    library.books[isbn].Available = false;
                    library.users[id].books[isbn].ReturnDate = DateTime.Now.AddDays(15);

                }
            }
        }
        public override void ReturnBooks(int id, int isbn, Library library)
        {

            if (library.users[id].books.ContainsKey(isbn)) 
            {
                
                library.users[id].books.Remove(isbn);
                library.books[isbn].Available = true;
            
            }

        }
    }
}
