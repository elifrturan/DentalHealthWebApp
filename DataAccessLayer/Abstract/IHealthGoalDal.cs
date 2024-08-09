using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IHealthGoalDal : IGenericDal<HealthGoal>
    {
        Task<List<HealthGoal>> GetHealthGoalsForLast7DaysAsync(int userId);
        Task<HealthGoal> GetByIdAsync(int id);
        Task<List<HealthRecord>> GetHealthRecordsByGoalIdAsync(int goalId);
        Task UpdateHealthRecordAsync(HealthRecord healthRecord);
        Task<List<HealthGoal>> GetAllAsync(Expression<Func<HealthGoal, bool>> filter);
        Task AddAsync(HealthGoal healthGoal);
        Task DeleteAsync(int id);
        List<HealthGoal> GetAllGoalsByUserId(int userId);

    }
}
