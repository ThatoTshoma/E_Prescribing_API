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



    }
}
