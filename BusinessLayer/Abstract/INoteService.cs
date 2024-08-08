using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface INoteService : IGenericService<Note>
    {
        Task AddAsync(Note note);
        Task<Note> GetByIdAsync(int noteId);
        Task<List<Note>> GetAllByUserIdAsync(int userId);
        Task UpdateAsync(Note note);
        Task DeleteAsync(int noteId);
    }
}
