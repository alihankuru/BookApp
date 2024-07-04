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

namespace SharedNoteApp.BusinessLayer.Concrete
{
    public class SharedNoteManager : ISharedNoteService
    {
        private readonly ISharedNoteDal _sharedNoteDal;
        private readonly IUowDal _uowDal;
        private readonly IAppLogger<SharedNote> _logger;

        public SharedNoteManager(ISharedNoteDal sharedNoteDal, IUowDal uowDal, IAppLogger<SharedNote> logger)
        {
            _sharedNoteDal = sharedNoteDal;
            _uowDal = uowDal;
            _logger = logger;
        }

        public void TDelete(int id)
        {
            try
            {
                _sharedNoteDal.Delete(id);
                _uowDal.Save();
                _logger.LogInformation($"sharedNote with ID {id} deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting sharedNote with ID {id}: {ex.Message}");
                throw;
            }
        }

        public SharedNote TGetById(int id)
        {
            try
            {
                var sharedNote = _sharedNoteDal.GetById(id);
                _logger.LogInformation($"sharedNote with ID {id} retrieved successfully.");
                return sharedNote;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving sharedNote with ID {id}: {ex.Message}");
                throw;
            }
        }

        public List<SharedNote> TGetList()
        {
            try
            {
                var sharedNotes = _sharedNoteDal.GetList();
                _logger.LogInformation("sharedNote list retrieved successfully.");
                return sharedNotes;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving sharedNote list: {ex.Message}");
                throw;
            }
        }

        public void TInsert(SharedNote sharedNote)
        {
            try
            {
                _sharedNoteDal.Insert(sharedNote);
                _uowDal.Save();
                _logger.LogInformation($"sharedNote inserted successfully. ID: {sharedNote.SharedNoteId}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error inserting sharedNote: {ex.Message}");
                throw;
            }
        }

        public void TMultiUpdate(List<SharedNote> sharedNote)
        {
            try
            {
                _sharedNoteDal.MultiUpdate(sharedNote);
                _uowDal.Save();
                _logger.LogInformation("sharedNote list updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating sharedNote list: {ex.Message}");
                throw;
            }
        }

        public void TUpdate(SharedNote sharedNote)
        {
            try
            {
                _sharedNoteDal.Update(sharedNote);
                _uowDal.Save();
                _logger.LogInformation($"sharedNote with ID {sharedNote.SharedNoteId} updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating sharedNote with ID {sharedNote.SharedNoteId}: {ex.Message}");
                throw;
            }
        }

    }
}
