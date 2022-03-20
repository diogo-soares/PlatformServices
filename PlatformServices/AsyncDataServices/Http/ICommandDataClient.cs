using System.Threading.Tasks;
using PlatformServices.Dtos;

namespace PlatformServices.SyncDataServices.Http
{
    public interface ICommandDataClient
    {
        Task SendPlatformToCommand(PlatformReadDto plat);
    }
}