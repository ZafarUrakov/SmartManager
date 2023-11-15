//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using Xeptions;

namespace SmartManager.Models.Attendances.Exceptions
{
    public class NullAttendanceException : Xeption
    {
        public NullAttendanceException()
            : base(message: "Attendance is null.")
        { }
    }
}
