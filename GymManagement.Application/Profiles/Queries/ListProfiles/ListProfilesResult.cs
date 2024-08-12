using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Application.Profiles.Queries.ListProfiles
{
    public record ListProfilesResult(Guid? AdminId, Guid? ParticipantId, Guid? TrainerId);    
}
