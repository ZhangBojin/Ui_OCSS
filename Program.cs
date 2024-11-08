using Blazored.LocalStorage;
using Ui_OCSS.Components;
using Ui_OCSS.Components.Layout;
using Ui_OCSS.Infrastructure.HeartbeatService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddAntDesign();

builder.Services.AddHttpClient();

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddSingleton<MenuService>();

#region ����
builder.Services.AddSingleton<HeartbeatService>();
builder.Services.AddHostedService<HeartbeatService>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.MapStaticAssets();

app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
