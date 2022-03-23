using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlatformServices.Dtos;

namespace PlatformServices.AsyncDataServices
{
    public interface IMessageBusClient
    {
        void PublishNewPlatform(PlatformPublishedDto platformPublishedDto);
    }
}