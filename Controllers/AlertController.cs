using DotNetAPI.Repositories;

namespace DotNetAPI.Controllers
{
    public class AlertController : BaseApiController
    {
        private readonly IAlertRepository _alertRepository;
        
        public AlertController(IAlertRepository alertRepository)
        {
            _alertRepository = alertRepository;
        }
    }
}