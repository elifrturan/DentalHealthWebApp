using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Note
    {
        [Key]
        public int NoteID { get; set; }
        public string NoteTitle { get; set; }
        public string NoteDescription { get; set; }
        public string? NoteImage { get; set; }
        public DateTime CreatedDate { get; set; }


        public int UserID { get; set; }
        public User User { get; set; }
    }
}
