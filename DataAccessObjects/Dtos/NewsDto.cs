﻿using BusinessObjects;

namespace DataAccessObjects.Dtos
{
    public class NewsDto
    {
        public string NewsArticleId { get; set; } = null!;

        public string? NewsTitle { get; set; }

        public string Headline { get; set; } = null!;

        public DateTime? CreatedDate { get; set; }

        public string? NewsContent { get; set; }

        public string? NewsSource { get; set; }

        public short? CategoryId { get; set; }

        public bool? NewsStatus { get; set; }

        public short? CreatedById { get; set; }

        public short? UpdatedById { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string? AuthorName { get; set; }
    }
}
