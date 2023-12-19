using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataBase.Migrations
{
    /// <inheritdoc />
    public partial class RenameModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SolarInstallationReports_SolarInstallations_InstallationId",
                table: "SolarInstallationReports");

            migrationBuilder.DropForeignKey(
                name: "FK_SolarInstallations_AspNetUsers_UserId",
                table: "SolarInstallations");

            migrationBuilder.DropForeignKey(
                name: "FK_SolarPanelReports_SolarPanels_PanelId",
                table: "SolarPanelReports");

            migrationBuilder.DropForeignKey(
                name: "FK_SolarPanels_SolarInstallations_InstallationId",
                table: "SolarPanels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SolarPanels",
                table: "SolarPanels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SolarPanelReports",
                table: "SolarPanelReports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SolarInstallations",
                table: "SolarInstallations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SolarInstallationReports",
                table: "SolarInstallationReports");

            migrationBuilder.DropColumn(
                name: "SolarInstallationId",
                table: "SolarPanels");

            migrationBuilder.DropColumn(
                name: "SolarPanelId",
                table: "SolarPanelReports");

            migrationBuilder.DropColumn(
                name: "SolarInstallationId",
                table: "SolarInstallationReports");

            migrationBuilder.RenameTable(
                name: "SolarPanels",
                newName: "Panels");

            migrationBuilder.RenameTable(
                name: "SolarPanelReports",
                newName: "PanelReports");

            migrationBuilder.RenameTable(
                name: "SolarInstallations",
                newName: "Installations");

            migrationBuilder.RenameTable(
                name: "SolarInstallationReports",
                newName: "InstallationReports");

            migrationBuilder.RenameIndex(
                name: "IX_SolarPanels_InstallationId",
                table: "Panels",
                newName: "IX_Panels_InstallationId");

            migrationBuilder.RenameColumn(
                name: "SolarRadiation",
                table: "PanelReports",
                newName: "Radiation");

            migrationBuilder.RenameColumn(
                name: "SolarIntensity",
                table: "PanelReports",
                newName: "Intensity");

            migrationBuilder.RenameIndex(
                name: "IX_SolarPanelReports_PanelId",
                table: "PanelReports",
                newName: "IX_PanelReports_PanelId");

            migrationBuilder.RenameIndex(
                name: "IX_SolarInstallations_UserId",
                table: "Installations",
                newName: "IX_Installations_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_SolarInstallationReports_InstallationId",
                table: "InstallationReports",
                newName: "IX_InstallationReports_InstallationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Panels",
                table: "Panels",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PanelReports",
                table: "PanelReports",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Installations",
                table: "Installations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InstallationReports",
                table: "InstallationReports",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InstallationReports_Installations_InstallationId",
                table: "InstallationReports",
                column: "InstallationId",
                principalTable: "Installations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Installations_AspNetUsers_UserId",
                table: "Installations",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PanelReports_Panels_PanelId",
                table: "PanelReports",
                column: "PanelId",
                principalTable: "Panels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Panels_Installations_InstallationId",
                table: "Panels",
                column: "InstallationId",
                principalTable: "Installations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InstallationReports_Installations_InstallationId",
                table: "InstallationReports");

            migrationBuilder.DropForeignKey(
                name: "FK_Installations_AspNetUsers_UserId",
                table: "Installations");

            migrationBuilder.DropForeignKey(
                name: "FK_PanelReports_Panels_PanelId",
                table: "PanelReports");

            migrationBuilder.DropForeignKey(
                name: "FK_Panels_Installations_InstallationId",
                table: "Panels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Panels",
                table: "Panels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PanelReports",
                table: "PanelReports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Installations",
                table: "Installations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InstallationReports",
                table: "InstallationReports");

            migrationBuilder.RenameTable(
                name: "Panels",
                newName: "SolarPanels");

            migrationBuilder.RenameTable(
                name: "PanelReports",
                newName: "SolarPanelReports");

            migrationBuilder.RenameTable(
                name: "Installations",
                newName: "SolarInstallations");

            migrationBuilder.RenameTable(
                name: "InstallationReports",
                newName: "SolarInstallationReports");

            migrationBuilder.RenameIndex(
                name: "IX_Panels_InstallationId",
                table: "SolarPanels",
                newName: "IX_SolarPanels_InstallationId");

            migrationBuilder.RenameColumn(
                name: "Radiation",
                table: "SolarPanelReports",
                newName: "SolarRadiation");

            migrationBuilder.RenameColumn(
                name: "Intensity",
                table: "SolarPanelReports",
                newName: "SolarIntensity");

            migrationBuilder.RenameIndex(
                name: "IX_PanelReports_PanelId",
                table: "SolarPanelReports",
                newName: "IX_SolarPanelReports_PanelId");

            migrationBuilder.RenameIndex(
                name: "IX_Installations_UserId",
                table: "SolarInstallations",
                newName: "IX_SolarInstallations_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_InstallationReports_InstallationId",
                table: "SolarInstallationReports",
                newName: "IX_SolarInstallationReports_InstallationId");

            migrationBuilder.AddColumn<int>(
                name: "SolarInstallationId",
                table: "SolarPanels",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SolarPanelId",
                table: "SolarPanelReports",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SolarInstallationId",
                table: "SolarInstallationReports",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SolarPanels",
                table: "SolarPanels",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SolarPanelReports",
                table: "SolarPanelReports",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SolarInstallations",
                table: "SolarInstallations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SolarInstallationReports",
                table: "SolarInstallationReports",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SolarInstallationReports_SolarInstallations_InstallationId",
                table: "SolarInstallationReports",
                column: "InstallationId",
                principalTable: "SolarInstallations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SolarInstallations_AspNetUsers_UserId",
                table: "SolarInstallations",
                column: "UserId",
                principalTable: "AspNetUsers",
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
    }
}
