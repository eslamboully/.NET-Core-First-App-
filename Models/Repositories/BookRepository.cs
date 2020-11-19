using System;
using System.Collections.Generic;
using System.Linq;

namespace Bookstore.Models.Repositories
{
    public class BookRepository : IBookstoreRepository<Book>
    {
        List<Book> books;

        public BookRepository()
        {
            books = new List<Book>()
            {
                new Book
                {
                    Id=1, Title="C# Programming", Description="No description", Author = new Author()
                },

                new Book
                {
                    Id=2, Title="Java Programming", Description="Nothing", Author = new Author()
                },
                new Book
                {
                    Id=3, Title="Python Programming", Description="No Data", Author = new Author()
                },
            };
        }
        public IList<Book> List()
        {
            return books;
        }
        public Book Find(int id)
        {
            var book = books.SingleOrDefault(b => b.Id == id);
            return book;
        }
        public void Add(Book entity)
        {
            entity.Id = books.Max(b => b.Id) + 1;
            books.Add(entity);
        }
        public void Update(int id,Book entity)
        {
            var book = Find(id);
            book.Title = entity.Title;
            book.Description = entity.Description;
            book.Author = entity.Author;
        }
        public void Delete(int id)
        {
            var book = Find(id);
            books.Remove(book);
        }
    }
}