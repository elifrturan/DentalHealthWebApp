using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repositories;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfNoteRepository : GenericRepository<Note>, INoteDal
    {
        private readonly Context _context;
        
        public EfNoteRepository(Context context) : base(context)
        {
            _context = context;
        }

        public async Task AddAsync(Note note)
        {
            _context.Notes.Add(note);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int noteId)
        {
            var note = await _context.Notes.FindAsync(noteId);
            if (note != null)
            {
                _context.Notes.Remove(note);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Note>> GetAllByUserIdAsync(int userId)
        {
            return await _context.Notes.Where(n => n.UserID == userId).ToListAsync();
        }

        public async Task<Note> GetByIdAsync(int noteId)
        {
            return await _context.Notes.FindAsync(noteId);
        }

        public async Task UpdateAsync(Note note)
        {
            _context.Notes.Update(note);
            await _context.SaveChangesAsync();
        }
    }
}
