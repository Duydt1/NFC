using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NFC.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddMic1PhaseLimitForWF : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MIC1PHASE_Limit",
                table: "KT_MIC_WF_SPLs",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MIC1PHASE_Limit",
                table: "KT_MIC_WF_SPLs");
        }
    }
}
