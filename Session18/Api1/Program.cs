using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Server.Kestrel.Https;

namespace Api1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel(options =>
                        options.ConfigureHttpsDefaults(httpOptions =>
                        {
                            httpOptions.AllowAnyClientCertificate();
                            httpOptions.ClientCertificateMode = ClientCertificateMode.AllowCertificate;
                            httpOptions.CheckCertificateRevocation = false;
                        }));
                    webBuilder.UseStartup<Startup>();
                });
    }
}
