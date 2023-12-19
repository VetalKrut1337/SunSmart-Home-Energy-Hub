using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataBase.Migrations
{
    /// <inheritdoc />
    public partial class MigrationF : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SolarInstallationReports_SolarInstallations_SolarInstallationId",
                table: "SolarInstallationReports");

            migrationBuilder.DropForeignKey(
                name: "FK_SolarPanelReports_SolarPanels_SolarPanelId",
                table: "SolarPanelReports");

            migrationBuilder.DropForeignKey(
                name: "FK_SolarPanels_SolarInstallations_SolarInstallationId",
                table: "SolarPanels");

            migrationBuilder.DropIndex(
                name: "IX_SolarPanels_SolarInstallationId",
                table: "SolarPanels");

            migrationBuilder.DropIndex(
                name: "IX_SolarPanelReports_SolarPanelId",
                table: "SolarPanelReports");

            migrationBuilder.DropIndex(
                name: "IX_SolarInstallationReports_SolarInstallationId",
                table: "SolarInstallationReports");

            migrationBuilder.RenameColumn(
                name: "Commisioning_Date",
                table: "SolarInstallations",
                newName: "CommissioningDate");

            migrationBuilder.AddColumn<int>(
                name: "InstallationId",
                table: "SolarPanels",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PanelId",
                table: "SolarPanelReports",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "SolarInstallations",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<int>(
                name: "InstallationId",
                table: "SolarInstallationReports",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SolarPanels_InstallationId",
                table: "SolarPanels",
                column: "InstallationId");

            migrationBuilder.CreateIndex(
                name: "IX_SolarPanelReports_PanelId",
                table: "SolarPanelReports",
                column: "PanelId");

            migrationBuilder.CreateIndex(
                name: "IX_SolarInstallationReports_InstallationId",
                table: "SolarInstallationReports",
                column: "InstallationId");

            migrationBuilder.AddForeignKey(
                name: "FK_SolarInstallationReports_SolarInstallations_InstallationId",
                table: "SolarInstallationReports",
                column: "InstallationId",
                principalTable: "SolarInstallations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SolarPanelReports_SolarPanels_PanelId",
                table: "SolarPanelReports",
                column: "PanelId",
                principalTable: "SolarPanels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SolarPanels_SolarInstallations_InstallationId",
                table: "SolarPanels",
                column: "InstallationId",
                principalTable: "SolarInstallations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SolarInstallationReports_SolarInstallations_InstallationId",
                table: "SolarInstallationReports");

            migrationBuilder.DropForeignKey(
                name: "FK_SolarPanelReports_SolarPanels_PanelId",
                table: "SolarPanelReports");

            migrationBuilder.DropForeignKey(
                name: "FK_SolarPanels_SolarInstallations_InstallationId",
                table: "SolarPanels");

            migrationBuilder.DropIndex(
                name: "IX_SolarPanels_InstallationId",
                table: "SolarPanels");

            migrationBuilder.DropIndex(
                name: "IX_SolarPanelReports_PanelId",
                table: "SolarPanelReports");

            migrationBuilder.DropIndex(
                name: "IX_SolarInstallationReports_InstallationId",
                table: "SolarInstallationReports");

            migrationBuilder.DropColumn(
                name: "InstallationId",
                table: "SolarPanels");

            migrationBuilder.DropColumn(
                name: "PanelId",
                table: "SolarPanelReports");

            migrationBuilder.DropColumn(
                name: "InstallationId",
                table: "SolarInstallationReports");

            migrationBuilder.RenameColumn(
                name: "CommissioningDate",
                table: "SolarInstallations",
                newName: "Commisioning_Date");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "SolarInstallations",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "INTEGER");

            migrationBuilder.CreateIndex(
                name: "IX_SolarPanels_SolarInstallationId",
                table: "SolarPanels",
                column: "SolarInstallationId");

            migrationBuilder.CreateIndex(
                name: "IX_SolarPanelReports_SolarPanelId",
                table: "SolarPanelReports",
                column: "SolarPanelId");

            migrationBuilder.CreateIndex(
                name: "IX_SolarInstallationReports_SolarInstallationId",
                table: "SolarInstallationReports",
                column: "SolarInstallationId");

            migrationBuilder.AddForeignKey(
                name: "FK_SolarInstallationReports_SolarInstallations_SolarInstallationId",
                table: "SolarInstallationReports",
                column: "SolarInstallationId",
                principalTable: "SolarInstallations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SolarPanelReports_SolarPanels_SolarPanelId",
                table: "SolarPanelReports",
                column: "SolarPanelId",
                principalTable: "SolarPanels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SolarPanels_SolarInstallations_SolarInstallationId",
                table: "SolarPanels",
                column: "SolarInstallationId",
                principalTable: "SolarInstallations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
