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
    public class BookNoteManager : IBookNoteService
    {
        private readonly IBookNoteDal _bookNoteDal;
        private readonly IUowDal _uowDal;
        private readonly IAppLogger<BookNote> _logger;

        public BookNoteManager(IBookNoteDal bookNoteDal, IUowDal uowDal, IAppLogger<BookNote> logger)
        {
            _bookNoteDal = bookNoteDal;
            _uowDal = uowDal;
            _logger = logger;
        }

        public void TDelete(int id)
        {
            try
            {
                _bookNoteDal.Delete(id);
                _uowDal.Save();
                _logger.LogInformation($"bookNote with ID {id} deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting bookNote with ID {id}: {ex.Message}");
                throw;
            }
        }

        public BookNote TGetById(int id)
        {
            try
            {
                var bookNote = _bookNoteDal.GetById(id);
                _logger.LogInformation($"bookNote with ID {id} retrieved successfully.");
                return bookNote;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving bookNote with ID {id}: {ex.Message}");
                throw;
            }
        }

        public List<BookNote> TGetList()
        {
            try
            {
                var bookNotes = _bookNoteDal.GetList();
                _logger.LogInformation("bookNote list retrieved successfully.");
                return bookNotes;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving bookNote list: {ex.Message}");
                throw;
            }
        }

        public void TInsert(BookNote bookNote)
        {
            try
            {
                _bookNoteDal.Insert(bookNote);
                _uowDal.Save();
                _logger.LogInformation($"bookNote inserted successfully. ID: {bookNote.BookId}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error inserting bookNote: {ex.Message}");
                throw;
            }
        }

        public void TMultiUpdate(List<BookNote> bookNote)
        {
            try
            {
                _bookNoteDal.MultiUpdate(bookNote);
                _uowDal.Save();
                _logger.LogInformation("bookNote list updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating bookNote list: {ex.Message}");
                throw;
            }
        }

        public void TUpdate(BookNote bookNote)
        {
            try
            {
                _bookNoteDal.Update(bookNote);
                _uowDal.Save();
                _logger.LogInformation($"bookNote with ID {bookNote.BookId} updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating bookNote with ID {bookNote.BookId}: {ex.Message}");
                throw;
            }
        }



    }
}
