using Microsoft.EntityFrameworkCore;
using TrendyolApiWithEntityFremowork.Data;
using TrendyolApiWithEntityFremowork.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("TrendyolApiEntegrationDatabase"));
    //options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient<TrendyolApiService>();
builder.Services.AddScoped<DatabaseService>();

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
