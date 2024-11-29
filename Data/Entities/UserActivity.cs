using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NFC.Data.Entities
{
    public class UserActivity
    {
		[DisplayName("Created By")]
		public string? CreatedById { get; set; }
		[DisplayName("Created On")]
		public DateTime? CreatedOn { get; set; }
		[DisplayName("Modified By")]
		public string? ModifiedById { get; set; }
		[DisplayName("Modified On")]
		public DateTime? ModifiedOn { get; set; }
    }
}
