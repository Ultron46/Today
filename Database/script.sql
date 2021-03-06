USE [master]
GO
/****** Object:  Database [DevOps]    Script Date: 23-03-2020 09:42:17 ******/
CREATE DATABASE [DevOps]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DevOps', FILENAME = N'C:\Users\Hardik\DevOps.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DevOps_log', FILENAME = N'C:\Users\Hardik\DevOps_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [DevOps] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DevOps].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DevOps] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DevOps] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DevOps] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DevOps] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DevOps] SET ARITHABORT OFF 
GO
ALTER DATABASE [DevOps] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DevOps] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DevOps] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DevOps] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DevOps] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DevOps] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DevOps] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DevOps] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DevOps] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DevOps] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DevOps] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DevOps] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DevOps] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DevOps] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DevOps] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DevOps] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DevOps] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DevOps] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [DevOps] SET  MULTI_USER 
GO
ALTER DATABASE [DevOps] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DevOps] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DevOps] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DevOps] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DevOps] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [DevOps] SET QUERY_STORE = OFF
GO
USE [DevOps]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [DevOps]
GO
/****** Object:  Table [dbo].[Branch]    Script Date: 23-03-2020 09:42:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Branch](
	[BranchId] [int] IDENTITY(1,1) NOT NULL,
	[BranchName] [varchar](10) NULL,
	[ProjectId] [int] NULL,
 CONSTRAINT [PK_Branch_BranchId] PRIMARY KEY CLUSTERED 
(
	[BranchId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BuildProject]    Script Date: 23-03-2020 09:42:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BuildProject](
	[BuildId] [int] IDENTITY(1,1) NOT NULL,
	[ProjectId] [int] NULL,
	[BuildBy] [int] NULL,
	[Mejor_Version] [tinyint] NULL,
	[Minor_Version] [tinyint] NULL,
	[Build_Version] [int] NULL,
	[DownloadURL] [nvarchar](max) NULL,
	[Status] [varchar](10) NULL,
	[BuildDate] [datetime] NOT NULL,
 CONSTRAINT [PK_BuildPorject_BuildId] PRIMARY KEY CLUSTERED 
(
	[BuildId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ELMAH_Error]    Script Date: 23-03-2020 09:42:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ELMAH_Error](
	[ErrorId] [uniqueidentifier] NOT NULL,
	[Application] [nvarchar](60) NOT NULL,
	[Host] [nvarchar](50) NOT NULL,
	[Type] [nvarchar](100) NOT NULL,
	[Source] [nvarchar](60) NOT NULL,
	[Message] [nvarchar](500) NOT NULL,
	[User] [nvarchar](50) NOT NULL,
	[StatusCode] [int] NOT NULL,
	[TimeUtc] [datetime] NOT NULL,
	[Sequence] [int] IDENTITY(1,1) NOT NULL,
	[AllXml] [ntext] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmailMaster]    Script Date: 23-03-2020 09:42:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmailMaster](
	[EmailMasterId] [int] IDENTITY(1,1) NOT NULL,
	[EmailId] [varchar](max) NULL,
	[OrganisationId] [int] NULL,
 CONSTRAINT [PK_EmailMaster_EmailMasterId] PRIMARY KEY CLUSTERED 
(
	[EmailMasterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ErrorLog]    Script Date: 23-03-2020 09:42:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ErrorLog](
	[ErrorId] [uniqueidentifier] NOT NULL,
	[Application] [nvarchar](60) NOT NULL,
	[Host] [nvarchar](50) NOT NULL,
	[Type] [nvarchar](100) NOT NULL,
	[Source] [nvarchar](60) NOT NULL,
	[Message] [nvarchar](500) NOT NULL,
	[User] [nvarchar](50) NOT NULL,
	[StatusCode] [int] NOT NULL,
	[TimeUtc] [datetime] NOT NULL,
	[Sequence] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MainMenu]    Script Date: 23-03-2020 09:42:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MainMenu](
	[MainMenuId] [int] IDENTITY(1,1) NOT NULL,
	[MainMenuName] [varchar](20) NULL,
 CONSTRAINT [PK_MainMenu_MainMenuId] PRIMARY KEY CLUSTERED 
(
	[MainMenuId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Organisation]    Script Date: 23-03-2020 09:42:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Organisation](
	[OrganisationId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Address] [nvarchar](max) NULL,
	[Nationality] [varchar](50) NULL,
	[Type] [varchar](20) NULL,
 CONSTRAINT [PK_Organisation_OrganisationId] PRIMARY KEY CLUSTERED 
(
	[OrganisationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Permissions]    Script Date: 23-03-2020 09:42:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permissions](
	[PermissionId] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [tinyint] NOT NULL,
	[SubMenuId] [int] NOT NULL,
	[Read] [bit] NOT NULL,
	[Write] [bit] NOT NULL,
 CONSTRAINT [PK_Permissions_PermissionId] PRIMARY KEY CLUSTERED 
(
	[PermissionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Project]    Script Date: 23-03-2020 09:42:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Project](
	[ProjectId] [int] IDENTITY(1,1) NOT NULL,
	[ProjectName] [varchar](50) NOT NULL,
	[SolutionName] [varchar](50) NOT NULL,
	[OrganisationId] [int] NULL,
	[SourceURL] [varchar](max) NOT NULL,
	[FileFormat] [varchar](max) NOT NULL,
	[Status] [varchar](20) NOT NULL,
	[Type] [varchar](20) NULL,
	[Plateform] [varchar](50) NULL,
	[CreatedBy] [int] NULL,
	[LastModifiedBy] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[LastModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Project_ProjectId] PRIMARY KEY CLUSTERED 
(
	[ProjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReleaseProject]    Script Date: 23-03-2020 09:42:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReleaseProject](
	[ReleaseProjectId] [int] IDENTITY(1,1) NOT NULL,
	[ServerBuildId] [int] NULL,
	[ReleaseBy] [int] NULL,
	[Mejor_Version] [tinyint] NULL,
	[Minor_Version] [tinyint] NULL,
	[Build_Version] [int] NULL,
	[ReleaseDate] [datetime] NULL,
 CONSTRAINT [PK_ReleaseProject_ReleaseProjectId] PRIMARY KEY CLUSTERED 
(
	[ReleaseProjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 23-03-2020 09:42:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[RoleId] [tinyint] IDENTITY(1,1) NOT NULL,
	[RoleName] [varchar](20) NOT NULL,
 CONSTRAINT [PK_Role_RoleId] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServerBuild]    Script Date: 23-03-2020 09:42:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServerBuild](
	[ServerBuildId] [int] IDENTITY(1,1) NOT NULL,
	[BuildId] [int] NULL,
	[ServerId] [int] NULL,
	[PublishedBy] [int] NULL,
	[Mejor_Version] [tinyint] NULL,
	[Minor_Version] [tinyint] NULL,
	[Build_Version] [int] NULL,
	[PublishDate] [datetime] NULL,
 CONSTRAINT [PK_ServerBuild_ServerBuildId] PRIMARY KEY CLUSTERED 
(
	[ServerBuildId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServerConfig]    Script Date: 23-03-2020 09:42:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServerConfig](
	[ServerId] [int] IDENTITY(1,1) NOT NULL,
	[ServerName] [varchar](20) NOT NULL,
	[IPAddress] [nvarchar](16) NOT NULL,
	[RAM] [varchar](10) NULL,
	[Processer] [varchar](10) NULL,
	[OS] [varchar](10) NULL,
	[Version] [nvarchar](20) NULL,
	[OrganisationId] [int] NULL,
 CONSTRAINT [PK_ServerConfig_ServerId] PRIMARY KEY CLUSTERED 
(
	[ServerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServerCredentials]    Script Date: 23-03-2020 09:42:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServerCredentials](
	[ServerCredentialsId] [int] IDENTITY(1,1) NOT NULL,
	[ServerId] [int] NULL,
	[HostName] [nvarchar](max) NULL,
	[Password] [nvarchar](max) NULL,
	[ConnectionString] [nvarchar](max) NULL,
 CONSTRAINT [PK_ServerCredentials_ServerCredentialsId] PRIMARY KEY CLUSTERED 
(
	[ServerCredentialsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SubMenu]    Script Date: 23-03-2020 09:42:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubMenu](
	[SubMenuId] [int] IDENTITY(1,1) NOT NULL,
	[MainMenuId] [int] NULL,
	[SubMenuName] [varchar](20) NULL,
 CONSTRAINT [PK_SubMenu_SubMenuId] PRIMARY KEY CLUSTERED 
(
	[SubMenuId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SupportTickets]    Script Date: 23-03-2020 09:42:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SupportTickets](
	[TicketId] [int] IDENTITY(1,1) NOT NULL,
	[GeneratedBy] [int] NULL,
	[FixedBy] [int] NULL,
	[Category] [varchar](50) NULL,
	[Description] [varchar](max) NULL,
	[Status] [varchar](20) NOT NULL,
	[GeneratedDate] [datetime] NOT NULL,
	[FixedDate] [datetime] NULL,
 CONSTRAINT [PK_SupportTickets_TicketId] PRIMARY KEY CLUSTERED 
(
	[TicketId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 23-03-2020 09:42:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Address] [nvarchar](max) NULL,
	[Nationality] [varchar](50) NULL,
	[Phone] [varchar](12) NULL,
	[Email] [varchar](50) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[DateOfBirth] [date] NULL,
	[OrganisationId] [int] NULL,
	[RoleId] [tinyint] NULL,
	[RegistrationDate] [date] NOT NULL,
 CONSTRAINT [PK_User_UserId] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserToken]    Script Date: 23-03-2020 09:42:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserToken](
	[UserID] [int] NOT NULL,
	[Token] [varchar](max) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[ELMAH_Error] ON 

INSERT [dbo].[ELMAH_Error] ([ErrorId], [Application], [Host], [Type], [Source], [Message], [User], [StatusCode], [TimeUtc], [Sequence], [AllXml]) VALUES (N'0a554f3d-de06-4b28-989f-a7b853ffbdb2', N'/LM/W3SVC/2/ROOT', N'DESKTOP-LS13G7I', N'System.Web.HttpException', N'System.Web.Mvc', N'The controller for path ''/'' was not found or does not implement IController.', N'', 404, CAST(N'2020-03-22T11:56:37.573' AS DateTime), 1, N'<error
  application="/LM/W3SVC/2/ROOT"
  host="DESKTOP-LS13G7I"
  type="System.Web.HttpException"
  message="The controller for path ''/'' was not found or does not implement IController."
  source="System.Web.Mvc"
  detail="System.Web.HttpException (0x80004005): The controller for path ''/'' was not found or does not implement IController.&#xD;&#xA;   at System.Web.Mvc.DefaultControllerFactory.GetControllerInstance(RequestContext requestContext, Type controllerType)&#xD;&#xA;   at System.Web.Mvc.DefaultControllerFactory.CreateController(RequestContext requestContext, String controllerName)&#xD;&#xA;   at System.Web.Mvc.MvcHandler.ProcessRequestInit(HttpContextBase httpContext, IController&amp; controller, IControllerFactory&amp; factory)&#xD;&#xA;   at System.Web.Mvc.MvcHandler.BeginProcessRequest(HttpContextBase httpContext, AsyncCallback callback, Object state)&#xD;&#xA;   at System.Web.Mvc.MvcHandler.BeginProcessRequest(HttpContext httpContext, AsyncCallback callback, Object state)&#xD;&#xA;   at System.Web.Mvc.MvcHandler.System.Web.IHttpAsyncHandler.BeginProcessRequest(HttpContext context, AsyncCallback cb, Object extraData)&#xD;&#xA;   at System.Web.HttpApplication.CallHandlerExecutionStep.System.Web.HttpApplication.IExecutionStep.Execute()&#xD;&#xA;   at System.Web.HttpApplication.ExecuteStepImpl(IExecutionStep step)&#xD;&#xA;   at System.Web.HttpApplication.ExecuteStep(IExecutionStep step, Boolean&amp; completedSynchronously)"
  time="2020-03-22T11:56:37.5733498Z"
  statusCode="404">
  <serverVariables>
    <item
      name="ALL_HTTP">
      <value
        string="HTTP_CONNECTION:keep-alive&#xD;&#xA;HTTP_ACCEPT:text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9&#xD;&#xA;HTTP_ACCEPT_ENCODING:gzip, deflate, br&#xD;&#xA;HTTP_ACCEPT_LANGUAGE:en-US,en;q=0.9,hi;q=0.8,gu;q=0.7&#xD;&#xA;HTTP_COOKIE:__test=1; __tawkuuid=e::localhost::EoJKgS7OZqqtNCtys4/Zoral91Cj9dDam/IBNIeLA4Nxvb6YD+9j859qVrgcRwWY::2; ASP.NET_SessionId=wlefqt4u0e0mjmz13hhjaeun; TawkConnectionTime=0&#xD;&#xA;HTTP_HOST:localhost:60969&#xD;&#xA;HTTP_USER_AGENT:Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Safari/537.36&#xD;&#xA;HTTP_UPGRADE_INSECURE_REQUESTS:1&#xD;&#xA;HTTP_SEC_FETCH_DEST:document&#xD;&#xA;HTTP_SEC_FETCH_SITE:none&#xD;&#xA;HTTP_SEC_FETCH_MODE:navigate&#xD;&#xA;HTTP_SEC_FETCH_USER:?1&#xD;&#xA;" />
    </item>
    <item
      name="ALL_RAW">
      <value
        string="Connection: keep-alive&#xD;&#xA;Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9&#xD;&#xA;Accept-Encoding: gzip, deflate, br&#xD;&#xA;Accept-Language: en-US,en;q=0.9,hi;q=0.8,gu;q=0.7&#xD;&#xA;Cookie: __test=1; __tawkuuid=e::localhost::EoJKgS7OZqqtNCtys4/Zoral91Cj9dDam/IBNIeLA4Nxvb6YD+9j859qVrgcRwWY::2; ASP.NET_SessionId=wlefqt4u0e0mjmz13hhjaeun; TawkConnectionTime=0&#xD;&#xA;Host: localhost:60969&#xD;&#xA;User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Safari/537.36&#xD;&#xA;Upgrade-Insecure-Requests: 1&#xD;&#xA;Sec-Fetch-Dest: document&#xD;&#xA;Sec-Fetch-Site: none&#xD;&#xA;Sec-Fetch-Mode: navigate&#xD;&#xA;Sec-Fetch-User: ?1&#xD;&#xA;" />
    </item>
    <item
      name="APPL_MD_PATH">
      <value
        string="/LM/W3SVC/2/ROOT" />
    </item>
    <item
      name="APPL_PHYSICAL_PATH">
      <value
        string="D:\Today\Today\DevOps\" />
    </item>
    <item
      name="AUTH_TYPE">
      <value
        string="" />
    </item>
    <item
      name="AUTH_USER">
      <value
        string="" />
    </item>
    <item
      name="AUTH_PASSWORD">
      <value
        string="*****" />
    </item>
    <item
      name="LOGON_USER">
      <value
        string="" />
    </item>
    <item
      name="REMOTE_USER">
      <value
        string="" />
    </item>
    <item
      name="CERT_COOKIE">
      <value
        string="" />
    </item>
    <item
      name="CERT_FLAGS">
      <value
        string="" />
    </item>
    <item
      name="CERT_ISSUER">
      <value
        string="" />
    </item>
    <item
      name="CERT_KEYSIZE">
      <value
        string="" />
    </item>
    <item
      name="CERT_SECRETKEYSIZE">
      <value
        string="" />
    </item>
    <item
      name="CERT_SERIALNUMBER">
      <value
        string="" />
    </item>
    <item
      name="CERT_SERVER_ISSUER">
      <value
        string="" />
    </item>
    <item
      name="CERT_SERVER_SUBJECT">
      <value
        string="" />
    </item>
    <item
      name="CERT_SUBJECT">
      <value
        string="" />
    </item>
    <item
      name="CONTENT_LENGTH">
      <value
        string="0" />
    </item>
    <item
      name="CONTENT_TYPE">
      <value
        string="" />
    </item>
    <item
      name="GATEWAY_INTERFACE">
      <value
        string="CGI/1.1" />
    </item>
    <item
      name="HTTPS">
      <value
        string="off" />
    </item>
    <item
      name="HTTPS_KEYSIZE">
      <value
        string="" />
    </item>
    <item
      name="HTTPS_SECRETKEYSIZE">
      <value
        string="" />
    </item>
    <item
      name="HTTPS_SERVER_ISSUER">
      <value
        string="" />
    </item>
    <item
      name="HTTPS_SERVER_SUBJECT">
      <value
        string="" />
    </item>
    <item
      name="INSTANCE_ID">
      <value
        string="2" />
    </item>
    <item
      name="INSTANCE_META_PATH">
      <value
        string="/LM/W3SVC/2" />
    </item>
    <item
      name="LOCAL_ADDR">
      <value
        string="::1" />
    </item>
    <item
      name="PATH_INFO">
      <value
        string="/" />
    </item>
    <item
      name="PATH_TRANSLATED">
      <value
        string="D:\Today\Today\DevOps" />
    </item>
    <item
      name="QUERY_STRING">
      <value
        string="" />
    </item>
    <item
      name="REMOTE_ADDR">
      <value
        string="::1" />
    </item>
    <item
      name="REMOTE_HOST">
      <value
        string="::1" />
    </item>
    <item
      name="REMOTE_PORT">
      <value
        string="56448" />
    </item>
    <item
      name="REQUEST_METHOD">
      <value
        string="GET" />
    </item>
    <item
      name="SCRIPT_NAME">
      <value
        string="/" />
    </item>
    <item
      name="SERVER_NAME">
      <value
        string="localhost" />
    </item>
    <item
      name="SERVER_PORT">
      <value
        string="60969" />
    </item>
    <item
      name="SERVER_PORT_SECURE">
      <value
        string="0" />
    </item>
    <item
      name="SERVER_PROTOCOL">
      <value
        string="HTTP/1.1" />
    </item>
    <item
      name="SERVER_SOFTWARE">
      <value
        string="Microsoft-IIS/10.0" />
    </item>
    <item
      name="URL">
      <value
        string="/" />
    </item>
    <item
      name="HTTP_CONNECTION">
      <value
        string="keep-alive" />
    </item>
    <item
      name="HTTP_ACCEPT">
      <value
        string="text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9" />
    </item>
    <item
      name="HTTP_ACCEPT_ENCODING">
      <value
        string="gzip, deflate, br" />
    </item>
    <item
      name="HTTP_ACCEPT_LANGUAGE">
      <value
        string="en-US,en;q=0.9,hi;q=0.8,gu;q=0.7" />
    </item>
    <item
      name="HTTP_COOKIE">
      <value
        string="__test=1; __tawkuuid=e::localhost::EoJKgS7OZqqtNCtys4/Zoral91Cj9dDam/IBNIeLA4Nxvb6YD+9j859qVrgcRwWY::2; ASP.NET_SessionId=wlefqt4u0e0mjmz13hhjaeun; TawkConnectionTime=0" />
    </item>
    <item
      name="HTTP_HOST">
      <value
        string="localhost:60969" />
    </item>
    <item
      name="HTTP_USER_AGENT">
      <value
        string="Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Safari/537.36" />
    </item>
    <item
      name="HTTP_UPGRADE_INSECURE_REQUESTS">
      <value
        string="1" />
    </item>
    <item
      name="HTTP_SEC_FETCH_DEST">
      <value
        string="document" />
    </item>
    <item
      name="HTTP_SEC_FETCH_SITE">
      <value
        string="none" />
    </item>
    <item
      name="HTTP_SEC_FETCH_MODE">
      <value
        string="navigate" />
    </item>
    <item
      name="HTTP_SEC_FETCH_USER">
      <value
        string="?1" />
    </item>
  </serverVariables>
  <cookies>
    <item
      name="__test">
      <value
        string="1" />
    </item>
    <item
      name="__tawkuuid">
      <value
        string="e::localhost::EoJKgS7OZqqtNCtys4/Zoral91Cj9dDam/IBNIeLA4Nxvb6YD+9j859qVrgcRwWY::2" />
    </item>
    <item
      name="ASP.NET_SessionId">
      <value
        string="wlefqt4u0e0mjmz13hhjaeun" />
    </item>
    <item
      name="TawkConnectionTime">
      <value
        string="0" />
    </item>
  </cookies>
</error>')
INSERT [dbo].[ELMAH_Error] ([ErrorId], [Application], [Host], [Type], [Source], [Message], [User], [StatusCode], [TimeUtc], [Sequence], [AllXml]) VALUES (N'b0e442a5-901d-4246-8f58-2bdbaa1526c7', N'/LM/W3SVC/2/ROOT', N'DESKTOP-LS13G7I', N'System.Web.HttpException', N'System.Web.Mvc', N'The controller for path ''/'' was not found or does not implement IController.', N'', 404, CAST(N'2020-03-22T11:59:04.843' AS DateTime), 2, N'<error
  application="/LM/W3SVC/2/ROOT"
  host="DESKTOP-LS13G7I"
  type="System.Web.HttpException"
  message="The controller for path ''/'' was not found or does not implement IController."
  source="System.Web.Mvc"
  detail="System.Web.HttpException (0x80004005): The controller for path ''/'' was not found or does not implement IController.&#xD;&#xA;   at System.Web.Mvc.DefaultControllerFactory.GetControllerInstance(RequestContext requestContext, Type controllerType)&#xD;&#xA;   at System.Web.Mvc.DefaultControllerFactory.CreateController(RequestContext requestContext, String controllerName)&#xD;&#xA;   at System.Web.Mvc.MvcHandler.ProcessRequestInit(HttpContextBase httpContext, IController&amp; controller, IControllerFactory&amp; factory)&#xD;&#xA;   at System.Web.Mvc.MvcHandler.BeginProcessRequest(HttpContextBase httpContext, AsyncCallback callback, Object state)&#xD;&#xA;   at System.Web.Mvc.MvcHandler.BeginProcessRequest(HttpContext httpContext, AsyncCallback callback, Object state)&#xD;&#xA;   at System.Web.Mvc.MvcHandler.System.Web.IHttpAsyncHandler.BeginProcessRequest(HttpContext context, AsyncCallback cb, Object extraData)&#xD;&#xA;   at System.Web.HttpApplication.CallHandlerExecutionStep.System.Web.HttpApplication.IExecutionStep.Execute()&#xD;&#xA;   at System.Web.HttpApplication.ExecuteStepImpl(IExecutionStep step)&#xD;&#xA;   at System.Web.HttpApplication.ExecuteStep(IExecutionStep step, Boolean&amp; completedSynchronously)"
  time="2020-03-22T11:59:04.8440546Z"
  statusCode="404">
  <serverVariables>
    <item
      name="ALL_HTTP">
      <value
        string="HTTP_CONNECTION:keep-alive&#xD;&#xA;HTTP_ACCEPT:text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9&#xD;&#xA;HTTP_ACCEPT_ENCODING:gzip, deflate, br&#xD;&#xA;HTTP_ACCEPT_LANGUAGE:en-US,en;q=0.9,hi;q=0.8,gu;q=0.7&#xD;&#xA;HTTP_COOKIE:__test=1; __tawkuuid=e::localhost::EoJKgS7OZqqtNCtys4/Zoral91Cj9dDam/IBNIeLA4Nxvb6YD+9j859qVrgcRwWY::2; ASP.NET_SessionId=wlefqt4u0e0mjmz13hhjaeun; TawkConnectionTime=0&#xD;&#xA;HTTP_HOST:localhost:60969&#xD;&#xA;HTTP_USER_AGENT:Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Safari/537.36&#xD;&#xA;HTTP_UPGRADE_INSECURE_REQUESTS:1&#xD;&#xA;HTTP_SEC_FETCH_DEST:document&#xD;&#xA;HTTP_SEC_FETCH_SITE:none&#xD;&#xA;HTTP_SEC_FETCH_MODE:navigate&#xD;&#xA;HTTP_SEC_FETCH_USER:?1&#xD;&#xA;" />
    </item>
    <item
      name="ALL_RAW">
      <value
        string="Connection: keep-alive&#xD;&#xA;Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9&#xD;&#xA;Accept-Encoding: gzip, deflate, br&#xD;&#xA;Accept-Language: en-US,en;q=0.9,hi;q=0.8,gu;q=0.7&#xD;&#xA;Cookie: __test=1; __tawkuuid=e::localhost::EoJKgS7OZqqtNCtys4/Zoral91Cj9dDam/IBNIeLA4Nxvb6YD+9j859qVrgcRwWY::2; ASP.NET_SessionId=wlefqt4u0e0mjmz13hhjaeun; TawkConnectionTime=0&#xD;&#xA;Host: localhost:60969&#xD;&#xA;User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Safari/537.36&#xD;&#xA;Upgrade-Insecure-Requests: 1&#xD;&#xA;Sec-Fetch-Dest: document&#xD;&#xA;Sec-Fetch-Site: none&#xD;&#xA;Sec-Fetch-Mode: navigate&#xD;&#xA;Sec-Fetch-User: ?1&#xD;&#xA;" />
    </item>
    <item
      name="APPL_MD_PATH">
      <value
        string="/LM/W3SVC/2/ROOT" />
    </item>
    <item
      name="APPL_PHYSICAL_PATH">
      <value
        string="D:\Today\Today\DevOps\" />
    </item>
    <item
      name="AUTH_TYPE">
      <value
        string="" />
    </item>
    <item
      name="AUTH_USER">
      <value
        string="" />
    </item>
    <item
      name="AUTH_PASSWORD">
      <value
        string="*****" />
    </item>
    <item
      name="LOGON_USER">
      <value
        string="" />
    </item>
    <item
      name="REMOTE_USER">
      <value
        string="" />
    </item>
    <item
      name="CERT_COOKIE">
      <value
        string="" />
    </item>
    <item
      name="CERT_FLAGS">
      <value
        string="" />
    </item>
    <item
      name="CERT_ISSUER">
      <value
        string="" />
    </item>
    <item
      name="CERT_KEYSIZE">
      <value
        string="" />
    </item>
    <item
      name="CERT_SECRETKEYSIZE">
      <value
        string="" />
    </item>
    <item
      name="CERT_SERIALNUMBER">
      <value
        string="" />
    </item>
    <item
      name="CERT_SERVER_ISSUER">
      <value
        string="" />
    </item>
    <item
      name="CERT_SERVER_SUBJECT">
      <value
        string="" />
    </item>
    <item
      name="CERT_SUBJECT">
      <value
        string="" />
    </item>
    <item
      name="CONTENT_LENGTH">
      <value
        string="0" />
    </item>
    <item
      name="CONTENT_TYPE">
      <value
        string="" />
    </item>
    <item
      name="GATEWAY_INTERFACE">
      <value
        string="CGI/1.1" />
    </item>
    <item
      name="HTTPS">
      <value
        string="off" />
    </item>
    <item
      name="HTTPS_KEYSIZE">
      <value
        string="" />
    </item>
    <item
      name="HTTPS_SECRETKEYSIZE">
      <value
        string="" />
    </item>
    <item
      name="HTTPS_SERVER_ISSUER">
      <value
        string="" />
    </item>
    <item
      name="HTTPS_SERVER_SUBJECT">
      <value
        string="" />
    </item>
    <item
      name="INSTANCE_ID">
      <value
        string="2" />
    </item>
    <item
      name="INSTANCE_META_PATH">
      <value
        string="/LM/W3SVC/2" />
    </item>
    <item
      name="LOCAL_ADDR">
      <value
        string="::1" />
    </item>
    <item
      name="PATH_INFO">
      <value
        string="/" />
    </item>
    <item
      name="PATH_TRANSLATED">
      <value
        string="D:\Today\Today\DevOps" />
    </item>
    <item
      name="QUERY_STRING">
      <value
        string="" />
    </item>
    <item
      name="REMOTE_ADDR">
      <value
        string="::1" />
    </item>
    <item
      name="REMOTE_HOST">
      <value
        string="::1" />
    </item>
    <item
      name="REMOTE_PORT">
      <value
        string="56469" />
    </item>
    <item
      name="REQUEST_METHOD">
      <value
        string="GET" />
    </item>
    <item
      name="SCRIPT_NAME">
      <value
        string="/" />
    </item>
    <item
      name="SERVER_NAME">
      <value
        string="localhost" />
    </item>
    <item
      name="SERVER_PORT">
      <value
        string="60969" />
    </item>
    <item
      name="SERVER_PORT_SECURE">
      <value
        string="0" />
    </item>
    <item
      name="SERVER_PROTOCOL">
      <value
        string="HTTP/1.1" />
    </item>
    <item
      name="SERVER_SOFTWARE">
      <value
        string="Microsoft-IIS/10.0" />
    </item>
    <item
      name="URL">
      <value
        string="/" />
    </item>
    <item
      name="HTTP_CONNECTION">
      <value
        string="keep-alive" />
    </item>
    <item
      name="HTTP_ACCEPT">
      <value
        string="text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9" />
    </item>
    <item
      name="HTTP_ACCEPT_ENCODING">
      <value
        string="gzip, deflate, br" />
    </item>
    <item
      name="HTTP_ACCEPT_LANGUAGE">
      <value
        string="en-US,en;q=0.9,hi;q=0.8,gu;q=0.7" />
    </item>
    <item
      name="HTTP_COOKIE">
      <value
        string="__test=1; __tawkuuid=e::localhost::EoJKgS7OZqqtNCtys4/Zoral91Cj9dDam/IBNIeLA4Nxvb6YD+9j859qVrgcRwWY::2; ASP.NET_SessionId=wlefqt4u0e0mjmz13hhjaeun; TawkConnectionTime=0" />
    </item>
    <item
      name="HTTP_HOST">
      <value
        string="localhost:60969" />
    </item>
    <item
      name="HTTP_USER_AGENT">
      <value
        string="Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Safari/537.36" />
    </item>
    <item
      name="HTTP_UPGRADE_INSECURE_REQUESTS">
      <value
        string="1" />
    </item>
    <item
      name="HTTP_SEC_FETCH_DEST">
      <value
        string="document" />
    </item>
    <item
      name="HTTP_SEC_FETCH_SITE">
      <value
        string="none" />
    </item>
    <item
      name="HTTP_SEC_FETCH_MODE">
      <value
        string="navigate" />
    </item>
    <item
      name="HTTP_SEC_FETCH_USER">
      <value
        string="?1" />
    </item>
  </serverVariables>
  <cookies>
    <item
      name="__test">
      <value
        string="1" />
    </item>
    <item
      name="__tawkuuid">
      <value
        string="e::localhost::EoJKgS7OZqqtNCtys4/Zoral91Cj9dDam/IBNIeLA4Nxvb6YD+9j859qVrgcRwWY::2" />
    </item>
    <item
      name="ASP.NET_SessionId">
      <value
        string="wlefqt4u0e0mjmz13hhjaeun" />
    </item>
    <item
      name="TawkConnectionTime">
      <value
        string="0" />
    </item>
  </cookies>
</error>')
INSERT [dbo].[ELMAH_Error] ([ErrorId], [Application], [Host], [Type], [Source], [Message], [User], [StatusCode], [TimeUtc], [Sequence], [AllXml]) VALUES (N'931fd6ae-2544-4848-8157-15a0144240af', N'/LM/W3SVC/2/ROOT', N'DESKTOP-LS13G7I', N'System.Web.HttpException', N'System.Web.Mvc', N'The controller for path ''/'' was not found or does not implement IController.', N'', 404, CAST(N'2020-03-22T12:36:07.380' AS DateTime), 3, N'<error
  application="/LM/W3SVC/2/ROOT"
  host="DESKTOP-LS13G7I"
  type="System.Web.HttpException"
  message="The controller for path ''/'' was not found or does not implement IController."
  source="System.Web.Mvc"
  detail="System.Web.HttpException (0x80004005): The controller for path ''/'' was not found or does not implement IController.&#xD;&#xA;   at System.Web.Mvc.DefaultControllerFactory.GetControllerInstance(RequestContext requestContext, Type controllerType)&#xD;&#xA;   at System.Web.Mvc.DefaultControllerFactory.CreateController(RequestContext requestContext, String controllerName)&#xD;&#xA;   at System.Web.Mvc.MvcHandler.ProcessRequestInit(HttpContextBase httpContext, IController&amp; controller, IControllerFactory&amp; factory)&#xD;&#xA;   at System.Web.Mvc.MvcHandler.BeginProcessRequest(HttpContextBase httpContext, AsyncCallback callback, Object state)&#xD;&#xA;   at System.Web.Mvc.MvcHandler.BeginProcessRequest(HttpContext httpContext, AsyncCallback callback, Object state)&#xD;&#xA;   at System.Web.Mvc.MvcHandler.System.Web.IHttpAsyncHandler.BeginProcessRequest(HttpContext context, AsyncCallback cb, Object extraData)&#xD;&#xA;   at System.Web.HttpApplication.CallHandlerExecutionStep.System.Web.HttpApplication.IExecutionStep.Execute()&#xD;&#xA;   at System.Web.HttpApplication.ExecuteStepImpl(IExecutionStep step)&#xD;&#xA;   at System.Web.HttpApplication.ExecuteStep(IExecutionStep step, Boolean&amp; completedSynchronously)"
  time="2020-03-22T12:36:07.3811509Z"
  statusCode="404">
  <serverVariables>
    <item
      name="ALL_HTTP">
      <value
        string="HTTP_CONNECTION:keep-alive&#xD;&#xA;HTTP_ACCEPT:text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9&#xD;&#xA;HTTP_ACCEPT_ENCODING:gzip, deflate, br&#xD;&#xA;HTTP_ACCEPT_LANGUAGE:en-US,en;q=0.9,hi;q=0.8,gu;q=0.7&#xD;&#xA;HTTP_COOKIE:__test=1; __tawkuuid=e::localhost::EoJKgS7OZqqtNCtys4/Zoral91Cj9dDam/IBNIeLA4Nxvb6YD+9j859qVrgcRwWY::2; ASP.NET_SessionId=wlefqt4u0e0mjmz13hhjaeun; TawkConnectionTime=0&#xD;&#xA;HTTP_HOST:localhost:60969&#xD;&#xA;HTTP_USER_AGENT:Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Safari/537.36&#xD;&#xA;HTTP_UPGRADE_INSECURE_REQUESTS:1&#xD;&#xA;HTTP_SEC_FETCH_DEST:document&#xD;&#xA;HTTP_SEC_FETCH_SITE:none&#xD;&#xA;HTTP_SEC_FETCH_MODE:navigate&#xD;&#xA;HTTP_SEC_FETCH_USER:?1&#xD;&#xA;" />
    </item>
    <item
      name="ALL_RAW">
      <value
        string="Connection: keep-alive&#xD;&#xA;Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9&#xD;&#xA;Accept-Encoding: gzip, deflate, br&#xD;&#xA;Accept-Language: en-US,en;q=0.9,hi;q=0.8,gu;q=0.7&#xD;&#xA;Cookie: __test=1; __tawkuuid=e::localhost::EoJKgS7OZqqtNCtys4/Zoral91Cj9dDam/IBNIeLA4Nxvb6YD+9j859qVrgcRwWY::2; ASP.NET_SessionId=wlefqt4u0e0mjmz13hhjaeun; TawkConnectionTime=0&#xD;&#xA;Host: localhost:60969&#xD;&#xA;User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Safari/537.36&#xD;&#xA;Upgrade-Insecure-Requests: 1&#xD;&#xA;Sec-Fetch-Dest: document&#xD;&#xA;Sec-Fetch-Site: none&#xD;&#xA;Sec-Fetch-Mode: navigate&#xD;&#xA;Sec-Fetch-User: ?1&#xD;&#xA;" />
    </item>
    <item
      name="APPL_MD_PATH">
      <value
        string="/LM/W3SVC/2/ROOT" />
    </item>
    <item
      name="APPL_PHYSICAL_PATH">
      <value
        string="D:\Today\Today\DevOps\" />
    </item>
    <item
      name="AUTH_TYPE">
      <value
        string="" />
    </item>
    <item
      name="AUTH_USER">
      <value
        string="" />
    </item>
    <item
      name="AUTH_PASSWORD">
      <value
        string="*****" />
    </item>
    <item
      name="LOGON_USER">
      <value
        string="" />
    </item>
    <item
      name="REMOTE_USER">
      <value
        string="" />
    </item>
    <item
      name="CERT_COOKIE">
      <value
        string="" />
    </item>
    <item
      name="CERT_FLAGS">
      <value
        string="" />
    </item>
    <item
      name="CERT_ISSUER">
      <value
        string="" />
    </item>
    <item
      name="CERT_KEYSIZE">
      <value
        string="" />
    </item>
    <item
      name="CERT_SECRETKEYSIZE">
      <value
        string="" />
    </item>
    <item
      name="CERT_SERIALNUMBER">
      <value
        string="" />
    </item>
    <item
      name="CERT_SERVER_ISSUER">
      <value
        string="" />
    </item>
    <item
      name="CERT_SERVER_SUBJECT">
      <value
        string="" />
    </item>
    <item
      name="CERT_SUBJECT">
      <value
        string="" />
    </item>
    <item
      name="CONTENT_LENGTH">
      <value
        string="0" />
    </item>
    <item
      name="CONTENT_TYPE">
      <value
        string="" />
    </item>
    <item
      name="GATEWAY_INTERFACE">
      <value
        string="CGI/1.1" />
    </item>
    <item
      name="HTTPS">
      <value
        string="off" />
    </item>
    <item
      name="HTTPS_KEYSIZE">
      <value
        string="" />
    </item>
    <item
      name="HTTPS_SECRETKEYSIZE">
      <value
        string="" />
    </item>
    <item
      name="HTTPS_SERVER_ISSUER">
      <value
        string="" />
    </item>
    <item
      name="HTTPS_SERVER_SUBJECT">
      <value
        string="" />
    </item>
    <item
      name="INSTANCE_ID">
      <value
        string="2" />
    </item>
    <item
      name="INSTANCE_META_PATH">
      <value
        string="/LM/W3SVC/2" />
    </item>
    <item
      name="LOCAL_ADDR">
      <value
        string="::1" />
    </item>
    <item
      name="PATH_INFO">
      <value
        string="/" />
    </item>
    <item
      name="PATH_TRANSLATED">
      <value
        string="D:\Today\Today\DevOps" />
    </item>
    <item
      name="QUERY_STRING">
      <value
        string="" />
    </item>
    <item
      name="REMOTE_ADDR">
      <value
        string="::1" />
    </item>
    <item
      name="REMOTE_HOST">
      <value
        string="::1" />
    </item>
    <item
      name="REMOTE_PORT">
      <value
        string="56256" />
    </item>
    <item
      name="REQUEST_METHOD">
      <value
        string="GET" />
    </item>
    <item
      name="SCRIPT_NAME">
      <value
        string="/" />
    </item>
    <item
      name="SERVER_NAME">
      <value
        string="localhost" />
    </item>
    <item
      name="SERVER_PORT">
      <value
        string="60969" />
    </item>
    <item
      name="SERVER_PORT_SECURE">
      <value
        string="0" />
    </item>
    <item
      name="SERVER_PROTOCOL">
      <value
        string="HTTP/1.1" />
    </item>
    <item
      name="SERVER_SOFTWARE">
      <value
        string="Microsoft-IIS/10.0" />
    </item>
    <item
      name="URL">
      <value
        string="/" />
    </item>
    <item
      name="HTTP_CONNECTION">
      <value
        string="keep-alive" />
    </item>
    <item
      name="HTTP_ACCEPT">
      <value
        string="text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9" />
    </item>
    <item
      name="HTTP_ACCEPT_ENCODING">
      <value
        string="gzip, deflate, br" />
    </item>
    <item
      name="HTTP_ACCEPT_LANGUAGE">
      <value
        string="en-US,en;q=0.9,hi;q=0.8,gu;q=0.7" />
    </item>
    <item
      name="HTTP_COOKIE">
      <value
        string="__test=1; __tawkuuid=e::localhost::EoJKgS7OZqqtNCtys4/Zoral91Cj9dDam/IBNIeLA4Nxvb6YD+9j859qVrgcRwWY::2; ASP.NET_SessionId=wlefqt4u0e0mjmz13hhjaeun; TawkConnectionTime=0" />
    </item>
    <item
      name="HTTP_HOST">
      <value
        string="localhost:60969" />
    </item>
    <item
      name="HTTP_USER_AGENT">
      <value
        string="Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Safari/537.36" />
    </item>
    <item
      name="HTTP_UPGRADE_INSECURE_REQUESTS">
      <value
        string="1" />
    </item>
    <item
      name="HTTP_SEC_FETCH_DEST">
      <value
        string="document" />
    </item>
    <item
      name="HTTP_SEC_FETCH_SITE">
      <value
        string="none" />
    </item>
    <item
      name="HTTP_SEC_FETCH_MODE">
      <value
        string="navigate" />
    </item>
    <item
      name="HTTP_SEC_FETCH_USER">
      <value
        string="?1" />
    </item>
  </serverVariables>
  <cookies>
    <item
      name="__test">
      <value
        string="1" />
    </item>
    <item
      name="__tawkuuid">
      <value
        string="e::localhost::EoJKgS7OZqqtNCtys4/Zoral91Cj9dDam/IBNIeLA4Nxvb6YD+9j859qVrgcRwWY::2" />
    </item>
    <item
      name="ASP.NET_SessionId">
      <value
        string="wlefqt4u0e0mjmz13hhjaeun" />
    </item>
    <item
      name="TawkConnectionTime">
      <value
        string="0" />
    </item>
  </cookies>
</error>')
INSERT [dbo].[ELMAH_Error] ([ErrorId], [Application], [Host], [Type], [Source], [Message], [User], [StatusCode], [TimeUtc], [Sequence], [AllXml]) VALUES (N'15af95b5-c803-42df-9ca4-f65206957ad0', N'/LM/W3SVC/2/ROOT', N'DESKTOP-LS13G7I', N'System.Web.HttpException', N'System.Web.Mvc', N'The controller for path ''/'' was not found or does not implement IController.', N'', 404, CAST(N'2020-03-22T12:37:55.597' AS DateTime), 4, N'<error
  application="/LM/W3SVC/2/ROOT"
  host="DESKTOP-LS13G7I"
  type="System.Web.HttpException"
  message="The controller for path ''/'' was not found or does not implement IController."
  source="System.Web.Mvc"
  detail="System.Web.HttpException (0x80004005): The controller for path ''/'' was not found or does not implement IController.&#xD;&#xA;   at System.Web.Mvc.DefaultControllerFactory.GetControllerInstance(RequestContext requestContext, Type controllerType)&#xD;&#xA;   at System.Web.Mvc.DefaultControllerFactory.CreateController(RequestContext requestContext, String controllerName)&#xD;&#xA;   at System.Web.Mvc.MvcHandler.ProcessRequestInit(HttpContextBase httpContext, IController&amp; controller, IControllerFactory&amp; factory)&#xD;&#xA;   at System.Web.Mvc.MvcHandler.BeginProcessRequest(HttpContextBase httpContext, AsyncCallback callback, Object state)&#xD;&#xA;   at System.Web.Mvc.MvcHandler.BeginProcessRequest(HttpContext httpContext, AsyncCallback callback, Object state)&#xD;&#xA;   at System.Web.Mvc.MvcHandler.System.Web.IHttpAsyncHandler.BeginProcessRequest(HttpContext context, AsyncCallback cb, Object extraData)&#xD;&#xA;   at System.Web.HttpApplication.CallHandlerExecutionStep.System.Web.HttpApplication.IExecutionStep.Execute()&#xD;&#xA;   at System.Web.HttpApplication.ExecuteStepImpl(IExecutionStep step)&#xD;&#xA;   at System.Web.HttpApplication.ExecuteStep(IExecutionStep step, Boolean&amp; completedSynchronously)"
  time="2020-03-22T12:37:55.5978647Z"
  statusCode="404">
  <serverVariables>
    <item
      name="ALL_HTTP">
      <value
        string="HTTP_CONNECTION:keep-alive&#xD;&#xA;HTTP_ACCEPT:text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9&#xD;&#xA;HTTP_ACCEPT_ENCODING:gzip, deflate, br&#xD;&#xA;HTTP_ACCEPT_LANGUAGE:en-US,en;q=0.9,hi;q=0.8,gu;q=0.7&#xD;&#xA;HTTP_COOKIE:__test=1; __tawkuuid=e::localhost::EoJKgS7OZqqtNCtys4/Zoral91Cj9dDam/IBNIeLA4Nxvb6YD+9j859qVrgcRwWY::2; ASP.NET_SessionId=wlefqt4u0e0mjmz13hhjaeun; TawkConnectionTime=0&#xD;&#xA;HTTP_HOST:localhost:60969&#xD;&#xA;HTTP_USER_AGENT:Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Safari/537.36&#xD;&#xA;HTTP_UPGRADE_INSECURE_REQUESTS:1&#xD;&#xA;HTTP_SEC_FETCH_DEST:document&#xD;&#xA;HTTP_SEC_FETCH_SITE:none&#xD;&#xA;HTTP_SEC_FETCH_MODE:navigate&#xD;&#xA;HTTP_SEC_FETCH_USER:?1&#xD;&#xA;" />
    </item>
    <item
      name="ALL_RAW">
      <value
        string="Connection: keep-alive&#xD;&#xA;Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9&#xD;&#xA;Accept-Encoding: gzip, deflate, br&#xD;&#xA;Accept-Language: en-US,en;q=0.9,hi;q=0.8,gu;q=0.7&#xD;&#xA;Cookie: __test=1; __tawkuuid=e::localhost::EoJKgS7OZqqtNCtys4/Zoral91Cj9dDam/IBNIeLA4Nxvb6YD+9j859qVrgcRwWY::2; ASP.NET_SessionId=wlefqt4u0e0mjmz13hhjaeun; TawkConnectionTime=0&#xD;&#xA;Host: localhost:60969&#xD;&#xA;User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Safari/537.36&#xD;&#xA;Upgrade-Insecure-Requests: 1&#xD;&#xA;Sec-Fetch-Dest: document&#xD;&#xA;Sec-Fetch-Site: none&#xD;&#xA;Sec-Fetch-Mode: navigate&#xD;&#xA;Sec-Fetch-User: ?1&#xD;&#xA;" />
    </item>
    <item
      name="APPL_MD_PATH">
      <value
        string="/LM/W3SVC/2/ROOT" />
    </item>
    <item
      name="APPL_PHYSICAL_PATH">
      <value
        string="D:\Today\Today\DevOps\" />
    </item>
    <item
      name="AUTH_TYPE">
      <value
        string="" />
    </item>
    <item
      name="AUTH_USER">
      <value
        string="" />
    </item>
    <item
      name="AUTH_PASSWORD">
      <value
        string="*****" />
    </item>
    <item
      name="LOGON_USER">
      <value
        string="" />
    </item>
    <item
      name="REMOTE_USER">
      <value
        string="" />
    </item>
    <item
      name="CERT_COOKIE">
      <value
        string="" />
    </item>
    <item
      name="CERT_FLAGS">
      <value
        string="" />
    </item>
    <item
      name="CERT_ISSUER">
      <value
        string="" />
    </item>
    <item
      name="CERT_KEYSIZE">
      <value
        string="" />
    </item>
    <item
      name="CERT_SECRETKEYSIZE">
      <value
        string="" />
    </item>
    <item
      name="CERT_SERIALNUMBER">
      <value
        string="" />
    </item>
    <item
      name="CERT_SERVER_ISSUER">
      <value
        string="" />
    </item>
    <item
      name="CERT_SERVER_SUBJECT">
      <value
        string="" />
    </item>
    <item
      name="CERT_SUBJECT">
      <value
        string="" />
    </item>
    <item
      name="CONTENT_LENGTH">
      <value
        string="0" />
    </item>
    <item
      name="CONTENT_TYPE">
      <value
        string="" />
    </item>
    <item
      name="GATEWAY_INTERFACE">
      <value
        string="CGI/1.1" />
    </item>
    <item
      name="HTTPS">
      <value
        string="off" />
    </item>
    <item
      name="HTTPS_KEYSIZE">
      <value
        string="" />
    </item>
    <item
      name="HTTPS_SECRETKEYSIZE">
      <value
        string="" />
    </item>
    <item
      name="HTTPS_SERVER_ISSUER">
      <value
        string="" />
    </item>
    <item
      name="HTTPS_SERVER_SUBJECT">
      <value
        string="" />
    </item>
    <item
      name="INSTANCE_ID">
      <value
        string="2" />
    </item>
    <item
      name="INSTANCE_META_PATH">
      <value
        string="/LM/W3SVC/2" />
    </item>
    <item
      name="LOCAL_ADDR">
      <value
        string="::1" />
    </item>
    <item
      name="PATH_INFO">
      <value
        string="/" />
    </item>
    <item
      name="PATH_TRANSLATED">
      <value
        string="D:\Today\Today\DevOps" />
    </item>
    <item
      name="QUERY_STRING">
      <value
        string="" />
    </item>
    <item
      name="REMOTE_ADDR">
      <value
        string="::1" />
    </item>
    <item
      name="REMOTE_HOST">
      <value
        string="::1" />
    </item>
    <item
      name="REMOTE_PORT">
      <value
        string="56274" />
    </item>
    <item
      name="REQUEST_METHOD">
      <value
        string="GET" />
    </item>
    <item
      name="SCRIPT_NAME">
      <value
        string="/" />
    </item>
    <item
      name="SERVER_NAME">
      <value
        string="localhost" />
    </item>
    <item
      name="SERVER_PORT">
      <value
        string="60969" />
    </item>
    <item
      name="SERVER_PORT_SECURE">
      <value
        string="0" />
    </item>
    <item
      name="SERVER_PROTOCOL">
      <value
        string="HTTP/1.1" />
    </item>
    <item
      name="SERVER_SOFTWARE">
      <value
        string="Microsoft-IIS/10.0" />
    </item>
    <item
      name="URL">
      <value
        string="/" />
    </item>
    <item
      name="HTTP_CONNECTION">
      <value
        string="keep-alive" />
    </item>
    <item
      name="HTTP_ACCEPT">
      <value
        string="text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9" />
    </item>
    <item
      name="HTTP_ACCEPT_ENCODING">
      <value
        string="gzip, deflate, br" />
    </item>
    <item
      name="HTTP_ACCEPT_LANGUAGE">
      <value
        string="en-US,en;q=0.9,hi;q=0.8,gu;q=0.7" />
    </item>
    <item
      name="HTTP_COOKIE">
      <value
        string="__test=1; __tawkuuid=e::localhost::EoJKgS7OZqqtNCtys4/Zoral91Cj9dDam/IBNIeLA4Nxvb6YD+9j859qVrgcRwWY::2; ASP.NET_SessionId=wlefqt4u0e0mjmz13hhjaeun; TawkConnectionTime=0" />
    </item>
    <item
      name="HTTP_HOST">
      <value
        string="localhost:60969" />
    </item>
    <item
      name="HTTP_USER_AGENT">
      <value
        string="Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Safari/537.36" />
    </item>
    <item
      name="HTTP_UPGRADE_INSECURE_REQUESTS">
      <value
        string="1" />
    </item>
    <item
      name="HTTP_SEC_FETCH_DEST">
      <value
        string="document" />
    </item>
    <item
      name="HTTP_SEC_FETCH_SITE">
      <value
        string="none" />
    </item>
    <item
      name="HTTP_SEC_FETCH_MODE">
      <value
        string="navigate" />
    </item>
    <item
      name="HTTP_SEC_FETCH_USER">
      <value
        string="?1" />
    </item>
  </serverVariables>
  <cookies>
    <item
      name="__test">
      <value
        string="1" />
    </item>
    <item
      name="__tawkuuid">
      <value
        string="e::localhost::EoJKgS7OZqqtNCtys4/Zoral91Cj9dDam/IBNIeLA4Nxvb6YD+9j859qVrgcRwWY::2" />
    </item>
    <item
      name="ASP.NET_SessionId">
      <value
        string="wlefqt4u0e0mjmz13hhjaeun" />
    </item>
    <item
      name="TawkConnectionTime">
      <value
        string="0" />
    </item>
  </cookies>
</error>')
INSERT [dbo].[ELMAH_Error] ([ErrorId], [Application], [Host], [Type], [Source], [Message], [User], [StatusCode], [TimeUtc], [Sequence], [AllXml]) VALUES (N'bf646f3b-c436-448f-8837-8563bd31d2d6', N'/LM/W3SVC/2/ROOT', N'DESKTOP-LS13G7I', N'System.Web.HttpException', N'System.Web.Mvc', N'The controller for path ''/'' was not found or does not implement IController.', N'', 404, CAST(N'2020-03-22T12:57:19.723' AS DateTime), 5, N'<error
  application="/LM/W3SVC/2/ROOT"
  host="DESKTOP-LS13G7I"
  type="System.Web.HttpException"
  message="The controller for path ''/'' was not found or does not implement IController."
  source="System.Web.Mvc"
  detail="System.Web.HttpException (0x80004005): The controller for path ''/'' was not found or does not implement IController.&#xD;&#xA;   at System.Web.Mvc.DefaultControllerFactory.GetControllerInstance(RequestContext requestContext, Type controllerType)&#xD;&#xA;   at System.Web.Mvc.DefaultControllerFactory.CreateController(RequestContext requestContext, String controllerName)&#xD;&#xA;   at System.Web.Mvc.MvcHandler.ProcessRequestInit(HttpContextBase httpContext, IController&amp; controller, IControllerFactory&amp; factory)&#xD;&#xA;   at System.Web.Mvc.MvcHandler.BeginProcessRequest(HttpContextBase httpContext, AsyncCallback callback, Object state)&#xD;&#xA;   at System.Web.Mvc.MvcHandler.BeginProcessRequest(HttpContext httpContext, AsyncCallback callback, Object state)&#xD;&#xA;   at System.Web.Mvc.MvcHandler.System.Web.IHttpAsyncHandler.BeginProcessRequest(HttpContext context, AsyncCallback cb, Object extraData)&#xD;&#xA;   at System.Web.HttpApplication.CallHandlerExecutionStep.System.Web.HttpApplication.IExecutionStep.Execute()&#xD;&#xA;   at System.Web.HttpApplication.ExecuteStepImpl(IExecutionStep step)&#xD;&#xA;   at System.Web.HttpApplication.ExecuteStep(IExecutionStep step, Boolean&amp; completedSynchronously)"
  time="2020-03-22T12:57:19.7241822Z"
  statusCode="404">
  <serverVariables>
    <item
      name="ALL_HTTP">
      <value
        string="HTTP_CONNECTION:keep-alive&#xD;&#xA;HTTP_ACCEPT:text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9&#xD;&#xA;HTTP_ACCEPT_ENCODING:gzip, deflate, br&#xD;&#xA;HTTP_ACCEPT_LANGUAGE:en-US,en;q=0.9,hi;q=0.8,gu;q=0.7&#xD;&#xA;HTTP_COOKIE:__test=1; __tawkuuid=e::localhost::EoJKgS7OZqqtNCtys4/Zoral91Cj9dDam/IBNIeLA4Nxvb6YD+9j859qVrgcRwWY::2&#xD;&#xA;HTTP_HOST:localhost:60969&#xD;&#xA;HTTP_USER_AGENT:Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Safari/537.36&#xD;&#xA;HTTP_UPGRADE_INSECURE_REQUESTS:1&#xD;&#xA;HTTP_SEC_FETCH_DEST:document&#xD;&#xA;HTTP_SEC_FETCH_SITE:none&#xD;&#xA;HTTP_SEC_FETCH_MODE:navigate&#xD;&#xA;HTTP_SEC_FETCH_USER:?1&#xD;&#xA;" />
    </item>
    <item
      name="ALL_RAW">
      <value
        string="Connection: keep-alive&#xD;&#xA;Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9&#xD;&#xA;Accept-Encoding: gzip, deflate, br&#xD;&#xA;Accept-Language: en-US,en;q=0.9,hi;q=0.8,gu;q=0.7&#xD;&#xA;Cookie: __test=1; __tawkuuid=e::localhost::EoJKgS7OZqqtNCtys4/Zoral91Cj9dDam/IBNIeLA4Nxvb6YD+9j859qVrgcRwWY::2&#xD;&#xA;Host: localhost:60969&#xD;&#xA;User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Safari/537.36&#xD;&#xA;Upgrade-Insecure-Requests: 1&#xD;&#xA;Sec-Fetch-Dest: document&#xD;&#xA;Sec-Fetch-Site: none&#xD;&#xA;Sec-Fetch-Mode: navigate&#xD;&#xA;Sec-Fetch-User: ?1&#xD;&#xA;" />
    </item>
    <item
      name="APPL_MD_PATH">
      <value
        string="/LM/W3SVC/2/ROOT" />
    </item>
    <item
      name="APPL_PHYSICAL_PATH">
      <value
        string="D:\Today\Today\DevOps\" />
    </item>
    <item
      name="AUTH_TYPE">
      <value
        string="" />
    </item>
    <item
      name="AUTH_USER">
      <value
        string="" />
    </item>
    <item
      name="AUTH_PASSWORD">
      <value
        string="*****" />
    </item>
    <item
      name="LOGON_USER">
      <value
        string="" />
    </item>
    <item
      name="REMOTE_USER">
      <value
        string="" />
    </item>
    <item
      name="CERT_COOKIE">
      <value
        string="" />
    </item>
    <item
      name="CERT_FLAGS">
      <value
        string="" />
    </item>
    <item
      name="CERT_ISSUER">
      <value
        string="" />
    </item>
    <item
      name="CERT_KEYSIZE">
      <value
        string="" />
    </item>
    <item
      name="CERT_SECRETKEYSIZE">
      <value
        string="" />
    </item>
    <item
      name="CERT_SERIALNUMBER">
      <value
        string="" />
    </item>
    <item
      name="CERT_SERVER_ISSUER">
      <value
        string="" />
    </item>
    <item
      name="CERT_SERVER_SUBJECT">
      <value
        string="" />
    </item>
    <item
      name="CERT_SUBJECT">
      <value
        string="" />
    </item>
    <item
      name="CONTENT_LENGTH">
      <value
        string="0" />
    </item>
    <item
      name="CONTENT_TYPE">
      <value
        string="" />
    </item>
    <item
      name="GATEWAY_INTERFACE">
      <value
        string="CGI/1.1" />
    </item>
    <item
      name="HTTPS">
      <value
        string="off" />
    </item>
    <item
      name="HTTPS_KEYSIZE">
      <value
        string="" />
    </item>
    <item
      name="HTTPS_SECRETKEYSIZE">
      <value
        string="" />
    </item>
    <item
      name="HTTPS_SERVER_ISSUER">
      <value
        string="" />
    </item>
    <item
      name="HTTPS_SERVER_SUBJECT">
      <value
        string="" />
    </item>
    <item
      name="INSTANCE_ID">
      <value
        string="2" />
    </item>
    <item
      name="INSTANCE_META_PATH">
      <value
        string="/LM/W3SVC/2" />
    </item>
    <item
      name="LOCAL_ADDR">
      <value
        string="::1" />
    </item>
    <item
      name="PATH_INFO">
      <value
        string="/" />
    </item>
    <item
      name="PATH_TRANSLATED">
      <value
        string="D:\Today\Today\DevOps" />
    </item>
    <item
      name="QUERY_STRING">
      <value
        string="" />
    </item>
    <item
      name="REMOTE_ADDR">
      <value
        string="::1" />
    </item>
    <item
      name="REMOTE_HOST">
      <value
        string="::1" />
    </item>
    <item
      name="REMOTE_PORT">
      <value
        string="60216" />
    </item>
    <item
      name="REQUEST_METHOD">
      <value
        string="GET" />
    </item>
    <item
      name="SCRIPT_NAME">
      <value
        string="/" />
    </item>
    <item
      name="SERVER_NAME">
      <value
        string="localhost" />
    </item>
    <item
      name="SERVER_PORT">
      <value
        string="60969" />
    </item>
    <item
      name="SERVER_PORT_SECURE">
      <value
        string="0" />
    </item>
    <item
      name="SERVER_PROTOCOL">
      <value
        string="HTTP/1.1" />
    </item>
    <item
      name="SERVER_SOFTWARE">
      <value
        string="Microsoft-IIS/10.0" />
    </item>
    <item
      name="URL">
      <value
        string="/" />
    </item>
    <item
      name="HTTP_CONNECTION">
      <value
        string="keep-alive" />
    </item>
    <item
      name="HTTP_ACCEPT">
      <value
        string="text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9" />
    </item>
    <item
      name="HTTP_ACCEPT_ENCODING">
      <value
        string="gzip, deflate, br" />
    </item>
    <item
      name="HTTP_ACCEPT_LANGUAGE">
      <value
        string="en-US,en;q=0.9,hi;q=0.8,gu;q=0.7" />
    </item>
    <item
      name="HTTP_COOKIE">
      <value
        string="__test=1; __tawkuuid=e::localhost::EoJKgS7OZqqtNCtys4/Zoral91Cj9dDam/IBNIeLA4Nxvb6YD+9j859qVrgcRwWY::2" />
    </item>
    <item
      name="HTTP_HOST">
      <value
        string="localhost:60969" />
    </item>
    <item
      name="HTTP_USER_AGENT">
      <value
        string="Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Safari/537.36" />
    </item>
    <item
      name="HTTP_UPGRADE_INSECURE_REQUESTS">
      <value
        string="1" />
    </item>
    <item
      name="HTTP_SEC_FETCH_DEST">
      <value
        string="document" />
    </item>
    <item
      name="HTTP_SEC_FETCH_SITE">
      <value
        string="none" />
    </item>
    <item
      name="HTTP_SEC_FETCH_MODE">
      <value
        string="navigate" />
    </item>
    <item
      name="HTTP_SEC_FETCH_USER">
      <value
        string="?1" />
    </item>
  </serverVariables>
  <cookies>
    <item
      name="__test">
      <value
        string="1" />
    </item>
    <item
      name="__tawkuuid">
      <value
        string="e::localhost::EoJKgS7OZqqtNCtys4/Zoral91Cj9dDam/IBNIeLA4Nxvb6YD+9j859qVrgcRwWY::2" />
    </item>
  </cookies>
</error>')
INSERT [dbo].[ELMAH_Error] ([ErrorId], [Application], [Host], [Type], [Source], [Message], [User], [StatusCode], [TimeUtc], [Sequence], [AllXml]) VALUES (N'5b85d948-112b-424f-9f34-a33136209a49', N'/LM/W3SVC/3/ROOT', N'DESKTOP-LS13G7I', N'System.Web.HttpException', N'System.Web.Mvc', N'The controller for path ''/User/Edit'' was not found or does not implement IController.', N'', 404, CAST(N'2020-03-22T12:58:22.027' AS DateTime), 6, N'<error
  application="/LM/W3SVC/3/ROOT"
  host="DESKTOP-LS13G7I"
  type="System.Web.HttpException"
  message="The controller for path ''/User/Edit'' was not found or does not implement IController."
  source="System.Web.Mvc"
  detail="System.Web.HttpException (0x80004005): The controller for path ''/User/Edit'' was not found or does not implement IController.&#xD;&#xA;   at System.Web.Mvc.DefaultControllerFactory.GetControllerInstance(RequestContext requestContext, Type controllerType)&#xD;&#xA;   at System.Web.Mvc.DefaultControllerFactory.CreateController(RequestContext requestContext, String controllerName)&#xD;&#xA;   at System.Web.Mvc.MvcHandler.ProcessRequestInit(HttpContextBase httpContext, IController&amp; controller, IControllerFactory&amp; factory)&#xD;&#xA;   at System.Web.Mvc.MvcHandler.BeginProcessRequest(HttpContextBase httpContext, AsyncCallback callback, Object state)&#xD;&#xA;   at System.Web.Mvc.MvcHandler.BeginProcessRequest(HttpContext httpContext, AsyncCallback callback, Object state)&#xD;&#xA;   at System.Web.Mvc.MvcHandler.System.Web.IHttpAsyncHandler.BeginProcessRequest(HttpContext context, AsyncCallback cb, Object extraData)&#xD;&#xA;   at System.Web.HttpApplication.CallHandlerExecutionStep.System.Web.HttpApplication.IExecutionStep.Execute()&#xD;&#xA;   at System.Web.HttpApplication.ExecuteStepImpl(IExecutionStep step)&#xD;&#xA;   at System.Web.HttpApplication.ExecuteStep(IExecutionStep step, Boolean&amp; completedSynchronously)"
  time="2020-03-22T12:58:22.0274225Z"
  statusCode="404">
  <serverVariables>
    <item
      name="ALL_HTTP">
      <value
        string="HTTP_CONNECTION:keep-alive&#xD;&#xA;HTTP_ACCEPT:text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9&#xD;&#xA;HTTP_ACCEPT_ENCODING:gzip, deflate, br&#xD;&#xA;HTTP_ACCEPT_LANGUAGE:en-US,en;q=0.9,hi;q=0.8,gu;q=0.7&#xD;&#xA;HTTP_COOKIE:__test=1; __tawkuuid=e::localhost::EoJKgS7OZqqtNCtys4/Zoral91Cj9dDam/IBNIeLA4Nxvb6YD+9j859qVrgcRwWY::2; ASP.NET_SessionId=2noxxccu54keacj30cynacu2; TawkConnectionTime=0&#xD;&#xA;HTTP_HOST:localhost:57996&#xD;&#xA;HTTP_USER_AGENT:Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Safari/537.36&#xD;&#xA;HTTP_UPGRADE_INSECURE_REQUESTS:1&#xD;&#xA;HTTP_SEC_FETCH_DEST:document&#xD;&#xA;HTTP_SEC_FETCH_SITE:none&#xD;&#xA;HTTP_SEC_FETCH_MODE:navigate&#xD;&#xA;HTTP_SEC_FETCH_USER:?1&#xD;&#xA;" />
    </item>
    <item
      name="ALL_RAW">
      <value
        string="Connection: keep-alive&#xD;&#xA;Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9&#xD;&#xA;Accept-Encoding: gzip, deflate, br&#xD;&#xA;Accept-Language: en-US,en;q=0.9,hi;q=0.8,gu;q=0.7&#xD;&#xA;Cookie: __test=1; __tawkuuid=e::localhost::EoJKgS7OZqqtNCtys4/Zoral91Cj9dDam/IBNIeLA4Nxvb6YD+9j859qVrgcRwWY::2; ASP.NET_SessionId=2noxxccu54keacj30cynacu2; TawkConnectionTime=0&#xD;&#xA;Host: localhost:57996&#xD;&#xA;User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Safari/537.36&#xD;&#xA;Upgrade-Insecure-Requests: 1&#xD;&#xA;Sec-Fetch-Dest: document&#xD;&#xA;Sec-Fetch-Site: none&#xD;&#xA;Sec-Fetch-Mode: navigate&#xD;&#xA;Sec-Fetch-User: ?1&#xD;&#xA;" />
    </item>
    <item
      name="APPL_MD_PATH">
      <value
        string="/LM/W3SVC/3/ROOT" />
    </item>
    <item
      name="APPL_PHYSICAL_PATH">
      <value
        string="D:\Today\Today\DevOps.UI\" />
    </item>
    <item
      name="AUTH_TYPE">
      <value
        string="" />
    </item>
    <item
      name="AUTH_USER">
      <value
        string="" />
    </item>
    <item
      name="AUTH_PASSWORD">
      <value
        string="*****" />
    </item>
    <item
      name="LOGON_USER">
      <value
        string="" />
    </item>
    <item
      name="REMOTE_USER">
      <value
        string="" />
    </item>
    <item
      name="CERT_COOKIE">
      <value
        string="" />
    </item>
    <item
      name="CERT_FLAGS">
      <value
        string="" />
    </item>
    <item
      name="CERT_ISSUER">
      <value
        string="" />
    </item>
    <item
      name="CERT_KEYSIZE">
      <value
        string="" />
    </item>
    <item
      name="CERT_SECRETKEYSIZE">
      <value
        string="" />
    </item>
    <item
      name="CERT_SERIALNUMBER">
      <value
        string="" />
    </item>
    <item
      name="CERT_SERVER_ISSUER">
      <value
        string="" />
    </item>
    <item
      name="CERT_SERVER_SUBJECT">
      <value
        string="" />
    </item>
    <item
      name="CERT_SUBJECT">
      <value
        string="" />
    </item>
    <item
      name="CONTENT_LENGTH">
      <value
        string="0" />
    </item>
    <item
      name="CONTENT_TYPE">
      <value
        string="" />
    </item>
    <item
      name="GATEWAY_INTERFACE">
      <value
        string="CGI/1.1" />
    </item>
    <item
      name="HTTPS">
      <value
        string="off" />
    </item>
    <item
      name="HTTPS_KEYSIZE">
      <value
        string="" />
    </item>
    <item
      name="HTTPS_SECRETKEYSIZE">
      <value
        string="" />
    </item>
    <item
      name="HTTPS_SERVER_ISSUER">
      <value
        string="" />
    </item>
    <item
      name="HTTPS_SERVER_SUBJECT">
      <value
        string="" />
    </item>
    <item
      name="INSTANCE_ID">
      <value
        string="3" />
    </item>
    <item
      name="INSTANCE_META_PATH">
      <value
        string="/LM/W3SVC/3" />
    </item>
    <item
      name="LOCAL_ADDR">
      <value
        string="::1" />
    </item>
    <item
      name="PATH_INFO">
      <value
        string="/User/Edit" />
    </item>
    <item
      name="PATH_TRANSLATED">
      <value
        string="D:\Today\Today\DevOps.UI\User\Edit" />
    </item>
    <item
      name="QUERY_STRING">
      <value
        string="" />
    </item>
    <item
      name="REMOTE_ADDR">
      <value
        string="::1" />
    </item>
    <item
      name="REMOTE_HOST">
      <value
        string="::1" />
    </item>
    <item
      name="REMOTE_PORT">
      <value
        string="60223" />
    </item>
    <item
      name="REQUEST_METHOD">
      <value
        string="GET" />
    </item>
    <item
      name="SCRIPT_NAME">
      <value
        string="/User/Edit" />
    </item>
    <item
      name="SERVER_NAME">
      <value
        string="localhost" />
    </item>
    <item
      name="SERVER_PORT">
      <value
        string="57996" />
    </item>
    <item
      name="SERVER_PORT_SECURE">
      <value
        string="0" />
    </item>
    <item
      name="SERVER_PROTOCOL">
      <value
        string="HTTP/1.1" />
    </item>
    <item
      name="SERVER_SOFTWARE">
      <value
        string="Microsoft-IIS/10.0" />
    </item>
    <item
      name="URL">
      <value
        string="/User/Edit" />
    </item>
    <item
      name="HTTP_CONNECTION">
      <value
        string="keep-alive" />
    </item>
    <item
      name="HTTP_ACCEPT">
      <value
        string="text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9" />
    </item>
    <item
      name="HTTP_ACCEPT_ENCODING">
      <value
        string="gzip, deflate, br" />
    </item>
    <item
      name="HTTP_ACCEPT_LANGUAGE">
      <value
        string="en-US,en;q=0.9,hi;q=0.8,gu;q=0.7" />
    </item>
    <item
      name="HTTP_COOKIE">
      <value
        string="__test=1; __tawkuuid=e::localhost::EoJKgS7OZqqtNCtys4/Zoral91Cj9dDam/IBNIeLA4Nxvb6YD+9j859qVrgcRwWY::2; ASP.NET_SessionId=2noxxccu54keacj30cynacu2; TawkConnectionTime=0" />
    </item>
    <item
      name="HTTP_HOST">
      <value
        string="localhost:57996" />
    </item>
    <item
      name="HTTP_USER_AGENT">
      <value
        string="Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Safari/537.36" />
    </item>
    <item
      name="HTTP_UPGRADE_INSECURE_REQUESTS">
      <value
        string="1" />
    </item>
    <item
      name="HTTP_SEC_FETCH_DEST">
      <value
        string="document" />
    </item>
    <item
      name="HTTP_SEC_FETCH_SITE">
      <value
        string="none" />
    </item>
    <item
      name="HTTP_SEC_FETCH_MODE">
      <value
        string="navigate" />
    </item>
    <item
      name="HTTP_SEC_FETCH_USER">
      <value
        string="?1" />
    </item>
  </serverVariables>
  <cookies>
    <item
      name="__test">
      <value
        string="1" />
    </item>
    <item
      name="__tawkuuid">
      <value
        string="e::localhost::EoJKgS7OZqqtNCtys4/Zoral91Cj9dDam/IBNIeLA4Nxvb6YD+9j859qVrgcRwWY::2" />
    </item>
    <item
      name="ASP.NET_SessionId">
      <value
        string="2noxxccu54keacj30cynacu2" />
    </item>
    <item
      name="TawkConnectionTime">
      <value
        string="0" />
    </item>
  </cookies>
</error>')
SET IDENTITY_INSERT [dbo].[ELMAH_Error] OFF
SET IDENTITY_INSERT [dbo].[MainMenu] ON 

INSERT [dbo].[MainMenu] ([MainMenuId], [MainMenuName]) VALUES (1, N'Dashboard')
INSERT [dbo].[MainMenu] ([MainMenuId], [MainMenuName]) VALUES (2, N'Users')
INSERT [dbo].[MainMenu] ([MainMenuId], [MainMenuName]) VALUES (3, N'Organization')
INSERT [dbo].[MainMenu] ([MainMenuId], [MainMenuName]) VALUES (4, N'Projects')
INSERT [dbo].[MainMenu] ([MainMenuId], [MainMenuName]) VALUES (5, N'Settings')
SET IDENTITY_INSERT [dbo].[MainMenu] OFF
SET IDENTITY_INSERT [dbo].[Organisation] ON 

INSERT [dbo].[Organisation] ([OrganisationId], [Name], [Address], [Nationality], [Type]) VALUES (1, N'Gateway', N'Bodakdev, Ahmedabad', N'India', N'MNC')
INSERT [dbo].[Organisation] ([OrganisationId], [Name], [Address], [Nationality], [Type]) VALUES (2, N'Coca Cola', N'4328 Pike Street', N'US', N'MNC')
SET IDENTITY_INSERT [dbo].[Organisation] OFF
SET IDENTITY_INSERT [dbo].[Permissions] ON 

INSERT [dbo].[Permissions] ([PermissionId], [RoleId], [SubMenuId], [Read], [Write]) VALUES (1, 1, 1, 1, 1)
INSERT [dbo].[Permissions] ([PermissionId], [RoleId], [SubMenuId], [Read], [Write]) VALUES (2, 1, 2, 1, 1)
INSERT [dbo].[Permissions] ([PermissionId], [RoleId], [SubMenuId], [Read], [Write]) VALUES (3, 1, 3, 1, 1)
INSERT [dbo].[Permissions] ([PermissionId], [RoleId], [SubMenuId], [Read], [Write]) VALUES (4, 1, 4, 1, 1)
INSERT [dbo].[Permissions] ([PermissionId], [RoleId], [SubMenuId], [Read], [Write]) VALUES (5, 1, 5, 1, 1)
INSERT [dbo].[Permissions] ([PermissionId], [RoleId], [SubMenuId], [Read], [Write]) VALUES (6, 1, 6, 1, 1)
INSERT [dbo].[Permissions] ([PermissionId], [RoleId], [SubMenuId], [Read], [Write]) VALUES (7, 2, 1, 1, 1)
INSERT [dbo].[Permissions] ([PermissionId], [RoleId], [SubMenuId], [Read], [Write]) VALUES (8, 2, 2, 1, 1)
INSERT [dbo].[Permissions] ([PermissionId], [RoleId], [SubMenuId], [Read], [Write]) VALUES (9, 2, 3, 1, 1)
INSERT [dbo].[Permissions] ([PermissionId], [RoleId], [SubMenuId], [Read], [Write]) VALUES (10, 2, 4, 1, 1)
INSERT [dbo].[Permissions] ([PermissionId], [RoleId], [SubMenuId], [Read], [Write]) VALUES (11, 2, 7, 1, 1)
INSERT [dbo].[Permissions] ([PermissionId], [RoleId], [SubMenuId], [Read], [Write]) VALUES (12, 2, 8, 1, 1)
INSERT [dbo].[Permissions] ([PermissionId], [RoleId], [SubMenuId], [Read], [Write]) VALUES (13, 3, 1, 1, 1)
INSERT [dbo].[Permissions] ([PermissionId], [RoleId], [SubMenuId], [Read], [Write]) VALUES (14, 3, 8, 1, 1)
INSERT [dbo].[Permissions] ([PermissionId], [RoleId], [SubMenuId], [Read], [Write]) VALUES (15, 3, 8, 1, 1)
SET IDENTITY_INSERT [dbo].[Permissions] OFF
SET IDENTITY_INSERT [dbo].[Project] ON 

INSERT [dbo].[Project] ([ProjectId], [ProjectName], [SolutionName], [OrganisationId], [SourceURL], [FileFormat], [Status], [Type], [Plateform], [CreatedBy], [LastModifiedBy], [CreatedDate], [LastModifiedDate]) VALUES (2, N'DevOps', N'DevOps', 1, N'github.com/DevOps', N'sln', N'In Progress', N'Web', N'.Net', 2, 2, CAST(N'2020-03-20T14:16:51.227' AS DateTime), CAST(N'2020-03-20T14:27:07.440' AS DateTime))
SET IDENTITY_INSERT [dbo].[Project] OFF
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([RoleId], [RoleName]) VALUES (1, N'Admin')
INSERT [dbo].[Role] ([RoleId], [RoleName]) VALUES (2, N'Release Manager')
INSERT [dbo].[Role] ([RoleId], [RoleName]) VALUES (3, N'User')
SET IDENTITY_INSERT [dbo].[Role] OFF
SET IDENTITY_INSERT [dbo].[SubMenu] ON 

INSERT [dbo].[SubMenu] ([SubMenuId], [MainMenuId], [SubMenuName]) VALUES (1, 1, N'Themes')
INSERT [dbo].[SubMenu] ([SubMenuId], [MainMenuId], [SubMenuName]) VALUES (2, 2, N'Registration')
INSERT [dbo].[SubMenu] ([SubMenuId], [MainMenuId], [SubMenuName]) VALUES (3, 2, N'Users')
INSERT [dbo].[SubMenu] ([SubMenuId], [MainMenuId], [SubMenuName]) VALUES (4, 5, N'Email Notifications')
INSERT [dbo].[SubMenu] ([SubMenuId], [MainMenuId], [SubMenuName]) VALUES (5, 3, N'Registration')
INSERT [dbo].[SubMenu] ([SubMenuId], [MainMenuId], [SubMenuName]) VALUES (6, 3, N'Organization')
INSERT [dbo].[SubMenu] ([SubMenuId], [MainMenuId], [SubMenuName]) VALUES (7, 4, N'Registration')
INSERT [dbo].[SubMenu] ([SubMenuId], [MainMenuId], [SubMenuName]) VALUES (8, 4, N'Projects')
SET IDENTITY_INSERT [dbo].[SubMenu] OFF
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([UserId], [Name], [Address], [Nationality], [Phone], [Email], [Password], [DateOfBirth], [OrganisationId], [RoleId], [RegistrationDate]) VALUES (1, N'Hardik', N'Ahmedabad', N'India', N'9727101146', N'hardik@xyz.com', N'abcd', CAST(N'1999-08-30' AS Date), 1, 1, CAST(N'2020-03-10' AS Date))
INSERT [dbo].[User] ([UserId], [Name], [Address], [Nationality], [Phone], [Email], [Password], [DateOfBirth], [OrganisationId], [RoleId], [RegistrationDate]) VALUES (2, N'Pooja', N'Ahmedabad', N'India', N'9727101446', N'pooja@xyz.com', N'abcd', CAST(N'1999-08-30' AS Date), 1, 2, CAST(N'2020-03-10' AS Date))
INSERT [dbo].[User] ([UserId], [Name], [Address], [Nationality], [Phone], [Email], [Password], [DateOfBirth], [OrganisationId], [RoleId], [RegistrationDate]) VALUES (1003, N'Daksh', N'25, Shantikunj Society,, Vadtal Road, Bakrol,', N'India', N'9876543210', N'daksh@xyz.com', N'abcd', CAST(N'2020-03-01' AS Date), 1, NULL, CAST(N'0001-01-01' AS Date))
INSERT [dbo].[User] ([UserId], [Name], [Address], [Nationality], [Phone], [Email], [Password], [DateOfBirth], [OrganisationId], [RoleId], [RegistrationDate]) VALUES (2002, N'Mit', N'4328 Pike Street', N'India', NULL, N'mit@xyz.com', N'abcd', CAST(N'2020-03-07' AS Date), 1, 3, CAST(N'0001-01-01' AS Date))
INSERT [dbo].[User] ([UserId], [Name], [Address], [Nationality], [Phone], [Email], [Password], [DateOfBirth], [OrganisationId], [RoleId], [RegistrationDate]) VALUES (2003, N'DK', N'4328 Pike Street', N'India', NULL, N'dk@xyz.com', N'abcd', CAST(N'2020-03-08' AS Date), 1, 2, CAST(N'0001-01-01' AS Date))
SET IDENTITY_INSERT [dbo].[User] OFF
/****** Object:  Index [PK_ELMAH_Error]    Script Date: 23-03-2020 09:42:18 ******/
ALTER TABLE [dbo].[ELMAH_Error] ADD  CONSTRAINT [PK_ELMAH_Error] PRIMARY KEY NONCLUSTERED 
(
	[ErrorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_ELMAH_Error_App_Time_Seq]    Script Date: 23-03-2020 09:42:18 ******/
CREATE NONCLUSTERED INDEX [IX_ELMAH_Error_App_Time_Seq] ON [dbo].[ELMAH_Error]
(
	[Application] ASC,
	[TimeUtc] DESC,
	[Sequence] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ELMAH_Error] ADD  CONSTRAINT [DF_ELMAH_Error_ErrorId]  DEFAULT (newid()) FOR [ErrorId]
GO
ALTER TABLE [dbo].[Branch]  WITH CHECK ADD  CONSTRAINT [FK_Project_Branch_ProjectId] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Project] ([ProjectId])
GO
ALTER TABLE [dbo].[Branch] CHECK CONSTRAINT [FK_Project_Branch_ProjectId]
GO
ALTER TABLE [dbo].[BuildProject]  WITH CHECK ADD  CONSTRAINT [FK_Project_BuildProject_ProjectId] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Project] ([ProjectId])
GO
ALTER TABLE [dbo].[BuildProject] CHECK CONSTRAINT [FK_Project_BuildProject_ProjectId]
GO
ALTER TABLE [dbo].[BuildProject]  WITH CHECK ADD  CONSTRAINT [FK_User_BuildProject_BuildBy] FOREIGN KEY([BuildBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[BuildProject] CHECK CONSTRAINT [FK_User_BuildProject_BuildBy]
GO
ALTER TABLE [dbo].[EmailMaster]  WITH CHECK ADD  CONSTRAINT [FK_Organisation_EmailMaster_OrganisationId] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[EmailMaster] CHECK CONSTRAINT [FK_Organisation_EmailMaster_OrganisationId]
GO
ALTER TABLE [dbo].[Permissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([RoleId])
GO
ALTER TABLE [dbo].[Permissions] CHECK CONSTRAINT [FK_Permissions_Role]
GO
ALTER TABLE [dbo].[Permissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_SubMenu] FOREIGN KEY([SubMenuId])
REFERENCES [dbo].[SubMenu] ([SubMenuId])
GO
ALTER TABLE [dbo].[Permissions] CHECK CONSTRAINT [FK_Permissions_SubMenu]
GO
ALTER TABLE [dbo].[Project]  WITH CHECK ADD  CONSTRAINT [FK_Organisation_Poeject_OrganisationId] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[Project] CHECK CONSTRAINT [FK_Organisation_Poeject_OrganisationId]
GO
ALTER TABLE [dbo].[Project]  WITH CHECK ADD  CONSTRAINT [FK_User_Project_CreatedBy] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Project] CHECK CONSTRAINT [FK_User_Project_CreatedBy]
GO
ALTER TABLE [dbo].[Project]  WITH CHECK ADD  CONSTRAINT [FK_User_Project_LastModifiedBy] FOREIGN KEY([LastModifiedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Project] CHECK CONSTRAINT [FK_User_Project_LastModifiedBy]
GO
ALTER TABLE [dbo].[ReleaseProject]  WITH CHECK ADD  CONSTRAINT [FK_ServerBuild_ReleaseProject_ServerBuildId] FOREIGN KEY([ServerBuildId])
REFERENCES [dbo].[ServerBuild] ([ServerBuildId])
GO
ALTER TABLE [dbo].[ReleaseProject] CHECK CONSTRAINT [FK_ServerBuild_ReleaseProject_ServerBuildId]
GO
ALTER TABLE [dbo].[ReleaseProject]  WITH CHECK ADD  CONSTRAINT [FK_User_ReleaseProject_ReleaseBy] FOREIGN KEY([ReleaseBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[ReleaseProject] CHECK CONSTRAINT [FK_User_ReleaseProject_ReleaseBy]
GO
ALTER TABLE [dbo].[ServerBuild]  WITH CHECK ADD  CONSTRAINT [FK_BuildProject_ServerBuild_BuildId] FOREIGN KEY([BuildId])
REFERENCES [dbo].[BuildProject] ([BuildId])
GO
ALTER TABLE [dbo].[ServerBuild] CHECK CONSTRAINT [FK_BuildProject_ServerBuild_BuildId]
GO
ALTER TABLE [dbo].[ServerBuild]  WITH CHECK ADD  CONSTRAINT [FK_ServerConfig_ServerBuild_ServerId] FOREIGN KEY([ServerId])
REFERENCES [dbo].[ServerConfig] ([ServerId])
GO
ALTER TABLE [dbo].[ServerBuild] CHECK CONSTRAINT [FK_ServerConfig_ServerBuild_ServerId]
GO
ALTER TABLE [dbo].[ServerBuild]  WITH CHECK ADD  CONSTRAINT [FK_User_ServerBuild_PublishedBy] FOREIGN KEY([PublishedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[ServerBuild] CHECK CONSTRAINT [FK_User_ServerBuild_PublishedBy]
GO
ALTER TABLE [dbo].[ServerConfig]  WITH CHECK ADD  CONSTRAINT [FK_Organisation_ServerConfig_OrganisationId] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[ServerConfig] CHECK CONSTRAINT [FK_Organisation_ServerConfig_OrganisationId]
GO
ALTER TABLE [dbo].[ServerCredentials]  WITH CHECK ADD  CONSTRAINT [FK_Server_ServerCredentials_ServerId] FOREIGN KEY([ServerId])
REFERENCES [dbo].[ServerConfig] ([ServerId])
GO
ALTER TABLE [dbo].[ServerCredentials] CHECK CONSTRAINT [FK_Server_ServerCredentials_ServerId]
GO
ALTER TABLE [dbo].[SubMenu]  WITH CHECK ADD  CONSTRAINT [FK_MainMenu_SubMneu_MainMenuId] FOREIGN KEY([MainMenuId])
REFERENCES [dbo].[MainMenu] ([MainMenuId])
GO
ALTER TABLE [dbo].[SubMenu] CHECK CONSTRAINT [FK_MainMenu_SubMneu_MainMenuId]
GO
ALTER TABLE [dbo].[SupportTickets]  WITH CHECK ADD  CONSTRAINT [FK_User_SupportTickets_FixedBy] FOREIGN KEY([FixedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[SupportTickets] CHECK CONSTRAINT [FK_User_SupportTickets_FixedBy]
GO
ALTER TABLE [dbo].[SupportTickets]  WITH CHECK ADD  CONSTRAINT [FK_User_SupportTickets_GeneratedBy] FOREIGN KEY([GeneratedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[SupportTickets] CHECK CONSTRAINT [FK_User_SupportTickets_GeneratedBy]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_Organisation_User_OrganisationId] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organisation] ([OrganisationId])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_Organisation_User_OrganisationId]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_Role_User_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([RoleId])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_Role_User_RoleId]
GO
ALTER TABLE [dbo].[UserToken]  WITH CHECK ADD  CONSTRAINT [FK_UserToken_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[UserToken] CHECK CONSTRAINT [FK_UserToken_User]
GO
/****** Object:  StoredProcedure [dbo].[ELMAH_GetErrorsXml]    Script Date: 23-03-2020 09:42:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ELMAH_GetErrorsXml]
(
    @Application NVARCHAR(60),
    @PageIndex INT = 0,
    @PageSize INT = 15,
    @TotalCount INT OUTPUT
)
AS 

    SET NOCOUNT ON

    DECLARE @FirstTimeUTC DATETIME
    DECLARE @FirstSequence INT
    DECLARE @StartRow INT
    DECLARE @StartRowIndex INT

    SELECT 
        @TotalCount = COUNT(1) 
    FROM 
        [ELMAH_Error]
    WHERE 
        [Application] = @Application

    -- Get the ID of the first error for the requested page

    SET @StartRowIndex = @PageIndex * @PageSize + 1

    IF @StartRowIndex <= @TotalCount
    BEGIN

        SET ROWCOUNT @StartRowIndex

        SELECT  
            @FirstTimeUTC = [TimeUtc],
            @FirstSequence = [Sequence]
        FROM 
            [ELMAH_Error]
        WHERE   
            [Application] = @Application
        ORDER BY 
            [TimeUtc] DESC, 
            [Sequence] DESC

    END
    ELSE
    BEGIN

        SET @PageSize = 0

    END

    -- Now set the row count to the requested page size and get
    -- all records below it for the pertaining application.

    SET ROWCOUNT @PageSize

    SELECT 
        errorId     = [ErrorId], 
        application = [Application],
        host        = [Host], 
        type        = [Type],
        source      = [Source],
        message     = [Message],
        [user]      = [User],
        statusCode  = [StatusCode], 
        time        = CONVERT(VARCHAR(50), [TimeUtc], 126) + 'Z'
    FROM 
        [ELMAH_Error] error
    WHERE
        [Application] = @Application
    AND
        [TimeUtc] <= @FirstTimeUTC
    AND 
        [Sequence] <= @FirstSequence
    ORDER BY
        [TimeUtc] DESC, 
        [Sequence] DESC
    FOR
        XML AUTO

GO
/****** Object:  StoredProcedure [dbo].[ELMAH_GetErrorXml]    Script Date: 23-03-2020 09:42:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ELMAH_GetErrorXml]
(
    @Application NVARCHAR(60),
    @ErrorId UNIQUEIDENTIFIER
)
AS

    SET NOCOUNT ON

    SELECT 
        [AllXml]
    FROM 
        [ELMAH_Error]
    WHERE
        [ErrorId] = @ErrorId
    AND
        [Application] = @Application

GO
/****** Object:  StoredProcedure [dbo].[ELMAH_LogError]    Script Date: 23-03-2020 09:42:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ELMAH_LogError]
(
    @ErrorId UNIQUEIDENTIFIER,
    @Application NVARCHAR(60),
    @Host NVARCHAR(30),
    @Type NVARCHAR(100),
    @Source NVARCHAR(60),
    @Message NVARCHAR(500),
    @User NVARCHAR(50),
    @AllXml NTEXT,
    @StatusCode INT,
    @TimeUtc DATETIME
)
AS

    SET NOCOUNT ON

    INSERT
    INTO
        [ELMAH_Error]
        (
            [ErrorId],
            [Application],
            [Host],
            [Type],
            [Source],
            [Message],
            [User],
            [AllXml],
            [StatusCode],
            [TimeUtc]
        )
    VALUES
        (
            @ErrorId,
            @Application,
            @Host,
            @Type,
            @Source,
            @Message,
            @User,
            @AllXml,
            @StatusCode,
            @TimeUtc
        )

GO
USE [master]
GO
ALTER DATABASE [DevOps] SET  READ_WRITE 
GO
