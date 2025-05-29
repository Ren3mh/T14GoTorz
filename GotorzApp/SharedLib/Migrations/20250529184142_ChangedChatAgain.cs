using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SharedLib.Migrations
{
    /// <inheritdoc />
    public partial class ChangedChatAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK2_Chats_Users",
                table: "Chats");

            migrationBuilder.AddForeignKey(
                name: "FK2_Chats_Users",
                table: "Chats",
                column: "ReceiverUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK2_Chats_Users",
                table: "Chats");

            migrationBuilder.AddForeignKey(
                name: "FK2_Chats_Users",
                table: "Chats",
                column: "ReceiverUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
