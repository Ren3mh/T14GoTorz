using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SharedLib.Migrations
{
    /// <inheritdoc />
    public partial class ChangedChat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_AspNetUsers_UserId",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "ReceiverUserName",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "SenderUserName",
                table: "Chats");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Chats",
                newName: "SenderUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Chats_UserId",
                table: "Chats",
                newName: "IX_Chats_SenderUserId");

            migrationBuilder.AddColumn<string>(
                name: "ReceiverUserId",
                table: "Chats",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_ReceiverUserId",
                table: "Chats",
                column: "ReceiverUserId");

            migrationBuilder.AddForeignKey(
                name: "FK1_Chats_Users",
                table: "Chats",
                column: "SenderUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK2_Chats_Users",
                table: "Chats",
                column: "ReceiverUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK1_Chats_Users",
                table: "Chats");

            migrationBuilder.DropForeignKey(
                name: "FK2_Chats_Users",
                table: "Chats");

            migrationBuilder.DropIndex(
                name: "IX_Chats_ReceiverUserId",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "ReceiverUserId",
                table: "Chats");

            migrationBuilder.RenameColumn(
                name: "SenderUserId",
                table: "Chats",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Chats_SenderUserId",
                table: "Chats",
                newName: "IX_Chats_UserId");

            migrationBuilder.AddColumn<string>(
                name: "ReceiverUserName",
                table: "Chats",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SenderUserName",
                table: "Chats",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_AspNetUsers_UserId",
                table: "Chats",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
