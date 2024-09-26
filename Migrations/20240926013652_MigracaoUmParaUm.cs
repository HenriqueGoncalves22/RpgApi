using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RpgApi.Migrations
{
    /// <inheritdoc />
    public partial class MigracaoUmParaUm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Derrotas",
                table: "TB_PERSONAGENS",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Disputas",
                table: "TB_PERSONAGENS",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Vitorias",
                table: "TB_PERSONAGENS",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PersonagemId",
                table: "TB_ARMAS",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "TB_ARMAS",
                keyColumn: "Id",
                keyValue: 1,
                column: "PersonagemId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "TB_ARMAS",
                keyColumn: "Id",
                keyValue: 2,
                column: "PersonagemId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "TB_ARMAS",
                keyColumn: "Id",
                keyValue: 3,
                column: "PersonagemId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "TB_ARMAS",
                keyColumn: "Id",
                keyValue: 4,
                column: "PersonagemId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "TB_ARMAS",
                keyColumn: "Id",
                keyValue: 5,
                column: "PersonagemId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "TB_ARMAS",
                keyColumn: "Id",
                keyValue: 6,
                column: "PersonagemId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "TB_ARMAS",
                keyColumn: "Id",
                keyValue: 7,
                column: "PersonagemId",
                value: 7);

            migrationBuilder.UpdateData(
                table: "TB_PERSONAGENS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Derrotas", "Disputas", "Vitorias" },
                values: new object[] { 0, 0, 0 });

            migrationBuilder.UpdateData(
                table: "TB_PERSONAGENS",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Derrotas", "Disputas", "Vitorias" },
                values: new object[] { 0, 0, 0 });

            migrationBuilder.UpdateData(
                table: "TB_PERSONAGENS",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Derrotas", "Disputas", "Vitorias" },
                values: new object[] { 0, 0, 0 });

            migrationBuilder.UpdateData(
                table: "TB_PERSONAGENS",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Derrotas", "Disputas", "Vitorias" },
                values: new object[] { 0, 0, 0 });

            migrationBuilder.UpdateData(
                table: "TB_PERSONAGENS",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Derrotas", "Disputas", "Vitorias" },
                values: new object[] { 0, 0, 0 });

            migrationBuilder.UpdateData(
                table: "TB_PERSONAGENS",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Derrotas", "Disputas", "Vitorias" },
                values: new object[] { 0, 0, 0 });

            migrationBuilder.UpdateData(
                table: "TB_PERSONAGENS",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Derrotas", "Disputas", "Vitorias" },
                values: new object[] { 0, 0, 0 });

            migrationBuilder.UpdateData(
                table: "TB_USUARIOS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 183, 13, 13, 233, 252, 113, 53, 88, 129, 58, 242, 56, 196, 243, 127, 207, 60, 217, 4, 45, 178, 197, 192, 181, 189, 63, 49, 49, 188, 135, 32, 144, 178, 181, 56, 127, 200, 71, 233, 81, 124, 33, 2, 218, 24, 79, 148, 179, 145, 247, 169, 136, 210, 3, 127, 162, 214, 37, 131, 201, 139, 208, 152, 119 }, new byte[] { 117, 187, 103, 239, 45, 34, 229, 200, 48, 157, 201, 220, 119, 167, 22, 192, 100, 133, 152, 161, 255, 190, 228, 51, 4, 164, 57, 138, 106, 22, 102, 180, 197, 234, 78, 91, 0, 16, 0, 101, 153, 100, 142, 42, 78, 218, 194, 14, 114, 206, 5, 54, 225, 88, 149, 51, 241, 147, 100, 125, 83, 178, 61, 253, 241, 32, 49, 11, 39, 170, 198, 228, 104, 219, 29, 255, 228, 186, 35, 143, 123, 62, 164, 167, 49, 205, 186, 11, 83, 10, 139, 205, 108, 202, 176, 206, 156, 220, 95, 57, 0, 203, 230, 169, 28, 250, 46, 252, 73, 239, 48, 20, 20, 244, 147, 189, 186, 175, 254, 196, 222, 32, 72, 51, 20, 60, 40, 68 } });

            migrationBuilder.CreateIndex(
                name: "IX_TB_ARMAS_PersonagemId",
                table: "TB_ARMAS",
                column: "PersonagemId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_ARMAS_TB_PERSONAGENS_PersonagemId",
                table: "TB_ARMAS",
                column: "PersonagemId",
                principalTable: "TB_PERSONAGENS",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_ARMAS_TB_PERSONAGENS_PersonagemId",
                table: "TB_ARMAS");

            migrationBuilder.DropIndex(
                name: "IX_TB_ARMAS_PersonagemId",
                table: "TB_ARMAS");

            migrationBuilder.DropColumn(
                name: "Derrotas",
                table: "TB_PERSONAGENS");

            migrationBuilder.DropColumn(
                name: "Disputas",
                table: "TB_PERSONAGENS");

            migrationBuilder.DropColumn(
                name: "Vitorias",
                table: "TB_PERSONAGENS");

            migrationBuilder.DropColumn(
                name: "PersonagemId",
                table: "TB_ARMAS");

            migrationBuilder.UpdateData(
                table: "TB_USUARIOS",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 113, 227, 77, 18, 87, 141, 195, 90, 226, 179, 49, 110, 28, 79, 53, 19, 114, 194, 6, 239, 175, 31, 164, 64, 130, 80, 129, 172, 242, 78, 113, 175, 231, 122, 111, 144, 70, 164, 178, 150, 83, 145, 237, 28, 98, 140, 218, 55, 195, 132, 255, 94, 161, 89, 179, 55, 236, 232, 254, 75, 4, 65, 203, 186 }, new byte[] { 99, 163, 186, 24, 142, 155, 40, 255, 157, 18, 193, 33, 76, 191, 120, 91, 134, 154, 61, 229, 135, 25, 191, 248, 67, 252, 8, 65, 83, 50, 114, 201, 126, 195, 227, 254, 113, 168, 248, 59, 56, 179, 226, 76, 68, 228, 38, 50, 141, 156, 49, 102, 124, 113, 121, 221, 38, 187, 125, 111, 246, 171, 75, 50, 212, 78, 139, 211, 1, 214, 242, 254, 230, 88, 41, 200, 233, 188, 159, 23, 250, 131, 197, 80, 19, 229, 39, 239, 81, 195, 215, 146, 49, 8, 218, 169, 222, 244, 42, 4, 249, 96, 145, 49, 7, 222, 84, 242, 141, 237, 216, 191, 219, 193, 107, 118, 93, 210, 90, 203, 61, 33, 249, 100, 251, 115, 37, 201 } });
        }
    }
}
