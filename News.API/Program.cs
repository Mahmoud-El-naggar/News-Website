using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using TahliliTask.BL;
using TahliliTask.DAL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Dependencies
builder.Services.AddScoped<INewsRepo, NewsRepo>();
builder.Services.AddScoped<IAuthorsRepo, AuthorsRepo>();
builder.Services.AddScoped<INewsManager, NewsManager>();
builder.Services.AddScoped<IAuthorsManager, AuthorsManager>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserManager, UserManager>();
#endregion

#region DataBase
var connectionString = builder.Configuration.GetConnectionString("News_DB");
builder.Services.AddDbContext<NewsContext>(options => options.UseSqlServer(connectionString));
#endregion

#region Identity

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
	options.Password.RequireUppercase = false;
	options.Password.RequireLowercase = false;
	options.Password.RequireDigit = false;
	options.Password.RequireNonAlphanumeric = false;
	options.Password.RequiredLength = 3;

})
	.AddEntityFrameworkStores<NewsContext>();

#endregion

#region Authentication

builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = "AuthSchema";
	options.DefaultChallengeScheme = "AuthSchema";
})
	.AddJwtBearer("AuthSchema", options =>
	{
		var key = builder.Configuration.GetSection("SecretKey").Value;
		var keyByteArr = Encoding.ASCII.GetBytes(key!);
		var securityKey = new SymmetricSecurityKey(keyByteArr);
		options.TokenValidationParameters = new TokenValidationParameters
		{
			IssuerSigningKey = securityKey,
			ValidateIssuer = false,
			ValidateAudience = false
		};
	});

#endregion

#region Authorization
builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("Admin", policy =>
	{
		policy.RequireClaim(ClaimTypes.Role, Constants.Role);
		policy.RequireClaim(ClaimTypes.NameIdentifier);
	});
});

#endregion


#region CORS
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAll", builder =>
	{
		builder
		.AllowAnyOrigin()
		.AllowAnyHeader()
		.AllowAnyMethod();
	});
});
#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
