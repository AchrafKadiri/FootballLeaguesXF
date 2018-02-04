using FootballLeaguesXF.Exceptions;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeaguesXF.Services
{
    public class ApiDriver
    {
              
        /// <summary>
        /// Calling HttpGet against Restfull URI
        /// </summary>
        /// <typeparam name="T">Expected Returning Type</typeparam>
        /// <param name="WebServiceUrl">Restfull rooting.</param>
        /// <returns>Value obtained from the server of type T.</returns>
        /// <remarks>For any error ApiException will be thrown.</remarks>
        protected async Task<T> GetAsync<T>(Uri WebServiceUrl)
        {
            try
            {
                CheckConnection();
                using (HttpClient client =  new HttpClient())
                {
                    Debug.WriteLine($">>> Get {WebServiceUrl} ");
                    var response = await client.GetAsync(WebServiceUrl);
                    Debug.WriteLine($"<<< Get {WebServiceUrl} ");

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<T>(content);
                        return result;
                    }
                    else
                    {
                        throw new Exception(response.ReasonPhrase);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ProcessException(ex);
            }
        }

       
        /// <summary>
        /// Method which checks the connection state
        /// </summary>        
        /// <remarks>For any error ConnectionException will be thrown.</remarks>
        private void CheckConnection()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                throw new ConnectionException("Connection error. Please, check the connection.");
            }
        }

        private Exception ProcessException(Exception ex)
        {
            if (ex is ConnectionException)
            {
                throw new ConnectionException("Please try again once connectivity is reestablished.", ex);
            }
            else if (ex is ApiException)
            {
                throw new ApiException(ex.Message, ex);
            }
            else
            {
                throw new ApiException("Issue calling the Backend. Please try again later.", ex);
            }
        }
      
    }
}
