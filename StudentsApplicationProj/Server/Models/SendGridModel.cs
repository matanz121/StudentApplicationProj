namespace StudentsApplicationProj.Server.Models
{
    public class SendGridModel
    {
        public string Subject { get; set; }
        public string To { get; set; }
        public string PlainText { get; set; }
        public string HtmlContent { get; set; }
    }
}
