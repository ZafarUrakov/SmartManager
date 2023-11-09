//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using System;

namespace SmartManager.Brokers.DateTimes
{
    public class DateTimeBroker : IDateTimeBroker
    {
        public DateTimeOffset GetCurrentDateTimeOffset() =>
            DateTimeOffset.UtcNow;
    }
}
