using BeatPulse.Core;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BeatPulse.Mundipagg.MongoDB
{
    public static class MongoDBExtension
    {
        public static BeatPulseContext AddMongoCheck(this BeatPulseContext beatPulseContext, IServiceCollection services, string name, Action<BeatPulseLivenessRegistrationOptions> setup)
        {
            var provider = Ioc.GetMongoDbLiveness(services);

            return beatPulseContext.AddLiveness(name, opt =>
            {
                opt.UsePath("mongodb");
                opt.UseLiveness(provider.GetService<IBeatPulseLiveness>());
            });
        }
    }
}
