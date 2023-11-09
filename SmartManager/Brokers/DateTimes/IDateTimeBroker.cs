//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using System;

namespace SmartManager.Brokers.DateTimes
{
    public interface IDateTimeBroker
    {
        DateTimeOffset GetCurrentDateTimeOffset();
    }
}
