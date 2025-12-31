using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AluraFlix.Banco.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarReferenciaCategoria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Videos_CategoriaVideos_CategoriaVideoId",
                table: "Videos");

            migrationBuilder.DropIndex(
                name: "IX_Videos_CategoriaVideoId",
                table: "Videos");

            migrationBuilder.RenameColumn(
                name: "CategoriaVideoId",
                table: "Videos",
                newName: "CategoriaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CategoriaId",
                table: "Videos",
                newName: "CategoriaVideoId");

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
    }
}
