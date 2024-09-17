using Business_Layer.BusinessLayerRepository;
using Business_Layer.MapperProfile;
using Data_Access_Layer.Context;
using Data_Access_Layer.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Shared_Layer.Interfaces;
using System;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<ExpenseDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


//builder.Services.AddDbContext<ExpenseDbContext>(options =>
//options.UseSqlServer(
//           builder.Configuration.GetConnectionString("DefaultConnection"),
//           sqlOptions => sqlOptions.EnableRetryOnFailure(
//               maxRetryCount: 5, // Number of retries
//               maxRetryDelay: TimeSpan.FromSeconds(10), // Delay between retries
//               errorNumbersToAdd: null // You can add custom SQL error codes here
//           )
//       )
//  );
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(x =>

{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "localhost",
        ValidAudience = "localhost",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["jwtConfig:Key"])),
        ClockSkew = TimeSpan.Zero

    };


});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IUserRepoBL, UserRepoBL>();
builder.Services.AddScoped<IExpenseGroupBL,ExpenseGroupRepoBL>();
builder.Services.AddScoped<IExpenseGroupRepo, ExpenseGroupRepo>();
builder.Services.AddScoped<IExpenseRepo,ExpensesRepo>();
builder.Services.AddScoped<IExpenseBL,ExpenseRepoBL>(); 
builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));
builder.Services.AddSwaggerGen();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ExpenseDbContext>();
    dbContext.Database.Migrate();
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
