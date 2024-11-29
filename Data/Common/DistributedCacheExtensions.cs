using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text;

namespace Data.Common
{
	public static class DistributedCacheExtensions
	{
		public static async Task SetRecordAsync<T>(this IDistributedCache cache, string recordId, T data, TimeSpan? absoluteExpireTime = null, TimeSpan? unusedExpireTime = null)
		{
			var options = new DistributedCacheEntryOptions();
			options.AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromHours(1);
			if (unusedExpireTime.HasValue)
			{
				options.SetSlidingExpiration(unusedExpireTime.Value);
			}

			var jsonData = JsonConvert.SerializeObject(data);
			var bytes = Encoding.UTF8.GetBytes(jsonData);
			await cache.SetAsync(recordId, bytes, options);

		}
		public static async Task<T> GetRecordAsync<T>(this IDistributedCache cache, string recordId)
		{
			var bytes = await cache.GetAsync(recordId);
			if (bytes == null)
			{
				return default(T);
			}
			var jsonData = Encoding.UTF8.GetString(bytes);
			return JsonConvert.DeserializeObject<T>(jsonData);
		}
	}
}
