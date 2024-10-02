using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RpgApi.Migrations
{
    /// <inheritdoc />
    public partial class MigracaoMuitosParaMuitos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_HABILIDADES",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Dano = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_HABILIDADES", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_PERSONAGENS_HABILIDADES",
                columns: table => new
                {
                    PersonagemId = table.Column<int>(type: "int", nullable: false),
                    HabilidadeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_PERSONAGENS_HABILIDADES", x => new { x.PersonagemId, x.HabilidadeId });
                    table.ForeignKey(
                        name: "FK_TB_PERSONAGENS_HABILIDADES_TB_HABILIDADES_HabilidadeId",
                        column: x => x.HabilidadeId,
                        principalTable: "TB_HABILIDADES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_PERSONAGENS_HABILIDADES_TB_PERSONAGENS_PersonagemId",
                        column: x => x.PersonagemId,
                        principalTable: "TB_PERSONAGENS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TB_HABILIDADES",
                columns: new[] { "Id", "Dano", "Nome" },
                values: new object[,]
                {
                    { 1, 39, "Adormecer" },
                    { 2, 41, "Congelar" },
                    { 3, 34, "Hipnotizar" }
                });

            migrationBuilder.UpdateData(
                table: "TB_USUARIOS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 209, 40, 43, 133, 136, 216, 5, 115, 15, 150, 101, 236, 156, 100, 184, 50, 75, 20, 139, 30, 120, 71, 169, 126, 147, 247, 82, 177, 41, 206, 43, 235, 46, 76, 236, 86, 162, 74, 191, 114, 91, 59, 162, 232, 172, 29, 101, 16, 204, 107, 238, 3, 190, 172, 145, 105, 108, 90, 137, 90, 148, 128, 53, 251 }, new byte[] { 221, 219, 69, 31, 156, 101, 218, 96, 57, 230, 25, 141, 232, 71, 139, 98, 78, 225, 232, 190, 14, 234, 24, 240, 34, 7, 153, 228, 90, 168, 103, 64, 243, 142, 62, 111, 252, 150, 9, 179, 47, 34, 69, 17, 157, 115, 114, 111, 251, 120, 105, 111, 215, 230, 28, 155, 194, 85, 1, 190, 51, 186, 91, 231, 95, 45, 33, 80, 62, 188, 109, 63, 171, 230, 88, 60, 87, 88, 101, 213, 99, 146, 142, 212, 205, 3, 243, 27, 149, 124, 193, 247, 192, 21, 194, 142, 82, 138, 132, 115, 193, 122, 243, 56, 242, 110, 15, 163, 89, 250, 105, 196, 217, 186, 121, 113, 73, 66, 226, 110, 74, 179, 237, 40, 93, 30, 28, 175 } });

            migrationBuilder.InsertData(
                table: "TB_PERSONAGENS_HABILIDADES",
                columns: new[] { "HabilidadeId", "PersonagemId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 2, 2 },
                    { 2, 3 },
                    { 3, 3 },
                    { 3, 4 },
                    { 1, 5 },
                    { 2, 6 },
                    { 3, 7 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_PERSONAGENS_HABILIDADES_HabilidadeId",
                table: "TB_PERSONAGENS_HABILIDADES",
                column: "HabilidadeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_PERSONAGENS_HABILIDADES");

            migrationBuilder.DropTable(
                name: "TB_HABILIDADES");

            migrationBuilder.UpdateData(
                table: "TB_USUARIOS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 183, 13, 13, 233, 252, 113, 53, 88, 129, 58, 242, 56, 196, 243, 127, 207, 60, 217, 4, 45, 178, 197, 192, 181, 189, 63, 49, 49, 188, 135, 32, 144, 178, 181, 56, 127, 200, 71, 233, 81, 124, 33, 2, 218, 24, 79, 148, 179, 145, 247, 169, 136, 210, 3, 127, 162, 214, 37, 131, 201, 139, 208, 152, 119 }, new byte[] { 117, 187, 103, 239, 45, 34, 229, 200, 48, 157, 201, 220, 119, 167, 22, 192, 100, 133, 152, 161, 255, 190, 228, 51, 4, 164, 57, 138, 106, 22, 102, 180, 197, 234, 78, 91, 0, 16, 0, 101, 153, 100, 142, 42, 78, 218, 194, 14, 114, 206, 5, 54, 225, 88, 149, 51, 241, 147, 100, 125, 83, 178, 61, 253, 241, 32, 49, 11, 39, 170, 198, 228, 104, 219, 29, 255, 228, 186, 35, 143, 123, 62, 164, 167, 49, 205, 186, 11, 83, 10, 139, 205, 108, 202, 176, 206, 156, 220, 95, 57, 0, 203, 230, 169, 28, 250, 46, 252, 73, 239, 48, 20, 20, 244, 147, 189, 186, 175, 254, 196, 222, 32, 72, 51, 20, 60, 40, 68 } });
        }
    }
}
