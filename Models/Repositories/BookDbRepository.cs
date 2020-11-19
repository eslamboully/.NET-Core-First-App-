using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Models.Repositories
{
    public class BookDbRepository : IBookstoreRepository<Book>
    {
        BookstoreDbContext db;

        public BookDbRepository(BookstoreDbContext _db)
        {
            db = _db;
        }
        public IList<Book> List()
        {
            return db.Books.Include(a => a.Author).ToList();
        }
        public Book Find(int id)
        {
            var book = db.Books.Include(a => a.Author).SingleOrDefault(b => b.Id == id);
            return book;
        }
        public void Add(Book entity)
        {
            db.Books.Add(entity);
            db.SaveChanges();
        }
        public void Update(int id,Book entity)
        {
            var book = Find(id);
            book.Title = entity.Title;
            book.Description = entity.Description;
            book.Author = entity.Author;
            db.SaveChanges();
        }
        public void Delete(int id)
        {
            var book = Find(id);
            db.Books.Remove(book);
            db.SaveChanges();
        }
    }
}