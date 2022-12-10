using ContractorsHub.Areas.Administration.Contracts;
using ContractorsHub.Areas.Administration.Service;
using ContractorsHub.Core.Contracts;
using ContractorsHub.Infrastructure.Data;
using ContractorsHub.Infrastructure.Data.Common;
using ContractorsHub.Infrastructure.Data.Models;
using ContractorsHub.ModelBinders;
using ContractorsHub.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContractorsHub.Areas.Admin.Service;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<User>(options => {
    options.SignIn.RequireConfirmedAccount = false;
    options.SignIn.RequireConfirmedEmail = false;
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 4;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/User/Login";
    options.LogoutPath = "/User/Logout";
    //options.AccessDeniedPath = "";
});

builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<IJobService, JobService>();
builder.Services.AddScoped<IOfferService, OfferService>();
builder.Services.AddScoped<IToolService, ToolService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IJobAdministrationService, JobAdministrationService>();
builder.Services.AddScoped<IContractorService, ContractorService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<StatisticAdministrationService>();

builder.Services.AddControllersWithViews().AddMvcOptions(options =>
{
    options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
    options.ModelBinderProviders.Insert(0, new DecimalModelBinderProvider());
});

builder.Services.AddResponseCaching();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    //app.UseDeveloperExceptionPage();
    //    app.UseExceptionHandler("/Home/Error"); 

}
else
{
    app.UseExceptionHandler("/Home/Error"); 
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );

    endpoints.MapControllerRoute(
      name: "default",
      pattern: "{controller=Home}/{action=Index}/{id?}"
        );
});

app.MapRazorPages();

app.Run();
