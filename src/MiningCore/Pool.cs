﻿using System;
using System.Threading.Tasks;
using Autofac;
using CodeContracts;
using Microsoft.Extensions.Logging;
using MiningCore.Configuration;
using MiningCore.Configuration.Extensions;
using MiningCore.Stratum;

namespace MiningCore
{
    public class Pool
    {
        public Pool(IComponentContext ctx, ILogger<Pool> logger)
        {
            this.ctx = ctx;
            this.logger = logger;
        }

        private readonly IComponentContext ctx;
        private readonly ILogger<Pool> logger;
        private StratumServer server;

        public Task InitAsync(PoolConfig poolConfig, PoolClusterConfig poolClusterConfig)
        {
            Contract.RequiresNonNull(poolConfig, nameof(poolConfig));
            Contract.RequiresNonNull(poolClusterConfig, nameof(poolClusterConfig));

            logger.Info(() => $"Pool {poolConfig.Coin.Name} initializing ...");

            InitializeStratum(poolConfig);

            return Task.FromResult(false);
        }

        private void InitializeStratum(Configuration.PoolConfig poolConfig)
        {
            server = ctx.Resolve<StratumServer>();
            server.Init(poolConfig);
        }
    }
}
