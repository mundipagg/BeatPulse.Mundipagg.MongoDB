using BeatPulse.Core;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BeatPulse.Mundipagg.MongoDB
{
    public static class MongoDBExtension
    {
        public static BeatPulseContext AddMongoCheck(this BeatPulseContext beatPulseContext, ServiceProvider serviceProvider, string name, Action<BeatPulseLivenessRegistrationOptions> setup)
        {
            return beatPulseContext.AddLiveness(name, opt =>
            {
                opt.UsePath("mongodb");
                opt.UseLiveness(serviceProvider.GetService<IBeatPulseLiveness>());
            });
        }
    }
}
