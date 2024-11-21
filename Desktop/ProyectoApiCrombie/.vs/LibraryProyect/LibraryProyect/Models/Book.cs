namespace LibraryProyect.Models
{
    public class Book
    {

        public int ISBN { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public bool Available { get; set; }

        public DateTime? ReturnDate;

        public Book(int iSBN, string title, string author)
        {
            ISBN = iSBN;
            Title = title;
            Author = author;
            Available = true;
            
        }
    }
}
