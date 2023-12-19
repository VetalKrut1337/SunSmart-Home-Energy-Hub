using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataBase.Migrations
{
    /// <inheritdoc />
    public partial class NewModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TiltAngle",
                table: "PanelReports");

            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "PanelReports");

            migrationBuilder.DropColumn(
                name: "TotalPowerOutput",
                table: "InstallationReports");

            migrationBuilder.RenameColumn(
                name: "TotalPowerRating",
                table: "InstallationReports",
                newName: "TiltAngle");

            migrationBuilder.AddColumn<int>(
                name: "InstallationReportId",
                table: "PanelReports",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PanelReports_InstallationReportId",
                table: "PanelReports",
                column: "InstallationReportId");

            migrationBuilder.AddForeignKey(
                name: "FK_PanelReports_InstallationReports_InstallationReportId",
                table: "PanelReports",
                column: "InstallationReportId",
                principalTable: "InstallationReports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PanelReports_InstallationReports_InstallationReportId",
                table: "PanelReports");

            migrationBuilder.DropIndex(
                name: "IX_PanelReports_InstallationReportId",
                table: "PanelReports");

            migrationBuilder.DropColumn(
                name: "InstallationReportId",
                table: "PanelReports");

            migrationBuilder.RenameColumn(
                name: "TiltAngle",
                table: "InstallationReports",
                newName: "TotalPowerRating");

            migrationBuilder.AddColumn<double>(
                name: "TiltAngle",
                table: "PanelReports",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateOnly>(
                name: "Timestamp",
                table: "PanelReports",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<double>(
                name: "TotalPowerOutput",
                table: "InstallationReports",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
