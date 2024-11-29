using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NFC.Data.Entities
{
    public class KT_TW_SPL : BaseNFC
    {
        [MaxLength(50)]
        [DisplayName("Speaker GRADE")]
        public string? Grade { get; set; }
        [MaxLength(50)]
		[DisplayName("Speaker1 SPL[1kHz]")]
		public string? SPL_1kHz { get; set; }
		[MaxLength(50)]
		[DisplayName("FRF Limit")]
		public string? FRFLimit { get; set; }
		[MaxLength(50)]
		[DisplayName("Speaker1 SPL[10kHz]")]
		public string? SPL_10kHz { get; set; }
		[MaxLength(50)]
		[DisplayName("Speaker1 Polarity")]
		public string? Polarity { get; set; }
        [MaxLength(50)]
		[DisplayName("Speaker1 THD[1kHz]")]
		public string? THD_1kHz { get; set; }
		[MaxLength(50)]
		[DisplayName("Speaker1 THD Limit")]
		public string? THDLimit { get; set; }
		[MaxLength(50)]
		[DisplayName("Speaker1 Impedance[1kHz]")]
		public string? Impedance_1kHz { get; set; }
		[MaxLength(50)]
		[DisplayName("Speaker1 Impedance[10kHz]")]
		public string? Impedance_10kHz { get; set; }
		[MaxLength(50)]
		[DisplayName("Speaker1 Impedance Limit")]
		public string? ImpedanceLimit { get; set; }

		[MaxLength(50)]
		[DisplayName("MIC1 SEQ2 FRF Limit")]
		public string? MIC1SEQ2_FRF_Limit { get; set; }
		[MaxLength(50)]
		[DisplayName("MIC1 SENS at 1.5kHz")]
		public string? MIC1SENS_15kHz { get; set; }

	}
}
