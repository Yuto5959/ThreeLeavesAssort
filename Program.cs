// ThreeLeavesAssort/Program.cs
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Data.Sqlite;

var builder = WebApplication.CreateBuilder(args);

// �f�[�^�x�[�X������
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

// �R���g���[���[�ƃr���[��ǉ�
builder.Services.AddControllersWithViews();
builder.Services.AddSession();

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

// �J�����ł̂�Top��ʂ��X�^�[�g�A�b�v�ɐݒ�
if (app.Environment.IsDevelopment())
{
    app.MapGet("/", async context =>
    {
        context.Response.Redirect("/Home/Top"); // Top�R���g���[���[��Index�A�N�V�����Ƀ��_�C���N�g
        await Task.CompletedTask;
    });
}

app.Run();