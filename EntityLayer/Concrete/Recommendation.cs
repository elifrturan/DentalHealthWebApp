using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Recommendation
    {
        [Key]
        public int RecommendationID { get; set; }
        public string RecommendationText { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
