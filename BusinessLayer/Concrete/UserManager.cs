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
    public class UserManager : IUserService
    {
        IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public void Add(User entity)
        {
            _userDal.Insert(entity);
        }

        public void Delete(User entity)
        {
            _userDal.Delete(entity);
        }

        public List<User> GetAll()
        {
            return _userDal.GetAll();
        }

        public Task<User> GetByEmailAsync(string email)
        {
            return _userDal.GetByEmailAsync(email);
        }

        public User GetById(int id)
        {
            return _userDal.GetById(id);
        }

        public void Update(User entity)
        {
           _userDal.Update(entity);
        }

        public Task UpdateAsync(User user)
        {
            return _userDal.UpdateAsync(user);
        }
    }
}
