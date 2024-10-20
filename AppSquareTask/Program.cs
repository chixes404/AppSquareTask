using AppSquareTask.Data.Models;
using AppSquareTask.Infrastracture.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using AppSquareTask.Application;
using AppSquareTask.Core;
using AppSquareTask.Infrastracture;
using AppSquareTask.Core.Middlewares;
using M = AppSquareTask.Data.Models;
using AppSquareTask.Application.Helper;

using AppSquareTask.Infrastracture.Configuration;
using AppSquareTask.Infrastracture.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
	?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// Register DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(connectionString));

// Configure Identity
builder.Services.AddIdentity<ApplicationUser, M.Role>(options => options.SignIn.RequireConfirmedAccount = false)
	.AddRoles<M.Role>()
	.AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>("AppSquareTask")
	.AddEntityFrameworkStores<ApplicationDbContext>()
	.AddDefaultTokenProviders();

builder.Services.Configure<JWT>(builder.Configuration.GetSection("Jwt"));

// Configure Authentication with JWT Bearer
builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(jwt =>
{
	var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);
	jwt.SaveToken = false;
	jwt.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuerSigningKey = true,
		IssuerSigningKey = new SymmetricSecurityKey(key),
		ValidateIssuer = true,
		ValidIssuer = builder.Configuration["Jwt:Issuer"],
		ValidateAudience = true,
		ValidAudience = builder.Configuration["Jwt:Audience"],
		RequireExpirationTime = true,
		ValidateLifetime = true,
		ClockSkew = TimeSpan.Zero
	};
});

builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("Owner", policy =>
		policy.RequireClaim("Role", "Owner")); 
});

builder.Services.AddControllers();

void RegisterApplicationServices(IServiceCollection services)
{
	services
		.AddServiceDependencies()
		.AddCoreDependencies()
		.AddInfrastructureDependencies();
}

// Configure Swagger
builder.Services.AddSwaggerGen(options =>
{
	options.SwaggerDoc("v1", new OpenApiInfo { Title = "App Square API", Version = "v1" });
	options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
	{
		Name = "Authorization",
		In = ParameterLocation.Header,
		Type = SecuritySchemeType.ApiKey,
		Scheme = JwtBearerDefaults.AuthenticationScheme
	});

	options.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = JwtBearerDefaults.AuthenticationScheme
				},
				Scheme = "Oauth2",
				Name = JwtBearerDefaults.AuthenticationScheme,
				In = ParameterLocation.Header,
			},
			new List<string>()
		}
	});
});

builder.Services.AddEndpointsApiExplorer();
RegisterApplicationServices(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

	app.UseSwagger();
	app.UseSwaggerUI(c =>
	{
		string swaggerJsonBasePath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
		c.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v1/swagger.json", "App Square API");
	});
}

app.MapHub<NotificationHub>("/notificationHub");
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
	endpoints.MapControllers(); 
	endpoints.MapHub<NotificationHub>("/notificationHub");

});

app.Run();