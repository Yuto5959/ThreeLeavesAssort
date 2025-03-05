// ThreeLeavesAssort/Controllers/HomeController.cs
using Microsoft.AspNetCore.Mvc;
using ThreeLeavesAssort.Models;

namespace ThreeLeavesAssort.Controllers
{
    public class HomeController : Controller
    {
        // インデックス画面
        public IActionResult Index()
        {
            return View();
        }

        // 認証処理
        [HttpPost]
        public IActionResult Authenticate(AuthModel model)
        {
            // 環境変数からパスコードを取得
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

        public IActionResult Favorites()
        {
            // お気に入り10選のデータ（仮にリストを返す）
            var favorites = new List<string> { "お気に入り1", "お気に入り2" }; // 仮データ
            return View(favorites);
        }

        [HttpPost]
        public IActionResult SetPenName(string penName)
        {
            // ペンネームを保存（仮にセッションに保存）
            HttpContext.Session.SetString("PenName", penName);
            return RedirectToAction("Top");
        }
    }
}