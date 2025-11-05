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
                    join pc in _db.PatientConditions on new { PatientId = patientId, ConditionId = ci.ConditionDiagnosisId } equals new { pc.PatientId, pc.ConditionId }
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
        public async Task<List<string>> CheckAllergyConflictsAsync(int patientId, IEnumerable<int> medicationIds)
        {
            try
            {
                var medicationIdsList = medicationIds.ToList();

                var conflicts = await (
                    from pa in _db.PatientAllergies
                    join mi in _db.MedicationIngredients on pa.ActiveIngredientId equals mi.ActiveIngredientId
                    join ai in _db.ActiveIngredients on mi.ActiveIngredientId equals ai.IngredientId
                    join m in _db.Medications on mi.MedicationId equals m.MedicationId
                    where pa.PatientId == patientId && medicationIdsList.Contains(mi.MedicationId)
                    select new { IngredientName = ai.Name, MedicationName = m.Name }
                )
                .Distinct()
                .Select(x => $"{x.IngredientName} ({x.MedicationName})")
                .ToListAsync();

                return conflicts;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while checking allergy conflicts for patient {PatientId}", patientId);
                return new List<string>();
            }
        }

        public async Task<List<string>> CheckMedicationInteractionsAsync(int patientId, IEnumerable<int> newMedicationIds)
        {
            try
            {
                var newMedicationIdsList = newMedicationIds.ToList();

                var warnings = await _db.PatientMedications
                    .Where(pm => pm.PatientId == patientId)
                    .Join(_db.MedicationIngredients,
                        pm => pm.MedicationId,
                        miCurrent => miCurrent.MedicationId,
                        (pm, miCurrent) => miCurrent)
                    .SelectMany(miCurrent => _db.MedicationIngredients
                        .Where(miNew => newMedicationIdsList.Contains(miNew.MedicationId)
                                       && miCurrent.ActiveIngredientId != miNew.ActiveIngredientId),
                        (miCurrent, miNew) => new { miCurrent, miNew })
                    .Join(_db.MedicationInteractions,
                        x => 1,
                        interaction => 1,
                        (x, interaction) => new { x.miCurrent, x.miNew, interaction })
                    .Where(x => (x.interaction.ActiveIngredient1Id == x.miCurrent.ActiveIngredientId
                                && x.interaction.ActiveIngredient2Id == x.miNew.ActiveIngredientId)
                                || (x.interaction.ActiveIngredient1Id == x.miNew.ActiveIngredientId
                                && x.interaction.ActiveIngredient2Id == x.miCurrent.ActiveIngredientId))
                    .Select(x => x.interaction.Description)
                    .Distinct()
                    .ToListAsync();

                return warnings;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while checking interactions for patient {PatientId}", patientId);
                return new List<string>();
            }
        }
    }
}
