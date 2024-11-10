using Microsoft.EntityFrameworkCore;
using Reservas.Data;  // Altere para o namespace correto do seu DbContext

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configurar o DbContext com a string de conexão
builder.Services.AddDbContext<ReservasContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"))); // Usando "DbConnection" que você definiu no appsettings.json

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
