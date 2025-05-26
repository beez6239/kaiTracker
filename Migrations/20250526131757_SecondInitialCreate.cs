using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KaiCryptoTracker.Migrations
{
    /// <inheritdoc />
    public partial class SecondInitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Verified_Contract",
                table: "Tokens");

            migrationBuilder.RenameColumn(
                name: "Balance",
                table: "Tokens",
                newName: "VerifiedContract");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VerifiedContract",
                table: "Tokens",
                newName: "Balance");

            migrationBuilder.AddColumn<bool>(
                name: "Verified_Contract",
                table: "Tokens",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
