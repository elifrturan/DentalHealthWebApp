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
    public class RecommendationManager : IRecommendationService
    {
        IRecommendationDal _recommendationDal;

        public RecommendationManager(IRecommendationDal recommendationDal)
        {
            _recommendationDal = recommendationDal;
        }

        public void Add(Recommendation entity)
        {
            _recommendationDal.Insert(entity);
        }

        public void Delete(Recommendation entity)
        {
            _recommendationDal.Delete(entity);
        }

        public List<Recommendation> GetAll()
        {
            return _recommendationDal.GetAll();
        }

        public Task<Recommendation> GetRandomRecommendationAsync()
        {
            return _recommendationDal.GetRandomRecommendationAsync();
        }

        public void Update(Recommendation entity)
        {
            _recommendationDal.Update(entity);
        }
    }
}
