using DevOps.Common;
using DevOps.UI.Models;
using Hangfire;
using Microsoft.Owin;
using Newtonsoft.Json;
using Owin;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

[assembly: OwinStartup("ServerBuildStartUp", typeof(DevOps.UI.App_Start.Startup))]

namespace DevOps.UI.App_Start
{
    public class Startup
    {
        string baseUrl = Constants.baseurl;
        public void Configuration(IAppBuilder app)
        {
            GlobalConfiguration.Configuration
                .UseSqlServerStorage("Data Source=DESKTOP-LS13G7I\\SQLEXPRESS;Initial Catalog=DevOps;User ID=sa;Password=Password12$");
            RecurringJob.AddOrUpdate(() => this.UpdateServerBuildStatus(), "*/3 * * * *");
            RecurringJob.AddOrUpdate(() => this.UpdateProjectBuildStatus(), "*/3 * * * *");
            RecurringJob.AddOrUpdate(() => this.UpdateBuildProjectStatus(), "*/3 * * * *");
            app.UseHangfireDashboard();
            app.UseHangfireServer();
        }

        public async Task UpdateServerBuildStatus()
        {
            var client = new HttpClient();

            client.BaseAddress = new Uri(baseUrl);

            client.DefaultRequestHeaders.Clear();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string address = "api/Servers/GetQueuedBuild";
            HttpResponseMessage Res = await client.GetAsync(address);
            ServerBuild build = new ServerBuild();
            if (Res.IsSuccessStatusCode)
            {
                var ServerBuildResponse = Res.Content.ReadAsStringAsync().Result;
                build = JsonConvert.DeserializeObject<ServerBuild>(ServerBuildResponse);
            }
            if (build != null)
            {
                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                address = "api/Servers/UpdateServerBuildStatus?id=" + build.ServerBuildId;
                Res = await client.GetAsync(address);
                if (Res.IsSuccessStatusCode)
                {
                    var ServerBuildResponse = Res.Content.ReadAsStringAsync().Result;
                }
            }
        }

        public async Task UpdateProjectBuildStatus()
        {
            var client = new HttpClient();

            client.BaseAddress = new Uri(baseUrl);

            client.DefaultRequestHeaders.Clear();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string address = "api/Projects/GetQueuedBuild";
            HttpResponseMessage Res = await client.GetAsync(address);
            BuildProject build = new BuildProject();
            if (Res.IsSuccessStatusCode)
            {
                var BuildResponse = Res.Content.ReadAsStringAsync().Result;
                build = JsonConvert.DeserializeObject<BuildProject>(BuildResponse);
            }
            if (build != null)
            {
                address = "api/Projects/UpdateProjectBuildStatus?id=" + build.BuildId;
                Res = await client.GetAsync(address);

                if (Res.IsSuccessStatusCode)
                {
                    var MainMEnuResponse = Res.Content.ReadAsStringAsync().Result;
                }
            }
        }

        public async Task UpdateBuildProjectStatus()
        {
            var client = new HttpClient();

            client.BaseAddress = new Uri(baseUrl);

            client.DefaultRequestHeaders.Clear();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string address = "api/Projects/GetQueuedBuildProject";
            HttpResponseMessage Res = await client.GetAsync(address);
            ReleaseProject releaseProject = new ReleaseProject();
            if (Res.IsSuccessStatusCode)
            {
                var BuildResponse = Res.Content.ReadAsStringAsync().Result;
                releaseProject = JsonConvert.DeserializeObject<ReleaseProject>(BuildResponse);
            }
            if (releaseProject != null)
            {
                address = "api/Projects/UpdateQueuedBuildProjectStatus?id=" + releaseProject.ReleaseProjectId;
                Res = await client.GetAsync(address);

                if (Res.IsSuccessStatusCode)
                {
                    var MainMEnuResponse = Res.Content.ReadAsStringAsync().Result;
                }
            }
        }
    }
}