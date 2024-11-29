using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NFC.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddNewFieldForSensorAndHearing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ACT_TEMP",
                table: "Sensors",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CARD_ADC_T0_OFF",
                table: "Sensors",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CARD_ADC_T0_ON",
                table: "Sensors",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CARD_ADC_T1_OFF",
                table: "Sensors",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CARD_ADC_T1_ON",
                table: "Sensors",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CT_ADC_T0_OFF",
                table: "Sensors",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CT_ADC_T0_ON",
                table: "Sensors",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CT_ADC_T1_OFF",
                table: "Sensors",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CT_ADC_T1_ON",
                table: "Sensors",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "POC_TEMP",
                table: "Sensors",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SKIN_ADC_T0_OFF",
                table: "Sensors",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SKIN_ADC_T0_ON",
                table: "Sensors",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SKIN_ADC_T1_OFF",
                table: "Sensors",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SKIN_ADC_T1_ON",
                table: "Sensors",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SKIN_RATIO",
                table: "Sensors",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "T0_TRIM_CODE",
                table: "Sensors",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "T0_TRIM_FACTOR",
                table: "Sensors",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "T1_TRIM_CODE",
                table: "Sensors",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "T1_TRIM_FACTOR",
                table: "Sensors",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TYP_1_T0_G4",
                table: "Sensors",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TYP_1_T0_TARGET",
                table: "Sensors",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TYP_1_T1_G4",
                table: "Sensors",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TYP_1_T1_TARGET",
                table: "Sensors",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "HistoryUploads",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Rub_Buz_FreqMax",
                table: "Hearings",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Rub_Buz_dBMax",
                table: "Hearings",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Rub_Buzz_Limit",
                table: "Hearings",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ACT_TEMP",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "CARD_ADC_T0_OFF",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "CARD_ADC_T0_ON",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "CARD_ADC_T1_OFF",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "CARD_ADC_T1_ON",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "CT_ADC_T0_OFF",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "CT_ADC_T0_ON",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "CT_ADC_T1_OFF",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "CT_ADC_T1_ON",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "POC_TEMP",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "SKIN_ADC_T0_OFF",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "SKIN_ADC_T0_ON",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "SKIN_ADC_T1_OFF",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "SKIN_ADC_T1_ON",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "SKIN_RATIO",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "T0_TRIM_CODE",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "T0_TRIM_FACTOR",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "T1_TRIM_CODE",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "T1_TRIM_FACTOR",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "TYP_1_T0_G4",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "TYP_1_T0_TARGET",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "TYP_1_T1_G4",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "TYP_1_T1_TARGET",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "HistoryUploads");

            migrationBuilder.DropColumn(
                name: "Rub_Buz_FreqMax",
                table: "Hearings");

            migrationBuilder.DropColumn(
                name: "Rub_Buz_dBMax",
                table: "Hearings");

            migrationBuilder.DropColumn(
                name: "Rub_Buzz_Limit",
                table: "Hearings");
        }
    }
}
