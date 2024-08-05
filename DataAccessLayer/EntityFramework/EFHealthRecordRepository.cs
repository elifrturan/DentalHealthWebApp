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
    public class EFHealthRecordRepository : GenericRepository<HealthRecord>, IHealthRecordDal
    {
        private readonly Context _context;

        public EFHealthRecordRepository(Context context) : base(context)
        {
            _context = context;
        }

        public async Task<List<HealthRecord>> GetHealthRecordsForLast7DaysAsync(int userId)
        {
            var sevenDaysAgo = DateTime.Now.AddDays(-7);
            var healthRecords = await _context.HealthRecords
                                              .Include(hr => hr.Goal) // Include Goal to ensure UserID is accessible
                                              .Where(hr => hr.Goal.UserID == userId && hr.RecordDate >= sevenDaysAgo)
                                              .OrderByDescending(hr => hr.RecordDate)
                                              .ToListAsync();
            return healthRecords;
        }
    }
}
