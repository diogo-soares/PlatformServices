using System.Text.Json;
using AutoMapper;
using CommandsService.Data;
using CommandsService.Dtos;
using CommandsService.Models;

namespace commandsservice.EventProcessing
{
    public class EventPrecessor : IEventPrecessor
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;
        public EventPrecessor(IServiceScopeFactory scopeFactory, IMapper mapper)
        {
            _scopeFactory = scopeFactory;
            _mapper = mapper;
        }

        public void ProcessEvent(string message)
        {
            var eventType = DeterminateEvent(message);

            switch (eventType)
            {
                case EventType.PlatformPublished:
                    break;
                default:
                    break;
            }
        }

        private EventType DeterminateEvent(string notificationMessage)
        {
            Console.WriteLine("--> Determing Event");

            var eventType = JsonSerializer.Deserialize<GenericEventDto>(notificationMessage);

            switch (eventType.Event)
            {
                case "Platform_Publisherd":
                    Console.WriteLine("--> Platform Published Event Detected");
                    return EventType.PlatformPublished;
                default:
                    Console.WriteLine("--> Could not determinate the event type");
                    return EventType.Undetermined;
            }
        }

        private void addPlatform(string platformPublishedMessage)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<ICommandRepo>();

                var platformPublishedDto = JsonSerializer.Deserialize<PlatformPublishedDto>(platformPublishedMessage);

                try
                {
                    var plat = _mapper.Map<Platform>(platformPublishedDto);
                    if (!repo.ExternalPlatformExists(plat.ExternalID))
                    {
                        repo.CreatePlatform(plat);
                        repo.SaveChanges();
                        Console.WriteLine("--> Platform added!");
                    }
                    else
                    {
                        Console.WriteLine("--> Platform already exisits...");
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not add Platform to DB {ex.Message}");
                }
            }
        }
    }

    enum EventType
    {
        PlatformPublished,
        Undetermined
    }


}