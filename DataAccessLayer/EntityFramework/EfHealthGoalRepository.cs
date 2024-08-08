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
    public class EfHealthGoalRepository : GenericRepository<HealthGoal>, IHealthGoalDal
    {
        private readonly Context _context;

        public EfHealthGoalRepository(Context context) : base(context)
        {
            _context = context;
        }

        public async Task<List<HealthGoal>> GetHealthGoalsForLast7DaysAsync(int userId)
        {
                var sevenDaysAgo = DateTime.Now.AddDays(-7);
                var healthGoals = await _context.HealthGoals
                                                  .Include(hr => hr.User)
                                                  .Where(hr => hr.User.UserID == userId && hr.GoalCreateDate >= sevenDaysAgo)
                                                  .OrderByDescending(hr => hr.GoalCreateDate)
                                                  .ToListAsync();
                return healthGoals;
            
        }

        public async Task<List<HealthRecord>> GetHealthRecordsByGoalIdAsync(int goalId)
        {
            return await _context.HealthRecords
                        .Where(hr => hr.GoalID == goalId)
                        .OrderByDescending(hr => hr.RecordDate)
                        .ToListAsync();
        }

        public Task UpdateHealthRecordAsync(HealthRecord healthRecord)
        {
            _context.HealthRecords.Update(healthRecord);
            return _context.SaveChangesAsync();
        }
    }
}
