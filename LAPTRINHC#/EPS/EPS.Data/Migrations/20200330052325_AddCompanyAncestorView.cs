using Microsoft.EntityFrameworkCore.Migrations;

namespace EPS.Data.Migrations
{
    public partial class AddCompanyAncestorView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql(@"CREATE VIEW CompanyAncestors AS
WITH CompanyAncestors AS 
(
   SELECT Id as CompanyId, Id AS CompanyAncestorId, Code, Name
   FROM Companys
   UNION ALL
   SELECT U.Id as CompanyId, A.CompanyAncestorId, A.Code, A.Name
   FROM CompanyS U
   INNER JOIN CompanyAncestors A on U.ParentId = A.CompanyId
) SELECT ROW_NUMBER() OVER (ORDER BY CompanyId) AS Id, CompanyId, CompanyAncestorId, Code, Name FROM CompanyAncestors");

            migrationBuilder.Sql(@"CREATE VIEW [dbo].[AllPossibleParents] AS
WITH Hierarchy(Id, Code, Name, ParentId, Parents)
AS
(
    SELECT Id, Code, Name, ParentId, CAST('' AS VARCHAR(MAX))
        FROM Companys AS FirtGeneration
        WHERE ParentId IS NULL    
    UNION ALL
    SELECT NextGeneration.Id, NextGeneration.Code, NextGeneration.Name, Parent.ID,
    CAST(CASE 
		WHEN Parent.Parents = 'C' THEN(CAST(NextGeneration.ParentId AS VARCHAR(MAX)))
        ELSE(Parent.Parents + 'C' + CAST(NextGeneration.ParentId AS VARCHAR(MAX))) + '.'
    END AS VARCHAR(MAX))
        FROM Companys AS NextGeneration
        INNER JOIN Hierarchy AS Parent ON NextGeneration.ParentId = Parent.ID    
)
SELECT *
    FROM Hierarchy
GO");
            migrationBuilder.Sql(@"CREATE VIEW [dbo].[AllPossibleChilds] AS
WITH Hierarchy(Id, Code, Name, ParentId, Childs)
AS
(
    SELECT Id, Code, Name, ParentId, CAST('' AS VARCHAR(MAX))
        FROM Companys AS LastGeneration
        WHERE Id NOT IN (SELECT COALESCE(ParentId, 0) FROM Companys)     
    UNION ALL
    SELECT PrevGeneration.Id, PrevGeneration.Code, PrevGeneration.Name, PrevGeneration.ParentId,
    CAST(CASE WHEN Child.Childs = ''
        THEN(CAST(Child.Id AS VARCHAR(MAX)))
        ELSE(Child.Childs + '.' + CAST(Child.Id AS VARCHAR(MAX))) + '.'
    END AS VARCHAR(MAX))
        FROM Companys AS PrevGeneration
        INNER JOIN Hierarchy AS Child ON PrevGeneration.Id = Child.ParentId    
)
SELECT *
    FROM Hierarchy
GO");

            //migrationBuilder.CreateTable(
            //    name: "CompanyAncestors",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        CompanyId = table.Column<int>(nullable: false),
            //        CompanyAncestorId = table.Column<int>(nullable: false),
            //        Code = table.Column<string>(maxLength: 250, nullable: false),
            //        Name = table.Column<string>(maxLength: 500, nullable: true),
            //        ParentId = table.Column<int>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_CompanyAncestors", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_CompanyAncestors_Companys_CompanyAncestorId",
            //            column: x => x.CompanyAncestorId,
            //            principalTable: "Companys",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_CompanyAncestors_Companys_CompanyId",
            //            column: x => x.CompanyId,
            //            principalTable: "Companys",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            migrationBuilder.CreateIndex(
                name: "IX_Companys_ParentId",
                table: "Companys",
                column: "ParentId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_CompanyAncestors_CompanyAncestorId",
            //    table: "CompanyAncestors",
            //    column: "CompanyAncestorId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_CompanyAncestors_CompanyId",
            //    table: "CompanyAncestors",
            //    column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companys_Companys_ParentId",
                table: "Companys",
                column: "ParentId",
                principalTable: "Companys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW CompanyAncestors");
            migrationBuilder.Sql("DROP VIEW AllPossibleParents");
            migrationBuilder.Sql("DROP VIEW AllPossibleChilds");

            migrationBuilder.DropForeignKey(
                name: "FK_Companys_Companys_ParentId",
                table: "Companys");

            //migrationBuilder.DropTable(
            //    name: "CompanyAncestors");

            migrationBuilder.DropIndex(
                name: "IX_Companys_ParentId",
                table: "Companys");
        }
    }
}
