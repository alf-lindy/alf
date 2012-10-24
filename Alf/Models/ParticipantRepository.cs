using Alf.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alf.App_Code
{
    public class ParticipantRepository : IParticipantRepository
    {
        private RegistrationContext db = new RegistrationContext();

        public IEnumerable<Participant> GetAllParticipants()
        {
            return db.Participants;
        }

        public AddParticipantResponse Add(Participant participant) 
        {
            Func<DanceClass, bool> classSelector = (dc) => dc.Level == participant.Level && dc.Track == participant.Track;
            
            var danceclass = db.Classes.SingleOrDefault(classSelector);
            if(danceclass == null) 
            {
                // Danceclass not found in db
                return AddParticipantResponse.NoClass;
            } 
            else if(danceclass.Limit <= db.Classes.Count(classSelector)) 
            {
                // Class is full, put on wait
                participant.Status = ParticipantStatus.PutInWaitingList;
            }else {
                // Got a space, awaiting payment
                participant.Status = ParticipantStatus.AwaitingPayment;
            }

            db.Participants.Add(participant);
            db.SaveChanges();
            return AddParticipantResponse.PutOnWait;
        }
    }
}