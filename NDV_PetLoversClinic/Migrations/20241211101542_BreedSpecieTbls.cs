using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NDV_PetLoversClinic.Migrations
{
    /// <inheritdoc />
    public partial class BreedSpecieTbls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "breed",
                table: "Pet");

            migrationBuilder.DropColumn(
                name: "specie",
                table: "Pet");

            migrationBuilder.AddColumn<int>(
                name: "breed_Id",
                table: "Pet",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Species",
                columns: table => new
                {
                    specie_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    specie_Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Species", x => x.specie_Id);
                });

            migrationBuilder.CreateTable(
                name: "Breeds",
                columns: table => new
                {
                    breed_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    breed_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    specie_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Breeds", x => x.breed_Id);
                    table.ForeignKey(
                        name: "FK_Breeds_Species_specie_Id",
                        column: x => x.specie_Id,
                        principalTable: "Species",
                        principalColumn: "specie_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pet_breed_Id",
                table: "Pet",
                column: "breed_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Breeds_specie_Id",
                table: "Breeds",
                column: "specie_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pet_Breeds_breed_Id",
                table: "Pet",
                column: "breed_Id",
                principalTable: "Breeds",
                principalColumn: "breed_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pet_Breeds_breed_Id",
                table: "Pet");

            migrationBuilder.DropTable(
                name: "Breeds");

            migrationBuilder.DropTable(
                name: "Species");

            migrationBuilder.DropIndex(
                name: "IX_Pet_breed_Id",
                table: "Pet");

            migrationBuilder.DropColumn(
                name: "breed_Id",
                table: "Pet");

            migrationBuilder.AddColumn<string>(
                name: "breed",
                table: "Pet",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "specie",
                table: "Pet",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
