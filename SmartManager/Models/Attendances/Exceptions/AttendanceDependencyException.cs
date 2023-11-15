//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using System;
using Xeptions;

namespace SmartManager.Models.Attendances.Exceptions
{
    public class AttendanceDependencyException : Xeption
    {
        public AttendanceDependencyException(Exception innerException)
            : base(message: "Attendance dependency error occurred, contact support.", innerException)
        { }
    }
}
