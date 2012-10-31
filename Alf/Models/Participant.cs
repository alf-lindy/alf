using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Alf.Models
{
    public class Participant
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Mail { get; set; }
        public Role Role { get; set; }
        public bool NTNUI { get; set; }
        public bool Paid { get; set; }
        
        public int DanceClassId { get; set; }
        [ForeignKey("DanceClassId")]
        public DanceClass DanceClass { get; set; }
        public ParticipantStatus Status { get; set; }
    }
}