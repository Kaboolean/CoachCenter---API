using Dhoojol.Domain.Entities.Sessions;
using Dhoojol.Infrastructure.EfCore.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dhoojol.Infrastructure.EfCore.Repositories.Sessions
{
    public interface ISessionParticipantRepository : IRepository<SessionParticipant>
    {

    }
    internal class SessionParticipantRepository : EfRepository<SessionParticipant>, ISessionParticipantRepository
    {
        private readonly DhoojolContext _dbContext;

        public SessionParticipantRepository(DhoojolContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
