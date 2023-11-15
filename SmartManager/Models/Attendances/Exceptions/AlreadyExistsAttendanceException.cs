using System;
using Xeptions;

namespace SmartManager.Models.Attendances.Exceptions
{
    public class AlreadyExistsAttendanceException : Xeption
    {
        public AlreadyExistsAttendanceException(Exception innerException)
            : base(message: "Attendance already exists.", innerException)
        { }
    }
}
