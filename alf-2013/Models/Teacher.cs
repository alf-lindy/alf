using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace alf_2013.Models
{
    public class Teacher
    {
        public int TeacherID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Image { get; set; }
        public string Bio { get; set; }
        public string Website { get; set; }
    }
}