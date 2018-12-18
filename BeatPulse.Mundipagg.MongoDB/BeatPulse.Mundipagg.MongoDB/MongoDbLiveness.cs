using BeatPulse.Core;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BeatPulse.Mundipagg.MongoDB
{
    public class MongoDbLiveness : IBeatPulseLiveness
    {
        private readonly IMongoDatabase _database;

        public MongoDbLiveness(IConfiguration configuration)
        {
            var connectionString = configuration.GetSection("MongoDbConnectionString").Value;
            var client = new MongoClient(connectionString);

            _database = client.GetDatabase(new MongoUrl(connectionString).DatabaseName);
        }

        public async Task<LivenessResult> IsHealthy(LivenessExecutionContext context, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                await _database.ListCollectionNamesAsync();

                return LivenessResult.Healthy("The service is operating");
            }
            catch (Exception ex)
            {
                return LivenessResult.UnHealthy(ex.Message);
            }
        }
    }
}
