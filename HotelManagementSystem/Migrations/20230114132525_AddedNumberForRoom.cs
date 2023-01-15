using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddedNumberForRoom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "customers");

            migrationBuilder.AddColumn<byte>(
                name: "Number",
                table: "rooms",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Number",
                table: "rooms");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
