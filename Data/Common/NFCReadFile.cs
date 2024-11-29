using NetTopologySuite.Index.HPRtree;
using NFC.Data.Entities;
using System.IO;
using System.Reflection.Metadata.Ecma335;

namespace NFC.Data.Common
{
	public class NFCReadFile
	{
		public static async Task<List<KT_TW_SPL>> ReadListKTTWAsync(StreamReader reader)
		{
			string? line;
			var lstKTTW = new List<KT_TW_SPL>();
			while ((line = await reader.ReadLineAsync()) != null)
			{
				var parts = line.Split(',');

				// Tìm chỉ số của các cột
				var numIndex = Array.IndexOf(parts, "[ID]");
				var chIndex = Array.IndexOf(parts, "[CH]");
				var modelIndex = Array.IndexOf(parts, "[MODEL]");
				var timeIndex = Array.IndexOf(parts, "[TIME]");
				var frfLimitIndex = Array.IndexOf(parts, "Speaker1 FRF Limit");
				var impedanceLimitIndex = Array.IndexOf(parts, "Speaker1 Impedance Limit");
				var impedance10kHzIndex = Array.IndexOf(parts, "Speaker1 Impedance[10kHz]");
				var spl10kHzIndex = Array.IndexOf(parts, "Speaker1 SPL[10kHz]");
				var polarityIndex = Array.IndexOf(parts, "Speaker1 Polarity");
				var seqFRFLimitIndex = Array.IndexOf(parts, "MIC1 SEQ2 FRF Limit");
				var sen15kHzIndex = Array.IndexOf(parts, "MIC1 SENS at 1.5kHz");
				var thdLimitIndex = Array.IndexOf(parts, "Speaker1 THD Limit");
				var resultIndex = Array.IndexOf(parts, "[P/F]");
				if (numIndex != -1 && chIndex != -1 && modelIndex != -1 && timeIndex != -1)
				{
					//var dateTimeString = timeIndex != -1 ? parts[timeIndex + 1] : null;
					//DateTime dateTime = !string.IsNullOrEmpty(dateTimeString) ? DateTime.Parse(dateTimeString) : DateTime.MinValue;

					string frfLimitValue = frfLimitIndex != -1 ? string.Join(",", parts.Skip(frfLimitIndex + 1).Take(4)) : string.Empty;
					string spl10kHzValue = spl10kHzIndex != -1 ? string.Join(",", parts.Skip(spl10kHzIndex + 1).Take(4)) : string.Empty;
					var polarityValue = polarityIndex != -1 ? string.Join(",", parts.Skip(polarityIndex + 1).Take(4)) : string.Empty;
					var thdLimitValue = thdLimitIndex != -1 ? string.Join(",", parts.Skip(thdLimitIndex + 1).Take(4)) : string.Empty;
					var impedanceLimitValue = impedanceLimitIndex != -1 ? string.Join(",", parts.Skip(impedanceLimitIndex + 1).Take(4)) : string.Empty;
					var impedance10kHzValue = impedance10kHzIndex != -1 ? string.Join(",", parts.Skip(impedance10kHzIndex + 1).Take(4)) : string.Empty;
					var micSEQ2FRFLimitValue = seqFRFLimitIndex != -1 ? string.Join(",", parts.Skip(seqFRFLimitIndex + 1).Take(4)) : string.Empty;
					var micSENSValue = sen15kHzIndex != -1 ? string.Join(",", parts.Skip(sen15kHzIndex + 1).Take(4)) : string.Empty;

					lstKTTW.Add(new KT_TW_SPL
					{
						NUM = numIndex != -1 ? parts[numIndex + 1] : string.Empty,
						CH = chIndex != -1 ? parts[chIndex + 1] : string.Empty,
						Model = modelIndex != -1 ? parts[modelIndex + 1] : string.Empty,
						DateTime = DateTime.Now,
						SPL_10kHz = spl10kHzValue,
						Polarity = polarityValue,
						Impedance_10kHz = impedance10kHzValue,
						FRFLimit = frfLimitValue,
						ImpedanceLimit = impedanceLimitValue,
						THDLimit = thdLimitValue,
						MIC1SEQ2_FRF_Limit = micSEQ2FRFLimitValue,
						MIC1SENS_15kHz = micSENSValue,
						Result = resultIndex != -1 ? parts[resultIndex + 1] : string.Empty
					});
				}

			}
			return lstKTTW;
		}
		public static async Task<List<KT_MIC_WF_SPL>> ReadListKTMICAsync(StreamReader reader)
		{
			string? line;
			var lstKTMIC = new List<KT_MIC_WF_SPL>();
			while ((line = await reader.ReadLineAsync()) != null)
			{
				var parts = line.Split(',');

				// Tìm chỉ số của các cột cần thiết
				var numIndex = Array.IndexOf(parts, "[ID]");
				var chIndex = Array.IndexOf(parts, "[CH]");
				var modelIndex = Array.IndexOf(parts, "[MODEL]");
				var timeIndex = Array.IndexOf(parts, "[TIME]");
				var spl1kHzIndex = Array.IndexOf(parts, "Speaker1 SPL[1kHz]");
				var frfLimitIndex = Array.IndexOf(parts, "Speaker1 FRF Limit");
				var thdLimitIndex = Array.IndexOf(parts, "Speaker1 THD Limit");
				var polarityIndex = Array.IndexOf(parts, "Speaker1 Polarity");
				var impedance1kHzIndex = Array.IndexOf(parts, "Speaker1 Impedance[1kHz]");
				var impedanceLimitIndex = Array.IndexOf(parts, "Speaker1 Impedance Limit");
				var mic1SENS1kHzIndex = Array.IndexOf(parts, "MIC1 SENS at 1kHz");
				var mic1CurrentIndex = Array.IndexOf(parts, "MIC1 Current");
				var mic1SEQ2FRFLimitIndex = Array.IndexOf(parts, "MIC1 SEQ2 FRF Limit");
				var mic1CUR_AVDDIndex = Array.IndexOf(parts, "MIC1 CUR_AVDD");
				var mic1CUR_DVDDIndex = Array.IndexOf(parts, "MIC1 CUR_DVDD");
				var mic1Phase_LimitIndex = Array.IndexOf(parts, "MIC1 PHASE Limit");
				var resultIndex = Array.IndexOf(parts, "[P/F]");
				if (numIndex != -1 && chIndex != -1 && modelIndex != -1 && timeIndex != -1)
				{
					//var dateTimeString = timeIndex != -1 ? parts[timeIndex + 1] : null;
					//DateTime dateTime = !string.IsNullOrEmpty(dateTimeString) ? DateTime.Parse(dateTimeString) : DateTime.MinValue;

					string frfLimitValue = frfLimitIndex != -1 ? string.Join(",", parts.Skip(frfLimitIndex + 1).Take(4)) : string.Empty;
					string spl1kHzValue = spl1kHzIndex != -1 ? string.Join(",", parts.Skip(spl1kHzIndex + 1).Take(4)) : string.Empty;
					var polarityValue = polarityIndex != -1 ? string.Join(",", parts.Skip(polarityIndex + 1).Take(4)) : string.Empty;
					var thdLimitValue = thdLimitIndex != -1 ? string.Join(",", parts.Skip(thdLimitIndex + 1).Take(4)) : string.Empty;
					var impedanceLimitValue = impedanceLimitIndex != -1 ? string.Join(",", parts.Skip(impedanceLimitIndex + 1).Take(4)) : string.Empty;
					var impedance1kHzValue = impedance1kHzIndex != -1 ? string.Join(",", parts.Skip(impedance1kHzIndex + 1).Take(4)) : string.Empty;
					var micSEQ2FRFLimitValue = mic1SEQ2FRFLimitIndex != -1 ? string.Join(",", parts.Skip(mic1SEQ2FRFLimitIndex + 1).Take(4)) : string.Empty;
					var micSENSValue = mic1SENS1kHzIndex != -1 ? string.Join(",", parts.Skip(mic1SENS1kHzIndex + 1).Take(4)) : string.Empty;
					var micCurrentValue = mic1CurrentIndex != -1 ? string.Join(",", parts.Skip(mic1CurrentIndex + 1).Take(4)) : string.Empty;
					var micCurAVDDValue = mic1CUR_AVDDIndex != -1 ? string.Join(",", parts.Skip(mic1CUR_AVDDIndex + 1).Take(4)) : string.Empty;
					var micCurDVDDValue = mic1CUR_DVDDIndex != -1 ? string.Join(",", parts.Skip(mic1CUR_DVDDIndex + 1).Take(4)) : string.Empty;
					var micPhaseLimitValue = mic1Phase_LimitIndex != -1 ? string.Join(",", parts.Skip(mic1Phase_LimitIndex + 1).Take(4)) : string.Empty;

					lstKTMIC.Add(new KT_MIC_WF_SPL
					{
						NUM = numIndex != -1 ? parts[numIndex + 1] : string.Empty,
						CH = chIndex != -1 ? parts[chIndex + 1] : string.Empty,
						Model = modelIndex != -1 ? parts[modelIndex + 1] : string.Empty,
						SPL_1kHz = spl1kHzValue,
						FRFLimit = frfLimitValue,
						THDLimit = thdLimitValue,
						Polarity = polarityValue,
						Impedance_1kHz = impedance1kHzValue,
						ImpedanceLimit = impedanceLimitValue,
						MIC1SENS_1kHz = micSENSValue,
						MIC1Current = micCurrentValue,
						MIC1SEQ2_FRFLimit = micSEQ2FRFLimitValue,
						MIC1CUR_AVDD = micCurAVDDValue,
						MIC1CUR_DVDD = micCurDVDDValue,
						MIC1PHASE_Limit = micPhaseLimitValue,
						DateTime = DateTime.Now,
						Result = resultIndex != -1 ? parts[resultIndex + 1] : string.Empty
					});
				}

			}
			return lstKTMIC;

		}
		public static async Task<List<Sensor>> ReadListSensorAsync(StreamReader reader)
		{

			string? line;
			var lstSensor = new List<Sensor>();

			// Tìm chỉ số của các cột cần thiết
			int numIndex = 0; // "NUM" nằm ở vị trí 0
			int chIndex = 1; // "CH" nằm ở vị trí 1
			int modelIndex = 2; // "Model" nằm ở vị trí 2
			int deviceNoIndex = 3; // "DEVICE NO" nằm ở vị trí 3
			int j15Index = 4; // "J1_5-6[GND-GND](ohm)" nằm ở vị trí 5
			int j16Index = 5; // "J1_6-11[GND-GND](ohm)" nằm ở vị trí 6
			int j111Index = 6; // "J1_11-17[GND-GND](ohm)" nằm ở vị trí 7
			int j117Index = 7; // "J1_17-18[GND-GND](ohm)" nằm ở vị trí 8
			int shortIndex = 8; // "SHORT(Kohm)" nằm ở vị trí 9
			int spkTWIndex = 9; // "SPK TW(ohm)" nằm ở vị trí 10
			int spkWFIndex = 10; // "SPK WF(ohm)" nằm ở vị trí 11
			int r2Index = 11; // "R2(Kohm)" nằm ở vị trí 12
			int deviceID1Index = 12; // "DEVICE ID1" nằm ở vị trí 13
			int deviceID2Index = 13; // "DEVICE ID2" nằm ở vị trí 14
			int deviceID3Index = 14; // "DEVICE ID3" nằm ở vị trí 15
			int deviceID4Index = 15; // "DEVICE ID4" nằm ở vị trí 16
			int typeIDIndex = 16; // "TYPE ID" nằm ở vị trí 17
			int t0OpenIndex = 17; // "T0 OPEN" nằm ở vị trí 18
			int t1OpenIndex = 18; // "T1 OPEN" nằm ở vị trí 19
			int t0KodakCloseIndex = 19; // "T0 KODAK CLOSE" nằm ở vị trí 20
			int t1KodakCloseIndex = 20; // "T1 KODAK CLOSE" nằm ở vị trí 21
			int t0KodakDiffIndex = 21; // "T0 KODAK DIFF" nằm ở vị trí 22
			int t1KodakDiffIndex = 22; // "T1 KODAK DIFF" nằm ở vị trí 23
			int t0SkinCloseIndex = 23; // "T0 SKIN CLOSE" nằm ở vị trí 24
			int t1SkinCloseIndex = 24; // "T1 SKIN CLOSE" nằm ở vị trí 25
			int skinRatioIndex = 25; // "SKIN_RATIO" nằm ở vị trí 30
			int t0TrimCodeIndex = 26; // "T0_TRIM_CODE" nằm ở vị trí 31
			int t1TrimCodeIndex = 27; // "T1_TRIM_CODE" nằm ở vị trí 32
			int t0TrimFactorIndex = 28; // "T0_TRIM_FACTOR" nằm ở vị trí 33
			int t1TrimFactorIndex = 29; // "T1_TRIM_FACTOR" nằm ở vị trí 34
			int ctAdcT0OnIndex = 30; // "CT_ADC_T0_ON" nằm ở vị trí 35
			int ctAdcT0OffIndex = 31; // "CT_ADC_T0_OFF" nằm ở vị trí 36
			int ctAdcT1OnIndex = 32; // "CT_ADC_T1_ON" nằm ở vị trí 37
			int ctAdcT1OffIndex = 33; // "CT_ADC_T1_OFF" nằm ở vị trí 38
			int cardAdcT0OnIndex = 34; // "CARD_ADC_T0_ON" nằm ở vị trí 39
			int cardAdcT0OffIndex = 35; // "CARD_ADC_T0_OFF" nằm ở vị trí 40
			int cardAdcT1OnIndex = 36; // "CARD_ADC_T1_ON" nằm ở vị trí 41
			int cardAdcT1OffIndex = 37; // "CARD_ADC_T1_OFF" nằm ở vị trí 42
			int skinAdcT0OnIndex = 38; // "SKIN_ADC_T0_ON" nằm ở vị trí 43
			int skinAdcT0OffIndex = 39; // "SKIN_ADC_T0_OFF" nằm ở vị trí 44
			int skinAdcT1OnIndex = 40; // "SKIN_ADC_T1_ON" nằm ở vị trí 45
			int skinAdcT1OffIndex = 41; // "SKIN_ADC_T1_OFF" nằm ở vị trí 46
			int typ1T0G4Index = 42; // "TYP_1_T0_G4" nằm ở vị trí 47
			int typ1T1G4Index = 43; // "TYP_1_T1_G4" nằm ở vị trí 48
			int typ1T0TargetIndex = 44; // "TYP_1_T0_TARGET" nằm ở vị trí 49
			int typ1T1TargetIndex = 45; // "TYP_1_T1_TARGET" nằm ở vị trí 50
			int pocTempIndex = 46; // "POC_TEMP" nằm ở vị trí 51
			int actTempIndex = 47; // "ACT_TEMP" nằm ở vị trí 52
			while ((line = await reader.ReadLineAsync()) != null)
			{
				if (line.Contains("CH") || string.IsNullOrEmpty(line))
					continue;
				else
				{
					var parts = line.Split(',');
					lstSensor.Add(new Sensor
					{
						NUM = parts[numIndex],
						CH = parts[chIndex],
						Model = parts[modelIndex],
						DeviceNo = parts[deviceNoIndex],
						J1_5 = parts[j15Index],
						J1_6 = parts[j16Index],
						J1_11 = parts[j111Index],
						J1_17 = parts[j117Index],
						SHORT = parts[shortIndex],
						SPK_TW = parts[spkTWIndex],
						SPK_WF = parts[spkWFIndex],
						R2 = parts[r2Index],
						DEVICE_ID1 = parts[deviceID1Index],
						DEVICE_ID2 = parts[deviceID2Index],
						DEVICE_ID3 = parts[deviceID3Index],
						DEVICE_ID4 = parts[deviceID4Index],
						TYPE_ID = parts[typeIDIndex],
						T0_OPEN = parts[t0OpenIndex],
						T1_OPEN = parts[t1OpenIndex],
						T0_KODAK_CLOSE = parts[t0KodakCloseIndex],
						T1_KODAK_CLOSE = parts[t1KodakCloseIndex],
						T0_KODAK_DIFF = parts[t0KodakDiffIndex],
						T1_KODAK_DIFF = parts[t1KodakDiffIndex],
						T0_SKIN_CLOSE = parts[t0SkinCloseIndex],
						T1_SKIN_CLOSE = parts[t1SkinCloseIndex],
						SKIN_RATIO = parts[skinRatioIndex],
						T0_TRIM_CODE = parts[t0TrimCodeIndex],
						T1_TRIM_CODE = parts[t1TrimCodeIndex],
						T0_TRIM_FACTOR = parts[t0TrimFactorIndex],
						T1_TRIM_FACTOR = parts[t1TrimFactorIndex],
						CT_ADC_T0_ON = parts[ctAdcT0OnIndex],
						CT_ADC_T0_OFF = parts[ctAdcT0OffIndex],
						CT_ADC_T1_ON = parts[ctAdcT1OnIndex],
						CT_ADC_T1_OFF = parts[ctAdcT1OffIndex],
						CARD_ADC_T0_ON = parts[cardAdcT0OnIndex],
						CARD_ADC_T0_OFF = parts[cardAdcT0OffIndex],
						CARD_ADC_T1_ON = parts[cardAdcT1OnIndex],
						CARD_ADC_T1_OFF = parts[cardAdcT1OffIndex],
						SKIN_ADC_T0_ON = parts[skinAdcT0OnIndex],
						SKIN_ADC_T0_OFF = parts[skinAdcT0OffIndex],
						SKIN_ADC_T1_ON = parts[skinAdcT1OnIndex],
						SKIN_ADC_T1_OFF = parts[skinAdcT1OffIndex],
						TYP_1_T0_G4 = parts[typ1T0G4Index],
						TYP_1_T1_G4 = parts[typ1T1G4Index],
						TYP_1_T0_TARGET = parts[typ1T0TargetIndex],
						TYP_1_T1_TARGET = parts[typ1T1TargetIndex],
						POC_TEMP = parts[pocTempIndex],
						ACT_TEMP = parts[actTempIndex],
						MIC_768KHz_Peak = parts[^2],
						BattVolt = parts[^3],
						DateTime = DateTime.Now,
						Result = parts[^1],
					});
				}
			}

			return lstSensor;


		}
		public static async Task<List<Hearing>> ReadListHearingAsync(StreamReader reader)
		{
			string? line;
			var lstHearing = new List<Hearing>();

			while ((line = await reader.ReadLineAsync()) != null)
			{
				if (line.Contains("CH") || string.IsNullOrEmpty(line))
					continue;
				else
				{
					var parts = line.Split(',');
					lstHearing.Add(new Hearing
					{
						NUM = parts[0],
						CH = parts[1],
						Model = parts[2],
						//DateTime = !string.IsNullOrEmpty(parts[3]) ? parts[1] == "7" ? DateTime.Now : DateTime.Parse(parts[3]) : DateTime.MinValue,
						DateTime = DateTime.Now,
						Speaker1SPL_1kHz = parts[4],
						Rub_Buzz_Limit = parts[5],
						Rub_Buz_FreqMax = parts[6],
						Rub_Buz_dBMax = parts[7],
						Result = parts[8],
					});
				}
			}

			return lstHearing;
		}
	}
}
