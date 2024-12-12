using eCommerceBase.Insfrastructure.Utilities.Identity.Middleware;
using MassTransit.Futures.Contracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Concurrent;
using System.Text;

namespace eCommerceBase.Insfrastructure.Utilities.Caching.Redis
{
    /// <summary>
    /// use redis for cache
    /// </summary>
    public class RedisCacheManager(IDistributedCache distributedCache, IConfiguration configuration,
        IHttpContextAccessor? httpContextAccessor) : ICacheService
    {
        private static readonly ConcurrentDictionary<string, bool> CacheKeys = new();
        private readonly IDistributedCache _distributedCache = distributedCache;
        private readonly IConfiguration _configuration = configuration;
        private readonly IHttpContextAccessor? _httpContextAccessor = httpContextAccessor;

        public string GetKey(string region, string methodName, object arg)
        {
            return $"{region}:{methodName}({BuildKey(arg)})";
        }
        public string? Get(string key)
        {
            return _distributedCache.GetString(key);
        }
        public void Set(string key, string value)
        {
            _distributedCache.SetString(key, value, GetDistributedCacheEntryOptions());
        }
        public async Task<string?> GetAsync(string key, CancellationToken cancellation = default)
        {
            return await _distributedCache.GetStringAsync(key, cancellation);
        }
        public async Task<T?> GetAsync<T>(string key, CancellationToken cancellation = default)
            where T : class
        {
            string? cachedValue = await _distributedCache.GetStringAsync(key, cancellation);
            if (cachedValue is null)
            {
                return null;
            }
            T? getValue = JsonConvert.DeserializeObject<T>(cachedValue);
            return getValue;
        }
        public async Task<T> GetAsync<T>(string key, Func<Task<T>> factory, CancellationToken cancellation = default)
           where T : class
        {
            T? cachedValue = await GetAsync<T>(key, cancellation);
            if (cachedValue is not null)
            {
                return (T)cachedValue;
            }
            cachedValue = await factory();
            await SetAsync(key, cachedValue, cancellation);
            return (T)cachedValue;
        }
        public async Task<T> GetAsync<T>(IRequest<T> arg, Func<Task<T>> factory, CancellationToken cancellation = default)
         where T : class
        {
            var region = _configuration["RegionName"];
            var methodName = arg.GetType().FullName?.Replace(region + ".Application.Handlers.", "");
            var key = $"{region}:{methodName}:{GetLanguageCode()}({BuildKey(arg)})";
            T? cachedValue = await GetAsync<T>(key, cancellation);
            if (cachedValue is not null)
            {
                return (T)cachedValue;
            }
            cachedValue = await factory();
            await SetAsync(key, cachedValue, cancellation);
            return (T)cachedValue;
        }
        public async Task<T> GetAsync<T>(IRequest<T> arg, UserScoped userScoped, Func<Task<T>> factory,  CancellationToken cancellation = default)
      where T : class
        {
            var region = _configuration["RegionName"];
            var methodName = arg.GetType().FullName?.Replace(region + ".Application.Handlers.", "");
            var key = $"{region}:{methodName}:{GetLanguageCode()}:{userScoped.Id}:{userScoped.UserGroupId}({BuildKey(arg)})";
            T? cachedValue = await GetAsync<T>(key, cancellation);
            if (cachedValue is not null)
            {
                return (T)cachedValue;
            }
            cachedValue = await factory();
            await SetAsync(key, cachedValue, cancellation);
            return (T)cachedValue;
        }
        public async Task SetAsync<T>(string key, T value, CancellationToken cancellation = default)
          where T : class
        {
            string? cachedValue = JsonConvert.SerializeObject(value);
            await _distributedCache.SetStringAsync(key, cachedValue, GetDistributedCacheEntryOptions(), cancellation);
            CacheKeys.TryAdd(key, false);
        }
        public async Task RemovePatternAsync(string key, CancellationToken cancellation = default)
        {
            var tasks = CacheKeys.Keys
             .Where(x => x.StartsWith(key))
             .Select(y => RemoveByPrefixAsync(y, cancellation));
            await Task.WhenAll(tasks);
        }
        public async Task RemoveByPrefixAsync(string prefixKey, CancellationToken cancellation = default)
        {
            await _distributedCache.RemoveAsync(prefixKey, cancellation);
            CacheKeys.TryRemove(prefixKey, out bool _);
        }
        private static string? BuildKey(object arg)
        {
            if (arg is null)
            {
                return null;
            }

            var sb = new StringBuilder();
            if (arg is IEnumerable<object> enumerable)
            {
                foreach (var item in enumerable)
                {
                    sb.Append(BuildKey(item));
                }
            }
            else
            {
                var properties = arg.GetType().GetProperties();
                foreach (var property in properties)
                {
                    var value = property.GetValue(arg);
                    if (value is IEnumerable<object> collection)
                    {
                        foreach (var item in collection)
                        {
                            sb.Append(BuildKey(item));
                        }
                    }
                    else
                    {
                        sb.Append(value?.ToString() ?? string.Empty);
                        sb.Append('_');
                    }
                }
            }
            return sb.ToString().TrimEnd('_');
        }
        private static DistributedCacheEntryOptions GetDistributedCacheEntryOptions(int ttlSecond = 60)
        {
            return new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(ttlSecond)
            };
        }
        private string GetLanguageCode (){
            var languageCode = "tr";
            if (!String.IsNullOrEmpty(_httpContextAccessor?.HttpContext?.Request.Headers["LanguageCode"].ToString()))
            {
                languageCode = _httpContextAccessor?.HttpContext?.Request.Headers["LanguageCode"].ToString();
            }
            return languageCode!;
        }
    }
}
