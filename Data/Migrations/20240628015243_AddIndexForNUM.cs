using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NFC.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexForNUM : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Sensors_NUM",
                table: "Sensors",
                column: "NUM");

            migrationBuilder.CreateIndex(
                name: "IX_KT_TW_SPLs_NUM",
                table: "KT_TW_SPLs",
                column: "NUM");

            migrationBuilder.CreateIndex(
                name: "IX_KT_MIC_WF_SPLs_NUM",
                table: "KT_MIC_WF_SPLs",
                column: "NUM");

            migrationBuilder.CreateIndex(
                name: "IX_Hearings_NUM",
                table: "Hearings",
                column: "NUM");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Sensors_NUM",
                table: "Sensors");

            migrationBuilder.DropIndex(
                name: "IX_KT_TW_SPLs_NUM",
                table: "KT_TW_SPLs");

            migrationBuilder.DropIndex(
                name: "IX_KT_MIC_WF_SPLs_NUM",
                table: "KT_MIC_WF_SPLs");

            migrationBuilder.DropIndex(
                name: "IX_Hearings_NUM",
                table: "Hearings");
        }
    }
}
