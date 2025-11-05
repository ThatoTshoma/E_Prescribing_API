using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Prescribing_API.Migrations
{
    /// <inheritdoc />
    public partial class addprescrip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Provinces_ProvinceId",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_ContraIndications_ActiveIngredients_ActiveIngredientId",
                table: "ContraIndications");

            migrationBuilder.DropForeignKey(
                name: "FK_ContraIndications_ConditionDiagnosis_ConditionDiagnosisId",
                table: "ContraIndications");

            migrationBuilder.DropForeignKey(
                name: "FK_Facilities_FacilitiesTypes_FacilityTypeId",
                table: "Facilities");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalStaffs_AspNetUsers_UserId",
                table: "MedicalStaffs");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalStaffs_Facilities_FacilityId",
                table: "MedicalStaffs");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicationIngredients_ActiveIngredients_ActiveIngredientId",
                table: "MedicationIngredients");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicationIngredients_Medications_MedicationId",
                table: "MedicationIngredients");

            migrationBuilder.DropForeignKey(
                name: "FK_Medications_DosageForms_DosageFormId",
                table: "Medications");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientCondition_ConditionDiagnosis_ConditionId",
                table: "PatientCondition");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientCondition_Patients_PatientId",
                table: "PatientCondition");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientMedications_Medications_MedicationId",
                table: "PatientMedications");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientMedications_Patients_PatientId",
                table: "PatientMedications");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_MedicalStaffs_MedicalSaffId",
                table: "Patients");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Suburbs_SuburbId",
                table: "Patients");

            migrationBuilder.DropForeignKey(
                name: "FK_Suburbs_Cities_CityId",
                table: "Suburbs");

            migrationBuilder.DropIndex(
                name: "IX_Suburbs_CityId",
                table: "Suburbs");

            migrationBuilder.DropIndex(
                name: "IX_Patients_MedicalSaffId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_SuburbId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_PatientMedications_MedicationId",
                table: "PatientMedications");

            migrationBuilder.DropIndex(
                name: "IX_PatientMedications_PatientId",
                table: "PatientMedications");

            migrationBuilder.DropIndex(
                name: "IX_Medications_DosageFormId",
                table: "Medications");

            migrationBuilder.DropIndex(
                name: "IX_MedicationIngredients_ActiveIngredientId",
                table: "MedicationIngredients");

            migrationBuilder.DropIndex(
                name: "IX_MedicationIngredients_MedicationId",
                table: "MedicationIngredients");

            migrationBuilder.DropIndex(
                name: "IX_MedicalStaffs_FacilityId",
                table: "MedicalStaffs");

            migrationBuilder.DropIndex(
                name: "IX_MedicalStaffs_UserId",
                table: "MedicalStaffs");

            migrationBuilder.DropIndex(
                name: "IX_Facilities_FacilityTypeId",
                table: "Facilities");

            migrationBuilder.DropIndex(
                name: "IX_ContraIndications_ActiveIngredientId",
                table: "ContraIndications");

            migrationBuilder.DropIndex(
                name: "IX_ContraIndications_ConditionDiagnosisId",
                table: "ContraIndications");

            migrationBuilder.DropIndex(
                name: "IX_Cities_ProvinceId",
                table: "Cities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PatientCondition",
                table: "PatientCondition");

            migrationBuilder.DropIndex(
                name: "IX_PatientCondition_ConditionId",
                table: "PatientCondition");

            migrationBuilder.RenameTable(
                name: "PatientCondition",
                newName: "PatientConditions");

            migrationBuilder.RenameIndex(
                name: "IX_PatientCondition_PatientId",
                table: "PatientConditions",
                newName: "IX_PatientConditions_PatientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PatientConditions",
                table: "PatientConditions",
                column: "PatientConditionId");

            migrationBuilder.CreateTable(
                name: "PrescribedMedications",
                columns: table => new
                {
                    PrescribedMedicationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicationId = table.Column<int>(type: "int", nullable: false),
                    PrescriptionId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Instruction = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrescribedMedications", x => x.PrescribedMedicationId);
                });

            migrationBuilder.CreateTable(
                name: "Prescriptions",
                columns: table => new
                {
                    PrescriptionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isUrgent = table.Column<bool>(type: "bit", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    PrescriberId = table.Column<int>(type: "int", nullable: false),
                    DispenserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescriptions", x => x.PrescriptionId);
                    table.ForeignKey(
                        name: "FK_Prescriptions_MedicalStaffs_DispenserId",
                        column: x => x.DispenserId,
                        principalTable: "MedicalStaffs",
                        principalColumn: "StaffId");
                    table.ForeignKey(
                        name: "FK_Prescriptions_MedicalStaffs_PrescriberId",
                        column: x => x.PrescriberId,
                        principalTable: "MedicalStaffs",
                        principalColumn: "StaffId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prescriptions_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_DispenserId",
                table: "Prescriptions",
                column: "DispenserId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_PatientId",
                table: "Prescriptions",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_PrescriberId",
                table: "Prescriptions",
                column: "PrescriberId");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientConditions_Patients_PatientId",
                table: "PatientConditions",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "PatientId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientConditions_Patients_PatientId",
                table: "PatientConditions");

            migrationBuilder.DropTable(
                name: "PrescribedMedications");

            migrationBuilder.DropTable(
                name: "Prescriptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PatientConditions",
                table: "PatientConditions");

            migrationBuilder.RenameTable(
                name: "PatientConditions",
                newName: "PatientCondition");

            migrationBuilder.RenameIndex(
                name: "IX_PatientConditions_PatientId",
                table: "PatientCondition",
                newName: "IX_PatientCondition_PatientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PatientCondition",
                table: "PatientCondition",
                column: "PatientConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_Suburbs_CityId",
                table: "Suburbs",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_MedicalSaffId",
                table: "Patients",
                column: "MedicalSaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_SuburbId",
                table: "Patients",
                column: "SuburbId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientMedications_MedicationId",
                table: "PatientMedications",
                column: "MedicationId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientMedications_PatientId",
                table: "PatientMedications",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Medications_DosageFormId",
                table: "Medications",
                column: "DosageFormId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicationIngredients_ActiveIngredientId",
                table: "MedicationIngredients",
                column: "ActiveIngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicationIngredients_MedicationId",
                table: "MedicationIngredients",
                column: "MedicationId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalStaffs_FacilityId",
                table: "MedicalStaffs",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalStaffs_UserId",
                table: "MedicalStaffs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Facilities_FacilityTypeId",
                table: "Facilities",
                column: "FacilityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ContraIndications_ActiveIngredientId",
                table: "ContraIndications",
                column: "ActiveIngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_ContraIndications_ConditionDiagnosisId",
                table: "ContraIndications",
                column: "ConditionDiagnosisId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_ProvinceId",
                table: "Cities",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientCondition_ConditionId",
                table: "PatientCondition",
                column: "ConditionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Provinces_ProvinceId",
                table: "Cities",
                column: "ProvinceId",
                principalTable: "Provinces",
                principalColumn: "ProvinceId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContraIndications_ActiveIngredients_ActiveIngredientId",
                table: "ContraIndications",
                column: "ActiveIngredientId",
                principalTable: "ActiveIngredients",
                principalColumn: "IngredientId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContraIndications_ConditionDiagnosis_ConditionDiagnosisId",
                table: "ContraIndications",
                column: "ConditionDiagnosisId",
                principalTable: "ConditionDiagnosis",
                principalColumn: "ConditionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Facilities_FacilitiesTypes_FacilityTypeId",
                table: "Facilities",
                column: "FacilityTypeId",
                principalTable: "FacilitiesTypes",
                principalColumn: "FacilityTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalStaffs_AspNetUsers_UserId",
                table: "MedicalStaffs",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalStaffs_Facilities_FacilityId",
                table: "MedicalStaffs",
                column: "FacilityId",
                principalTable: "Facilities",
                principalColumn: "FacilityId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicationIngredients_ActiveIngredients_ActiveIngredientId",
                table: "MedicationIngredients",
                column: "ActiveIngredientId",
                principalTable: "ActiveIngredients",
                principalColumn: "IngredientId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicationIngredients_Medications_MedicationId",
                table: "MedicationIngredients",
                column: "MedicationId",
                principalTable: "Medications",
                principalColumn: "MedicationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Medications_DosageForms_DosageFormId",
                table: "Medications",
                column: "DosageFormId",
                principalTable: "DosageForms",
                principalColumn: "DosageId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientCondition_ConditionDiagnosis_ConditionId",
                table: "PatientCondition",
                column: "ConditionId",
                principalTable: "ConditionDiagnosis",
                principalColumn: "ConditionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientCondition_Patients_PatientId",
                table: "PatientCondition",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "PatientId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientMedications_Medications_MedicationId",
                table: "PatientMedications",
                column: "MedicationId",
                principalTable: "Medications",
                principalColumn: "MedicationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientMedications_Patients_PatientId",
                table: "PatientMedications",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "PatientId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_MedicalStaffs_MedicalSaffId",
                table: "Patients",
                column: "MedicalSaffId",
                principalTable: "MedicalStaffs",
                principalColumn: "StaffId");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Suburbs_SuburbId",
                table: "Patients",
                column: "SuburbId",
                principalTable: "Suburbs",
                principalColumn: "SuburbId");

            migrationBuilder.AddForeignKey(
                name: "FK_Suburbs_Cities_CityId",
                table: "Suburbs",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "CityId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
