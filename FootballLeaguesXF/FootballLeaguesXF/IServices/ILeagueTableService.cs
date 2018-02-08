using FootballLeaguesXF.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeaguesXF.IServices
{
    public interface ILeagueTableService
    {
        /// <summary>
        /// Method which obtains League table from the server
        /// </summary>    
        /// <returns>List of teams ordered by rank.</returns>
        Task<List<Standing>> GetLeagueTable(int idcompetition);
    }
}
