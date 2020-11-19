using System;
using System.Collections.Generic;
using System.Linq;

namespace Bookstore.Models.Repositories
{
    public class AuthorRepository : IBookstoreRepository<Author>
    {
        List<Author> authors;
        public AuthorRepository()
        {
            authors = new List<Author>()
            {
                new Author { Id=1,FullName="Abdelrahman Osama" },
                new Author { Id=2,FullName="Ahmed Osama" },
                new Author { Id=3,FullName="Abdo Osama" },
            };
        }
        public IList<Author> List()
        {
            return authors;
        }
        public Author Find(int id)
        {
            var author = authors.SingleOrDefault(b => b.Id == id);
            return author;
        }
        public void Add(Author entity)
        {
            entity.Id = authors.Max(b => b.Id) + 1;
            authors.Add(entity);
        }
        public void Update(int id,Author entity)
        {
            var author = Find(id);
            author.FullName = entity.FullName;
        }
        public void Delete(int id)
        {
            var author = Find(id);
            authors.Remove(author);
        }
    }
}