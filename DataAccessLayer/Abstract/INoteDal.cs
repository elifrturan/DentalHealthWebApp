using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface INoteDal : IGenericDal<Note>
    {
        Task AddAsync(Note note);
        Task<Note> GetByIdAsync(int noteId);
        Task<List<Note>> GetAllByUserIdAsync(int userId);
        Task UpdateAsync(Note note);
        Task DeleteAsync(int noteId);
    }
}
