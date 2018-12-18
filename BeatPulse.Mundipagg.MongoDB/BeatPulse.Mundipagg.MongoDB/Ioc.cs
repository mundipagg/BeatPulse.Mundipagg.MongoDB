using BeatPulse.Core;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeatPulse.Mundipagg.MongoDB
{
    public class Ioc
    {
        public static ServiceProvider GetMongoDbLiveness(IServiceCollection services)
        {
            services.AddTransient<IBeatPulseLiveness, MongoDbLiveness>();

            return services.BuildServiceProvider();
        }
    }
}
