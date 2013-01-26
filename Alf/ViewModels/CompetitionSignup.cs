using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Alf.Models;

namespace Alf.ViewModels
{
    public class CompetitionSignup
    {
        public string Guid { get; set; }
        public string Username { get; set; }
        public IEnumerable<Competition> Available { get; set; }
        public IEnumerable<Competition> SignedUp { get; set; }
    }
}