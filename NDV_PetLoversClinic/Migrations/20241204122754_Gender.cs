using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NDV_PetLoversClinic.Migrations
{
    /// <inheritdoc />
    public partial class Gender : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "gender",
                table: "Person",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_person_Id",
                table: "Clients",
                column: "person_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Person_person_Id",
                table: "Clients",
                column: "person_Id",
                principalTable: "Person",
                principalColumn: "person_Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Person_person_Id",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_person_Id",
                table: "Clients");

            migrationBuilder.AlterColumn<string>(
                name: "gender",
                table: "Person",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
