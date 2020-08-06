using Microsoft.AspNetCore.Mvc;
using server.Data;
namespace server
{
    [Route("[controller]/json")]
    [ApiController]
    public class UnityController : Controller
    {
        UnityService unityService;

        public UnityController(UnityService unityService)
        {
            this.unityService = unityService;
        }

        [HttpPost]
        public ActionResult<string> Index(UnityMessage message)
        {
            unityService.SendMessage(message);
            return "Success";
        }
    }

}
