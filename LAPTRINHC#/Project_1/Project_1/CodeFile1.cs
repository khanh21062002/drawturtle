using System.Text.Json;     // using phải sử dụng ở đầu file
/*
    Đây là các namespace tự động nạp (có sẵn mà không cần using)
    Nếu muốn bỏ tự động nạp hãy sửa (file .csproj):
        <ImplicitUsings>enable</ImplicitUsings> thành
        <ImplicitUsings>disable</ImplicitUsings>

    using System;
    using System.IO;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
*/

// Sau phần using là các top-level statements
Console.WriteLine("Hello, World!");
XYZ.Abc.TestAbc();

// Có thể truy cập lấy tham số truyền cho Main qua biến
// có sẵn args
Console.WriteLine("Số thám số: " + args.Count());

// Có thể viết mệnh đề return, code thoát chương trình.s
return 1;


// Phần cuối của file có thể khai báo các lớp
namespace XYZ
{
    class Abc
    {
        public static void TestAbc() { }
    }
}