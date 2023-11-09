//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using System;

namespace SmartManager.Brokers.Loggings
{
    public interface ILoggingBroker
    {
        public void LogCritical(Exception exception);
        public void LogError(Exception exception);
    }
}
