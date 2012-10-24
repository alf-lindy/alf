﻿using Alf.App_Code;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public Track Track { get; set; }
        public Level Level { get; set; }
        public bool NTNUI { get; set; }
        public bool Paid { get; set; }
        public ParticipantStatus Status { get; set; }
    }
}