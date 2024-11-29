using NFC.Data.Entities;

namespace Data.Models
{
	internal class BaseNFCModel
	{
		public long Id { get; set; }
		public string NUM { get; set; }
		public string? Model { get; set; }
		public string? CH { get; set; }
		public string? Result { get; set; }
		public DateTime DateTime { get; set; }
		public int ProductionLineId { get; set; }
		public ProductionLine? ProductionLine { get; set; }
		public string? HistoryUpdate { get; set; }
	}
}
