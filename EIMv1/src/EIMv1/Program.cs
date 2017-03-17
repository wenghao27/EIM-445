using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using EIMv1.Encryption;

namespace EIMv1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            byte[] keys = RSAService.RSAKeyGeneration();
            AESService.Test();
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
