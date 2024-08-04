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
    public class PasswordResetManager : IPasswordResetService
    {
        IPasswordResetDal _passwordResetDal;

        public PasswordResetManager(IPasswordResetDal passwordResetDal)
        {
            _passwordResetDal = passwordResetDal;
        }

        public void Add(PasswordReset entity)
        {
            _passwordResetDal.Insert(entity);
        }

        public void Delete(PasswordReset entity)
        {
            _passwordResetDal.Delete(entity);
        }

        public List<PasswordReset> GetAll()
        {
            return _passwordResetDal.GetAll();
        }

        public void Update(PasswordReset entity)
        {
            _passwordResetDal.Update(entity);
        }
    }
}
