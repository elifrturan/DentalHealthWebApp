using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IHealthRecordDal : IGenericDal<HealthRecord>
    {
        Task<List<HealthRecord>> GetHealthRecordsForLast7DaysAsync(int userId);
    }
}
