using Microsoft.AspNetCore.Mvc;
using ThreeLeavesAssort.Models;
using Microsoft.Data.Sqlite;
using System;

namespace ThreeLeavesAssort.Controllers
{
    public class HomeController : Controller
    {
        private readonly string _connectionString = "Data Source=scrap.db";

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Authenticate(AuthModel model)
        {
            string validPasscode = Environment.GetEnvironmentVariable("PASSCODE");

            if (model.Passcode == validPasscode)
            {
                return RedirectToAction("Top");
            }

            ViewBag.Error = "認証コードが正しくありません";
            return View("Index");
        }

        public IActionResult Top()
        {
            return View();
        }

        public IActionResult Terms()
        {
            return View();
        }

        public IActionResult Source()
        {
            return View();
        }

        public IActionResult AboutMe()
        {
            return View();
        }

        public IActionResult Scrap()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegisterScrap([FromBody] ScrapData data)
        {
            try
            {
                using (var connection = new SqliteConnection(_connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = @"
                        INSERT INTO Scraps (Id, Text, Length, Font, FgColor, BgColor, IsVertical)
                        VALUES (@Id, @Text, @Length, @Font, @FgColor, @BgColor, @IsVertical)";
                    command.Parameters.AddWithValue("@Id", data.Id);
                    command.Parameters.AddWithValue("@Text", data.Text);
                    command.Parameters.AddWithValue("@Length", data.Length);
                    command.Parameters.AddWithValue("@Font", data.Font);
                    command.Parameters.AddWithValue("@FgColor", data.FgColor);
                    command.Parameters.AddWithValue("@BgColor", data.BgColor);
                    command.Parameters.AddWithValue("@IsVertical", data.IsVertical ? 1 : 0);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Json(new { success = false });
            }
        }

        public class ScrapData
        {
            public string Id { get; set; }
            public string Text { get; set; }
            public int Length { get; set; }
            public string Font { get; set; }
            public string FgColor { get; set; }
            public string BgColor { get; set; }
            public bool IsVertical { get; set; }
        }

        public IActionResult Favorites()
        {
            var favorites = new List<string> { "お気に入り1", "お気に入り2" };
            return View(favorites);
        }

        [HttpPost]
        public IActionResult SetPenName(string penName)
        {
            HttpContext.Session.SetString("PenName", penName);
            return RedirectToAction("Top");
        }
    }
}