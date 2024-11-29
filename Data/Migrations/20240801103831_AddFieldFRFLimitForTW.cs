using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NFC.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldFRFLimitForTW : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FRFLimit",
                table: "KT_TW_SPLs",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FRFLimit",
                table: "KT_TW_SPLs");
        }
    }
}
