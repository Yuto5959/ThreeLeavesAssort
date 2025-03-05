// ThreeLeavesAssort/Program.cs
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

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();