using Alf.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alf.App_Code
{
    public interface IParticipantRepository
    {
        IEnumerable<Participant> GetAllParticipants();
    }
}
