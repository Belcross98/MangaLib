using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vaporAPI.Migrations
{
    /// <inheritdoc />
    public partial class fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Rating",
                table: "Mangas",
                type: "decimal(2,1)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(2,2)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Rating",
                table: "Mangas",
                type: "decimal(2,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(2,1)",
                oldNullable: true);
        }
    }
}
