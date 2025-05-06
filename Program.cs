using Lab08_AlonsoSahuanay.Controllers;
using Lab08_AlonsoSahuanay.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configuración de la base de datos
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<Lab08DbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Configuración de Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Laboratorio 08 - Alonso Sahuanay",
        Version = "v1",
        Description = "Ejercicios de LINQ con Swagger"
    });
    
    // Opcional: Ordenar los ejercicios
    c.DocumentFilter<OrderEndpointsDocumentFilter>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Lab08 API V1");
    c.RoutePrefix = "swagger"; // Esto hace que Swagger esté en /swagger
});

// Redirección automática a Swagger
app.MapWhen(context => context.Request.Path == "/", appBuilder =>
{
    appBuilder.Run(async context =>
    {
        context.Response.Redirect("/swagger/index.html");
        await Task.CompletedTask;
    });
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();