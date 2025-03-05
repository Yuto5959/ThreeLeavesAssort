// ThreeLeavesAssort/Program.cs
using Microsoft.AspNetCore.StaticFiles;

var builder = WebApplication.CreateBuilder(args);

// コントローラーとビューを追加
builder.Services.AddControllersWithViews();

var app = builder.Build();

// 本番環境用設定
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

//画像用設定
var provider = new FileExtensionContentTypeProvider();
provider.Mappings[".jpg"] = "image/jpeg";
provider.Mappings[".png"] = "image/png";
app.UseStaticFiles(new StaticFileOptions
{
    ContentTypeProvider = provider
});

//文字コード対応
app.Use(async (context, next) =>
{
    context.Response.Headers["Content-Type"] = "text/html; charset=UTF-8";
    await next();
});

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();