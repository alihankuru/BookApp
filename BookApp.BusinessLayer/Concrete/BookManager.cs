using BookApp.BusinessLayer.Abstract;
using BookApp.BusinessLayer.Serilog;
using BookApp.DataAccessLayer.Abstract;
using BookApp.DataAccessLayer.EntityFramework;
using BookApp.DataAccessLayer.UnitOfWork;
using BookApp.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.BusinessLayer.Concrete
{
    public class BookManager : IBookService
    {
        private readonly IBookDal _bookDal;
        private readonly IUowDal _uowDal;
        private readonly IAppLogger<Book> _logger;

        public BookManager(IBookDal bookDal, IUowDal uowDal, IAppLogger<Book> logger)
        {
            _bookDal = bookDal;
            _uowDal = uowDal;
            _logger = logger;
        }

        public void TDelete(int id)
        {
            try
            {
                _bookDal.Delete(id);
                _uowDal.Save();
                _logger.LogInformation($"book with ID {id} deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting book with ID {id}: {ex.Message}");
                throw;
            }
        }

        public Book TGetById(int id)
        {
            try
            {
                var book = _bookDal.GetById(id);
                _logger.LogInformation($"book with ID {id} retrieved successfully.");
                return book;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving book with ID {id}: {ex.Message}");
                throw;
            }
        }

        public List<Book> TGetList()
        {
            try
            {
                var books = _bookDal.GetList();
                _logger.LogInformation("book list retrieved successfully.");
                return books;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving book list: {ex.Message}");
                throw;
            }
        }

        public void TInsert(Book book)
        {
            try
            {
                _bookDal.Insert(book);
                _uowDal.Save();
                _logger.LogInformation($"book inserted successfully. ID: {book.BookId}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error inserting book: {ex.Message}");
                throw;
            }
        }

        public void TMultiUpdate(List<Book> book)
        {
            try
            {
                _bookDal.MultiUpdate(book);
                _uowDal.Save();
                _logger.LogInformation("book list updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating book list: {ex.Message}");
                throw;
            }
        }

        public void TUpdate(Book book)
        {
            try
            {
                _bookDal.Update(book);
                _uowDal.Save();
                _logger.LogInformation($"book with ID {book.BookId} updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating book with ID {book.BookId}: {ex.Message}");
                throw;
            }
        }
    }
}
