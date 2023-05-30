using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Missing_Person.Migrations
{
    /// <inheritdoc />
    public partial class fifth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "AspNetUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Timestamp",
                table: "AspNetUsers",
                type: "rowversion",
                rowVersion: true,
                nullable: true);
        }
    }
}
