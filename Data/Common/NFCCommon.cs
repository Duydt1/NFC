using Microsoft.Extensions.Caching.Distributed;

namespace NFC.Data.Common
{
    public class NFCCommon
    {
		public enum NFCType
        {
            KT_TW_SPL = 1,
            KT_MIC_WF_SPL = 2,
            SENSOR = 3,
            HEARING = 4,
        }

		public enum HistoryStatus
		{
			New = 1,
			Processing = 2,
			Completed = 3,
			Declined = 4,
			Pending = 5,
			Failed = 6,
		}

		public static string GetNFCType(int nfcTypeCode)
        {
			return nfcTypeCode switch
			{
				1 => "KT TW SPL",
				2 => "KT MIC & WF SPL ",
				3 => "SENSOR",
				4 => "HEARING",
				_ => "",
			};
		}

		public static string GetHistoryStatus(int historyStatus)
        {
			return historyStatus switch
			{
				1 => "New",
				2 => "Processing",
				3 => "Completed",
				4 => "Declined",
				5 => "Pending",
				6 => "Failed",
				_ => "",
			};
		}
    }
}
