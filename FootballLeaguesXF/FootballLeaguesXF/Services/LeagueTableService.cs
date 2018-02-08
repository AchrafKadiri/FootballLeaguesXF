using FootballLeaguesXF.Constants;
using FootballLeaguesXF.IServices;
using FootballLeaguesXF.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeaguesXF.Services
{
    public class LeagueTableService : ILeagueTableService
    {
        private IApiService apiService;

        public LeagueTableService(IApiService apiService)
        {
            this.apiService = apiService;
        }


        async Task<List<Standing>> ILeagueTableService.GetLeagueTable(int idCompetition)
        {
            try
            {
                var root = await apiService.GetApi<RootObject3>(ApiUris.LeagueTable_Get, idCompetition);

                return root.standing;
            }
            catch (Exception cex)
            {
                throw cex;
            }
        }
    }
}
