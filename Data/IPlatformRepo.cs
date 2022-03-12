using System.Collections.Generic;
using PlatformServices.Models;

namespace PlatformServices.Data
{
    public interface IPlatformRepo
    {
        bool SaveChanges();

        IEnumerable<Platform> GetallPlatforms();
        Platform GetPlatformById(int id);
        void CreatePlatform(Platform plat);
    } 
}