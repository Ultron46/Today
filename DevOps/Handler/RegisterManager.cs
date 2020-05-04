using Autofac;
using DevOps.Business.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevOps.Handler
{
    public class RegisterManager
    {
        public static void Register(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType(typeof(UserManager)).AsImplementedInterfaces();
            containerBuilder.RegisterType(typeof(MainMenuManager)).AsImplementedInterfaces();
            containerBuilder.RegisterType(typeof(SubMenuManager)).AsImplementedInterfaces();
            containerBuilder.RegisterType(typeof(OrganizationsManager)).AsImplementedInterfaces();
            containerBuilder.RegisterType(typeof(ProjectManager)).AsImplementedInterfaces();
            containerBuilder.RegisterType(typeof(ServerManager)).AsImplementedInterfaces();
            containerBuilder.RegisterType(typeof(AuthorizationManager)).AsImplementedInterfaces();
            containerBuilder.RegisterTypes(typeof(SupportTicketManager)).AsImplementedInterfaces();
            containerBuilder.RegisterTypes(typeof(EmailMasterManager)).AsImplementedInterfaces();
        }
    }
}