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
    public class EfUserRepository : GenericRepository<User>, IUserDal
    {
        Context context = new Context();

        public EfUserRepository(Context context) : base(context)
        {
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await context.Users.FirstOrDefaultAsync(u => u.UserEmail == email); 
        }
    }
}
