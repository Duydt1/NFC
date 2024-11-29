using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NFC.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddFullFieldsFor4NFCType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DEVICE_ID1",
                table: "Sensors",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DEVICE_ID2",
                table: "Sensors",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DEVICE_ID3",
                table: "Sensors",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DEVICE_ID4",
                table: "Sensors",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "J1_11",
                table: "Sensors",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "J1_17",
                table: "Sensors",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "J1_5",
                table: "Sensors",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "J1_6",
                table: "Sensors",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MIC_768KHz_Peak",
                table: "Sensors",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "R2",
                table: "Sensors",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SHORT",
                table: "Sensors",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SKIN_DIFF",
                table: "Sensors",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SPK_TW",
                table: "Sensors",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SPK_WF",
                table: "Sensors",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "T0_KODAK_CLOSE",
                table: "Sensors",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "T0_KODAK_DIFF",
                table: "Sensors",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "T0_OPEN",
                table: "Sensors",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "T0_SKIN_CLOSE",
                table: "Sensors",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "T1_KODAK_CLOSE",
                table: "Sensors",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "T1_KODAK_DIFF",
                table: "Sensors",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "T1_OPEN",
                table: "Sensors",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "T1_SKIN_CLOSE",
                table: "Sensors",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TYPE_ID",
                table: "Sensors",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImpedanceLimit",
                table: "KT_TW_SPLs",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Impedance_10kHz",
                table: "KT_TW_SPLs",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MIC1SENS_15kHz",
                table: "KT_TW_SPLs",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MIC1SEQ2_FRF_Limit",
                table: "KT_TW_SPLs",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SPL_10kHz",
                table: "KT_TW_SPLs",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "THDLimit",
                table: "KT_TW_SPLs",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FRFLimit",
                table: "KT_MIC_WF_SPLs",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImpedanceLimit",
                table: "KT_MIC_WF_SPLs",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MIC1CUR_AVDD",
                table: "KT_MIC_WF_SPLs",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MIC1CUR_DVDD",
                table: "KT_MIC_WF_SPLs",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MIC1SEQ2_FRFLimit",
                table: "KT_MIC_WF_SPLs",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "THDLimit",
                table: "KT_MIC_WF_SPLs",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DEVICE_ID1",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "DEVICE_ID2",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "DEVICE_ID3",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "DEVICE_ID4",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "J1_11",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "J1_17",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "J1_5",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "J1_6",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "MIC_768KHz_Peak",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "R2",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "SHORT",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "SKIN_DIFF",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "SPK_TW",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "SPK_WF",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "T0_KODAK_CLOSE",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "T0_KODAK_DIFF",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "T0_OPEN",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "T0_SKIN_CLOSE",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "T1_KODAK_CLOSE",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "T1_KODAK_DIFF",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "T1_OPEN",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "T1_SKIN_CLOSE",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "TYPE_ID",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "ImpedanceLimit",
                table: "KT_TW_SPLs");

            migrationBuilder.DropColumn(
                name: "Impedance_10kHz",
                table: "KT_TW_SPLs");

            migrationBuilder.DropColumn(
                name: "MIC1SENS_15kHz",
                table: "KT_TW_SPLs");

            migrationBuilder.DropColumn(
                name: "MIC1SEQ2_FRF_Limit",
                table: "KT_TW_SPLs");

            migrationBuilder.DropColumn(
                name: "SPL_10kHz",
                table: "KT_TW_SPLs");

            migrationBuilder.DropColumn(
                name: "THDLimit",
                table: "KT_TW_SPLs");

            migrationBuilder.DropColumn(
                name: "FRFLimit",
                table: "KT_MIC_WF_SPLs");

            migrationBuilder.DropColumn(
                name: "ImpedanceLimit",
                table: "KT_MIC_WF_SPLs");

            migrationBuilder.DropColumn(
                name: "MIC1CUR_AVDD",
                table: "KT_MIC_WF_SPLs");

            migrationBuilder.DropColumn(
                name: "MIC1CUR_DVDD",
                table: "KT_MIC_WF_SPLs");

            migrationBuilder.DropColumn(
                name: "MIC1SEQ2_FRFLimit",
                table: "KT_MIC_WF_SPLs");

            migrationBuilder.DropColumn(
                name: "THDLimit",
                table: "KT_MIC_WF_SPLs");
        }
    }
}
