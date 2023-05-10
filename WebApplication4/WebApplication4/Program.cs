using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using WebApplication4.DB_Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers( options =>
                                            {
                                                var jsonInputFormatter = options.InputFormatters
                                                    .OfType<Microsoft.AspNetCore.Mvc.Formatters.SystemTextJsonInputFormatter>()
                                                    .Single();
                                                jsonInputFormatter.SupportedMediaTypes.Add("application/csp-report");
                                            });
builder.Services.AddMvc();
builder.Services.AddDbContext<UsersContext>(options => options.UseNpgsql("Host=localhost;Port=5432;Database=Users;Username=postgres;Password=penek2281337"));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseStaticFiles();

app.MapControllers();

app.Run();
