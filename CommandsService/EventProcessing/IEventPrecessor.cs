using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace commandsservice.EventProcessing
{
    public interface IEventPrecessor
    {
        void ProcessEvent(string message);
    }
}