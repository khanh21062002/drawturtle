var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://0.0.0.0:5005");
// Add services to the container.
builder.Services.AddRazorPages();

var testoptions = builder.Configuration.GetSection("TestOptions");
var services = builder.Services;
/* ============================================================
     Copy code cũ trong Startup.ConfigureServices vào đây, ví dụ
   =========================================================== */
services.AddControllersWithViews();
services.AddDistributedMemoryCache();
services.AddSession(cfg => {
    cfg.Cookie.Name = "HaDuyKhanh";
    cfg.IdleTimeout = new TimeSpan(0, 40, 0);
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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
