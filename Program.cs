// ThreeLeavesAssort/Program.cs
var builder = WebApplication.CreateBuilder(args);

// �R���g���[���[�ƃr���[��ǉ�
builder.Services.AddControllersWithViews();

var app = builder.Build();

// �{�Ԋ��p�ݒ�
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