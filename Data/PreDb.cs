using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PlatformServices.Models;

namespace PlatformServices.Data
{
    public static class PreDb
    {
        public static void PrePopulation(IApplicationBuilder app)
        {
            using(var servicesScope = app.ApplicationServices.CreateScope())
            {
                SeedData(servicesScope.ServiceProvider.GetService<AppDbContext>());
            }
        }

        private static void SeedData(AppDbContext context)
        {
           if(!context.Platforms.Any())
           {
               Console.WriteLine("--> Seeding Data...");

               context.Platforms.AddRange(
                   new Platform() {Name="Dot Net", Publisher="Microsoft", Cost="Free"},
                   new Platform() {Name="Sql Server Express", Publisher="Microsoft", Cost="Free"},
                   new Platform() {Name="Kubernetes", Publisher="Cloud Native Computing Foundation", Cost="Free"}
               );

               context.SaveChanges();
           }
           else
           {
               Console.WriteLine("--> we already have data");
           }
        }
    }
}