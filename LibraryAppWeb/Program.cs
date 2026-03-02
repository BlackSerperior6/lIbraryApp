using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using LibraryAppWeb.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

DataBaseConnectionFactory.Init();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

// В .NET 8.0 используем UseStaticFiles вместо MapStaticAssets
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// В .NET 8.0 Razor Pages не используют WithStaticAssets
app.MapRazorPages();

app.Run();