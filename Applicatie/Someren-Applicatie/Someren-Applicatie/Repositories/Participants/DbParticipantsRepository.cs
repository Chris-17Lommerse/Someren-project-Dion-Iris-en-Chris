using Microsoft.Data.SqlClient;
using Someren_Applicatie.Models;
using System.Runtime.InteropServices;

namespace Someren_Applicatie.Repositories.Participants
{
    public class DbParticipantsRepository : BaseRepository, IParticipantsRepository
    {
        public DbParticipantsRepository(IConfiguration configuration) : base(configuration) { }
    }
}
