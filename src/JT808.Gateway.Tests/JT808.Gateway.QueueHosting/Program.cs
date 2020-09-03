﻿using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using JT808.Protocol;
using Microsoft.Extensions.Configuration;
using NLog.Extensions.Logging;
using JT808.Gateway.MsgLogging;
using JT808.Gateway.ReplyMessage;
using JT808.Gateway.Transmit;
using JT808.Gateway.Traffic;
using JT808.Gateway.Abstractions;
using JT808.Gateway.SessionNotice;
using JT808.Gateway.Client;
using JT808.Gateway.QueueHosting.Jobs;
using JT808.Gateway.Kafka;

namespace JT808.Gateway.QueueHosting
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var serverHostBuilder = new HostBuilder()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                          .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                          .AddJsonFile($"appsettings.{ hostingContext.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true);
                })
                .ConfigureLogging((context, logging) =>
                {
                    Console.WriteLine($"Environment.OSVersion.Platform:{Environment.OSVersion.Platform.ToString()}");
                    NLog.LogManager.LoadConfiguration($"Configs/nlog.{Environment.OSVersion.Platform.ToString()}.config");
                    logging.AddNLog(new NLogProviderOptions { CaptureMessageTemplates = true, CaptureMessageProperties = true });
                    logging.SetMinimumLevel(LogLevel.Trace);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<ILoggerFactory, LoggerFactory>();
                    services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
                    services.AddJT808Configure()
                            //.AddQueueGateway(options =>
                            //{
                            //    options.TcpPort = 808;
                            //    options.UdpPort = 808;
                            //})
                            .AddGateway(hostContext.Configuration)
                            .AddServerKafkaMsgProducer(hostContext.Configuration)
                            .AddServerKafkaSessionProducer(hostContext.Configuration)
                            .AddServerKafkaMsgReplyConsumer(hostContext.Configuration)
                            .AddTcp()
                            .AddUdp()
                            .Builder()
                            //添加客户端工具
                            .AddClient()
                            //添加客户端服务
                            .AddClientKafka()
                            .AddMsgConsumer(hostContext.Configuration)
                            //添加消息应答服务 
                            .AddMsgReplyProducer(hostContext.Configuration)
                            //添加消息应答处理
                            //.AddReplyMessage()
                            ;
                    //grpc客户端调用
                    //services.AddHostedService<CallGrpcClientJob>();
                    //客户端测试
                    services.AddHostedService<UpJob>();
                });
            await serverHostBuilder.RunConsoleAsync();
        }
    }
}
