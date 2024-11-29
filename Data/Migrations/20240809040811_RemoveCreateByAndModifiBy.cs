using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NFC.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCreateByAndModifiBy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hearings_AspNetUsers_CreatedById",
                table: "Hearings");

            migrationBuilder.DropForeignKey(
                name: "FK_Hearings_AspNetUsers_ModifiedById",
                table: "Hearings");

            migrationBuilder.DropForeignKey(
                name: "FK_HistoryUploads_AspNetUsers_CreatedById",
                table: "HistoryUploads");

            migrationBuilder.DropForeignKey(
                name: "FK_HistoryUploads_AspNetUsers_ModifiedById",
                table: "HistoryUploads");

            migrationBuilder.DropForeignKey(
                name: "FK_KT_MIC_WF_SPLs_AspNetUsers_CreatedById",
                table: "KT_MIC_WF_SPLs");

            migrationBuilder.DropForeignKey(
                name: "FK_KT_MIC_WF_SPLs_AspNetUsers_ModifiedById",
                table: "KT_MIC_WF_SPLs");

            migrationBuilder.DropForeignKey(
                name: "FK_KT_TW_SPLs_AspNetUsers_CreatedById",
                table: "KT_TW_SPLs");

            migrationBuilder.DropForeignKey(
                name: "FK_KT_TW_SPLs_AspNetUsers_ModifiedById",
                table: "KT_TW_SPLs");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductionLines_AspNetUsers_CreatedById",
                table: "ProductionLines");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductionLines_AspNetUsers_ModifiedById",
                table: "ProductionLines");

            migrationBuilder.DropForeignKey(
                name: "FK_Sensors_AspNetUsers_CreatedById",
                table: "Sensors");

            migrationBuilder.DropForeignKey(
                name: "FK_Sensors_AspNetUsers_ModifiedById",
                table: "Sensors");

            migrationBuilder.DropIndex(
                name: "IX_Sensors_CreatedById",
                table: "Sensors");

            migrationBuilder.DropIndex(
                name: "IX_Sensors_ModifiedById",
                table: "Sensors");

            migrationBuilder.DropIndex(
                name: "IX_ProductionLines_CreatedById",
                table: "ProductionLines");

            migrationBuilder.DropIndex(
                name: "IX_ProductionLines_ModifiedById",
                table: "ProductionLines");

            migrationBuilder.DropIndex(
                name: "IX_KT_TW_SPLs_CreatedById",
                table: "KT_TW_SPLs");

            migrationBuilder.DropIndex(
                name: "IX_KT_TW_SPLs_ModifiedById",
                table: "KT_TW_SPLs");

            migrationBuilder.DropIndex(
                name: "IX_KT_MIC_WF_SPLs_CreatedById",
                table: "KT_MIC_WF_SPLs");

            migrationBuilder.DropIndex(
                name: "IX_KT_MIC_WF_SPLs_ModifiedById",
                table: "KT_MIC_WF_SPLs");

            migrationBuilder.DropIndex(
                name: "IX_HistoryUploads_CreatedById",
                table: "HistoryUploads");

            migrationBuilder.DropIndex(
                name: "IX_HistoryUploads_ModifiedById",
                table: "HistoryUploads");

            migrationBuilder.DropIndex(
                name: "IX_Hearings_CreatedById",
                table: "Hearings");

            migrationBuilder.DropIndex(
                name: "IX_Hearings_ModifiedById",
                table: "Hearings");

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedById",
                table: "Sensors",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "Sensors",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedById",
                table: "ProductionLines",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "ProductionLines",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedById",
                table: "KT_TW_SPLs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "KT_TW_SPLs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedById",
                table: "KT_MIC_WF_SPLs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "KT_MIC_WF_SPLs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedById",
                table: "HistoryUploads",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "HistoryUploads",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedById",
                table: "Hearings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "Hearings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ModifiedById",
                table: "Sensors",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "Sensors",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedById",
                table: "ProductionLines",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "ProductionLines",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedById",
                table: "KT_TW_SPLs",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "KT_TW_SPLs",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedById",
                table: "KT_MIC_WF_SPLs",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "KT_MIC_WF_SPLs",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedById",
                table: "HistoryUploads",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "HistoryUploads",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedById",
                table: "Hearings",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "Hearings",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sensors_CreatedById",
                table: "Sensors",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Sensors_ModifiedById",
                table: "Sensors",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionLines_CreatedById",
                table: "ProductionLines",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionLines_ModifiedById",
                table: "ProductionLines",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_KT_TW_SPLs_CreatedById",
                table: "KT_TW_SPLs",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_KT_TW_SPLs_ModifiedById",
                table: "KT_TW_SPLs",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_KT_MIC_WF_SPLs_CreatedById",
                table: "KT_MIC_WF_SPLs",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_KT_MIC_WF_SPLs_ModifiedById",
                table: "KT_MIC_WF_SPLs",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryUploads_CreatedById",
                table: "HistoryUploads",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryUploads_ModifiedById",
                table: "HistoryUploads",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Hearings_CreatedById",
                table: "Hearings",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Hearings_ModifiedById",
                table: "Hearings",
                column: "ModifiedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Hearings_AspNetUsers_CreatedById",
                table: "Hearings",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Hearings_AspNetUsers_ModifiedById",
                table: "Hearings",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryUploads_AspNetUsers_CreatedById",
                table: "HistoryUploads",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryUploads_AspNetUsers_ModifiedById",
                table: "HistoryUploads",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_KT_MIC_WF_SPLs_AspNetUsers_CreatedById",
                table: "KT_MIC_WF_SPLs",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_KT_MIC_WF_SPLs_AspNetUsers_ModifiedById",
                table: "KT_MIC_WF_SPLs",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_KT_TW_SPLs_AspNetUsers_CreatedById",
                table: "KT_TW_SPLs",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_KT_TW_SPLs_AspNetUsers_ModifiedById",
                table: "KT_TW_SPLs",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductionLines_AspNetUsers_CreatedById",
                table: "ProductionLines",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductionLines_AspNetUsers_ModifiedById",
                table: "ProductionLines",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sensors_AspNetUsers_CreatedById",
                table: "Sensors",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sensors_AspNetUsers_ModifiedById",
                table: "Sensors",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
