// ThreeLeavesAssort/Program.cs
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Data.Sqlite;

var builder = WebApplication.CreateBuilder(args);

// データベース初期化
using (var connection = new SqliteConnection("Data Source=scrap.db"))
{
    connection.Open();
    var command = connection.CreateCommand();
    command.CommandText = @"
        CREATE TABLE IF NOT EXISTS Scraps (
            Id TEXT PRIMARY KEY,
            Text TEXT NOT NULL,
            Length INTEGER NOT NULL,
            Font TEXT NOT NULL,
            FgColor TEXT NOT NULL,
            BgColor TEXT NOT NULL,
            IsVertical INTEGER NOT NULL
        )";
    command.ExecuteNonQuery();
    connection.Close();
}

// コントローラーとビューを追加
builder.Services.AddControllersWithViews();
builder.Services.AddSession();

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

// 開発環境でのみTop画面をスタートアップに設定
if (app.Environment.IsDevelopment())
{
    app.MapGet("/", async context =>
    {
        context.Response.Redirect("/Home/Top"); // TopコントローラーのIndexアクションにリダイレクト
        await Task.CompletedTask;
    });
}

app.Run();