using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DessertAPI.Migrations
{
    /// <inheritdoc />
    public partial class DessertCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "desserts",
                columns: table => new
                {
                    DessertID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DessertName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_desserts", x => x.DessertID);
                });

            migrationBuilder.CreateTable(
                name: "flavours",
                columns: table => new
                {
                    FlavourId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlavourName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flavours", x => x.FlavourId);
                });

            migrationBuilder.CreateTable(
                name: "dessertFlavours",
                columns: table => new
                {
                    DessertId = table.Column<int>(type: "int", nullable: false),
                    FlavourId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dessertFlavours", x => new { x.DessertId, x.FlavourId });
                    table.ForeignKey(
                        name: "FK_dessertFlavours_desserts_DessertId",
                        column: x => x.DessertId,
                        principalTable: "desserts",
                        principalColumn: "DessertID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dessertFlavours_flavours_FlavourId",
                        column: x => x.FlavourId,
                        principalTable: "flavours",
                        principalColumn: "FlavourId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_dessertFlavours_FlavourId",
                table: "dessertFlavours",
                column: "FlavourId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "dessertFlavours");

            migrationBuilder.DropTable(
                name: "desserts");

            migrationBuilder.DropTable(
                name: "flavours");
        }
    }
}
