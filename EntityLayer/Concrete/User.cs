using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        public string UserEmail { get; set; }
        public byte[] UserPassword { get; set; }
        public string UserFullName { get; set; }
        public DateTime UserBirthDate { get; set; }
        public DateTime AccountCreateDate { get; set; }
        public DateTime AccountUpdateDate { get; set; }
    }
}
