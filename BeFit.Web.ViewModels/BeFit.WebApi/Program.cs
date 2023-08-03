using BeFit.Data;
using BeFit.Services.Data.Interfaces;
using BeFit.Web.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace BeFit.WebApi
{
	public class Program
	{
		public static void Main(string[] args)
		{
			WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
			string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

			builder.Services.AddDbContext<BeFitDbContext>(opt => opt.UseSqlServer(connectionString));

			builder.Services.AddApplicationServices(typeof(IEventService));

			builder.Services.AddControllers();

			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.AddCors(setup =>
			{
				setup.AddPolicy("BeFit", policyBuilder =>
				{
					policyBuilder.WithOrigins("https://localhost:7239")
						.AllowAnyHeader()
						.AllowAnyMethod();
				});
			});

			WebApplication app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();

			app.MapControllers();

			app.UseCors("BeFit");

			app.Run();
		}
	}
}