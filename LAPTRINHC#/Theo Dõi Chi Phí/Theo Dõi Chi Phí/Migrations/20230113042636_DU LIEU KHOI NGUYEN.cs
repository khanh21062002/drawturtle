using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Theo_Dõi_Chi_Phí.Migrations
{
    public partial class DULIEUKHOINGUYEN : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Danh_mục",
                columns: table => new
                {
                    Số_danh_mục = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tiêu_đề = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Biểu_tượng = table.Column<string>(type: "nvarchar(5)", nullable: false),
                    Loại = table.Column<string>(type: "nvarchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Danh_mục", x => x.Số_danh_mục);
                });

            migrationBuilder.CreateTable(
                name: "Giao_dịch",
                columns: table => new
                {
                    Mã_giao_dịch = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Số_danh_mục = table.Column<int>(type: "int", nullable: false),
                    CategorySố_danh_mục = table.Column<int>(type: "int", nullable: true),
                    Giá_trị = table.Column<int>(type: "int", nullable: false),
                    Ghi_chú = table.Column<string>(type: "nvarchar(75)", nullable: true),
                    Thời_gian = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Giao_dịch", x => x.Mã_giao_dịch);
                    table.ForeignKey(
                        name: "FK_Giao_dịch_Danh_mục_CategorySố_danh_mục",
                        column: x => x.CategorySố_danh_mục,
                        principalTable: "Danh_mục",
                        principalColumn: "Số_danh_mục");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Giao_dịch_CategorySố_danh_mục",
                table: "Giao_dịch",
                column: "CategorySố_danh_mục");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Giao_dịch");

            migrationBuilder.DropTable(
                name: "Danh_mục");
        }
    }
}
