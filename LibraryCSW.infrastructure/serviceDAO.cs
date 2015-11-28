using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Threading;

namespace LibraryCSW.infrastructure
{
    public class serviceDAO
    {
        LibraryEntities context = new LibraryEntities();

        #region Categories
        public Task<List<Category>> GetAllCategories()
        {
            return context.Category.ToListAsync();
        }

        public Task<List<Category>> GetCategory(int idCategory)
        {
            return context.Category.Where(c => c.Id == idCategory).ToListAsync();
        }
        public Task<List<Book>> GetAssignmentsCategories(int idCategory)
        {
            return context.Book.Where(c => c.IdCategory == idCategory).ToListAsync();
        }

        public void AddCategory(Category c)
        {
            context.Category.Add(c);
            context.SaveChanges();
        }

        public void DeleteCategory(Category c)
        {
            context.Category.Remove(c);
            context.SaveChanges();
        }

        public void UpdateCategory(Category categoryUpdated)
        {
            Category category = context.Category.Where(c => c.Id == categoryUpdated.Id).FirstOrDefault();
            if (category != null)
            {
                category.Name = categoryUpdated.Name;
                context.SaveChanges();
            }
        }
        #endregion

        #region Authors
        public Task<List<Author>> GetAllAuthors()
        {
            return context.Author.ToListAsync();
        }

        public Task<List<Author>> GetAuthor(int idAuthor)
        {
            return context.Author.Where(c => c.Id == idAuthor).ToListAsync();
        }
        public Task<List<Book>> GetAssignmentsAuthors(int idAuthor)
        {
            return context.Book.Where(c => c.Id == idAuthor).ToListAsync();
        }

        public void AddAuthor(Author a)
        {
            context.Author.Add(a);
            context.SaveChanges();
        }

        public void DeleteAuthor(Author a)
        {
            context.Author.Remove(a);
            context.SaveChanges();
        }

        public void UpdateAuthor(Author authorUpdated)
        {
            Author author = context.Author.Where(c => c.Id == authorUpdated.Id).FirstOrDefault();
            if (author != null)
            {
                author.Name = authorUpdated.Name;
                author.LastName = authorUpdated.LastName;
                author.IdCountry = authorUpdated.IdCountry;
                context.SaveChanges();
            }
        }
        #endregion

        #region Country
        public Task<List<Country>> GetCountry(int idCountry)
        {
            return context.Country.Where(c => c.Id == idCountry).ToListAsync();
        }

        public Task<List<Country>> GetAllCountry()
        {
            return context.Country.ToListAsync();
        }
        #endregion

        #region Book
        public async Task<List<BookFull>> GetAllBooksFull(int idAuthor)
        {
            List<Book> books=await context.Book.ToListAsync();
            if (idAuthor == 0)
                books = await context.Book.ToListAsync();
            else            
                books = context.Book.Where(b => b.IdAuthor == idAuthor).ToList();            
            List<BookFull> booksFull = new List<BookFull>();
            foreach (Book book in books)
            {
                BookFull bookFull = new BookFull();
                bookFull.IdBook = book.Id;
                bookFull.ISBN = book.ISBN;
                bookFull.Title = book.Title;
                bookFull.Publisher = book.Publisher;
                bookFull.IdAuthor = book.IdAuthor;

                List<Author> autor = await GetAuthor(book.IdAuthor);
                bookFull.Author = autor[0].Name + " " + autor[0].LastName;

                List<Category> category = await GetCategory(book.IdCategory);
                bookFull.Category = category[0].Name;

                List<Country> country = await GetCountry(autor[0].IdCountry);
                bookFull.Country = country[0].Code;

                booksFull.Add(bookFull);
            }
            return booksFull;
        }


        public void AddBook(Book b)
        {
            context.Book.Add(b);
            context.SaveChanges();
        }

        public void DeleteBook(Book b)
        {
            context.Book.Remove(b);
            context.SaveChanges();
        }

        public Task<List<Book>> GetBook(int idBook)
        {
            return context.Book.Where(c => c.Id == idBook).ToListAsync();
        }
        #endregion
    }
}
