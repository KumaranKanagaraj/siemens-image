using System;
using ImageAccessor.DbContexts;
using ImageAccessor.Repositories;
using ImageAccessor.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ImageAccessor
{
	public class Startup
	{
		private readonly IConfiguration _configuration;

		public Startup(IHostEnvironment env)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.AddEnvironmentVariables();

			_configuration = builder.Build();
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddCors();
			services.AddControllers();

			services.AddEntityFrameworkNpgsql().AddDbContext<ImageAccessorContext>(options =>
			{
				var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
				string connStr;
				// Depending on if in development or production, use either Heroku-provided
				// connection string, or development connection string from env var.
				if (env == "Development")
				{
					// Use connection string from file.
					connStr = _configuration.GetConnectionString("local");
				}
				else
				{
					// Use connection string provided at runtime by Heroku.
					var connUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
					// Parse connection URL to connection string for Npgsql
					connUrl = connUrl.Replace("postgres://", string.Empty);
					var pgUserPass = connUrl.Split("@")[0];
					var pgHostPortDb = connUrl.Split("@")[1];
					var pgHostPort = pgHostPortDb.Split("/")[0];
					var pgDb = pgHostPortDb.Split("/")[1];
					var pgUser = pgUserPass.Split(":")[0];
					var pgPass = pgUserPass.Split(":")[1];
					var pgHost = pgHostPort.Split(":")[0];
					var pgPort = pgHostPort.Split(":")[1];
					connStr = $"Server={pgHost};Port={pgPort};User Id={pgUser};Password={pgPass};Database={pgDb}";
				}
				// Whether the connection string came from the local development configuration file
				// or from the environment variable from Heroku, use it to set up your DbContext.
				options.UseNpgsql(connStr);
			});

			services.AddTransient<ImageAccessorContext>();
			services.AddTransient<IImageService, ImageService>();
			services.AddTransient<IImageRepository, ImageRepository>();

		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
			}

			app.UseCors(o => o
			.AllowAnyHeader()
			.AllowAnyMethod()
			.AllowAnyOrigin());

			app.UseRouting();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
