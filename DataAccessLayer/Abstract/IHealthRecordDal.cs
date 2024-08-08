using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IHealthRecordDal : IGenericDal<HealthRecord>
    {
        Task<List<HealthRecord>> GetHealthRecordsForLast7DaysAsync(int userId);
        Task<HealthRecord> GetAsync(int id);
        Task UpdateAsync(HealthRecord entity);
        Task<List<HealthRecord>> GetAllByUserIdAsync(int userId);
    }
}
