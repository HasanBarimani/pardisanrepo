namespace Pardisan.Models
{
    public class Page : BaseModel
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string Content { get; set; }
    }
}