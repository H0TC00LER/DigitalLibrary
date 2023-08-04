﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Description { get; set; }
        public string CoverUrl { get; set; }
        public Author Author { get; set; }
        public string Text { get; set; }
    }
}
