using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Bookstore.Models;
using Bookstore.Models.Repositories;
using Bookstore.ViewModels;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Bookstore.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookstoreRepository<Book> bookRepository;
        private readonly IBookstoreRepository<Author> authorRepository;
        private readonly IWebHostEnvironment hosting;
        
        public BookController(IBookstoreRepository<Book> bookRepository,
         IBookstoreRepository<Author> authorRepository,IWebHostEnvironment hosting)
        {
            this.bookRepository = bookRepository;
            this.authorRepository = authorRepository;
            this.hosting = hosting;
        }
        // GET: Book
        public ActionResult Index()
        {
            var books = bookRepository.List();
            return View(books);
        }

        // GET: Book/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Book/Create
        public ActionResult Create()
        {
            var model = new BookAuthorViewModel
            {
                Authors = FillSelectList()
            };
            return View(model);
        }

        // POST: Book/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookAuthorViewModel model)
        {
            var vmodel = new BookAuthorViewModel
            {
                Authors = FillSelectList()
            };
            if(ModelState.IsValid)
            {
                try
                {
                    string fileName = string.Empty;
                    if (model.File != null) 
                    {
                        string uploads = Path.Combine(hosting.WebRootPath,"uploads");
                        fileName = model.File.FileName;
                        string fullPath = Path.Combine(uploads, fileName);
                        model.File.CopyTo(new FileStream(fullPath, FileMode.Create));
                    }
                    if (model.AuthorId == -1)
                    {
                        ViewBag.Message = "Please Select an author from the list";
                        
                        return View(vmodel);
                    }
                    Book book = new Book
                    {
                        Id = model.BookId,
                        Title = model.Title,
                        Description = model.Description,
                        Author = authorRepository.Find(model.AuthorId),
                        ImageUrl = model.File.FileName
                    };
                    bookRepository.Add(book);

                    return RedirectToAction(nameof(Index));
                } 
                catch
                {
                    return View(vmodel);
                }
        
            }

            ModelState.AddModelError("","You Have To fill all the required fields");
            
            return View(vmodel);
        }

        // GET: Book/Edit/5
        public ActionResult Edit(int id)
        {
            var book = bookRepository.Find(id);
            var authorId = book.Author == null ? book.Author.Id = 0 : book.Author.Id;

            var viewModel = new BookAuthorViewModel
            {
                BookId = book.Id,
                Title = book.Title,
                Description = book.Description,
                AuthorId = authorId,
                Authors = FillSelectList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id,BookAuthorViewModel viewModel)
        {
            try
            {
                Console.WriteLine("Error");
                var book = new Book
                {
                    Id = viewModel.BookId,
                    Title = viewModel.Title,
                    Description = viewModel.Description,
                    Author = authorRepository.Find(viewModel.AuthorId)
                };

                bookRepository.Update(viewModel.AuthorId,book);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                Console.WriteLine("Error");
                return View();
            }
        }

        // GET: Book/Delete/5
        public ActionResult Delete(int id)
        {
            var book = bookRepository.Find(id);
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Book book)
        {
            try
            {
                bookRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        List<Author> FillSelectList()
        {
            var authors = authorRepository.List().ToList();
            authors.Insert(0, new Author { Id=-1 , FullName="... Please select an author ..."});

            return authors;
        }
    }
}