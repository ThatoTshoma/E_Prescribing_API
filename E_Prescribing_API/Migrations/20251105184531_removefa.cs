using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Prescribing_API.Migrations
{
    /// <inheritdoc />
    public partial class removefa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_MedicalStaffs_MedicalStaffId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_MedicalStaffId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "MedicalStaffId",
                table: "Patients");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MedicalStaffId",
                table: "Patients",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_MedicalStaffId",
                table: "Patients",
                column: "MedicalStaffId");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_MedicalStaffs_MedicalStaffId",
                table: "Patients",
                column: "MedicalStaffId",
                principalTable: "MedicalStaffs",
                principalColumn: "StaffId");
        }
    }
}
