//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using Microsoft.Extensions.Logging;
using System;

namespace SmartManager.Brokers.Loggings
{
    public class LoggingBroker : ILoggingBroker
    {
        private readonly ILogger<ILoggingBroker> logger;

        public LoggingBroker(ILogger<ILoggingBroker> logger)
        {
            this.logger = logger;
        }

        public void LogCritical(Exception exception) =>
            this.logger.LogCritical(exception.Message, exception);

        public void LogError(Exception exception) =>
            this.logger.LogError(exception.Message, exception);
    }
}
