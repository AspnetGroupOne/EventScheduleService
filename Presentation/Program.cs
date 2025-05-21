using Application.Contexts;
using Application.Data.Repositories;
using Application.External.Interfaces;
using Application.External.Models;
using Application.External.Services;
using Application.Interfaces;
using Application.Internal.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("AspDB")));

builder.Services.Configure<EventSettings>(builder.Configuration.GetSection("EventApi"));

builder.Services.AddHttpClient<IEventIdValidationService, EventIdValidationService>();

builder.Services.AddScoped<IScheduleRepository, ScheduleRepository>();
builder.Services.AddScoped<IScheduleService, ScheduleService>();


var app = builder.Build();

app.MapOpenApi();
app.UseHttpsRedirection();

//Must come before controllers.
app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseAuthorization();
app.MapControllers();

app.Run();
