namespace Book.API.Entities
{
    public class Book
    {
        public int ID { get; set; }
        public string Name { get; set; }=string.Empty; 
        public string Title { get; set; }= string.Empty;
        public string Description { get; set; } = string.Empty;
        public string AuthorName { get; set; }   =   string.Empty ;
    }
}
