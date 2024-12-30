using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Sample.Server.WebAuthenticator
{
	public class Startup
	{
		public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
		{
			Configuration = configuration;
			WebHostEnvironment = webHostEnvironment;
		}

		IConfiguration Configuration { get; }

		IWebHostEnvironment WebHostEnvironment { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();

			services.AddAuthentication(o =>
				{
					o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
				})
				.AddCookie()
				.AddFacebook(fb =>
				{
					fb.AppId = "567607782721743";
					fb.AppSecret = "a7eeb74285e20f2614f706a38b0dfc3f";
					fb.SaveTokens = true;
				});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
