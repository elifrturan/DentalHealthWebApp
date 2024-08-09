using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repositories;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EFHealthRecordRepository : GenericRepository<HealthRecord>, IHealthRecordDal
    {
        private readonly Context _context;

        public EFHealthRecordRepository(Context context) : base(context)
        {
            _context = context;
        }

        public async Task AddAsync(HealthRecord healthRecord)
        {
            _context.HealthRecords.Add(healthRecord);
            await _context.SaveChangesAsync();
        }

        public async Task<List<HealthRecord>> GetAllByUserIdAsync(int userId)
        {
            return await _context.HealthRecords
                        .Include(hr => hr.Goal)
                        .Where(hr => hr.Goal.UserID == userId)
                        .ToListAsync();
        }

        public async Task<HealthRecord> GetAsync(int id)
        {
            return await _context.HealthRecords.FindAsync(id);
        }

        public async Task<List<HealthRecord>> GetHealthRecordsForLast7DaysAsync(int userId)
        {
            var sevenDaysAgo = DateTime.Now.AddDays(-7);
            var healthRecords = await _context.HealthRecords
                                              .Include(hr => hr.Goal)
                                              .Where(hr => hr.Goal.UserID == userId && hr.RecordDate >= sevenDaysAgo)
                                              .OrderByDescending(hr => hr.RecordDate)
                                              .ToListAsync();
            return healthRecords;
        }

        public async Task UpdateAsync(HealthRecord entity)
        {
            var existingRecord = await _context.HealthRecords.FindAsync(entity.RecordID);

            if (existingRecord != null)
            {
                existingRecord.RecordDescription = entity.RecordDescription;
                existingRecord.RecordDate = entity.RecordDate;
                existingRecord.IsApplied = entity.IsApplied;
                existingRecord.RecordDuration = entity.RecordDuration;

                _context.Entry(existingRecord).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Kullanıcı bulunamadı.");
            }
        }
    }
}
