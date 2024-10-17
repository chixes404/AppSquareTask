using AppSquareTask.Core.Models;
using AppSquareTask.Infrastracture.Configuration;
using AppSquareTask.Infrastracture.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using M = AppSquareTask.Core.Models;

using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using AppSquareTask.Application.IServices;
using AppSquareTask.Application.Services;
using AppSquareTask.Core.IRepositories;
using AppSquareTask.Infrastracture.Repositories;
using AppSquareTask.Infrastracture.Hubs;
using AppSquareTask.Middlewares;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using System.Reflection;
using AppSquareTask.Infrastracture.Helper;
using AppSquareTask.Application.Responses;
using FluentValidation.AspNetCore;
using MediatR;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Loader;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// Register DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(connectionString));

	



// Configure Identity
builder.Services.AddIdentity<ApplicationUser, M.Role>(options => options.SignIn.RequireConfirmedAccount = false)
		.AddRoles<M.Role>()
		.AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>("OfferxProject")
		.AddEntityFrameworkStores<ApplicationDbContext>()
		.AddDefaultTokenProviders();

// Configure JWT settings from appsettings.json
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



builder.Services.AddControllers().
	AddJsonOptions(options =>
	{
		options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
	});// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


builder.Services.AddSignalR();
builder.Services.AddMediatR(config => config.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

builder.Services.AddSwaggerGen(options =>
{
	options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "AppSquare API", Version = "v1" });
	options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new Microsoft.OpenApi.Models.OpenApiSecurityScheme
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

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<ApiResponseHandler>();
builder.Services.AddScoped<JwtTokenGenerator>(); 
builder.Services.AddScoped<IOwnerService, OwnerService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddFluentValidationAutoValidation()
	.AddFluentValidationClientsideAdapters()
	.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

// Register ValidationBehavior
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));


builder.Services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));

// Register OwnerService
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());




builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAll",
		b => b.AllowAnyMethod()
	.AllowAnyHeader()
	.AllowAnyOrigin());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(c =>
	{
		string swaggerJsonBasePath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
		c.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v1/swagger.json", "AppSquare API");
	}); ;
}
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
