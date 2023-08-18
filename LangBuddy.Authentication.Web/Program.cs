using LangBuddy.Authentication.Service;
using LangBuddy.Authentication.Service.Options;

var builder = WebApplication.CreateBuilder(args);


builder.Services.Configure<ApiConnections>(u => builder.Configuration.GetSection("ApiConnections").Bind(u));
builder.Services.Configure<JwtConfiguration>(u => builder.Configuration.GetSection("JwtConfiguration").Bind(u));
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddServices(builder.Configuration);

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
