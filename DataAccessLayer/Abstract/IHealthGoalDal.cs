using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IHealthGoalDal : IGenericDal<HealthGoal>
    {
        Task<List<HealthGoal>> GetHealthGoalsForLast7DaysAsync(int userId);

        Task<List<HealthRecord>> GetHealthRecordsByGoalIdAsync(int goalId);
        Task UpdateHealthRecordAsync(HealthRecord healthRecord);
    }
}
