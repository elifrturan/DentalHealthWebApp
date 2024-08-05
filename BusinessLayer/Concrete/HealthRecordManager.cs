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
    public class HealthRecordManager : IHealthRecordService
    {
        IHealthRecordDal _healthrecordDal;

        public HealthRecordManager(IHealthRecordDal healthrecordDal)
        {
            _healthrecordDal = healthrecordDal;
        }

        public void Add(HealthRecord entity)
        {
            _healthrecordDal.Insert(entity);
        }

        public void Delete(HealthRecord entity)
        {
            _healthrecordDal.Delete(entity);
        }

        public List<HealthRecord> GetAll()
        {
            return _healthrecordDal.GetAll();
        }

        public Task<List<HealthRecord>> GetHealthRecordsForLast7DaysAsync(int userId)
        {
            return _healthrecordDal.GetHealthRecordsForLast7DaysAsync(userId);
        }

        public void Update(HealthRecord entity)
        {
            _healthrecordDal.Update(entity);
        }
    }
}
