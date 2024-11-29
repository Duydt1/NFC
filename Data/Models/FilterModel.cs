namespace NFC.Data.Models
{
	public class FilterModel
	{
		public string? SortOrder { get; set; }
		public string? SearchString { get; set; }
		public DateTime? FromDate { get; set; }
		public DateTime? ToDate { get; set; }	
		public int? ProductionLineId { get; set; }	
		public int PageNumber { get; set; }	
		public int PageSize { get; set; }	
		public List<string> ExistedNum { get; set; }
	}
	public class CountResult
	{
		public int Count { get; set; }
	}
}
