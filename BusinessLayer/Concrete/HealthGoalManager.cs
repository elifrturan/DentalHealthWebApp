using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task AddAsync(HealthGoal healthGoal)
        {
            await _healthGoalDal.AddAsync(healthGoal);
        }

        public void Delete(HealthGoal entity)
        {
            _healthGoalDal.Delete(entity);
        }

        public Task DeleteAsync(int id)
        {
            return _healthGoalDal.DeleteAsync(id);
        }

        public List<HealthGoal> GetAll()
        {
            return _healthGoalDal.GetAll();
        }

        public Task<List<HealthGoal>> GetAllByUserIdAsync(int userId)
        {
            return _healthGoalDal.GetAllAsync(g => g.UserID == userId);
        }

        public List<HealthGoal> GetAllGoalsByUserId(int userId)
        {
            return _healthGoalDal.GetAllGoalsByUserId(userId);
        }

        public Task<HealthGoal> GetByIdAsync(int id)
        {
            return _healthGoalDal.GetByIdAsync(id);
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
