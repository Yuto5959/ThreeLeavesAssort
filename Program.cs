// ThreeLeavesAssort/Program.cs
using Microsoft.AspNetCore.StaticFiles;

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

//�摜�p�ݒ�
var provider = new FileExtensionContentTypeProvider();
provider.Mappings[".jpg"] = "image/jpeg";
provider.Mappings[".png"] = "image/png";
app.UseStaticFiles(new StaticFileOptions
{
    ContentTypeProvider = provider
});

//�����R�[�h�Ή�
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