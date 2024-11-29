using NFC.Data.Common;
using System.ComponentModel.DataAnnotations;

namespace NFC.Data.Entities
{
	public class HistoryUpload : UserActivity
    {
        public long Id { get; set; }
        public int Status { get; set; }
        public int Type { get; set; }
		[MaxLength(300)]
		public string? Title { get; set; }
		public string TypeStr => NFCCommon.GetNFCType(Type);
        public string StatusStr => NFCCommon.GetHistoryStatus(Status);
        public string? FileContent { get; set; }
        [MaxLength(255)]
        public string? Message { get; set; }
		public int? ProductionLineId { get; set; }
		public ProductionLine? ProductionLine { get; set; }
	}
}
