using FootballLeaguesXF.Constants;
using FootballLeaguesXF.Exceptions;
using FootballLeaguesXF.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeaguesXF.Services
{
    public class ApiService : ApiDriver, IApiService
    {

        async Task<T> IApiService.GetApi<T>(ApiUris apiUris,int id)
        {
            try
            {
                var uri = FabricateUrl(apiUris, id);
                return await base.GetAsync<T>(uri);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, ex);
            }
        }
       
        /// <summary>
        /// Method which fabricates the appropiate Uri
        /// </summary>      
        /// <param name="apiUris">Restfull rooting.</param>
        /// <param name="args">Optional params</param>
        /// <returns>Expected Uri.</returns>
        /// <remarks>For any error ApiException will be thrown.</remarks>
        private Uri FabricateUrl(ApiUris apiUri, params object[] args)
        {
            string urlrel = string.Empty;
            switch (apiUri)
            {
                case ApiUris.AllCompetitions_Get:
                    urlrel = StringFormat(ApiConstants.GetAllCompetitions, args);
                    break;
                case ApiUris.AllTeamsByCompetition_Get:
                    urlrel = StringFormat(ApiConstants.GetAllTeamsByCompetition, args);
                    break;
                case ApiUris.LeagueTable_Get:
                    urlrel = StringFormat(ApiConstants.GetLeagueTable, args);
                    break;
                default:
                    throw new Exception("Code error. Please add missing APiUris Enum");
            }

            return new Uri($"{ApiConstants.API_PROTOCOL}://{ApiConstants.API_HOST}/{urlrel}", UriKind.Absolute);
        }

        /// <summary>
        /// Method which returns the correct string format for the uri
        /// </summary>      
        /// <param name="pattern">The corresponding uri pattern</param>
        /// <param name="args">Optional params</param>
        /// <returns>Expected Uri format</returns>
        private string StringFormat(string pattern, object[] args)
        {
            return args == null ? pattern : string.Format(pattern, args);
        }
    }
}
