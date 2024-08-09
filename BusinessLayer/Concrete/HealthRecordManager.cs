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

        public Task AddAsync(HealthRecord healthRecord)
        {
            return _healthrecordDal.AddAsync(healthRecord);
        }

        public void Delete(HealthRecord entity)
        {
            _healthrecordDal.Delete(entity);
        }

        public List<HealthRecord> GetAll()
        {
            return _healthrecordDal.GetAll();
        }

        public Task<List<HealthRecord>> GetAllByUserIdAsync(int userId)
        {
            return _healthrecordDal.GetAllByUserIdAsync(userId);
        }

        public async Task<HealthRecord> GetAsync(int id)
        {
            return await _healthrecordDal.GetAsync(id);
        }

        public Task<HealthRecord> GetByIdAsync(int recordId)
        {
            return _healthrecordDal.GetAsync(recordId);
        }

        public Task<List<HealthRecord>> GetHealthRecordsForLast7DaysAsync(int userId)
        {
            return _healthrecordDal.GetHealthRecordsForLast7DaysAsync(userId);
        }

        public void Update(HealthRecord entity)
        {
            _healthrecordDal.Update(entity);
        }

        public async Task UpdateAsync(HealthRecord entity)
        {
            await _healthrecordDal.UpdateAsync(entity);   
        }
    }
}
