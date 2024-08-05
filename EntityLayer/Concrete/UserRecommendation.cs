using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class UserRecommendation
    {
        [Key]
        public int UserRecommendationID { get; set; }
        public int UserID { get; set; }
        public int RecommendationID { get; set; }
        public DateTime CreatedAt { get; set; }
        public User User { get; set; }
        public Recommendation Recommendation { get; set; }
    }
}
