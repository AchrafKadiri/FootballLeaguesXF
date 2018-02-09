using FootballLeaguesXF.Extensions;
using FootballLeaguesXF.IServices;
using FootballLeaguesXF.Plugins;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeaguesXF.Services
{
    public class CacheService : ICacheService
    {
#if DEBUG
        const int EXPIRE_TIME = 20; // MIN
#else
        const int EXPIRE_TIME = 300; // MIN
#endif

        private IFileService fileService;

        public CacheService(IFileService fileService)
        {
            this.fileService = fileService;
        }

        async Task ICacheService.AddObject2Cache<T>(T content, string filename)
        {
            List<T> cacheObjects = null;
            try
            {
                //read from cache
                cacheObjects = (await ReadObjectFileAsync<IEnumerable<T>>(filename)).ToList();
            }
            catch (Exception ex)
            {
                cacheObjects = new List<T>();
                Debug.WriteLine($">>> Exception cache service {ex.InnerException}--/--{ex.Message} ");
            }
            finally
            {
                //add content to cache list
                cacheObjects.Add(content);

                //save list to cache
                await SaveObjectFileAsync<IEnumerable<T>>(cacheObjects, filename);
            }

        }

        private async Task SaveObjectFileAsync<T>(T content, string filename)
        {
            await fileService.SaveObjectFileAsync<T>(content, filename);
        }

        async Task ICacheService.DeleteObjectCache(string filename)
        {
            await fileService.DeleteFileAsync(filename);
        }

        async Task<bool> ICacheService.ExistRecentCacheAsync(string fileName)
        {
            return await fileService.ExistRecentCacheAsync(fileName, new TimeSpan(0, EXPIRE_TIME, 0));
        }

        async Task<bool> ICacheService.ExistCacheAsync(string fileName)
        {
            return await fileService.ExistFileAsync(fileName);
        }

        async Task<int> ICacheService.NumberOfObjects(string filename)
        {
            var cacheObjects = await ReadObjectFileAsync<IEnumerable<object>>(filename);

            return cacheObjects.IsNullOrCount();
        }

        async Task<T> ICacheService.ReadObjectFileAsync<T>(string filename)
        {
            return await ReadObjectFileAsync<T>(filename);
        }

        private async Task<T> ReadObjectFileAsync<T>(string filename)
        {
            return await fileService.ReadObjectFileAsync<T>(filename);
        }

        async Task ICacheService.SaveObjectFileAsync<T>(T content, string filename)
        {
            await SaveObjectFileAsync<T>(content, filename);
        }

    }
}
