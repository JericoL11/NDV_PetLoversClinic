using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NDV_PetLoversClinic.Migrations
{
    /// <inheritdoc />
    public partial class AlterTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Person_Clients_Clientsclient_Id",
                table: "Person");

            migrationBuilder.DropIndex(
                name: "IX_Person_Clientsclient_Id",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "Clientsclient_Id",
                table: "Person");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Clientsclient_Id",
                table: "Person",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Person_Clientsclient_Id",
                table: "Person",
                column: "Clientsclient_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Clients_Clientsclient_Id",
                table: "Person",
                column: "Clientsclient_Id",
                principalTable: "Clients",
                principalColumn: "client_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
