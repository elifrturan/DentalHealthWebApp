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
    public class HealthGoalManager : IHealthGoalService
    {
        IHealthGoalDal _healthGoalDal;

        public HealthGoalManager(IHealthGoalDal healthGoalDal)
        {
            _healthGoalDal = healthGoalDal;
        }

        public void Add(HealthGoal entity)
        {
            _healthGoalDal.Insert(entity);
        }

        public void Delete(HealthGoal entity)
        {
            _healthGoalDal.Delete(entity);
        }
        public List<HealthGoal> GetAll()
        {
            return _healthGoalDal.GetAll();
        }

        public Task<List<HealthGoal>> GetHealthGoalsForLast7DaysAsync(int userId)
        {
            return _healthGoalDal.GetHealthGoalsForLast7DaysAsync(userId);
        }

        public Task<List<HealthRecord>> GetHealthRecordsByGoalIdAsync(int goalId)
        {
            return _healthGoalDal.GetHealthRecordsByGoalIdAsync(goalId);
        }

        public void Update(HealthGoal entity)
        {
            _healthGoalDal.Update(entity);
        }

        public Task UpdateHealthRecordAsync(HealthRecord healthRecord)
        {
            return _healthGoalDal.UpdateHealthRecordAsync(healthRecord);
        }
    }
}
