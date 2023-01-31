using Microsoft.EntityFrameworkCore;
using Theo_Doi_Chi_Phi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//DI
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection")));

//Register Syncfusion license
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mgo+DSMBaFt/QHRqVVhkX1pFdEBBXHxAd1p/VWJYdVt5flBPcDwsT3RfQF5jSH9RdERhXH5Xc3VVQQ==;Mgo+DSMBPh8sVXJ0S0J+XE9AdVRDX3xKf0x/TGpQb19xflBPallYVBYiSV9jS31Td0RhW35cdXVcRWJbVg==;ORg4AjUWIQA/Gnt2VVhkQlFaclxJXGFWfVJpTGpQdk5xdV9DaVZUTWY/P1ZhSXxQdkRiW31fcnFUT2NZV0M=;OTMxODY4QDMyMzAyZTM0MmUzMFd4cFZkUWQ1Y3dkWVNpckY4K1EweERGcGN3NEJhVTZOWUxTTWZ4UCt2MUE9;OTMxODY5QDMyMzAyZTM0MmUzMGJvS0hGcTlEanRYWU1Wdm1xMFJYZlhLMHJ2TU41Nmt6ZUhLeGpGcWd6SE09;NRAiBiAaIQQuGjN/V0Z+WE9EaFtBVmJLYVB3WmpQdldgdVRMZVVbQX9PIiBoS35RdUViWH1ccXZQR2lZUUZx;OTMxODcxQDMyMzAyZTM0MmUzMGd4TEZGSW1HdnRLampER2RWVkljcjkzYlpmUnBUMys5S3FiUElsQllWTTg9;OTMxODcyQDMyMzAyZTM0MmUzME9haEh5Y05NbXdFcWVwM0gydUhpbktBbjZPeFFSbll3bGdVZ0t2c2dyYUU9;Mgo+DSMBMAY9C3t2VVhkQlFaclxJXGFWfVJpTGpQdk5xdV9DaVZUTWY/P1ZhSXxQdkRiW31fcnFUT2VVVkM=;OTMxODc0QDMyMzAyZTM0MmUzMElSYVhPVWJ3MlUzbHdudVZTLzI4S1F0RjRBeXh6QzdHcmVuL25qMGcyUWc9;OTMxODc1QDMyMzAyZTM0MmUzMENCREhic0ZNQm5qVVJkRGJrOEdhTVhuUjhkZjU0RVFBeVVnaHh2NU8zWk09;OTMxODc2QDMyMzAyZTM0MmUzMGd4TEZGSW1HdnRLampER2RWVkljcjkzYlpmUnBUMys5S3FiUElsQllWTTg9");

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
