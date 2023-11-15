using Xeptions;

namespace SmartManager.Models.Attendances.Exceptions
{
    public class InvalidAttendanceException : Xeption
    {
        public InvalidAttendanceException()
            : base(message: "Attendance is invalid.")
        { }
    }
}
