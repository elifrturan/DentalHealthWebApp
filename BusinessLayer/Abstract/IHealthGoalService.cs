using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IHealthGoalService : IGenericService<HealthGoal>
    {
        Task<List<HealthGoal>> GetHealthGoalsForLast7DaysAsync(int userId);
        Task<HealthGoal> GetByIdAsync(int id);
        Task<List<HealthRecord>> GetHealthRecordsByGoalIdAsync(int goalId);
        Task UpdateHealthRecordAsync(HealthRecord healthRecord);
        Task<List<HealthGoal>> GetAllByUserIdAsync(int userId);
        Task AddAsync(HealthGoal healthGoal);
        Task DeleteAsync(int id);
        List<HealthGoal> GetAllGoalsByUserId(int userId);


    }
}
