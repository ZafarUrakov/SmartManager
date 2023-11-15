//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using System;
using Xeptions;

namespace SmartManager.Models.Attendances.Exceptions
{
    public class FailedAttendanceServiceException : Xeption
    {
        public FailedAttendanceServiceException(Exception innerException)
            : base(message: "Failed attendance service error occurred, please contact support.", innerException)
        { }
    }
}
