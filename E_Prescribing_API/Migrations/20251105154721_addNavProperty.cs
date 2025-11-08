using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Prescribing_API.Migrations
{
    /// <inheritdoc />
    public partial class addNavProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MedicalSaffId",
                table: "Patients",
                newName: "MedicalStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Suburbs_CityId",
                table: "Suburbs",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_PrescribedMedications_MedicationId",
                table: "PrescribedMedications",
                column: "MedicationId");

            migrationBuilder.CreateIndex(
                name: "IX_PrescribedMedications_PrescriptionId",
                table: "PrescribedMedications",
                column: "PrescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_MedicalStaffId",
                table: "Patients",
                column: "MedicalStaffId");

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
                name: "IX_PatientConditions_ConditionId",
                table: "PatientConditions",
                column: "ConditionId");

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
                name: "FK_PatientConditions_ConditionDiagnosis_ConditionId",
                table: "PatientConditions",
                column: "ConditionId",
                principalTable: "ConditionDiagnosis",
                principalColumn: "ConditionId",
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
                name: "FK_Patients_MedicalStaffs_MedicalStaffId",
                table: "Patients",
                column: "MedicalStaffId",
                principalTable: "MedicalStaffs",
                principalColumn: "StaffId");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Suburbs_SuburbId",
                table: "Patients",
                column: "SuburbId",
                principalTable: "Suburbs",
                principalColumn: "SuburbId");

            migrationBuilder.AddForeignKey(
                name: "FK_PrescribedMedications_Medications_MedicationId",
                table: "PrescribedMedications",
                column: "MedicationId",
                principalTable: "Medications",
                principalColumn: "MedicationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PrescribedMedications_Prescriptions_PrescriptionId",
                table: "PrescribedMedications",
                column: "PrescriptionId",
                principalTable: "Prescriptions",
                principalColumn: "PrescriptionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Suburbs_Cities_CityId",
                table: "Suburbs",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "CityId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "FK_PatientConditions_ConditionDiagnosis_ConditionId",
                table: "PatientConditions");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientMedications_Medications_MedicationId",
                table: "PatientMedications");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientMedications_Patients_PatientId",
                table: "PatientMedications");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_MedicalStaffs_MedicalStaffId",
                table: "Patients");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Suburbs_SuburbId",
                table: "Patients");

            migrationBuilder.DropForeignKey(
                name: "FK_PrescribedMedications_Medications_MedicationId",
                table: "PrescribedMedications");

            migrationBuilder.DropForeignKey(
                name: "FK_PrescribedMedications_Prescriptions_PrescriptionId",
                table: "PrescribedMedications");

            migrationBuilder.DropForeignKey(
                name: "FK_Suburbs_Cities_CityId",
                table: "Suburbs");

            migrationBuilder.DropIndex(
                name: "IX_Suburbs_CityId",
                table: "Suburbs");

            migrationBuilder.DropIndex(
                name: "IX_PrescribedMedications_MedicationId",
                table: "PrescribedMedications");

            migrationBuilder.DropIndex(
                name: "IX_PrescribedMedications_PrescriptionId",
                table: "PrescribedMedications");

            migrationBuilder.DropIndex(
                name: "IX_Patients_MedicalStaffId",
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
                name: "IX_PatientConditions_ConditionId",
                table: "PatientConditions");

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

            migrationBuilder.RenameColumn(
                name: "MedicalStaffId",
                table: "Patients",
                newName: "MedicalSaffId");
        }
    }
}
