using Microsoft.OpenApi.Models;
using PruebaTecnicaCycle.Application.DI;
using PruebaTecnicaCycle.Identity.DI;
using PruebaTecnicaCycle.Infraestructure.DI;
using PruebaTecnicaCycle.Infraestructure.Mapping;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    //titulo del diseño
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Cycle", Version = "v1" });

    //boton Autorize
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Jwt Autorization",
        Name = "Autorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference= new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

//CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("Policy", options =>
    {
        options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

builder.Services.AddApplicationServices();
builder.Services.AddInfraestructureService(builder.Configuration);
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
builder.Services.AddIdentityService(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseCors("Policy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();