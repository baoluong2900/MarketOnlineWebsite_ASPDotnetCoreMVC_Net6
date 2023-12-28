﻿using AspNetCoreHero.ToastNotification;
using MarketOnlineWebsite.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;
using System.Text.Unicode;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddNotyf(config => { config.DurationInSeconds = 3; config.IsDismissable = true; config.Position = NotyfPosition.TopRight; });
builder.Services.AddMvc(option => option.EnableEndpointRouting = false);

#region Cấu hình login google
var configuration = builder.Configuration;

//builder.Services
//	   .AddAuthentication(options =>
//	   {
//		   options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//		   options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
//	   })
//	   .AddCookie()
//	   .AddGoogle(options =>
//	   {
//		   options.ClientId = configuration["Authentication:Google:ClientId"];
//		   options.ClientSecret = configuration["Authentication:Google:ClientSecret"];
//	   });
//builder.Services.AddAuthentication()
//				 .AddGoogle(options =>
//				 {
//					 options.ClientId = configuration["App:GoogleClientId"];
//					 options.ClientSecret = configuration["App:GoogleClientSecret"];
//				 });



#endregion

#region Cấu hình session 
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.AddAuthentication(options =>
{
	options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
	.AddCookie( p=> 
    {
       p.LoginPath = "/dang-nhap.html";
       p.LogoutPath = "/Admin/dang-nhap-user.html";
       p.AccessDeniedPath = "/";
    })
	.AddGoogle(options =>
	{
		options.ClientId = configuration["Authentication:Google:ClientId"];
		options.ClientSecret = configuration["Authentication:Google:ClientSecret"];
	});


builder.Services.AddControllersWithViews()
					.AddSessionStateTempDataProvider();

#endregion

#region Cấu hình sercices sql sever

builder.Services.AddDbContext<dbMarketsContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("dbMarketOnlineWebsite"));
});

#endregion

#region Cấu hình Services nhận font tiếng việt

builder.Services.AddSingleton<HtmlEncoder>( HtmlEncoder.Create(allowedRanges: new[] { UnicodeRanges.All}));

#endregion

#region Cấu hình HTTP
builder.Services.AddHttpContextAccessor();
#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

#region Tạo sever riêng cho trang admin

app.MapControllerRoute(
    name: "Admin",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);

#endregion

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
