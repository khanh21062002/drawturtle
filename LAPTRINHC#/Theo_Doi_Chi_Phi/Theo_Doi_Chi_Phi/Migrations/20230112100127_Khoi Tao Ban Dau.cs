using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Theo_Doi_Chi_Phi.Migrations
{
    public partial class KhoiTaoBanDau : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Danh_muc",
                columns: table => new
                {
                    So_Danh_Muc = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tieu_De = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Bieu_Tuong = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    Loai = table.Column<string>(type: "nvarchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Danh_muc", x => x.So_Danh_Muc);
                });

            migrationBuilder.CreateTable(
                name: "Giao_dich",
                columns: table => new
                {
                    Id_Giao_Dich = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    So_Danh_Muc = table.Column<int>(type: "int", nullable: false),
                    CategorySo_Danh_Muc = table.Column<int>(type: "int", nullable: true),
                    Gia_Tri = table.Column<int>(type: "int", nullable: false),
                    Ghi_Chu = table.Column<string>(type: "nvarchar(75)", nullable: true),
                    Thoi_gian = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Giao_dich", x => x.Id_Giao_Dich);
                    table.ForeignKey(
                        name: "FK_Giao_dich_Danh_muc_CategorySo_Danh_Muc",
                        column: x => x.CategorySo_Danh_Muc,
                        principalTable: "Danh_muc",
                        principalColumn: "So_Danh_Muc");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Giao_dich_CategorySo_Danh_Muc",
                table: "Giao_dich",
                column: "CategorySo_Danh_Muc");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Giao_dich");

            migrationBuilder.DropTable(
                name: "Danh_muc");
        }
    }
}
