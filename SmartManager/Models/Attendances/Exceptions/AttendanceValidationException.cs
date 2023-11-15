//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using Xeptions;

namespace SmartManager.Models.Attendances.Exceptions
{
    public class AttendanceValidationException : Xeption
    {
        public AttendanceValidationException(Xeption innerException)
            : base(message: "Attendance validation error occurred, fix the errors and try again.", innerException)
        { }
    }
}
