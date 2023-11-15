//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using System;
using Xeptions;

namespace SmartManager.Models.Attendances.Exceptions
{
    public class NotFoundAttendanceException : Xeption
    {
        public NotFoundAttendanceException(Guid attendanceId)
            : base(message: $"Couldn't find attendance with id {attendanceId}.")
        { }
    }
}
