
using AgendaWeb.Infra.Data.Interfaces;
using AgendaWeb.Infra.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Configurando o projeto para MVC
builder.Services.AddControllersWithViews();
//captura a connectionstring mapeada no appsettings.json
var connectionString = builder.Configuration.GetConnectionString("AgendaWeb");

//Envia a connectionstring para a classe EventoRepository
builder.Services.AddTransient<IEventoRepository>
(map => new EventoRepository(connectionString));

//Envia a connectionstring para a classe IHistoricoRepository
builder.Services.AddTransient<IHistoricoRepository>
(map => new HistoricoRepository(connectionString));

builder.Services.AddRazorPages();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Definindo a página inicial do projeto
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}"
    );
app.MapRazorPages();
app.Run();
