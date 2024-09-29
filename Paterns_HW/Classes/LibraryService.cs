using Paterns_HW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paterns_HW.Classes
{
    internal class LibraryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LibraryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddBook(string title, int authorId, int genreId, int publishedYear)
        {
            var book = new Book
            {
                Title = title,
                AuthorId = authorId,
                GenreId = genreId,
                PublishedYear = publishedYear
            };
            _unitOfWork.Books.Add(book);
            _unitOfWork.Commit();
        }

        public void UpdateBook(int id, string title, int authorId, int genreId, int publishedYear)
        {
            var book = _unitOfWork.Books.GetById(id);
            if (book != null)
            {
                book.Title = title;
                book.AuthorId = authorId;
                book.GenreId = genreId;
                book.PublishedYear = publishedYear;
                _unitOfWork.Books.Update(book);
                _unitOfWork.Commit();
            }
        }

        public void DeleteBook(int id) {
            _unitOfWork.Books.Delete(id);
            _unitOfWork.Commit();
        }

        public IEnumerable<Book> GetBooks(string genre = null, string author = null)
        {
            IEnumerable<Book> books = _unitOfWork.Books.GetAll();

            if (!string.IsNullOrEmpty(genre))
            {
                var genreEntity = _unitOfWork.Genres.GetAll().FirstOrDefault(g => g.Name.Contains(genre));
                if (genreEntity != null)
                {
                    books = books.Where(b => b.GenreId == genreEntity.Id);
                }
                else
                {
                    books = Enumerable.Empty<Book>(); 
                }
            }

           
            if (!string.IsNullOrEmpty(author))
            {
                var authorEntity = _unitOfWork.Authors.GetAll().FirstOrDefault(a => a.Name.Contains(author));
                if (authorEntity != null)
                {
                    books = books.Where(b => b.AuthorId == authorEntity.Id);
                }
                else
                {
                    books = Enumerable.Empty<Book>(); 
                }
            }

            return books;
        }




    }
}
