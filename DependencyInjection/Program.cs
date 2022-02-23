using DependencyInjection.Data;
using DependencyInjection.Data.ServiceScope;
using DependencyInjection.Data.ServiceWithInterface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<SingletonService>();
builder.Services.AddScoped<ScopedService>();
builder.Services.AddTransient<TransientService>();
builder.Services.AddTransient<IServiceInterface, ServiceWithInterface>();
builder.Services.AddTransient<ServiceWithCustomData>(serviceProvider => new ("Blazor School"));
builder.Services.AddTransient<DependentService>(serviceProvider => new(serviceProvider.GetRequiredService<ServiceWithCustomData>()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();