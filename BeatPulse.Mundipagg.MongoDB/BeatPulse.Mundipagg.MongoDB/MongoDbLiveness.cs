﻿using BeatPulse.Core;
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
            var port = int.Parse(configuration.GetSection("MongoDbConnectionPort").Value);
            var servers = new List<MongoServerAddress>
            {
                new MongoServerAddress(configuration.GetSection("MongoDbConnectionHost").Value, port)
            };

            var settings = new MongoClientSettings()
            {
                Servers = servers
            };
            
            var client = new MongoClient(settings);

            _database = client.GetDatabase(configuration.GetSection("MongoDbDatabaseName").Value);
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
