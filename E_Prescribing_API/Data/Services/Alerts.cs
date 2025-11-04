using Microsoft.EntityFrameworkCore;

namespace E_Prescribing_API.Data.Services
{
    public class Alerts
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<Alerts> _logger;

        public Alerts(ApplicationDbContext db, ILogger<Alerts> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<List<string>> CheckContraindicationsAsync(int patientId, IEnumerable<int> medicationIds)
        {
            try
            {
                var medicationIdsList = medicationIds.ToList();

                var contraindications = await (
                    from ci in _db.ContraIndications
                    join ai in _db.ActiveIngredients on ci.ActiveIngredientId equals ai.IngredientId
                    join mi in _db.MedicationIngredients on ai.IngredientId equals mi.ActiveIngredientId
                    join pc in _db.PatientConditions on new { PatientId = patientId, ConditionId = ci.ConditionDiagnosisId }
                                                        equals new { pc.PatientId, pc.ConditionId }
                    where medicationIdsList.Contains(mi.MedicationId)
                    select ai.Name
                )
                .Distinct()
                .Select(name => $"{name} (contraindicated)")
                .ToListAsync();

                return contraindications;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while checking contraindications for patient {PatientId}", patientId);
                return new List<string>();
            }
        }
    }
}
