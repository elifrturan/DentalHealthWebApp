using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class NoteManager : INoteService
    {
        INoteDal _notDal;

        public NoteManager(INoteDal notDal)
        {
            _notDal = notDal;
        }

        public void Add(Note entity)
        {
            _notDal.Insert(entity);
        }

        public Task AddAsync(Note note)
        {
            return _notDal.AddAsync(note);
        }

        public void Delete(Note entity)
        {
            _notDal.Delete(entity);
        }

        public Task DeleteAsync(int noteId)
        {
            return _notDal.DeleteAsync(noteId);
        }

        public List<Note> GetAll()
        {
            return _notDal.GetAll();
        }

        public Task<List<Note>> GetAllByUserIdAsync(int userId)
        {
            return _notDal.GetAllByUserIdAsync(userId);
        }

        public Task<Note> GetByIdAsync(int noteId)
        {
            return _notDal.GetByIdAsync(noteId);
        }

        public void Update(Note entity)
        {
            _notDal.Update(entity);
        }

        public Task UpdateAsync(Note note)
        {
            return _notDal.UpdateAsync(note);
        }
    }
}
