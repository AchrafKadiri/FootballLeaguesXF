using FootballLeaguesXF.Constants;
using FootballLeaguesXF.IServices;
using FootballLeaguesXF.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeaguesXF.Services
{
    public class TeamsService : ITeamsService
    {
        private IApiService apiService;

        public TeamsService(IApiService apiService)
        {
            this.apiService = apiService;
        }

        async Task<IEnumerable<Team>> ITeamsService.GetTeams(int idCompetition)
        {
            try
            {
               var root= await apiService.GetApi<RootObject2>(ApiUris.AllTeamsByCompetition_Get,idCompetition);


               return root.teams;
            }
            catch (Exception cex)
            {
                throw cex;
            }
        }
    }
}
