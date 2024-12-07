using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NDV_PetLoversClinic.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAge : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "age",
                table: "Pet");

            migrationBuilder.DropColumn(
                name: "age",
                table: "Person");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "age",
                table: "Pet",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "age",
                table: "Person",
                type: "int",
                nullable: true);
        }
    }
}
