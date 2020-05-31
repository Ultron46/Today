using DevOps.Common;
using DevOps.UI.Models;
using Hangfire;
using Microsoft.Owin;
using Newtonsoft.Json;
using Owin;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Ionic.Zip;

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
            //RecurringJob.AddOrUpdate(() => this.UpdateReleaseProjectStatus(), "*/3 * * * *");
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
                    string addressUrl = "api/EmailMaster/GetEmails?id=" + build.BuildProject.Project.OrganisationId;
                    List<EmailMaster> emails = new List<EmailMaster>();
                    Res = await Helpers.Get(addressUrl, "");
                    if (Res.IsSuccessStatusCode)
                    {
                        var Emails = Res.Content.ReadAsStringAsync().Result;
                        emails = JsonConvert.DeserializeObject<List<EmailMaster>>(Emails);
                    }
                    List<string> emailIds = emails.Select(x => x.EmailId).ToList();
                    string msg = "Version: " + build.Mejor_Version + "." + build.Minor_Version + "." + build.Build_Version + "\nServer Build Status: ";
                    msg += build.ServerBuildId % 2 == 1 ? "failure" : "success";
                    await Helpers.SendEmail(emailIds, build.BuildProject.Project.ProjectName, msg);
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
                    string addressUrl = "api/EmailMaster/GetEmails?id=" + build.Project.OrganisationId;
                    List<EmailMaster> emails = new List<EmailMaster>();
                    Res = await Helpers.Get(addressUrl, "");
                    if (Res.IsSuccessStatusCode)
                    {
                        var Emails = Res.Content.ReadAsStringAsync().Result;
                        emails = JsonConvert.DeserializeObject<List<EmailMaster>>(Emails);
                    }
                    List<string> emailIds = emails.Select(x => x.EmailId).ToList();
                    string msg = "Version: " + build.Mejor_Version + "." + build.Minor_Version + "." + build.Build_Version + "\nProject Build Status: ";
                    string status = build.BuildId % 2 == 1 ? "failure" : "success";
                    msg += status;
                    await Helpers.SendEmail(emailIds, build.Project.ProjectName, msg);
                    if(status == "success")
                    {
                        string ReadmeText = "Hello!\n\nThis is a " + build.Project.ProjectName +" README..." ;
                        using (ZipFile zip = new ZipFile())
                        {
                            zip.AddEntry("Redme.txt",ReadmeText);
                            //Generate zip file folder into loation
                            string path = build.DownloadURL + "Builds\\" + build.Project.ProjectName + "_" + build.Mejor_Version + "." + build.Minor_Version + "." + build.Build_Version + ".zip";
                            zip.Save(path);
                        }
                    }
                }
            }
        }

        //public async Task UpdateReleaseProjectStatus()
        //{
        //    var client = new HttpClient();

        //    client.BaseAddress = new Uri(baseUrl);

        //    client.DefaultRequestHeaders.Clear();

        //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //    string address = "api/Projects/GetQueuedBuildProject";
        //    HttpResponseMessage Res = await client.GetAsync(address);
        //    ReleaseProject releaseProject = new ReleaseProject();
        //    if (Res.IsSuccessStatusCode)
        //    {
        //        var BuildResponse = Res.Content.ReadAsStringAsync().Result;
        //        releaseProject = JsonConvert.DeserializeObject<ReleaseProject>(BuildResponse);
        //    }
        //    if (releaseProject != null)
        //    {
        //        address = "api/Projects/UpdateQueuedBuildProjectStatus?id=" + releaseProject.ReleaseProjectId;
        //        Res = await client.GetAsync(address);

        //        if (Res.IsSuccessStatusCode)
        //        {
        //            var MainMEnuResponse = Res.Content.ReadAsStringAsync().Result;
        //            string addressUrl = "api/EmailMaster/GetEmails?id=" + releaseProject.BuildProject.Project.OrganisationId;
        //            List<EmailMaster> emails = new List<EmailMaster>();
        //            Res = await Helpers.Get(addressUrl, "");
        //            if (Res.IsSuccessStatusCode)
        //            {
        //                var Emails = Res.Content.ReadAsStringAsync().Result;
        //                emails = JsonConvert.DeserializeObject<List<EmailMaster>>(Emails);
        //            }
        //            List<string> emailIds = emails.Select(x => x.EmailId).ToList();
        //            string msg = "Version: " + releaseProject.BuildProject.Mejor_Version + "." + releaseProject.BuildProject.Minor_Version + "." + releaseProject.BuildProject.Build_Version + "\nRelease Build Status: ";
        //            string status = releaseProject.ReleaseProjectId % 2 == 1 ? "failure" : "success";
        //            msg += status;
        //            await Helpers.SendEmail(emailIds, releaseProject.BuildProject.Project.ProjectName, msg);
        //            if (status == "success")
        //            {
        //                string ReadmeText = "Hello!\n\nThis is a " + releaseProject.BuildProject.Project.ProjectName + " README...";
        //                using (ZipFile zip = new ZipFile())
        //                {
        //                    zip.AddEntry("Redme.txt", ReadmeText);
        //                    //Generate zip file folder into loation
        //                    string path = releaseProject.DownloadURL + "Releases\\" + releaseProject.BuildProject.Project.ProjectName + "_" + releaseProject.BuildProject.Mejor_Version + "." + releaseProject.BuildProject.Minor_Version + "." + releaseProject.BuildProject.Build_Version + ".zip";
        //                    zip.Save(path);
        //                }
        //            }
        //        }
        //    }
        //}
    }
}