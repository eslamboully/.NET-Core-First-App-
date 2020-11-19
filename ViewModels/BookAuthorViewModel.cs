using System;
using System.Linq;
using System.Collections.Generic;
using Bookstore.Models;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Bookstore.ViewModels
{
    public class BookAuthorViewModel
    {
        public int BookId { get; set; }
        [Required,MinLength(5),MaxLength(20)]
        public string Title { get; set; }
        [Required,StringLength(120, MinimumLength=5)]
        public string Description { get; set; }
        public int AuthorId { get; set; }
        public List<Author> Authors { get; set; }
        public IFormFile File { get; set; }
    }
}