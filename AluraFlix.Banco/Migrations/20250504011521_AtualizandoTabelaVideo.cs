using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AluraFlix.Banco.Migrations
{
    /// <inheritdoc />
    public partial class AtualizandoTabelaVideo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoriaVideoId",
                table: "Videos",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Videos_CategoriaVideoId",
                table: "Videos",
                column: "CategoriaVideoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_CategoriaVideos_CategoriaVideoId",
                table: "Videos",
                column: "CategoriaVideoId",
                principalTable: "CategoriaVideos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Videos_CategoriaVideos_CategoriaVideoId",
                table: "Videos");

            migrationBuilder.DropIndex(
                name: "IX_Videos_CategoriaVideoId",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "CategoriaVideoId",
                table: "Videos");
        }
    }
}
