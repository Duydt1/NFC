using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NFC.Data.Entities
{
    public class Hearing : BaseNFC
    {
        [MaxLength(50)]
		[DisplayName("Speaker1 SPL[1kHz]")]
		public string? Speaker1SPL_1kHz { get; set; }
		[MaxLength(50)]
		[DisplayName("Speaker1 Rub&Buzz Limit")]
		public string? Rub_Buzz_Limit { get; set; }
		[MaxLength(50)]
		[DisplayName("Speaker1 Rub&Buzz[Freq Max]")]
		public string? Rub_Buz_FreqMax { get; set; }
		[MaxLength(50)]
		[DisplayName("Speaker1 Rub&Buzz[dB Max]")]
		public string? Rub_Buz_dBMax { get; set; }
	}
}
