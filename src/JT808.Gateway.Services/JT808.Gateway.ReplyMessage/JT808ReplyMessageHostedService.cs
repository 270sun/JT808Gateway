﻿using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using System.Threading;
using JT808.Gateway.Abstractions;

namespace JT808.Gateway.ReplyMessage
{
    //public class JT808ReplyMessageHostedService : IHostedService
    //{
    //    //private readonly IJT808MsgConsumer jT808MsgConsumer;
    //    //private readonly JT808QueueReplyMessageHandler jT808ReplyMessageHandler;

    //    //public JT808ReplyMessageHostedService(
    //    //    JT808QueueReplyMessageHandler jT808ReplyMessageHandler,
    //    //    IJT808MsgConsumer jT808MsgConsumer)
    //    //{
    //    //    this.jT808MsgConsumer = jT808MsgConsumer;
    //    //    this.jT808ReplyMessageHandler = jT808ReplyMessageHandler;
    //    //}

    //    //public Task StartAsync(CancellationToken cancellationToken)
    //    //{
    //    //    jT808MsgConsumer.Subscribe();
    //    //    jT808MsgConsumer.OnMessage(jT808ReplyMessageHandler.Processor);
    //    //    return Task.CompletedTask;
    //    //}

    //    //public Task StopAsync(CancellationToken cancellationToken)
    //    //{
    //    //    jT808MsgConsumer.Unsubscribe();
    //    //    return Task.CompletedTask;
    //    //}
    //}
}
