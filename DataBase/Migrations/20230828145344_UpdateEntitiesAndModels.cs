using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataBase.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntitiesAndModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TiltAngle",
                table: "Panels");

            migrationBuilder.DropColumn(
                name: "AmbientTemperature",
                table: "PanelReports");

            migrationBuilder.DropColumn(
                name: "Humidity",
                table: "PanelReports");

            migrationBuilder.DropColumn(
                name: "StatusComment",
                table: "PanelReports");

            migrationBuilder.DropColumn(
                name: "StatusReason",
                table: "PanelReports");

            migrationBuilder.DropColumn(
                name: "Weather",
                table: "PanelReports");

            migrationBuilder.RenameColumn(
                name: "Radiation",
                table: "PanelReports",
                newName: "Voltage");

            migrationBuilder.RenameColumn(
                name: "PowerOutput",
                table: "PanelReports",
                newName: "Temperature");

            migrationBuilder.RenameColumn(
                name: "MaxTiltAngle",
                table: "PanelReports",
                newName: "Current");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Voltage",
                table: "PanelReports",
                newName: "Radiation");

            migrationBuilder.RenameColumn(
                name: "Temperature",
                table: "PanelReports",
                newName: "PowerOutput");

            migrationBuilder.RenameColumn(
                name: "Current",
                table: "PanelReports",
                newName: "MaxTiltAngle");

            migrationBuilder.AddColumn<double>(
                name: "TiltAngle",
                table: "Panels",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AmbientTemperature",
                table: "PanelReports",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Humidity",
                table: "PanelReports",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "StatusComment",
                table: "PanelReports",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StatusReason",
                table: "PanelReports",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Weather",
                table: "PanelReports",
                type: "TEXT",
                nullable: true);
        }
    }
}
