using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alf.Models
{
    public enum AddParticipantResponse
    {
        Success,
        PutOnWait,
        Full,
        Error,
        NoClass
    }
    public enum ParticipantStatus
    {
        Unprocessed,
        AwaitingPayment,
        PutInWaitingList,
        Enlisted
    }
    
    public enum Role
    {
        Lead,
        Follow
    }

    public enum Track
    {
        Lindy_Hop,
        Boogie_Woogie
    }

    public enum Level
    {
        Beginner,
        Intermediate,
        Advanced
    }
}