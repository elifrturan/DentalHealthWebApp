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
            this.context = context;
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await context.Users.FirstOrDefaultAsync(u => u.UserEmail == email); 
        }

        public User GetById(int id)
        {
            return context.Users.FirstOrDefault(u => u.UserID == id);
        }

        public async Task UpdateAsync(User user)
        {
            var existingUser = await context.Users.FindAsync(user.UserID);

            if (existingUser != null)
            {
                existingUser.UserFullName = user.UserFullName;
                existingUser.UserEmail = user.UserEmail;
                existingUser.UserPassword = user.UserPassword;
                existingUser.UserBirthDate = user.UserBirthDate;
                existingUser.AccountUpdateDate = user.AccountUpdateDate;

                await context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Kullanıcı bulunamadı.");
            }
        }
    }
}
