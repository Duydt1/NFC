using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NFC.Data.Entities
{
    public class KT_MIC_WF_SPL : BaseNFC
    {
        [MaxLength(50)]
		[DisplayName("Speaker1 SPL[100Hz]")]
		public string? SPL_100Hz { get; set; }
		[MaxLength(50)]
		[DisplayName("FRF Limit")]
		public string? FRFLimit { get; set; }
		[MaxLength(50)]
		[DisplayName("Speaker1 SPL[1kHz]")]
		public string? SPL_1kHz { get; set; }
        [MaxLength(50)]
		[DisplayName("THD Limit")]
		public string? THDLimit { get; set; }
		[MaxLength(50)]
		[DisplayName("Speaker1 Polarity")]
		public string? Polarity { get; set; }

		[MaxLength(50)]
		[DisplayName("Speaker1 Impedance[1kHz]")]
		public string? Impedance_1kHz { get; set; }
		[MaxLength(50)]
		[DisplayName("Speaker1 Impedance Limit")]
		public string? ImpedanceLimit { get; set; }
		[MaxLength(50)]
		[DisplayName("MIC1 SENS at 1kHz")]
		public string? MIC1SENS_1kHz { get; set; }
        [MaxLength(50)]
		[DisplayName("MIC1 Current")]
		public string? MIC1Current { get; set; }
		[MaxLength(50)]
		[DisplayName("MIC1 SEQ2 FRF Limit")]
		public string? MIC1SEQ2_FRFLimit { get; set; }
		[MaxLength(50)]
		[DisplayName("MIC1 CUR_AVDD")]
		public string? MIC1CUR_AVDD { get; set; }
		[MaxLength(50)]
		[DisplayName("MIC1 CUR_DVDD")]
		public string? MIC1CUR_DVDD { get; set; }
		[MaxLength(50)]
		[DisplayName("MIC1 PHASE Limit")]
		public string? MIC1PHASE_Limit { get; set; }
	}
}
