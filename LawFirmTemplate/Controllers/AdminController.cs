using LawFirmTemplate.Data;
using LawFirmTemplate.Data.Entities;
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

                TempData["Info"] = "Bilgiler Güncellendi.";
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
            TempData["Info"] = "Resim Kaldırıldı.";
            return RedirectToAction("EditFirm");
         }



        #endregion

        #region ClientSays

        public async Task<IActionResult> ListClientSays()
        {
            var clientSays = await _context.ClientSays.ToListAsync();

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
                TempData["Info"] = "Müşteri Yorumu Silindi";
                return RedirectToAction("ListClientSays");
            }

            return RedirectToAction("ListClientSays");
        }

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
