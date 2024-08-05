using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IHealthRecordService : IGenericService<HealthRecord>
    {
        Task<List<HealthRecord>> GetHealthRecordsForLast7DaysAsync(int userId);
    }
}
