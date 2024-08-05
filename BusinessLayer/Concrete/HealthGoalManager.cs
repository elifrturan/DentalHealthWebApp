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

        public void Update(HealthGoal entity)
        {
            _healthGoalDal.Update(entity);
        }
    }
}
