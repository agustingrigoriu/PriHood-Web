using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.AspNetCore.Session;
using PriHood.Auth;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace PriHood
{
  public class Startup
  {
    public Startup(IHostingEnvironment env)
    {
      var builder = new ConfigurationBuilder()
          .SetBasePath(env.ContentRootPath)
          .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
          .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
          .AddEnvironmentVariables();
      Configuration = builder.Build();
    }

    public IConfigurationRoot Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddCors();

      // Add framework services.
      services.AddMvc().AddJsonOptions(options =>
            {
              options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
              options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
      services.AddDistributedMemoryCache();
      services.AddSession(options =>
      {
        options.CookieHttpOnly = true;
      });
      services.AddDbContext<Models.PrihoodContext>(options => options.UseMySql(@"server=localhost;database=Prihood;user=root;password=root"));
      services.AddSingleton<AuthService>();
      services.AddSingleton<PushService>();
      services.AddSingleton<EmailService>();
      services.AddSingleton<UploadService>();
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
    {
      loggerFactory.AddConsole(Configuration.GetSection("Logging"));
      loggerFactory.AddDebug();

      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseBrowserLink();
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
      }

      app.UseCors(builder => builder
          .AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader()
          .AllowCredentials());

      app.UseStaticFiles();
      app.UseSession();
      app.UseMiddleware<ApiMiddleware>();


      app.UseMvc(routes =>
      {
        routes.MapRoute(
                  name: "panel",
                  template: "panel/{*page}",
                  defaults: new { controller = "Panel", action = "Index" });
        routes.MapRoute(
                  name: "default",
                  template: "{controller=Home}/{action=Index}/{id?}");
      });
    }
  }
}
