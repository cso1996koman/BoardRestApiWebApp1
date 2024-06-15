using System;
using System.Net;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Config;
using NLog.Targets;
using NLog.Web;
using Sentry;

namespace RestApiProject
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            #region Sentry/NLog
            WebRequest.DefaultWebProxy = new WebProxy("http://127.0.0.1:8118", true) { UseDefaultCredentials = true };
            var logger = LogManager.GetCurrentClassLogger();
            #endregion
            try
            {
                logger.Debug("init main");
                await CreateHostBuilder(args).Build().RunAsync();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Stopped program because of exception");
                throw;
            }
            finally
            {
                LogManager.Flush();
                LogManager.Shutdown();
            }
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureLogging(options => options.ClearProviders())
                .UseNLog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        private static void UsingCodeConfiguration()
        {
            var config = new LoggingConfiguration();
            config.AddSentry(options =>
            {
                options.Layout = "${message}";
                options.BreadcrumbLayout = "${logger}: ${message}"; 
                options.MinimumBreadcrumbLevel = NLog.LogLevel.Debug; 
                options.MinimumEventLevel = NLog.LogLevel.Error; 
                options.Dsn = "https://a48f67497c814561aca2c66fa5ee37fc:a5af1a051d6f4f09bdd82472d5c2629d@sentry.io/1340240";
                options.AttachStacktrace = true;
                options.SendDefaultPii = true;
                options.IncludeEventDataOnBreadcrumbs = true;
                options.ShutdownTimeoutSeconds = 5;
                options.AddTag("logger", "${logger}"); 
                options.HttpProxy = new WebProxy("http://127.0.0.1:8118", true) { UseDefaultCredentials = true };
            });
            config.AddTarget(new DebuggerTarget("Debugger"));
            config.AddTarget(new ColoredConsoleTarget("Console"));
            config.AddRuleForAllLevels("Console");
            config.AddRuleForAllLevels("Debugger");
            LogManager.Configuration = config;
        }
    }
}