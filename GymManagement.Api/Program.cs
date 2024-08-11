using GymManagement.Infrastructure;
using GymManagement.Application;
using GymManagement.Api;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
    .AddPresentation()
    .AddApplication()
    .AddInfrastructure(builder.Configuration);
}
// Add services to the container.

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.AddInfrastructureMiddleware();
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
