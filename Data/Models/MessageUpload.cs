namespace NFC.Data.Models
{
	public class MessageUpload
	{
		public MessageUpload() { }
		public long Id { get; set; }
		public int Type { get;set; }
		public string Title { get;set; }
		public int ProductionLineId { get;set; }
		public string UserId { get;set; }
		public string Datas { get;set; }

	}
}
