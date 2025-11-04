using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Prescribing_API.Migrations
{
    /// <inheritdoc />
    public partial class updateConditionDiagnosisId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContraIndications_ConditionDiagnosis_ConditionDiagnosisConditionId",
                table: "ContraIndications");

            migrationBuilder.DropIndex(
                name: "IX_ContraIndications_ConditionDiagnosisConditionId",
                table: "ContraIndications");

            migrationBuilder.DropColumn(
                name: "ConditionDiagnosisConditionId",
                table: "ContraIndications");

            migrationBuilder.RenameColumn(
                name: "ConditionId",
                table: "ContraIndications",
                newName: "ConditionDiagnosisId");

            migrationBuilder.CreateIndex(
                name: "IX_ContraIndications_ConditionDiagnosisId",
                table: "ContraIndications",
                column: "ConditionDiagnosisId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContraIndications_ConditionDiagnosis_ConditionDiagnosisId",
                table: "ContraIndications",
                column: "ConditionDiagnosisId",
                principalTable: "ConditionDiagnosis",
                principalColumn: "ConditionId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContraIndications_ConditionDiagnosis_ConditionDiagnosisId",
                table: "ContraIndications");

            migrationBuilder.DropIndex(
                name: "IX_ContraIndications_ConditionDiagnosisId",
                table: "ContraIndications");

            migrationBuilder.RenameColumn(
                name: "ConditionDiagnosisId",
                table: "ContraIndications",
                newName: "ConditionId");

            migrationBuilder.AddColumn<int>(
                name: "ConditionDiagnosisConditionId",
                table: "ContraIndications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ContraIndications_ConditionDiagnosisConditionId",
                table: "ContraIndications",
                column: "ConditionDiagnosisConditionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContraIndications_ConditionDiagnosis_ConditionDiagnosisConditionId",
                table: "ContraIndications",
                column: "ConditionDiagnosisConditionId",
                principalTable: "ConditionDiagnosis",
                principalColumn: "ConditionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
