using System;
using System.Collections.Generic;
using System.Linq;

namespace Bookstore.Models.Repositories
{
    public class AuthorDbRepository : IBookstoreRepository<Author>
    {
        BookstoreDbContext db;
        public AuthorDbRepository(BookstoreDbContext _db)
        {
            db = _db;
        }
        public IList<Author> List()
        {
            return db.Authors.ToList();
        }
        public Author Find(int id)
        {
            var author = db.Authors.SingleOrDefault(b => b.Id == id);
            return author;
        }
        public void Add(Author entity)
        {
            db.Authors.Add(entity);
            db.SaveChanges();
        }
        public void Update(int id,Author entity)
        {
            db.Update(entity);
            db.SaveChanges();
        }
        public void Delete(int id)
        {
            var author = Find(id);
            db.Authors.Remove(author);
            db.SaveChanges();
        }
    }
}