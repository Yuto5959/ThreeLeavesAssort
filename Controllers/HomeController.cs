// ThreeLeavesAssort/Controllers/HomeController.cs
using Microsoft.AspNetCore.Mvc;
using ThreeLeavesAssort.Models;

namespace ThreeLeavesAssort.Controllers
{
    public class HomeController : Controller
    {
        // �C���f�b�N�X���
        public IActionResult Index()
        {
            return View();
        }

        // �F�؏���
        [HttpPost]
        public IActionResult Authenticate(AuthModel model)
        {
            // ���ϐ�����p�X�R�[�h���擾
            string validPasscode = Environment.GetEnvironmentVariable("PASSCODE");

            if (model.Passcode == validPasscode)
            {
                return RedirectToAction("Top");
            }

            ViewBag.Error = "�p�X�R�[�h������������܂���";
            return View("Index");
        }

        // �g�b�v���
        public IActionResult Top()
        {
            return View();
        }
    }
}