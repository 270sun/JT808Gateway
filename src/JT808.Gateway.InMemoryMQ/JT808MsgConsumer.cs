﻿using JT808.Gateway.Abstractions;
using JT808.Gateway.InMemoryMQ.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JT808.Gateway.InMemoryMQ
{
    public class JT808MsgConsumer : IJT808MsgConsumer
    {
        private readonly JT808MsgService JT808MsgService;
        public CancellationTokenSource Cts => new CancellationTokenSource();
        private readonly ILogger logger;
        public string TopicName => JT808GatewayConstants.MsgTopic;
        public JT808MsgConsumer(
            JT808MsgService jT808MsgService,
            ILoggerFactory loggerFactory)
        {
            JT808MsgService = jT808MsgService;
            logger = loggerFactory.CreateLogger("JT808MsgConsumer");
        }

        public void OnMessage(Action<(string TerminalNo, byte[] Data)> callback)
        {
            Task.Run(() =>
            {
                while (!Cts.IsCancellationRequested)
                {
                    try
                    {
                        if (JT808MsgService.TryRead(out var item))
                        {
                            callback(item);
                        }
                    }
                    catch(Exception ex)
                    {
                        logger.LogError(ex, "");
                    }
                }
            }, Cts.Token);
        }

        public void Subscribe()
        {

        }

        public void Unsubscribe()
        {
            Cts.Cancel();
        }

        public void Dispose()
        {
            Cts.Dispose();
        }
    }
}
