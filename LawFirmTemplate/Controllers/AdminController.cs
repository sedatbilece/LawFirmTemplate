using LawFirmTemplate.Data;
using LawFirmTemplate.Data.Entities;
using LawFirmTemplate.Data.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LawFirmTemplate.Controllers
{

    [Authorize]
    public class AdminController : Controller
    {

        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region Firm

        public async Task<IActionResult> EditFirm()
        {
            var model = await _context.Firms.FirstOrDefaultAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditFirm(Firm model, IFormFile ImageFile)
        {

            
                var firm = await _context.Firms.FindAsync(model.Id);
              
                if (firm != null)
                {
                    firm.Name = model.Name;
                    firm.Description = model.Description; 
                    firm.PhoneNumber = model.PhoneNumber;
                    firm.Mail = model.Mail;
                    firm.AddressLine1 = model.AddressLine1;
                    firm.AddressLine2 = model.AddressLine2;
                    firm.AddressLine3 = model.AddressLine3;
                    firm.Social1 = model.Social1;
                    firm.Social2 = model.Social2;
                    firm.Social3 = model.Social3;
                    firm.Social4 = model.Social4;
                    firm.Social5 = model.Social5;

                    string imageUrl = await SaveImageAsync(ImageFile,firm.ImageUrl);

                    if (imageUrl != null)
                    {
                        firm.ImageUrl = imageUrl;
                    }


                _context.Firms.Update(firm);
                    await _context.SaveChangesAsync();

                }
            

            return View(firm);
        }


        public async Task<IActionResult> DeleteFirmImage(int Id)
        {
            var firm = await _context.Firms.FindAsync(Id);

            if (firm != null)
            {

                //Delete Image
                DeleteItemsImage(firm.ImageUrl);


                //Set null Image
                firm.ImageUrl = "";
                _context.Firms.Update(firm);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("EditFirm");
         }



        #endregion

        #region ClientSays

        public async Task<IActionResult> ListClientSays()
        {
            var clientSays = await _context.ClientSays.OrderBy(x=>x.Order).ToListAsync();

            return View(clientSays);
        }

        public async Task<IActionResult> AddClientSays()
        {

            return View(new ClientSays() { });
        }

        [HttpPost]
        public async Task<IActionResult> AddClientSays(ClientSays model, IFormFile ImageFile)
        {
            var client = new ClientSays();
   
                client.Name = model.Name;
                client.Title = model.Title;
                client.Description = model.Description;
                client.Order = model.Order;
                
                string imageUrl = await SaveImageAsync(ImageFile, client.ImageUrl);

            if (imageUrl != null)
            {
                client.ImageUrl = imageUrl;
            }
            else
            {
                client.ImageUrl = "";
            }

                _context.ClientSays.Add(client);
                await _context.SaveChangesAsync();


            return RedirectToAction("ListClientSays");
        }

        public async Task<IActionResult> DeleteClientSays(int Id)
        {
            var client =await _context.ClientSays.FindAsync(Id);

            if (client != null)
            {
                DeleteItemsImage(client.ImageUrl);
                _context.ClientSays.Remove(client);
                await _context.SaveChangesAsync();
                return RedirectToAction("ListClientSays");
            }

            return RedirectToAction("ListClientSays");
        }


        public async Task<IActionResult> EditClientSays(int Id)
        {
            var client = await _context.ClientSays.FindAsync(Id);
            if(client != null)
            {
                return View(client);
            }
            return RedirectToAction("Index");
           
        }

        [HttpPost]
        public async Task<IActionResult> EditClientSays(ClientSays model, IFormFile ImageFile)
        {


            var client = await _context.ClientSays.FindAsync(model.Id);

            if (client != null)
            {
                client.Name = model.Name;
                client.Title = model.Title;
                client.Description = model.Description;
                client.Order = model.Order;
               
                string imageUrl = await SaveImageAsync(ImageFile, client.ImageUrl);

                if (imageUrl != null)
                {
                    client.ImageUrl = imageUrl;
                }
                _context.ClientSays.Update(client);
                await _context.SaveChangesAsync();

            }
            return RedirectToAction("ListClientSays");

        }

        #endregion

        #region PracticeAreas

        public async Task<IActionResult> ListPracticeArea()
        {
            var practiceAreas = await _context.PracticeAreas.OrderBy(x => x.Order).ToListAsync();

            return View(practiceAreas);
        }


        public async Task<IActionResult> AddPracticeArea()
        {

            return View(new PracticeArea() { });
        }

        [HttpPost]
        public async Task<IActionResult> AddPracticeArea(PracticeArea model, IFormFile ImageFile)
        {
            var practiceArea = new PracticeArea();


            practiceArea.Title = model.Title;
            practiceArea.Description = model.Description;
            practiceArea.Order = model.Order;

            string imageUrl = await SaveImageAsync(ImageFile, practiceArea.ImageUrl);

            if (imageUrl != null)
            {
                practiceArea.ImageUrl = imageUrl;
            }
            else
            {
                practiceArea.ImageUrl = "";
            }

            _context.PracticeAreas.Add(practiceArea);
            await _context.SaveChangesAsync();


            return RedirectToAction("ListPracticeArea");
        }

        public async Task<IActionResult> EditPracticeArea(int Id)
        {
            var practice = await _context.PracticeAreas.FindAsync(Id);
            if (practice != null)
            {
                return View(practice);
            }
            return RedirectToAction("Index");

        }

        [HttpPost]
        public async Task<IActionResult> EditPracticeArea(PracticeArea model, IFormFile ImageFile)
        {


            var practiceArea = await _context.PracticeAreas.FindAsync(model.Id);

            if (practiceArea != null)
            {
                practiceArea.Title = model.Title;
                practiceArea.Description = model.Description;
                practiceArea.Order = model.Order;

                string imageUrl = await SaveImageAsync(ImageFile, practiceArea.ImageUrl);

                if (imageUrl != null)
                {
                    practiceArea.ImageUrl = imageUrl;
                }
                _context.PracticeAreas.Update(practiceArea);
                await _context.SaveChangesAsync();

            }
            return RedirectToAction("ListPracticeArea");

        }


        public async Task<IActionResult> DeletePracticeArea(int Id)
        {
            var practiceArea = await _context.PracticeAreas.FindAsync(Id);

            if (practiceArea != null)
            {
                DeleteItemsImage(practiceArea.ImageUrl);
                _context.PracticeAreas.Remove(practiceArea);
                await _context.SaveChangesAsync();     
            }
            return RedirectToAction("ListPracticeArea");
        }
        #endregion

        #region Users

        public async Task<IActionResult> ListUsers()
        {
            var users = await _context.Users.OrderBy(x => x.Order).Where(x=>x.RoleType==Data.Enums.RoleType.Normal).ToListAsync();

            return View(users);
        }

        public async Task<IActionResult> AddUser()
        {
            return View(new User { });
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(User model, IFormFile ImageFile)
        {

            var user = new User();


            user.DisplayName = model.DisplayName;
            user.Title= model.Title;
            user.PhoneNumber = model.PhoneNumber;
            user.Mail=model.Mail;
            user.PracticeArea = model.PracticeArea;
            user.Social1 = model.Social1;
            user.Social2 = model.Social2;
            user.Social3 = model.Social3;
            user.Title = model.Title;
            user.Order = model.Order;
            user.RoleType = RoleType.Normal;
            user.Status = Status.Active;
            user.UserName = "";
            user.Password = "";

            string imageUrl = await SaveImageAsync(ImageFile, user.ImageUrl);

            if (imageUrl != null)
            {
                user.ImageUrl = imageUrl;
            }
            else
            {
                user.ImageUrl = "";
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();


            return RedirectToAction("ListUsers");

        }

        public async Task<IActionResult> EditUser(int Id)
        {
            var user = await _context.Users.Where(x => x.RoleType != RoleType.Admin && x.Id == Id).FirstOrDefaultAsync();
            if (user != null)
            {
                return View(user);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(User model, IFormFile ImageFile)
        {

            var user = await _context.Users.Where(x => x.RoleType != RoleType.Admin && x.Id == model.Id).FirstOrDefaultAsync();

            if(user != null)
            {
                user.DisplayName = model.DisplayName;
                user.Title = model.Title;
                user.PhoneNumber = model.PhoneNumber;
                user.Mail = model.Mail;
                user.PracticeArea = model.PracticeArea;
                user.Social1 = model.Social1;
                user.Social2 = model.Social2;
                user.Social3 = model.Social3;
                user.Title = model.Title;
                user.Order = model.Order;
                user.RoleType = RoleType.Normal;
                user.Status = Status.Active;
                user.UserName = "";
                user.Password = "";

                string imageUrl = await SaveImageAsync(ImageFile, user.ImageUrl);

                if (imageUrl != null)
                {
                    user.ImageUrl = imageUrl;
                }

                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
           
            return RedirectToAction("ListUsers");
        }

        public async Task<IActionResult> DeleteUser(int Id)
        {
            var user = await _context.Users.Where(x => x.RoleType != RoleType.Admin && x.Id == Id).FirstOrDefaultAsync();

            if (user != null)
            {
                DeleteItemsImage(user.ImageUrl);
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("ListUsers");
        }

        #endregion

        #region Contacts

        #endregion

        #region Shared Methods
        private async Task<string> SaveImageAsync(IFormFile imageFile, string oldImageUrl)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                // Generate a unique file name for the new image
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                string filePath = Path.Combine("wwwroot/images", uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                // Delete the old image if it exists
                if (!string.IsNullOrEmpty(oldImageUrl))
                {
                    string oldImagePath = Path.Combine("wwwroot", oldImageUrl.TrimStart('/'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                return "/images/" + uniqueFileName;
            }

            return oldImageUrl; // Return the old image URL if no new image was provided
        }

        private void  DeleteItemsImage(string path)
        {
            string oldImagePath = Path.Combine("wwwroot", path.TrimStart('/'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
        }
        #endregion


    }
}
