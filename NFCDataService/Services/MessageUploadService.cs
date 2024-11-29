using Data.Repositories;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using NFC.Data.Common;
using NFC.Data.Entities;
using NFC.Data.Models;
using System.Transactions;
using static MassTransit.Monitoring.Performance.BuiltInCounters;

namespace NFCDataService.Services
{
	public interface IMessageUploadService
	{
		Task InsertNFCDataAsync(int type, List<MessageUpload> messages);
	}
	public class MessageUploadService(IServiceProvider serviceProvider) : IMessageUploadService
	{
		private readonly IServiceProvider _serviceProvider = serviceProvider;
		public async Task InsertNFCDataAsync(int type, List<MessageUpload> messages)
		{
			switch (type)
			{
				case (int)NFCCommon.NFCType.KT_TW_SPL:
					await InsertListKTTW(messages);
					break;
				case (int)NFCCommon.NFCType.KT_MIC_WF_SPL:
					await InsertListKTMIC(messages);
					break;
				case (int)NFCCommon.NFCType.SENSOR:
					await InsertListSensor(messages);
					break;
				case (int)NFCCommon.NFCType.HEARING:
					await InsertListHearing(messages);
					break;
			}
		}

		private async Task InsertListKTTW(List<MessageUpload> messages)
		{
			try
			{
				var lstKTTW = messages
						.SelectMany(m => JsonConvert.DeserializeObject<List<KT_TW_SPL>>(m.Datas)
						.Select(item => new { item, m.ProductionLineId, m.UserId }))
						.GroupBy(x => x.item.NUM)
						.Select(g => g.Last().item)
						.ToList();

				var repoHistoryUpload = _serviceProvider.GetService<IHistoryUploadRepository>();
				if (lstKTTW.Count != 0)
				{
					try
					{
						var repo = _serviceProvider.GetService<IKT_TW_SPLRepository>();
						var existNUMs = lstKTTW.Select(x => x.NUM).Distinct().ToList();
						//var kttws = await repo.GetExistNums(existNUMs);

						//var kttwsDictionary = kttws.GroupBy(x => x.NUM).ToDictionary(g => g.Key, g => g.Last());
						var entitiesToUpdate = new List<KT_TW_SPL>();
						var entitiesToInsert = new List<KT_TW_SPL>();

						var productionLineIdByNum = messages
														.SelectMany(m => JsonConvert.DeserializeObject<List<KT_TW_SPL>>(m.Datas)
														.Select(item => new { item.NUM, m.ProductionLineId, m.UserId }))
														.GroupBy(x => x.NUM)
														.ToDictionary(g => g.Key, g => (g.Last().ProductionLineId, g.Last().UserId));
						
						foreach (var item in lstKTTW)
						{
							//if (kttwsDictionary.TryGetValue(item.NUM, out var oldEntity))
							//{
							//	UpdateKTTWEntity(oldEntity, item, productionLineIdByNum[item.NUM].UserId, productionLineIdByNum[item.NUM].ProductionLineId);
							//	entitiesToUpdate.Add(oldEntity);
							//}
							//else
							//{
								item.ProductionLineId = productionLineIdByNum[item.NUM].ProductionLineId;
								item.CreatedById = productionLineIdByNum[item.NUM].UserId;
								item.CreatedOn = DateTime.Now;
								entitiesToInsert.Add(item);
							//}
						}
						using var transaction = new TransactionScope(
						TransactionScopeOption.Required,
						new TransactionOptions
						{
							Timeout = TimeSpan.FromMinutes(40) // Adjust timeout as needed
						},
						TransactionScopeAsyncFlowOption.Enabled);
						try
						{
							//if (entitiesToUpdate.Count != 0)
							//{
							//	await repo.UpdateRangeAsync(entitiesToUpdate);
							//}

							if (entitiesToInsert.Count != 0)
							{
								await repo.CreateRangeAsync(entitiesToInsert);
							}
							
							transaction.Complete();
							await UpdateHistoryAsync(messages, "");

						}
						catch (Exception ex)
						{
							var logger = _serviceProvider.GetService<ILogger<MessageUploadService>>();
							logger.LogError(ex, message: "Error Create and Update NFC Data: " + ex.Message);
							await UpdateHistoryAsync(messages, ex.Message);
						}

					}
					catch (Exception ex)
					{
						var logger = _serviceProvider.GetService<ILogger<MessageUploadService>>();
						logger.LogError(ex, message: "Error Create and Update NFC Data: " + ex.Message);
						await UpdateHistoryAsync(messages, ex.Message);
					}
				}
			}
			catch (Exception ex)
			{
				var logger = _serviceProvider.GetService<ILogger<MessageUploadService>>();
				logger.LogError(ex, message: "Error Create and Update NFC Data: " + ex.Message);
				await UpdateHistoryAsync(messages, ex.Message);
			}

		}
		private async Task InsertListKTMIC(List<MessageUpload> messages)
		{
			try
			{
				var lstKTMIC = messages
						.SelectMany(m => JsonConvert.DeserializeObject<List<KT_MIC_WF_SPL>>(m.Datas)
						.Select(item => new { item, m.ProductionLineId, m.UserId }))
						.GroupBy(x => x.item.NUM)
						.Select(g => g.Last().item)
						.ToList();
				var repoHistoryUpload = _serviceProvider.GetService<IHistoryUploadRepository>();
				if (lstKTMIC.Count != 0)
				{
					try
					{
						var repo = _serviceProvider.GetService<IKT_MIC_WF_SPLRepository>();
						var existNUMs = lstKTMIC.Select(x => x.NUM).Distinct().ToList();
						//var ktmics = await repo.GetExistNums(existNUMs);
						//var ktmicsDictionary = ktmics.GroupBy(x => x.NUM).ToDictionary(g => g.Key, g => g.Last());
						var entitiesToUpdate = new List<KT_MIC_WF_SPL>();
						var entitiesToInsert = new List<KT_MIC_WF_SPL>();

						var productionLineIdByNum = messages
														.SelectMany(m => JsonConvert.DeserializeObject<List<KT_MIC_WF_SPL>>(m.Datas)
														.Select(item => new { item.NUM, m.ProductionLineId, m.UserId }))
														.GroupBy(x => x.NUM)
														.ToDictionary(g => g.Key, g => (g.Last().ProductionLineId, g.Last().UserId));

						

						foreach (var item in lstKTMIC)
						{
							//if (ktmicsDictionary.TryGetValue(item.NUM, out var oldEntity))
							//{
							//	UpdateKTMICEntity(oldEntity, item, productionLineIdByNum[item.NUM].UserId, productionLineIdByNum[item.NUM].ProductionLineId);
							//	entitiesToUpdate.Add(oldEntity);
							//}
							//else
							//{
								item.ProductionLineId = productionLineIdByNum[item.NUM].ProductionLineId;
								item.CreatedById = productionLineIdByNum[item.NUM].UserId;
								item.CreatedOn = DateTime.Now;
								entitiesToInsert.Add(item);
							//}
						}
						using var transaction = new TransactionScope(
						TransactionScopeOption.Required,
						new TransactionOptions
						{
							Timeout = TimeSpan.FromMinutes(40) // Adjust timeout as needed
						},
						TransactionScopeAsyncFlowOption.Enabled);
						try
						{
							//if (entitiesToUpdate.Count != 0)
							//{
							//	await repo.UpdateRangeAsync(entitiesToUpdate);
							//}

							if (entitiesToInsert.Count != 0)
							{
								await repo.CreateRangeAsync(entitiesToInsert);
							}
							transaction.Complete();
							await UpdateHistoryAsync(messages, "");
						}
						catch (Exception ex)
						{
							var logger = _serviceProvider.GetService<ILogger<MessageUploadService>>();
							logger.LogError(ex, message: "Error Create and Update NFC Data: " + ex.Message);
							await UpdateHistoryAsync(messages, ex.Message);
						}
					}
					catch (Exception ex)
					{
						var logger = _serviceProvider.GetService<ILogger<MessageUploadService>>();
						logger.LogError(ex, message: "Error Create and Update NFC Data: " + ex.Message);
						await UpdateHistoryAsync(messages, ex.Message);
					}
				}
			}
			catch (Exception ex)
			{
				var logger = _serviceProvider.GetService<ILogger<MessageUploadService>>();
				logger.LogError(ex, message: "Error Create and Update NFC Data: " + ex.Message);
				await UpdateHistoryAsync(messages, ex.Message);
			}

		}
		private async Task InsertListSensor(List<MessageUpload> messages)
		{
			try
			{
				var lstSensor = messages
							.SelectMany(m => JsonConvert.DeserializeObject<List<Sensor>>(m.Datas)
							.Select(item => new { item, m.ProductionLineId, m.UserId }))
							.GroupBy(x => x.item.NUM)
							.Select(g => g.Last().item)
							.ToList(); ;
				var repoHistoryUpload = _serviceProvider.GetService<IHistoryUploadRepository>();
				if (lstSensor.Count > 0)
				{
					try
					{
						var repo = _serviceProvider.GetService<ISensorRepository>();
						var existNUMs = lstSensor.Select(x => x.NUM).Distinct().ToList();
						//var sensors = await repo.GetExistNums(existNUMs);
						//var sensorsDictionary = sensors.GroupBy(x => x.NUM).ToDictionary(g => g.Key, g => g.Last());
						var entitiesToUpdate = new List<Sensor>();
						var entitiesToInsert = new List<Sensor>();
						var productionLineIdByNum = messages
												.SelectMany(m => JsonConvert.DeserializeObject<List<Sensor>>(m.Datas)
												.Select(item => new { item.NUM, m.ProductionLineId, m.UserId }))
												.GroupBy(x => x.NUM)
												.ToDictionary(g => g.Key, g => (g.Last().ProductionLineId, g.Last().UserId));

						
						foreach (var item in lstSensor)
						{
							//if (sensorsDictionary.TryGetValue(item.NUM, out var oldEntity))
							//{
							//	UpdateSensorEntity(oldEntity, item, productionLineIdByNum[item.NUM].UserId, productionLineIdByNum[item.NUM].ProductionLineId);
							//	entitiesToUpdate.Add(oldEntity);
							//}
							//else
							//{
								item.ProductionLineId = messages.First(m => m.Datas.Contains(item.NUM)).ProductionLineId;
								item.CreatedById = messages.First(m => m.Datas.Contains(item.NUM)).UserId;
								item.CreatedOn = DateTime.Now;
								entitiesToInsert.Add(item);
							//}
						}
						using var transaction = new TransactionScope(
						TransactionScopeOption.Required,
						new TransactionOptions
						{
							Timeout = TimeSpan.FromMinutes(40) // Adjust timeout as needed
						},
						TransactionScopeAsyncFlowOption.Enabled);
						try
						{
							//if (entitiesToUpdate.Count != 0)
							//{
							//	await repo.UpdateRangeAsync(entitiesToUpdate);
							//}

							if (entitiesToInsert.Count != 0)
							{
								await repo.CreateRangeAsync(entitiesToInsert);
							}
						
							transaction.Complete();
							await UpdateHistoryAsync(messages, "");

						}
						catch (Exception ex)
						{
							var logger = _serviceProvider.GetService<ILogger<MessageUploadService>>();
							logger.LogError(ex, message: "Error Create and Update NFC Data: " + ex.Message);
							await UpdateHistoryAsync(messages, ex.Message);
						}

					}
					catch (Exception ex)
					{
						var logger = _serviceProvider.GetService<ILogger<MessageUploadService>>();
						logger.LogError(ex, message: "Error Create and Update NFC Data: " + ex.Message);
						await UpdateHistoryAsync(messages, ex.Message);
					}


				}
			}
			catch (Exception ex)
			{
				var logger = _serviceProvider.GetService<ILogger<MessageUploadService>>();
				logger.LogError(ex, message: "Error Create and Update NFC Data: " + ex.Message);
				await UpdateHistoryAsync(messages, ex.Message);
			}

		}
		private async Task InsertListHearing(List<MessageUpload> messages)
		{
			try
			{
				var lstHearing = messages
				.SelectMany(m => JsonConvert.DeserializeObject<List<Hearing>>(m.Datas)
				.Select(item => new { item, m.ProductionLineId, m.UserId }))
				.GroupBy(x => x.item.NUM)
				.Select(g => g.Last().item)
				.ToList(); ;
				var repoHistoryUpload = _serviceProvider.GetService<IHistoryUploadRepository>();
				if (lstHearing.Count > 0)
				{
					try
					{
						var repo = _serviceProvider.GetService<IHearingRepository>();
						var existNUMs = lstHearing.Select(x => x.NUM).Distinct().ToList();
						//var hearings = await repo.GetExistNums(existNUMs);
						//var hearingsDictionary = hearings.GroupBy(x => x.NUM).ToDictionary(g => g.Key, g => g.Last());
						var entitiesToUpdate = new List<Hearing>();
						var entitiesToInsert = new List<Hearing>();

						var productionLineIdByNum = messages
												.SelectMany(m => JsonConvert.DeserializeObject<List<Hearing>>(m.Datas)
												.Select(item => new { item.NUM, m.ProductionLineId, m.UserId }))
												.GroupBy(x => x.NUM)
												.ToDictionary(g => g.Key, g => (g.Last().ProductionLineId, g.Last().UserId));

						
						foreach (var item in lstHearing)
						{
							//if (hearingsDictionary.TryGetValue(item.NUM, out var oldEntity))
							//{
							//	UpdateHearingEntity(oldEntity, item, productionLineIdByNum[item.NUM].UserId, productionLineIdByNum[item.NUM].ProductionLineId);
							//	entitiesToUpdate.Add(oldEntity);
							//}
							//else
							//{
								item.ProductionLineId = messages.First(m => m.Datas.Contains(item.NUM)).ProductionLineId;
								item.CreatedById = messages.First(m => m.Datas.Contains(item.NUM)).UserId;
								item.CreatedOn = DateTime.Now;
								entitiesToInsert.Add(item);
							//}
						}
						using var transaction = new TransactionScope(
						TransactionScopeOption.Required,
						new TransactionOptions
						{
							Timeout = TimeSpan.FromMinutes(40) // Adjust timeout as needed
						},
						TransactionScopeAsyncFlowOption.Enabled);
						try
						{
							//if (entitiesToUpdate.Count != 0)
							//{
							//	await repo.UpdateRangeAsync(entitiesToUpdate);
							//}

							if (entitiesToInsert.Count != 0)
							{
								await repo.CreateRangeAsync(entitiesToInsert);
							}
							
							transaction.Complete();
							await UpdateHistoryAsync(messages, "");
						}
						catch (Exception ex)
						{
							var logger = _serviceProvider.GetService<ILogger<MessageUploadService>>();
							logger.LogError(ex, message: "Error Create and Update NFC Data: " + ex.Message);
							await UpdateHistoryAsync(messages, ex.Message);
						}

					}
					catch (Exception ex)
					{
						var logger = _serviceProvider.GetService<ILogger<MessageUploadService>>();
						logger.LogError(ex, message: "Error Create and Update NFC Data: " + ex.Message);
						await UpdateHistoryAsync(messages, ex.Message);
					}

				}
			}
			catch (Exception ex)
			{
				var logger = _serviceProvider.GetService<ILogger<MessageUploadService>>();
				logger.LogError(ex, message: "Error Create and Update NFC Data: " + ex.Message);
				await UpdateHistoryAsync(messages, ex.Message);
			}

		}

