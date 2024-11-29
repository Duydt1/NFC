using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NFC.Data.Entities
{
    public class Sensor : BaseNFC
    {
        [MaxLength(50)]
		[DisplayName("DEVICE NO")]
		public string? DeviceNo { get; set; }
		[MaxLength(50)]
		public string? J1_5 { get; set; }
		[MaxLength(50)]
		public string? J1_6 { get; set; }
		[MaxLength(50)]
		public string? J1_11 { get; set; }
		[MaxLength(50)] 
		public string? J1_17 { get; set; }
		[MaxLength(50)]
		public string? SHORT { get; set; }
		[MaxLength(50)]
		public string? SPK_TW { get; set; }
		[MaxLength(50)]
		public string? SPK_WF { get; set; }
		[MaxLength(50)]
		public string? R2 { get; set; }
		[MaxLength(50)]
		public string? DEVICE_ID1 { get; set; }
		[MaxLength(50)]
		public string? DEVICE_ID2 { get; set; }
		[MaxLength(50)]
		public string? DEVICE_ID3 { get; set; }
		[MaxLength(50)]
		public string? DEVICE_ID4 { get; set; }
		[MaxLength(50)]
		public string? TYPE_ID { get; set; }
		[MaxLength(50)]
		public string? T0_OPEN { get; set; }
		[MaxLength(50)]
		public string? T1_OPEN { get; set; }
		[MaxLength(50)]
		public string? T0_KODAK_CLOSE { get; set; }
		[MaxLength(50)]
		public string? T1_KODAK_CLOSE { get; set; }
		[MaxLength(50)]
		public string? T0_KODAK_DIFF { get; set; }
		[MaxLength(50)]
		public string? T1_KODAK_DIFF { get; set; }
		[MaxLength(50)]
		public string? T0_SKIN_CLOSE { get; set; }
		[MaxLength(50)]
		public string? T1_SKIN_CLOSE { get; set; }
		[MaxLength(50)]
		public string? SKIN_RATIO { get; set; }
		[MaxLength(50)]
		public string? T0_TRIM_CODE { get; set; }
		[MaxLength(50)]
		public string? T1_TRIM_CODE { get; set; }
		[MaxLength(50)]
		public string? T0_TRIM_FACTOR { get; set; }
		[MaxLength(50)]
		public string? T1_TRIM_FACTOR { get; set; }
		[MaxLength(50)]
		public string? CT_ADC_T0_ON { get; set; }
		[MaxLength(50)]
		public string? CT_ADC_T0_OFF { get; set; }
		[MaxLength(50)]
		public string? CT_ADC_T1_ON { get; set; }
		[MaxLength(50)]
		public string? CT_ADC_T1_OFF { get; set; }
		[MaxLength(50)]
		public string? CARD_ADC_T0_ON { get; set; }
		[MaxLength(50)]
		public string? CARD_ADC_T0_OFF { get; set; }
		[MaxLength(50)]
		public string? CARD_ADC_T1_ON { get; set; }
		[MaxLength(50)]
		public string? CARD_ADC_T1_OFF { get; set; }
		[MaxLength(50)]
		public string? SKIN_ADC_T0_ON { get; set; }
		[MaxLength(50)]
		public string? SKIN_ADC_T0_OFF { get; set; }
		[MaxLength(50)]
		public string? SKIN_ADC_T1_ON { get; set; }
		[MaxLength(50)]
		public string? SKIN_ADC_T1_OFF { get; set; }
		[MaxLength(50)]
		public string? TYP_1_T0_G4 { get; set; }
		[MaxLength(50)]
		public string? TYP_1_T1_G4 { get; set; }
		[MaxLength(50)]
		public string? TYP_1_T0_TARGET { get; set; }
		[MaxLength(50)]
		public string? TYP_1_T1_TARGET { get; set; }
		[MaxLength(50)]
		public string? POC_TEMP { get; set; }
		[MaxLength(50)]
		public string? ACT_TEMP { get; set; }
		[MaxLength(50)]
		public string? SKIN_DIFF { get; set; }
		[MaxLength(50)]
		public string? MIC_768KHz_Peak { get; set; }
		[MaxLength(50)]
		public string? BattVolt { get; set; }
	}
}
