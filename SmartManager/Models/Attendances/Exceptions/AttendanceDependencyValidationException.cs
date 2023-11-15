//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using Xeptions;

namespace SmartManager.Models.Attendances.Exceptions
{
    public class AttendanceDependencyValidationException : Xeption
    {
        public AttendanceDependencyValidationException(Xeption innerException)
           : base(message: "Attendance dependency validation error occurred, fix the errors and try again.",
                 innerException)
        { }
    }
}
