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

            ViewBag.Error = "パスコードが正しくありません";
            return View("Index");
        }

        // トップ画面
        public IActionResult Top()
        {
            return View();
        }
    }
}