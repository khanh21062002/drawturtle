using Microsoft.EntityFrameworkCore.Migrations;

namespace EPS.Data.Migrations
{
    public partial class AddUnitAncestorView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql(@"CREATE VIEW UnitAncestors AS
WITH UnitAncestors AS 
(
   SELECT Id as UnitId, Id AS UnitAncestorId, Code, Name
   FROM Units
   UNION ALL
   SELECT U.Id as UnitId, A.UnitAncestorId, A.Code, A.Name
   FROM UNITS U
   INNER JOIN UnitAncestors A on U.ParentId = A.UnitId
) SELECT ROW_NUMBER() OVER (ORDER BY UnitId) AS Id, UnitId, UnitAncestorId, Code, Name FROM UnitAncestors");

            //migrationBuilder.CreateTable(
            //    name: "UnitAncestors",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        UnitId = table.Column<int>(nullable: false),
            //        UnitAncestorId = table.Column<int>(nullable: false),
            //        Code = table.Column<string>(maxLength: 250, nullable: false),
            //        Name = table.Column<string>(maxLength: 500, nullable: true),
            //        ParentId = table.Column<int>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_UnitAncestors", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_UnitAncestors_Units_UnitAncestorId",
            //            column: x => x.UnitAncestorId,
            //            principalTable: "Units",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_UnitAncestors_Units_UnitId",
            //            column: x => x.UnitId,
            //            principalTable: "Units",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            migrationBuilder.CreateIndex(
                name: "IX_Units_ParentId",
                table: "Units",
                column: "ParentId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_UnitAncestors_UnitAncestorId",
            //    table: "UnitAncestors",
            //    column: "UnitAncestorId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_UnitAncestors_UnitId",
            //    table: "UnitAncestors",
            //    column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Units_Units_ParentId",
                table: "Units",
                column: "ParentId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW UnitAncestors");

            migrationBuilder.DropForeignKey(
                name: "FK_Units_Units_ParentId",
                table: "Units");

            //migrationBuilder.DropTable(
            //    name: "UnitAncestors");

            migrationBuilder.DropIndex(
                name: "IX_Units_ParentId",
                table: "Units");
        }
    }
}
