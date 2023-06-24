using Microsoft.EntityFrameworkCore.Migrations;

namespace INTERVIEW.Migrations
{
    public partial class added_testCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "testCounts",
                columns: table => new
                {
                    Course = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Frequency = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_testCounts", x => x.Course);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "testCounts");
        }
    }
}
