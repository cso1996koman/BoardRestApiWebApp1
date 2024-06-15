using Common;
using Common.Utilities;
using ElmahCore.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
namespace WebFramework.Swagger
{
    public static class SwaggerConfigurationExtensions
    {
        public static void AddSwagger(this IServiceCollection services)
        {
            Assert.NotNull(services, nameof(services));
            #region AddSwaggerExamples
            var mainAssembly = Assembly.GetEntryAssembly(); 
            var mainType = mainAssembly.GetExportedTypes()[0];
            const string methodName = nameof(Swashbuckle.AspNetCore.Filters.ServiceCollectionExtensions.AddSwaggerExamplesFromAssemblyOf);
            MethodInfo method = typeof(Swashbuckle.AspNetCore.Filters.ServiceCollectionExtensions).GetRuntimeMethods().FirstOrDefault(x => x.Name == methodName && x.IsGenericMethod);
            MethodInfo generic = method.MakeGenericMethod(mainType);
            generic.Invoke(null, new[] { services });
            #endregion
            services.AddSwaggerGen(options =>
            {
                var xmlDocPath = Path.Combine(AppContext.BaseDirectory, "RestApiProject.xml");
                options.IncludeXmlComments(xmlDocPath, true);
                options.EnableAnnotations();
                #region DescribeAllEnumsAsStrings
                options.SwaggerDoc("v1", new OpenApiInfo { Version = "v1", Title = "API V1" });
                options.SwaggerDoc("v2", new OpenApiInfo { Version = "v2", Title = "API V2" });
                #region Filters
                options.ExampleFilters();
                options.OperationFilter<ApplySummariesOperationFilter>();
                #region Add UnAuthorized to Response
                options.OperationFilter<UnauthorizedResponsesOperationFilter>(true, "OAuth2");
                #endregion            
                options.AddSecurityDefinition("OAuth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Password = new OpenApiOAuthFlow
                        {
                            TokenUrl = new Uri("/api/v1/users/Token", UriKind.Relative),

                        }
                    }
                });
                #endregion
                #region Versioning
                options.OperationFilter<RemoveVersionParameters>();
                options.DocumentFilter<SetVersionInPaths>();
                options.DocInclusionPredicate((docName, apiDesc) =>
                {
                    if (!apiDesc.TryGetMethodInfo(out MethodInfo methodInfo)) return false;

                    var versions = methodInfo.DeclaringType
                        .GetCustomAttributes<ApiVersionAttribute>(true)
                        .SelectMany(attr => attr.Versions);
                    return versions.Any(v => $"v{v}" == docName);
                });
                #endregion
                #endregion
            });
        }

        public static IApplicationBuilder UseElmahCore(this IApplicationBuilder app, SiteSettings siteSettings)
        {
            Assert.NotNull(app, nameof(app));
            Assert.NotNull(siteSettings, nameof(siteSettings));
            app.UseWhen(context => context.Request.Path.StartsWithSegments(siteSettings.ElmahPath, StringComparison.OrdinalIgnoreCase), appBuilder =>
            {
                appBuilder.Use((ctx, next) =>
                {
                    ctx.Features.Get<IHttpBodyControlFeature>().AllowSynchronousIO = true;
                    return next();
                });
            });
            app.UseElmah();
            return app;
        }
        public static IApplicationBuilder UseSwaggerAndUI(this IApplicationBuilder app)
        {
            Assert.NotNull(app, nameof(app));
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "V1 Docs");
                options.SwaggerEndpoint("/swagger/v2/swagger.json", "V2 Docs");

                #region Customizing
                options.DocExpansion(DocExpansion.None);
                #endregion
            });
            app.UseReDoc(options =>
            {
                options.SpecUrl("/swagger/v1/swagger.json");
                #region Customizing
                options.EnableUntrustedSpec();
                options.ScrollYOffset(10);
                options.HideHostname();
                options.HideDownloadButton();
                options.ExpandResponses("200,201");
                options.RequiredPropsFirst();
                options.NoAutoAuth();
                options.PathInMiddlePanel();
                options.HideLoading();
                options.NativeScrollbars();
                options.DisableSearch();
                options.OnlyRequiredInSamples();
                options.SortPropsAlphabetically();
                #endregion
            });
            return app;
        }
    }
}
