using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class RenamedRoomLogsToRoomOperationAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                newName: "roomOperations");

            migrationBuilder.RenameIndex(
                name: "IX_roomLogs_RoomId",
                table: "roomOperations",
                newName: "IX_roomOperations_RoomId");

            migrationBuilder.RenameIndex(
                name: "IX_roomLogs_CustomerId",
                table: "roomOperations",
                newName: "IX_roomOperations_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_roomOperations",
                table: "roomOperations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_roomOperations_customers_CustomerId",
                table: "roomOperations",
                column: "CustomerId",
                principalTable: "customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_roomOperations_rooms_RoomId",
                table: "roomOperations",
                column: "RoomId",
                principalTable: "rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_roomOperations_customers_CustomerId",
                table: "roomOperations");

            migrationBuilder.DropForeignKey(
                name: "FK_roomOperations_rooms_RoomId",
                table: "roomOperations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_roomOperations",
                table: "roomOperations");

            migrationBuilder.RenameTable(
                name: "roomOperations",
                newName: "roomLogs");

            migrationBuilder.RenameIndex(
                name: "IX_roomOperations_RoomId",
                table: "roomLogs",
                newName: "IX_roomLogs_RoomId");

            migrationBuilder.RenameIndex(
                name: "IX_roomOperations_CustomerId",
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
    }
}
