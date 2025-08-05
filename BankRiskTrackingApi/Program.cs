using BankRiskTracking.Business.Services;
using BankRiskTracking.DataAccess;
using BankRiskTracking.DataAccess.Repository;
using BankRiskTracking.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/*builder.Services.AddDbContext<ApiDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
*/
builder.Services.AddScoped<ICustomerService, CustomerService>();


builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
//Servis implamantasyonlari
//AddTransient :Her istekte yeni bir instance olusturur
//AddScoped:Her istekte bir instance olusturur ve istegin sonunda yok eder
builder.Services.AddDbContext<DatabaseConnection>();
builder.Services.AddScoped<ICustomerNoteService, CustomerNoteService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped <IRiskHistoryService, RiskHistoryService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
