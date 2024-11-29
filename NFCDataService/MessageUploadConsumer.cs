using Data.Common;
using MassTransit;
using Microsoft.Extensions.Caching.Distributed;
using NFC.Data.Common;
using NFC.Data.Entities;
using NFC.Data.Models;
using NFCDataService.Services;
using System.Collections.Concurrent;

namespace NFCDataService
{
	public class MessageUploadConsumer(IServiceProvider serviceProvider) : IConsumer<MessageUpload>
	{
		private readonly IServiceProvider _serviceProvider = serviceProvider;
		//private readonly ConcurrentQueue<MessageUpload> _messageQueue = new ConcurrentQueue<MessageUpload>();
		public async Task Consume(ConsumeContext<MessageUpload> context)
		{
			var config = _serviceProvider.GetService<IConfiguration>();
			var cache = _serviceProvider.GetService<IDistributedCache>();
			var msg = context.Message;
			//_messageQueue.Enqueue(msg);

			//if (_messageQueue.Count >= config.GetValue<int>("BatchSize"))
			//{
			//	await ProcessQueue(null);
			//}
			var cacheKey = GetCacheKey(msg.Type);

			var cachedMessages = await cache.GetRecordAsync<List<MessageUpload>>(cacheKey);

			if (cachedMessages == null)
				cachedMessages = [msg];
			else
				cachedMessages.Add(msg);

			if (cachedMessages.Count >= config.GetValue<int>("BatchSize"))
			{
				await ProcessBatchByType(cacheKey, cachedMessages);
				await cache.RemoveAsync(cacheKey);
			}
			else
			{
				await cache.SetRecordAsync(cacheKey, cachedMessages, TimeSpan.FromDays(1), TimeSpan.FromHours(10));
			}

		}
		
		//private async Task ProcessQueue(object state)
		//{
		//	var batchMessages = new List<MessageUpload>();
		//	var config = _serviceProvider.GetService<IConfiguration>();
		//	while (_messageQueue.TryDequeue(out var message))
		//	{
		//		batchMessages.Add(message);
		//		if (batchMessages.Count >= config.GetValue<int>("BatchSize"))
		//		{
		//			break;
		//		}
		//	}

		//	if (batchMessages.Count > 0)
		//	{
		//		await ProcessBatch(batchMessages);
		//	}
		//}

		private async Task ProcessBatch(List<MessageUpload> messages)
		{
			var groupedMessages = messages.GroupBy(m => GetCacheKey(m.Type));

			foreach (var group in groupedMessages)
			{
				await ProcessBatchByType(group.Key, group.ToList());
			}
		}

		private async Task ProcessBatchByType(string cacheKey, List<MessageUpload> messages)
		{
			var messageUploadService = _serviceProvider.GetService<IMessageUploadService>();
			switch (cacheKey)
			{
				case "rbmessage_kttws":
					await messageUploadService.InsertNFCDataAsync((int)NFCCommon.NFCType.KT_TW_SPL, messages);
					break;
				case "rbmessage_ktmics":
					await messageUploadService.InsertNFCDataAsync((int)NFCCommon.NFCType.KT_MIC_WF_SPL, messages);
					break;
				case "rbmessage_sensors":
					await messageUploadService.InsertNFCDataAsync((int)NFCCommon.NFCType.SENSOR, messages);
					break;
				case "rbmessage_hearings":
					await messageUploadService.InsertNFCDataAsync((int)NFCCommon.NFCType.HEARING, messages);
					break;
				default:
					throw new ArgumentException("Invalid cache key", nameof(cacheKey));
			}
		}

		private string GetCacheKey(int type)
		{
			return type switch
			{
				(int)NFCCommon.NFCType.KT_TW_SPL => "rbmessage_kttws",
				(int)NFCCommon.NFCType.KT_MIC_WF_SPL => "rbmessage_ktmics",
				(int)NFCCommon.NFCType.SENSOR => "rbmessage_sensors",
				(int)NFCCommon.NFCType.HEARING => "rbmessage_hearings",
				_ => throw new ArgumentException("Invalid message type", nameof(type)),
			};
		}
	}
}
