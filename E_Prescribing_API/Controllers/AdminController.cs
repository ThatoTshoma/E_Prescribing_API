using E_Prescribing_API.CollectionModel;
using E_Prescribing_API.Data;
using E_Prescribing_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Prescribing_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        private readonly ILogger<AdminController> _logger;

        public AdminController(ApplicationDbContext db,ILogger<AdminController> logger)
        {
            _db = db;
            _logger = logger;
        }
        [HttpPost("AddProvince")]
        public async Task<IActionResult> AddProvince(Province model)
        {
            try
            {
                if(model == null || string.IsNullOrEmpty(model.Name))
                    return BadRequest("Invalid province");

                if (await _db.Provinces.AnyAsync(a => a.Name == model.Name))
                {
                    return BadRequest("A province with this name already exist");
                }

                var province = new Province
                {
                    Name = model.Name
                };
                _db.Provinces.Add(province);
                await _db.SaveChangesAsync();

                return Ok(new
                {
                    message = "Province added successfully.",
                    name = province.Name
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding province {ProvinceName}", model?.Name);
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }

        [HttpPost("AddCity")]
        public async Task<IActionResult> AddCity(City model)
        {
            try
            {
                if (model == null || string.IsNullOrEmpty(model.Name))
                    return BadRequest("Invalid city");

                if (await _db.Cities.AnyAsync(a => a.Name == model.Name))
                {
                    return BadRequest("A city with this name already exist");
                }

                var city = new City
                {
                    Name = model.Name,
                    ProvinceId = model.ProvinceId
                };
                _db.Cities.Add(city);
                await _db.SaveChangesAsync();

                return Ok(new
                {
                    message = "City added successfully.",
                    name = city.Name
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding city {Name}", model?.Name);
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }

        [HttpPost("AddSuburb")]
        public async Task<IActionResult> AddSuburb(Suburb model)
        {
            try
            {
                if (model == null || string.IsNullOrEmpty(model.Name))
                    return BadRequest("Invalid suburb");

                if (await _db.Suburbs.AnyAsync(a => a.Name == model.Name))
                {
                    return BadRequest("A city with this name already exist");
                }

                var suburb = new Suburb
                {
                    Name = model.Name,
                    PostalCode = model.PostalCode,
                    CityId = model.CityId
                };
                _db.Suburbs.Add(suburb);
                await _db.SaveChangesAsync();

                return Ok(new
                {
                    message = "Suburb added successfully.",
                    name = suburb.Name
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding suburb {Name}", model?.Name);
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }

        [HttpPost("AddFacility")]
        public async Task<IActionResult> AddFacility(Facility model)
        {
            try
            {
                if (model == null || string.IsNullOrEmpty(model.Name))
                    return BadRequest("Invalid suburb");

                if (await _db.Facilities.AnyAsync(a => a.Name == model.Name))
                {
                    return BadRequest("A facility with this name already exist");
                }

                var facility = new Facility
                {
                    Name = model.Name,
                    AddressLine1 = model.AddressLine1,
                    AddressLine2 = model.AddressLine2,
                    ContactNumber = model.ContactNumber,
                    SuburbId = model.SuburbId,
                    FacilityTypeId = model.FacilityTypeId
                };
                _db.Facilities.Add(facility);
                await _db.SaveChangesAsync();

                return Ok(new
                {
                    message = "Facility added successfully.",
                    name = facility.Name
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding facility {Name}", model?.Name);
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }
         [HttpPost("AddActiveIngredient")]
        public async Task<IActionResult> AddActiveIngredient(ActiveIngredient model)
        {
            try
            {
                if (model == null || string.IsNullOrEmpty(model.Name))
                    return BadRequest("Invalid Active ingredient");

                if (await _db.ActiveIngredients.AnyAsync(a => a.Name == model.Name))
                {
                    return BadRequest("A ActiveIngredient with this name already exist");
                }

                var activeIngredient = new ActiveIngredient
                {
                    Name = model.Name
      
                };
                _db.ActiveIngredients.Add(activeIngredient);
                await _db.SaveChangesAsync();

                return Ok(new
                {
                    message = "Active ingredient added successfully.",
                    name = activeIngredient.Name
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding Active ingredient {Name}", model?.Name);
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }
        [HttpPost("AddDiagnosis")]
        public async Task<IActionResult> AddDiagnosis(ConditionDiagnosis model)
        {
            try
            {
                if (model == null || string.IsNullOrEmpty(model.Name))
                    return BadRequest("Invalid condition diagnosis");

                if (await _db.ConditionDiagnosis.AnyAsync(a => a.Name == model.Name || a.Icd10Code == model.Icd10Code))
                {
                    return BadRequest("A condition diagnosis  with this name already exist");
                }

                var diagnosis = new ConditionDiagnosis
                {
                    Name = model.Name,
                    Icd10Code = model.Icd10Code

                };
                _db.ConditionDiagnosis.Add(diagnosis);
                await _db.SaveChangesAsync();

                return Ok(new
                {
                    message = "Condition diagnosis  added successfully.",
                    name = diagnosis.Name
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding condition diagnosis  {Name}", model?.Name);
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }

        [HttpPost("AddContraIndication")]
        public async Task<IActionResult> AddContraIndication(ContraIndication model)
        {
            try
            {
                if (model == null)
                    return BadRequest("Invalid contra-indication");

                if (await _db.ContraIndications.AnyAsync(a => a.ConditionDiagnosisId == model.ConditionDiagnosisId && a.ActiveIngredientId == model.ActiveIngredientId))
                {
                    return BadRequest("A contra-indication  with this name already exist");
                }

                var contraIndications = new ContraIndication
                {
                    ActiveIngredientId = model.ActiveIngredientId,
                    ConditionDiagnosisId = model.ConditionDiagnosisId

                };
                _db.ContraIndications.Add(contraIndications);
                await _db.SaveChangesAsync();

                return Ok(new
                {
                    message = "Contra-Indication  added successfully.",
                    name = contraIndications.ContraIndicationId
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding Contra-Indication  {Id}", model?.ContraIndicationId);
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }

        [HttpPost("AddDosageForm")]
        public async Task<IActionResult> AddDosageForm(DosageForm model)
        {
            try
            {
                if (model == null)
                    return BadRequest("Invalid dosage form data");

                if (await _db.DosageForms.AnyAsync(a => a.Name == model.Name))
                {
                    return BadRequest("A dosage form with this name already exist");
                }

                var dosageForm = new DosageForm
                {
                    Name = model.Name
                };
                _db.DosageForms.Add(dosageForm);
                await _db.SaveChangesAsync();

                return Ok(new
                {
                    message = "Dosage form added successfully.",
                    name = dosageForm.Name
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding dosage form {Name}", model?.Name);
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }

        [HttpPost("AddMedication")]
        public async Task<IActionResult> AddMedication(MedicationCollection model)
        {
            try
            {
                if (model == null)
                    return BadRequest("Invalid Mediaction data");

                if (await _db.Medications.AnyAsync(a => a.Name == model.Medication.Name))
                {
                    return BadRequest("A medication with this name already exist");
                }

                var medication = new Medication
                {
                    Name = model.Medication.Name,
                    DosageFormId = model.Medication.DosageFormId,
                    Schedule = model.Medication.Schedule

                };
                _db.Medications.Add(medication);
                await _db.SaveChangesAsync();

                foreach (var ingredient in model.SelectedIngredient)
                {
                    var medIngredient = new MedicationIngredient
                    {
                        MedicationId = medication.MedicationId,
                        ActiveIngredientId = ingredient,
                        ActiveIngredientStrength = model.Strengths[ingredient]
                    };
                    _db.MedicationIngredients.Add(medIngredient);
                }
                await _db.SaveChangesAsync();

                return Ok(new
                {
                    message = "Medication added successfully.",
                    name = medication.Name
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding Medication {Name}", model?.Medication.Name);
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }


    }
}
