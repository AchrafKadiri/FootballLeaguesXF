using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeaguesXF.IServices
{
    public interface ICacheService
    {
        /// <summary>
        /// Method which checks the cache state
        /// </summary>    
        /// <param name="fileName">Cache file name</param>
        /// <returns>True if cache state is valid, false if cache state is not valid.</returns>
        Task<bool> ExistRecentCacheAsync(string fileName);

        /// <summary>
        /// Method which checks the cache state
        /// </summary>    
        /// <param name="fileName">Cache file name</param>
        /// <returns>True if cache state is valid, false if cache state is not valid.</returns>
        Task<bool> ExistCacheAsync(string fileName);

        /// <summary>
        /// Method which reads the cache content
        /// </summary>    
        /// <param name="fileName">Cache file name</param>
        /// <returns>Cache content.</returns>
        Task<T> ReadObjectFileAsync<T>(string filename);

        /// <summary>
        /// Method which saves content on cache
        /// </summary>    
        /// <param name="content">Data which will be saved.</param>
        /// <param name="fileName">Cache file name</param>
        Task SaveObjectFileAsync<T>(T content, string filename);

        /// <summary>
        /// Method which adds content on cache
        /// </summary>    
        /// <param name="content">Data which will be added.</param>
        /// <param name="fileName">Cache file name</param>
        Task AddObject2Cache<T>(T content, string filename);

        /// <summary>
        /// Method which deletes content from cache
        /// </summary>    
        /// <param name="fileName">Cache file name</param>
        Task DeleteObjectCache(string filename);

        /// <summary>
        /// Method which returns the number of objects from file
        /// </summary>    
        /// <param name="fileName">Cache file name</param>
        /// <returns>Number of objects.</returns>
        Task<int> NumberOfObjects(string filename);
    }
}
