namespace DataAccessObjects.Dtos
{
    public class CreateNewsDto
    {
        public string NewsArticleId { get; set; } = null!;

        public string? NewsTitle { get; set; }

        public string Headline { get; set; } = null!;

        public string NewsContent { get; set; }

        public string? NewsSource { get; set; }

        public short CategoryId { get; set; }

        public bool NewsStatus { get; set; }

        public short? CreatedById { get; set; }
    }
}
