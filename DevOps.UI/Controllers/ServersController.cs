using DevOps.Common;
using DevOps.DataEntities.Models;
using DevOps.UI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DevOps.UI.Controllers
{
    public class ServersController : Controller
    {
        string baseUrl = Constants.baseurl;
        // GET: Servers
        [HttpGet]
        public ActionResult Servers()
        {
            return PartialView("Servers");
        }

        [HttpGet]
        public ActionResult Registration()
        {
            return PartialView("Registration");
        }
        [HttpPost]
        public ActionResult Registration(Server server)
        {
            server.OrganisationId = Convert.ToInt32(Session["Organization"].ToString());
            var client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var stringContent = new StringContent(JsonConvert.SerializeObject(server), Encoding.UTF8, "application/json");
            var addressUri = new Uri("api/Servers/InsertServer", UriKind.Relative);
            HttpResponseMessage Res = client.PostAsync(addressUri, stringContent).Result;
            if (Res.IsSuccessStatusCode)
            {
                return Json(new { success = true }); ;
            }
            return Json(new { error = true });
        }

        [HttpGet]
        public async Task<ActionResult> EditServer(int id)
        {
            ServerConfig serverConfig = new ServerConfig();
            ServerCredential serverCredential = new ServerCredential();
            var client = new HttpClient();

            client.BaseAddress = new Uri(baseUrl);

            client.DefaultRequestHeaders.Clear();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string address = "api/Servers/GetServerConfig?id=" + id.ToString();
            HttpResponseMessage Res = await client.GetAsync(address);

            if (Res.IsSuccessStatusCode)
            {
                var MainMEnuResponse = Res.Content.ReadAsStringAsync().Result;

                serverConfig = JsonConvert.DeserializeObject<ServerConfig>(MainMEnuResponse);
            }
            Server server = new Server { 
                ServerId = serverConfig.ServerId,
                ServerName = serverConfig.ServerName,
                IPAddress = serverConfig.IPAddress,
                RAM = serverConfig.RAM,
                Processer = serverConfig.Processer,
                OS = serverConfig.OS,
                Version = serverConfig.Version,
                HostName = serverConfig.ServerCredentials.First().HostName,
                Password = serverConfig.ServerCredentials.First().Password,
                ConnectionString = serverConfig.ServerCredentials.First().ConnectionString
            };
            return PartialView(server);
        }
    }
}