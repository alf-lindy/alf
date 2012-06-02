using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace alf_2013.Models
{
    public class Teacher
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Name { 
            get 
            {
                return FirstName + " " + LastName;
            } 
        }
    }
}