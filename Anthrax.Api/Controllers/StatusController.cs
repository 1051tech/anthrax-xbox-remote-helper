using Anthrax.Api.Helpers;
using Anthrax.Api.Models;
using System.Web.Http;

namespace Anthrax.Api.Controllers
{
    public class StatusController : ApiController
    {
        AnthraxModel model;

        public JsonResponse Get()
        {
            model = new StatusModel() { IPAddress = Network.GetPublicIP(), XboxOnline = Xbox.IsReachable(2000) };
            return new JsonResponse(JsonResponse.Status.Success, model);
        }

        // POST api/<controller>
        public JsonResponse Post()
        {
            Xbox.TogglePower();

            model = new StatusModel() { IPAddress = Network.GetPublicIP(), XboxOnline = Xbox.IsReachable(5000) };
            return new JsonResponse(JsonResponse.Status.Success, model);
        }
    }
}