using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentDetails.Migrations
{
    /// <inheritdoc />
    public partial class FourthMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Student_Address_Info",
                columns: table => new
                {
                    AddressId = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student_Address_Info", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_Student_Address_Info_Student_Information_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Student_Information",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Student_Address_Info");
        }
    }
}
