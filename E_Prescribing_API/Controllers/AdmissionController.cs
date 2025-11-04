using E_Prescribing_API.CollectionModel;
using E_Prescribing_API.Data;
using E_Prescribing_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace E_Prescribing_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdmissionController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<AdmissionController> _logger;

        public AdmissionController(ApplicationDbContext db, ILogger<AdmissionController> logger)
        {
            _db = db;
            _logger = logger;
        }
        [HttpGet("AdmitPatient")]
        public async Task<IActionResult> AdmitPatient(int id)
        {
            try
            {
                var medicationList = new SelectList(
                    await _db.Medications.OrderBy(c => c.Name).ToListAsync(),
                    "MedicationId",
                    "Name"
                );

                var collection = new AdmissionCollection
                {
                    PatientMedication = new PatientMedication { PatientId = id },
                    Patients = await _db.Patients.ToListAsync(),
                    CurrentStep = 1
                };

                //ViewBag.MedicationList = medicationList;
                return Ok(collection);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading AdmitPatient GET action");
                return StatusCode(500, "An error occurred while loading admission form.");
            }
        }


        [HttpPost("AdmitPatient")]
        public async Task<IActionResult> AdmitPatient([FromBody] AdmissionCollection model)
        {
            try
            {
                if (model == null || model.PatientMedication == null)
                    return BadRequest("Invalid admission data.");


                if (model.CurrentStep == 1)
                {
                    if (model.PatientMedication.SelectedMedication != null && model.PatientMedication.SelectedMedication.Any())
                    {
                        foreach (var selectedMedicationId in model.PatientMedication.SelectedMedication)
                        {
                            var patientMedication = new PatientMedication
                            {
                                MedicationId = selectedMedicationId,
                                PatientId = model.PatientMedication.PatientId
                            };

                            _db.PatientMedications.Add(patientMedication);
                        }

                        await _db.SaveChangesAsync();

                        return Ok(new
                        {
                            message = "Medications added successfully.",
                            patientId = model.PatientMedication.PatientId,
                            medications = model.PatientMedication.SelectedMedication
                        });
                    }
                    else
                    {
                        return BadRequest("No medications selected for this patient.");
                    }
                }

                _logger.LogWarning("Unknown step {Step} in admission process", model.CurrentStep);
                return BadRequest($"Invalid step: {model.CurrentStep}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while admitting patient");
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }

    }
}
