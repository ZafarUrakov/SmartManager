//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using System;
using Xeptions;

namespace SmartManager.Models.Attendances.Exceptions
{
    public class LockedAttendanceException : Xeption
    {
        public LockedAttendanceException(Exception innerException)
            : base(message: "Attendance is locked, please try again.", innerException)
        { }
    }
}
