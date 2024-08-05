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
    public class UserRecommendationManager : IUserRecommendationService
    {
        IUserRecommendationDal _userRecommendationDal;

        public UserRecommendationManager(IUserRecommendationDal userRecommendationDal)
        {
            _userRecommendationDal = userRecommendationDal;
        }

        public void Add(UserRecommendation entity)
        {
            _userRecommendationDal.Insert(entity);
        }

        public void Delete(UserRecommendation entity)
        {
            _userRecommendationDal.Delete(entity);
        }

        public List<UserRecommendation> GetAll()
        {
            return _userRecommendationDal.GetAll();
        }

        public void Update(UserRecommendation entity)
        {
            _userRecommendationDal.Update(entity);
        }
    }
}
