using Alf.App_Code;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Alf.Models
{
    public class DanceClass
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public Track Track { get; set; }
        public Level Level { get; set; }
        public int Limit { get; set; }
        public IEnumerable<Participant> Participants { get; set; }
    }
}