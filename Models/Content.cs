namespace NishuPortFolio.Models
{
    public class Content
    {
        public int Id { get; set; } // Map to [ant]
        public string Title { get; set; } // Map to [title]
        public string Detail { get; set; } // Map to [detail]
    }

    public class list_project
    {
        public long atn { get; set; }
        public string title { get; set; }
        public string heading { get; set; }
        public string details { get; set; }

       
    }
}
