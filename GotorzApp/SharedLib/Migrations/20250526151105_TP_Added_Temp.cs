using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SharedLib.Migrations
{
    /// <inheritdoc />
    public partial class TP_Added_Temp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "LocalTemperature",
                table: "TravelPackages",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocalTemperature",
                table: "TravelPackages");
        }
    }
}
