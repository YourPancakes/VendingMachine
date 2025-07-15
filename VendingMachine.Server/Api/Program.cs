using FluentValidation;
using Microsoft.EntityFrameworkCore;
using VendingMachine.Api.DependencyInjection;
using VendingMachine.Api.Middlewares;
using VendingMachine.Application.Mappings;
using VendingMachine.Application.Validators;
using VendingMachine.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost3000",
        policy => policy
            .WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
    );
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1.0", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Vending Machine API",
        Version = "v1.0",
        Description = "API for managing vending machine operations"
    });
});

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddValidatorsFromAssembly(typeof(CreateProductDtoValidator).Assembly, ServiceLifetime.Scoped);

builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseExceptionHandling();
app.UseRequestLogging();

app.UseCors("AllowLocalhost3000");

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.Migrate();
    await DataSeeder.SeedAsync(context);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1.0/swagger.json", "Vending Machine API v1.0");
    });
}

app.MapControllers();

app.Run();
