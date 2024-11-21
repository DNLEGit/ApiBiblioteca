namespace LibraryProyect.Models
{
    public class User
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public Dictionary<int, Book> books = new Dictionary<int, Book>();
        public User(int id, string name)
        {
        
            this.Id = id;
            this.Name = name;
           

        }

        public virtual void WithdrawBook(int id, int isbn, Library library) { }

        public virtual void ReturnBooks(int id, int isbn, Library library) { }

    }
}
