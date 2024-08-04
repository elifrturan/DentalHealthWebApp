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
    public class UserSessionManager : IUserSessionService
    {
        IUserSessionDal _userSessionDal;

        public UserSessionManager(IUserSessionDal userSessionDal)
        {
            _userSessionDal = userSessionDal;
        }

        public void Add(UserSession entity)
        {
            _userSessionDal.Insert(entity);
        }

        public void Delete(UserSession entity)
        {
            _userSessionDal.Delete(entity);
        }

        public List<UserSession> GetAll()
        {
            return _userSessionDal.GetAll();
        }

        public void Update(UserSession entity)
        {
            _userSessionDal.Update(entity);
        }
    }
}
