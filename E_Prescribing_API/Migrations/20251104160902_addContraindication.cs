using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Prescribing_API.Migrations
{
    /// <inheritdoc />
    public partial class addContraindication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContraIndications",
                columns: table => new
                {
                    ContraIndicationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActiveIngredientId = table.Column<int>(type: "int", nullable: false),
                    ConditionDiagnosisConditionId = table.Column<int>(type: "int", nullable: false),
                    ConditionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContraIndications", x => x.ContraIndicationId);
                    table.ForeignKey(
                        name: "FK_ContraIndications_ActiveIngredients_ActiveIngredientId",
                        column: x => x.ActiveIngredientId,
                        principalTable: "ActiveIngredients",
                        principalColumn: "IngredientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContraIndications_ConditionDiagnosis_ConditionDiagnosisConditionId",
                        column: x => x.ConditionDiagnosisConditionId,
                        principalTable: "ConditionDiagnosis",
                        principalColumn: "ConditionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContraIndications_ActiveIngredientId",
                table: "ContraIndications",
                column: "ActiveIngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_ContraIndications_ConditionDiagnosisConditionId",
                table: "ContraIndications",
                column: "ConditionDiagnosisConditionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContraIndications");
        }
    }
}
