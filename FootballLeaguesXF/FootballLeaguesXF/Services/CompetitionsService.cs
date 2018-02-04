using FootballLeaguesXF.Constants;
using FootballLeaguesXF.IServices;
using FootballLeaguesXF.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeaguesXF.Services
{
    public class CompetitionsService : ICompetitionsService
    {
        private IApiService apiService;

        public CompetitionsService(IApiService apiService)
        {
            this.apiService = apiService;
        }

        async Task<IEnumerable<RootObject>> ICompetitionsService.GetCompetitions()
        {
            try
            {
                return await apiService.GetApi<IEnumerable<RootObject>>(ApiUris.AllCompetitions_Get);
            }
            catch (Exception cex)
            {
                throw cex;
            }
        }
    }
}
