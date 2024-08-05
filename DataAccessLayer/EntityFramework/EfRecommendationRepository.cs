using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repositories;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfRecommendationRepository : GenericRepository<Recommendation>, IRecommendationDal
    {
        Context context = new Context();

        public EfRecommendationRepository(Context context) : base(context)
        {
        }

        public async Task<Recommendation> GetRandomRecommendationAsync()
        {
            int count = await context.Recommendations.CountAsync();
            int index = new Random().Next(count);
            return await context.Recommendations.Skip(index).FirstOrDefaultAsync();
        }
    }
}
