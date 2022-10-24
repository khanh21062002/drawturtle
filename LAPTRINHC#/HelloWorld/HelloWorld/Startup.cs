using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HelloWorld
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            /*
                Phương thức ConfigureServices cho phép truy cập đến các dịch vụ, dependency được Inject vào
                Webhost. Hoặc bạn cũng có thể đưa thêm các dependency tại đây.
            */

        }

        // IHostingEnvironment  env cho phép truy cập các biến môi trường, thư mục nguồn, thư mục file.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                // Điểm cuỗi của pipeline khi địa chỉ truy cập là /Abc
                endpoints.Map("/Abc", async context => {
                    await context.Response.WriteAsync("Hello  Abc");
                });

                // Điểm cuỗi của pipeline khi địa chỉ truy cập là /
                endpoints.MapGet("/", async context =>
                {
                    string html = @"
                    <!DOCTYPE html>
                    <html>
                    <head>
                        <meta charset=""UTF-8"">
                        <title>Trang web đầu tiên</title>
                    </head>
                    <body>
                        <p>Đây là trang web đầu tiên</p>
                    </body>
                    </html>
        ";

                    await context.Response.WriteAsync(html);
                });
            });
        }
    }
}