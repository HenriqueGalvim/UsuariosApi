using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using UsuariosApi.Authorization;
using UsuariosApi.Data;
using UsuariosApi.Models;
using UsuariosApi.Services;

var builder = WebApplication.CreateBuilder(args);
var  AllowedOrigin = "allowedOrigin";

builder.Services.AddCors(option =>
                   {
                       option.AddPolicy("allowedOrigin",
                           builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
                           );
                   });

// Add services to the container.
builder.Services.AddDbContext<UsuarioDbContext>(opts => opts.UseNpgsql(builder.Configuration.GetConnectionString("UsuarioConnection")));

builder.Services.AddIdentity<Usuario, IdentityRole>()
	.AddEntityFrameworkStores<UsuarioDbContext>()
	.AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme =
		JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
	options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
	{
		ValidateIssuerSigningKey = true,
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["SymmetricSecurityKey"])),
		ValidateAudience = false,
		ValidateIssuer = false,
		ClockSkew = TimeSpan.Zero
	};
});

builder.Services.AddSingleton<IAuthorizationHandler,IdadeAuthorization>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("IdadeMinima", policy =>
		 policy.AddRequirements(new IdadeMinima(18))
	);
});

builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<TokenService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<TokenService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
app.UseCors(AllowedOrigin);
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
