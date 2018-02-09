using FootballLeaguesXF.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeaguesXF.IServices
{
    public interface ICompetitionsService
    {
        /// <summary>
        /// Method which obtains Competitions from the server
        /// </summary>    
        /// <returns>List of competitions.</returns>
        Task<IEnumerable<RootObject>> GetCompetitions(bool force);
    }
}