		private static void UpdateKTTWEntity(KT_TW_SPL oldEntity, KT_TW_SPL item, string userId, int productionLineId)
		{
			oldEntity.NUM = item.NUM;
			oldEntity.CH = item.CH;
			oldEntity.Model = item.Model;
			oldEntity.DateTime = item.DateTime;
			oldEntity.SPL_1kHz = item.SPL_1kHz;
			oldEntity.Grade = item.Grade;
			oldEntity.FRFLimit = item.FRFLimit;
			oldEntity.SPL_10kHz = item.SPL_10kHz;
			oldEntity.THDLimit = item.THDLimit;
			oldEntity.Polarity = item.Polarity;
			oldEntity.THD_1kHz = item.THD_1kHz;
			oldEntity.Impedance_1kHz = item.Impedance_1kHz;
			oldEntity.Impedance_10kHz = item.Impedance_10kHz;
			oldEntity.ImpedanceLimit = item.ImpedanceLimit;
			oldEntity.MIC1SEQ2_FRF_Limit = item.MIC1SEQ2_FRF_Limit;
			oldEntity.MIC1SENS_15kHz = item.MIC1SENS_15kHz;
			oldEntity.Result = item.Result;
			oldEntity.ModifiedOn = DateTime.Now;
			oldEntity.ProductionLineId = productionLineId;
			oldEntity.ModifiedById = userId;
		}
		private static void UpdateKTMICEntity(KT_MIC_WF_SPL oldEntity, KT_MIC_WF_SPL item, string userId, int productionLineId)
		{
			oldEntity.NUM = item.NUM;
			oldEntity.CH = item.CH;
			oldEntity.Model = item.Model;
			oldEntity.DateTime = item.DateTime;
			oldEntity.SPL_1kHz = item.SPL_1kHz;
			oldEntity.SPL_100Hz = item.SPL_100Hz;
			oldEntity.FRFLimit = item.FRFLimit;
			oldEntity.THDLimit = item.THDLimit;
			oldEntity.ImpedanceLimit = item.ImpedanceLimit;
			oldEntity.MIC1SENS_1kHz = item.MIC1SENS_1kHz;
			oldEntity.MIC1Current = item.MIC1Current;
			oldEntity.MIC1SEQ2_FRFLimit = item.MIC1SEQ2_FRFLimit;
			oldEntity.MIC1CUR_AVDD = item.MIC1CUR_AVDD;
			oldEntity.MIC1CUR_DVDD = item.MIC1CUR_DVDD;
			oldEntity.MIC1PHASE_Limit = item.MIC1PHASE_Limit;
			oldEntity.Polarity = item.Polarity;
			oldEntity.Impedance_1kHz = item.Impedance_1kHz;
			oldEntity.Result = item.Result;
			oldEntity.ModifiedOn = DateTime.Now;
			oldEntity.ProductionLineId = productionLineId;
			oldEntity.ModifiedById = userId;
		}
		private static void UpdateSensorEntity(Sensor oldEntity, Sensor item, string userId, int productionLineId)
		{
			oldEntity.NUM = item.NUM;
			oldEntity.Model = item.Model;
			oldEntity.CH = item.CH;
			oldEntity.DateTime = item.DateTime;
			oldEntity.Result = item.Result;
			oldEntity.BattVolt = item.BattVolt;
			oldEntity.DeviceNo = item.DeviceNo;
			oldEntity.J1_5 = item.J1_5;
			oldEntity.J1_6 = item.J1_6;
			oldEntity.J1_11 = item.J1_11;
			oldEntity.J1_17 = item.J1_17;
			oldEntity.SHORT = item.SHORT;
			oldEntity.SPK_TW = item.SPK_TW;
			oldEntity.SPK_WF = item.SPK_WF;
			oldEntity.R2 = item.R2;
			oldEntity.DEVICE_ID1 = item.DEVICE_ID1;
			oldEntity.DEVICE_ID2 = item.DEVICE_ID2;
			oldEntity.DEVICE_ID3 = item.DEVICE_ID3;
			oldEntity.DEVICE_ID4 = item.DEVICE_ID4;
			oldEntity.TYPE_ID = item.TYPE_ID;
			oldEntity.T0_OPEN = item.T0_OPEN;
			oldEntity.T1_OPEN = item.T1_OPEN;
			oldEntity.T0_KODAK_CLOSE = item.T0_KODAK_CLOSE;
			oldEntity.T1_KODAK_CLOSE = item.T1_KODAK_CLOSE;
			oldEntity.T0_KODAK_DIFF = item.T0_KODAK_DIFF;
			oldEntity.T1_KODAK_DIFF = item.T1_KODAK_DIFF;
			oldEntity.T0_SKIN_CLOSE = item.T0_SKIN_CLOSE;
			oldEntity.T1_SKIN_CLOSE = item.T1_SKIN_CLOSE;
			oldEntity.SKIN_DIFF = item.SKIN_DIFF;
			oldEntity.SKIN_RATIO = item.SKIN_RATIO;
			oldEntity.T0_TRIM_CODE = item.T0_TRIM_CODE;
			oldEntity.T1_TRIM_CODE = item.T1_TRIM_CODE;
			oldEntity.T0_TRIM_FACTOR = item.T0_TRIM_FACTOR;
			oldEntity.T1_TRIM_FACTOR = item.T1_TRIM_FACTOR;
			oldEntity.CT_ADC_T0_ON = item.CT_ADC_T0_ON;
			oldEntity.CT_ADC_T0_OFF = item.CT_ADC_T0_OFF;
			oldEntity.CT_ADC_T1_ON = item.CT_ADC_T1_ON;
			oldEntity.CT_ADC_T1_OFF = item.CT_ADC_T1_OFF;
			oldEntity.CARD_ADC_T0_ON = item.CARD_ADC_T0_ON;
			oldEntity.CARD_ADC_T0_OFF = item.CARD_ADC_T0_OFF;
			oldEntity.CARD_ADC_T1_ON = item.CARD_ADC_T1_ON;
			oldEntity.CARD_ADC_T1_OFF = item.CARD_ADC_T1_OFF;
			oldEntity.SKIN_ADC_T0_ON = item.SKIN_ADC_T0_ON;
			oldEntity.SKIN_ADC_T0_OFF = item.SKIN_ADC_T0_OFF;
			oldEntity.SKIN_ADC_T1_ON = item.SKIN_ADC_T1_ON;
			oldEntity.SKIN_ADC_T1_OFF = item.SKIN_ADC_T1_OFF;
			oldEntity.TYP_1_T0_G4 = item.TYP_1_T0_G4;
			oldEntity.TYP_1_T1_G4 = item.TYP_1_T1_G4;
			oldEntity.TYP_1_T0_TARGET = item.TYP_1_T0_TARGET;
			oldEntity.TYP_1_T1_TARGET = item.TYP_1_T1_TARGET;
			oldEntity.POC_TEMP = item.POC_TEMP;
			oldEntity.ACT_TEMP = item.ACT_TEMP;
			oldEntity.MIC_768KHz_Peak = item.MIC_768KHz_Peak;
			oldEntity.ModifiedOn = DateTime.Now;
			oldEntity.ModifiedById = userId;
			oldEntity.ProductionLineId = productionLineId;

		}
		private static void UpdateHearingEntity(Hearing oldEntity, Hearing item, string userId, int productionLineId)
		{
			oldEntity.NUM = item.NUM;
			oldEntity.CH = item.CH;
			oldEntity.Model = item.Model;
			oldEntity.DateTime = item.DateTime;
			oldEntity.Result = item.Result;
			oldEntity.Speaker1SPL_1kHz = item.Speaker1SPL_1kHz;
			oldEntity.Rub_Buzz_Limit = item.Rub_Buzz_Limit;
			oldEntity.Rub_Buz_FreqMax = item.Rub_Buz_FreqMax;
			oldEntity.Rub_Buz_dBMax = item.Rub_Buz_dBMax;
			oldEntity.ModifiedOn = DateTime.Now;
			oldEntity.ProductionLineId = productionLineId;
			oldEntity.ModifiedById = userId;

		}
		
		private async Task UpdateHistoryAsync(List<MessageUpload> messages, string errorMes)
		{
			var failedHistoryUploads = messages
												.GroupBy(m => m.Id)
												.Select(g => new HistoryUpload
												{
													Id = g.Key,
													Status = string.IsNullOrEmpty(errorMes) ? (int)NFCCommon.HistoryStatus.Completed : (int)NFCCommon.HistoryStatus.Failed,
													ProductionLineId = g.First().ProductionLineId,
													CreatedOn = DateTime.Now,
													CreatedById = g.First().UserId,
													ModifiedOn = DateTime.Now,
													ModifiedById = g.First().UserId,
													Type = g.First().Type,
													Title = g.First().Title,
													FileContent = g.First().Datas,
													Message = string.IsNullOrEmpty(errorMes) ? "Insert data success" : errorMes.Length > 255 ? errorMes.Substring(0, 255) : errorMes
												}).ToList();
			var repoHistoryUpload = _serviceProvider.GetService<IHistoryUploadRepository>();
			await repoHistoryUpload.BulkUpdateAsync(failedHistoryUploads);
		}
	}
}
