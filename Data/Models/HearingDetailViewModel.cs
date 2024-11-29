using NFC.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
	public class HearingDetailViewModel
	{
		public Hearing hearing { get; set; }
		public KT_TW_SPL tw { get; set; }
		public KT_MIC_WF_SPL wf { get; set; }
		public Sensor sensor { get; set; }
	}
	public class HearingModel
	{
		public string? Speaker1SPL_1kHz { get; set; }
		public long Id { get; set; }
		public string NUM { get; set; }
		public string? Model { get; set; }
		public string? CH { get; set; }
		public string? Result { get; set; }
		public DateTime DateTime { get; set; }
		public int ProductionLineId { get; set; }
		public ProductionLine? ProductionLine { get; set; }
		public string? ProductionLineName { get; set; }
		public string? HistoryUpdate { get; set; }
	}
}
