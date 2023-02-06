using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class RenamedReservationDbSetToRoomLogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_reservations_customers_CustomerId",
                table: "reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_reservations_rooms_RoomId",
                table: "reservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_reservations",
                table: "reservations");

            migrationBuilder.RenameTable(
                name: "reservations",
                newName: "roomLogs");

            migrationBuilder.RenameIndex(
                name: "IX_reservations_RoomId",
                table: "roomLogs",
                newName: "IX_roomLogs_RoomId");

            migrationBuilder.RenameIndex(
                name: "IX_reservations_CustomerId",
                table: "roomLogs",
                newName: "IX_roomLogs_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_roomLogs",
                table: "roomLogs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_roomLogs_customers_CustomerId",
                table: "roomLogs",
                column: "CustomerId",
                principalTable: "customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_roomLogs_rooms_RoomId",
                table: "roomLogs",
                column: "RoomId",
                principalTable: "rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_roomLogs_customers_CustomerId",
                table: "roomLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_roomLogs_rooms_RoomId",
                table: "roomLogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_roomLogs",
                table: "roomLogs");

            migrationBuilder.RenameTable(
                name: "roomLogs",
                newName: "reservations");

            migrationBuilder.RenameIndex(
                name: "IX_roomLogs_RoomId",
                table: "reservations",
                newName: "IX_reservations_RoomId");

            migrationBuilder.RenameIndex(
                name: "IX_roomLogs_CustomerId",
                table: "reservations",
                newName: "IX_reservations_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_reservations",
                table: "reservations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_reservations_customers_CustomerId",
                table: "reservations",
                column: "CustomerId",
                principalTable: "customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_reservations_rooms_RoomId",
                table: "reservations",
                column: "RoomId",
                principalTable: "rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
