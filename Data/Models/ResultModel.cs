using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
	public class ResultModel
	{
		public string CH { get; set; }
		public int Total { get; set; }
		public int TotalPass { get; set; }
		public int TotalFail { get; set; }
		public int? TotalUpdate { get; set; }
		public string PercentFail { get; set; }
	}

	public class CountModel
	{
		public int Total { get; set; }
	}
}
