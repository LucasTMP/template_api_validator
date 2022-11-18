using Microsoft.AspNetCore;
using Template.Validator.Api;

namespace Template.Validator.Api;
public class Program
{
    public static void Main(string[] args)
    {
        CreateWebHostBuilder(args).Build().Run();
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
                    WebHost.CreateDefaultBuilder(args)
                           .UseStartup<Startup>();
}