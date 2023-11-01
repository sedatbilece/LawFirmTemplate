using LawFirmTemplate.Data;
using LawFirmTemplate.Data.Entities;
using LawFirmTemplate.Data.Enums;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
                   options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(x =>
                {
                    x.LoginPath = "/User/Login";
                });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var context = serviceProvider.GetRequiredService<AppDbContext>();

    // User tablosunda herhangi bir kullanýcý olup olmadýðýný kontrol et
    if (!context.Users.Any())
    {

        var newUser = new User
        {
            UserName = "Admin",
            Password = "Password123*",
            RoleType = RoleType.Admin,
            Status = Status.Active ,
            ImageUrl = "initImageUrl",
            DisplayName = "initDisplayName",
            Title = "initTitle",
            PracticeArea = "initPracticeArea",
            PhoneNumber = "initPhoneNumber",
            Mail = "initMail",
            Social1 = "initSocial1",
            Social2 = "initSocial2",
            Social3 = "initSocial3",
            Order =1
        };
        context.Users.Add(newUser);
        context.SaveChanges();
    }
    if (!context.Firms.Any())
    {

var newFirm = new Firm{
    Name = "initName",
    Description = "initDescription",
    PhoneNumber = "initPhoneNumber",
    Mail = "initMail",
    ImageUrl = "initImageUrl",
    AddressLine1 = "initAddressLine1",
    AddressLine2 = "initAddressLine2",
    AddressLine3 = "initAddressLine3",
    Social1 = "initSocial1",
    Social2 = "initSocial2",
    Social3 = "initSocial3",
    Social4 = "initSocial4",
    Social5 = "initSocial5"
};
        context.Firms.Add(newFirm);
        context.SaveChanges();
    }
}




app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
