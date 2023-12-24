using AirBnb.Infrastructure.Common.Settings;
using AirBnB.DoMain.Common.Caching;
using AirBnB.Persistence.Caching.Brokers;
using Force.DeepCloner;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;

namespace AirBnb.Infrastructure.Common.Caching.Brokers;

public class RedisDistributedCacheBroker(IOptions<CacheSettings> cacheSettings, IDistributedCache distributedCache) : ICacheBroker
{
    private readonly DistributedCacheEntryOptions _entryOptions = new()
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(cacheSettings.Value.AbsoluteExpirationInSeconds),
        SlidingExpiration = TimeSpan.FromSeconds(cacheSettings.Value.SlidingExpirationInSeconds)
    };
    
    public async ValueTask<T?> GetAsync<T>(string key)
    {
        var value = await distributedCache.GetAsync(key);
        return value is not null ? JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(value)) : default;
    }
    public ValueTask<bool> TryGetAsync<T>(string key, out T value)
    {
        var foundEntry = distributedCache.GetString(key);

        if (foundEntry is not null)
        {
            value = JsonConvert.DeserializeObject<T>(foundEntry);
            return ValueTask.FromResult(true);
        }

        value = default;
        return ValueTask.FromResult(false);
    }

    public async ValueTask<T?> GetOrSetAsync<T>(string key, Func<Task<T>> valueFactory, CacheEntryOptions? entryOptions = null)
    {
        var cachedValue = await distributedCache.GetStringAsync(key);
        if (cachedValue is not null) JsonConvert.DeserializeObject<T>(cachedValue);
        
        var value = await valueFactory();
        await SetAsync(key, value, entryOptions);

        return value;
    }

    
    public async ValueTask SetAsync<T>(string key, T value, CacheEntryOptions? entryOptions = null)
    {
        await distributedCache.SetStringAsync(key, JsonConvert.SerializeObject(value), GetCacheEntryOptions(entryOptions));
    }
    
    public ValueTask DeleteAsync(string key)
    {
        distributedCache.Remove(key);

        return ValueTask.CompletedTask;
    }

    public DistributedCacheEntryOptions GetCacheEntryOptions(CacheEntryOptions? cacheEntryOptions)
    {
        if (cacheEntryOptions == default || (!cacheEntryOptions.AbsoluteExpirationRelativeToNow.HasValue && !cacheEntryOptions.SlidingExpiration.HasValue))
            return _entryOptions;

        var currentEntryOptions = _entryOptions.DeepClone();

        currentEntryOptions.AbsoluteExpirationRelativeToNow = cacheEntryOptions.AbsoluteExpirationRelativeToNow;
        currentEntryOptions.SlidingExpiration = cacheEntryOptions.SlidingExpiration;

        return currentEntryOptions;
    }
}  
