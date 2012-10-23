using Alf.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alf.App_Code
{
    public class ParticipantRepository : IParticipantRepository
    {
        public IEnumerable<Participant> GetAllParticipants()
        {
            return new[] { 
                new Participant { Mail = "Jonas@mail.com", Name = "Jonas" },
                new Participant { Mail = "Petter@mail.com", Name = "Petter" },
                new Participant { Mail = "Magnus@mail.com", Name = "Magnus" },
                new Participant { Mail = "Charlotte@mail.com", Name = "Charlotte" },
            };
        }
    }
}