using LawFirmTemplate.Data;
using LawFirmTemplate.Data.Entities;
using LawFirmTemplate.Models;
using LawFirmTemplate.Models.Home;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LawFirmTemplate.Controllers
{
    public class HomeController : Controller
    {

        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()//test
        {
            var model = new HomeIndexModel()
            {
                clientSays = _context.ClientSays.OrderBy(x => x.Order).ToList(),
                teams = _context.Users.OrderBy(x => x.Order).Where(x => x.RoleType == Data.Enums.RoleType.Normal).ToList(),
                firm = _context.Firms.FirstOrDefault(),
                practiceAreas = _context.PracticeAreas.ToList(),
            };
            
            return View(model);
        }


        [HttpPost]
        public IActionResult AddQuestion(HomeIndexModel model)
        {

            var SaveModel = new Contact()
            {
                Name = model.QuestionArea.Name,
                Mail = model.QuestionArea.Mail,
                Phone = model.QuestionArea.Phone,
                Subject = model.QuestionArea.Subject,
                Message = model.QuestionArea.Message,
                Area = "",
            };

            _context.Contacts.Add(SaveModel);
            _context.SaveChanges();

            TempData["Message"] = "Mesajınız Kaydedildi.";
            return RedirectToAction("Index");
        }

       
    }
}