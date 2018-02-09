using FootballLeaguesXF.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeaguesXF.IServices
{
    public interface ITeamsService
    {
        /// <summary>
        /// Method which obtains Teams from the server
        /// </summary>    
        /// <returns>List of teams.</returns>
        Task<IEnumerable<Team>> GetTeams(int idcompetition,bool force);
    }
}
