using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using powman.Code.Models;

namespace powman.Code
{
    public class ConsumptionReporter
    {
        private readonly IHubContext<PowerConsumptionHub> _hub;

        public ConsumptionReporter(IHubContext<PowerConsumptionHub> hub)
        {
            _hub = hub;
        }

        public async Task ReportConsumptionAsync(PowerConsumption consumption)
        {
            await _hub.Clients.All.SendAsync("ReportConsumption", consumption);
        }
    }
}