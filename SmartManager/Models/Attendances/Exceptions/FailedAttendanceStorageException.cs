//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using System;
using Xeptions;

namespace SmartManager.Models.Attendances.Exceptions
{
    public class FailedAttendanceStorageException : Xeption
    {
        public FailedAttendanceStorageException(Exception innerException)
            : base(message: "Failed attendance storage error occurred, contact support.", innerException)
        { }
    }
}
