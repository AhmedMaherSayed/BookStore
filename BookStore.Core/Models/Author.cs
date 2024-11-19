namespace BookStore.Core.Models
{
    public class Author : BaseEntity<int>
    {
        public string FullName { get; set; }
        public string Bio { get; set; }
        public int Age { get; set; }
        public int NumberOfBooks { get; set; }
        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    }
}