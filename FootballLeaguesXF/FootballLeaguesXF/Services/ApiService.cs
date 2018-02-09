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
        private ICacheService cacheService;

        public ApiService(ICacheService cacheService)
        {
            this.cacheService = cacheService;
        }

        async Task<T> IApiService.GetApi<T>(ApiUris apiUris, bool force)
        {
            var uri = FabricateUrl(apiUris, null);
            var filename = GetCacheFileName(apiUris);

            return await GetApi<T>(apiUris, force, filename, uri);
        }

        async Task<T> IApiService.GetApi<T>(ApiUris apiUris, bool force, int id)
        {
            var uri = FabricateUrl(apiUris, id);
            var filename = GetCacheFileName(apiUris, id);

            return await GetApi<T>(apiUris, force, filename, uri);
        }


        private async Task<T> GetApi<T>(ApiUris apiUris, bool force, string filename, Uri uri)
        {
            try
            {
                T content;

                if (force || !await cacheService.ExistRecentCacheAsync(filename))
                {
                    content = await base.GetAsync<T>(uri);

                    await cacheService.SaveObjectFileAsync<T>(content, filename);
                }
                else
                {
                    //We nw that cache is valid.
                    content = await cacheService.ReadObjectFileAsync<T>(filename);
                }

                return content;
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, ex);
            }
        }

        private string GetCacheFileName(ApiUris apiUris)
        {
            return apiUris.ToString().Split('_')[0];
        }
        private string GetCacheFileName(ApiUris apiUris, int id)
        {
            return $"{apiUris.ToString().Split('_')[0]}_{id.ToString()}";
        }

        private async Task<bool> AddObject2Cache<T>(T item, ApiUris apiUris)
        {
            var file_name = GetCacheFileName(apiUris);

            if (!await cacheService.ExistCacheAsync(file_name))
            {
                //add content to cache list
                List<T> cacheObjects = new List<T>();
                cacheObjects.Add(item);

                //save list to cache
                await cacheService.SaveObjectFileAsync<List<T>>(cacheObjects, file_name);
            }
            else
            {
                await cacheService.AddObject2Cache<T>(item, GetCacheFileName(apiUris));
            }

            return true;
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
