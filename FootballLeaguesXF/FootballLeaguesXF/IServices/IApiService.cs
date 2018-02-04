using FootballLeaguesXF.Constants;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeaguesXF.IServices
{
    public interface IApiService
    {
        /// <summary>
        /// Calling Get method from API
        /// </summary>
        /// <typeparam name="T">Expected Returning Type</typeparam>
        /// <param name="apiUris">Restfull rooting.</param>
        /// <returns>Value obtained from the API of type T.</returns>
        /// <remarks>For any error ApiException will be thrown.</remarks>
        Task<T> GetApi<T>(ApiUris apiUris);
    }
}
