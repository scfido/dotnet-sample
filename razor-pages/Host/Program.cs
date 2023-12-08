using System.Reflection;
using Dotnet.Samples.RazorPages;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var mvcBuilder = builder.Services.AddRazorPages();
mvcBuilder.PartManager.ApplicationParts.Add(new AssemblyPart(typeof(Page1Model).Assembly));
mvcBuilder.AddRazorRuntimeCompilation(options =>
{
    // 获取当前进程所在的路径
    var libPath = Path.GetFullPath("../../../../Lib", Path.GetDirectoryName(Environment.ProcessPath) ?? "") ?? throw new Exception("lib path is null");
    Console.WriteLine("Lib path: " + libPath);
    options.FileProviders.Add(new PhysicalFileProvider(libPath));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
