﻿using JT808.Gateway.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Gateway.Traffic
{
    public static class JT808TrafficServiceExtensions
    {
        /// <summary>
        /// 消息流量统计服务（不同的消费者实例）
        /// </summary>
        /// <param name="jT808ClientBuilder"></param>
        /// <returns></returns>
        public static IJT808ClientBuilder AddTraffic<TIJT808Traffic>(this IJT808ClientBuilder jT808ClientBuilder)
            where TIJT808Traffic:IJT808Traffic
        {
            jT808ClientBuilder.JT808Builder.Services.AddSingleton(typeof(IJT808Traffic), typeof(TIJT808Traffic));
            jT808ClientBuilder.JT808Builder.Services.AddHostedService<JT808TrafficServiceHostedService>();
            return jT808ClientBuilder;
        }

        /// <summary>
        /// 消息流量统计服务（不同的消费者实例）
        /// </summary>
        /// <param name="jT808ClientBuilder"></param>
        /// <returns></returns>
        public static IJT808ClientBuilder AddTraffic(this IJT808ClientBuilder jT808ClientBuilder)
        {
            jT808ClientBuilder.JT808Builder.Services.AddSingleton(typeof(IJT808Traffic), typeof(JT808TrafficDefault));
            jT808ClientBuilder.JT808Builder.Services.AddHostedService<JT808TrafficServiceHostedService>();
            return jT808ClientBuilder;
        }

        /// <summary>
        /// 消息流量统计服务（不同的消费者实例）
        /// </summary>
        /// <param name="jT808NormalGatewayBuilder"></param>
        /// <returns></returns>
        public static IJT808GatewayBuilder AddTraffic<TIJT808Traffic>(this IJT808GatewayBuilder jT808NormalGatewayBuilder)
            where TIJT808Traffic : IJT808Traffic
        {
            jT808NormalGatewayBuilder.JT808Builder.Services.AddSingleton(typeof(IJT808Traffic), typeof(TIJT808Traffic));
            return jT808NormalGatewayBuilder;
        }


        /// <summary>
        /// 消息流量统计服务（不同的消费者实例）
        /// </summary>
        /// <param name="jT808NormalGatewayBuilder"></param>
        /// <returns></returns>
        public static IJT808GatewayBuilder AddTraffic(this IJT808GatewayBuilder jT808NormalGatewayBuilder)
        {
            jT808NormalGatewayBuilder.JT808Builder.Services.AddSingleton(typeof(IJT808Traffic), typeof(JT808TrafficDefault));
            return jT808NormalGatewayBuilder;
        }
    }
}
