using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Prescribing_API.Migrations
{
    /// <inheritdoc />
    public partial class addInd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isUrgent",
                table: "Prescriptions",
                newName: "IsUrgent");

            migrationBuilder.CreateTable(
                name: "MedicationInteractions",
                columns: table => new
                {
                    InteractionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActiveIngredient1Id = table.Column<int>(type: "int", nullable: false),
                    ActiveIngredient2Id = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicationInteractions", x => x.InteractionId);
                    table.ForeignKey(
                        name: "FK_MedicationInteractions_ActiveIngredients_ActiveIngredient1Id",
                        column: x => x.ActiveIngredient1Id,
                        principalTable: "ActiveIngredients",
                        principalColumn: "IngredientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicationInteractions_ActiveIngredients_ActiveIngredient2Id",
                        column: x => x.ActiveIngredient2Id,
                        principalTable: "ActiveIngredients",
                        principalColumn: "IngredientId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PatientAllergies",
                columns: table => new
                {
                    AllergyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    ActiveIngredientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientAllergies", x => x.AllergyId);
                    table.ForeignKey(
                        name: "FK_PatientAllergies_ActiveIngredients_ActiveIngredientId",
                        column: x => x.ActiveIngredientId,
                        principalTable: "ActiveIngredients",
                        principalColumn: "IngredientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientAllergies_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicationInteractions_ActiveIngredient1Id",
                table: "MedicationInteractions",
                column: "ActiveIngredient1Id");

            migrationBuilder.CreateIndex(
                name: "IX_MedicationInteractions_ActiveIngredient2Id",
                table: "MedicationInteractions",
                column: "ActiveIngredient2Id");

            migrationBuilder.CreateIndex(
                name: "IX_PatientAllergies_ActiveIngredientId",
                table: "PatientAllergies",
                column: "ActiveIngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientAllergies_PatientId",
                table: "PatientAllergies",
                column: "PatientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicationInteractions");

            migrationBuilder.DropTable(
                name: "PatientAllergies");

            migrationBuilder.RenameColumn(
                name: "IsUrgent",
                table: "Prescriptions",
                newName: "isUrgent");
        }
    }
}
