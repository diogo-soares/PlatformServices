using System.Threading.Tasks;
using PlatformServices.Dtos;

namespace PlatformServices.SyncDataServices.Http
{
    public interface ICommandDataCliente
    {
        Task SendPlatformToCommand(PlatformReadDto plat);
    }
}