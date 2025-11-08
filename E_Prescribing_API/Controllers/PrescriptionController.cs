using E_Prescribing_API.CollectionModel;
using E_Prescribing_API.Data;
using E_Prescribing_API.Data.Services;
using E_Prescribing_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace E_Prescribing_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<AdminController> _logger;
        private readonly Alerts _alerts;
        public PrescriptionController(ApplicationDbContext db, ILogger<AdminController> logger, Alerts alerts)
        {
            _db = db;
            _logger = logger;
            _alerts = alerts;

        }
        [HttpPost("PrescribeMedication")]
        public async Task<IActionResult> PrescribeMedication([FromBody] PrescriptionCollection model)
        {
            try
            {
                if (model == null || model.Quantities == null || !model.Quantities.Any())
                    return BadRequest("Invalid prescription data.");

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var prescriber = await _db.MedicalStaffs.FirstOrDefaultAsync(s => s.UserId == 10);

                if (prescriber == null)
                    return Unauthorized("Prescriber not found.");

                int patientId = model.Prescription.PatientId;
                var medicationIds = model.Quantities.Keys.ToList();


                var allergyConflicts = await _alerts.CheckAllergyConflictsAsync(patientId, medicationIds);
                var contraindications = await _alerts.CheckContraindicationsAsync(patientId, medicationIds);
                var medicationInteraction = await _alerts.CheckMedicationInteractionsAsync(patientId, medicationIds);


                if ((allergyConflicts.Any() || contraindications.Any() || medicationInteraction.Any())
)
                {
                    return BadRequest(new
                    {
                        Allergies = allergyConflicts.Distinct(),
                        Contraindications = contraindications.Distinct(),
                        Interactions = medicationInteraction.Distinct(),
                        ErrorMessage = "Potential medical conflicts detected. Please review before proceeding."
                    });
                }

                var prescription = new Prescription
                {
                    PrescriberId = prescriber.StaffId,
                    Date = DateTime.Now,
                    Status = "Prescribed",
                    IsUrgent = model.Prescription.IsUrgent,  
                    Note = model.Prescription.Note,
                    PatientId = patientId,
                    DispenserId = null
                };

                await _db.Prescriptions.AddAsync(prescription);
                await _db.SaveChangesAsync();

                foreach (var (medicationId, quantity) in model.Quantities)
                {
                    if (quantity <= 0) continue;

                    var instruction = model.Instructions != null && model.Instructions.ContainsKey(medicationId)? model.Instructions[medicationId]: null;

                    var prescribedMedication = new PrescribedMedication
                    {
                        MedicationId = medicationId,
                        PrescriptionId = prescription.PrescriptionId,
                        Quantity = quantity,
                        Instruction = instruction
                    };

                    _db.PrescribedMedications.Add(prescribedMedication);
                }

                await _db.SaveChangesAsync();

                return Ok(new
                {
                    message = "Prescription added successfully.",
                    prescriptionId = prescription.PrescriptionId
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while prescribing medication");
                return StatusCode(500, "An unexpected error occurred while processing the prescription.");
            }
        }


    }
}
