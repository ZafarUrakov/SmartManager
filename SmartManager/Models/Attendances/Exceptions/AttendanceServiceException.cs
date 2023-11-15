//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using System;
using Xeptions;

namespace SmartManager.Models.Attendances.Exceptions
{
    public class AttendanceServiceException : Xeption
    {
        public AttendanceServiceException(Exception innerException)
            : base(message: "Attendance service error occurred, contact support.", innerException)
        { }
    }
}
