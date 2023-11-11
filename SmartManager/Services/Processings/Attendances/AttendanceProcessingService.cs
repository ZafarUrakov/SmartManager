using SmartManager.Models.Attendances;
using SmartManager.Models.Students;
using SmartManager.Services.Foundations.Attendances;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartManager.Services.Processings.Attendances
{
    public class AttendanceProcessingService : IAttendanceProcessingService
    {
        private readonly IAttendanceService attendanceService;

        public AttendanceProcessingService(IAttendanceService attendanceService)
        {
            this.attendanceService = attendanceService;
        }
        public async ValueTask<Attendance> AddAttendanceAsync(Student student, bool isPresent)
        {

            var attendance = new Attendance
            {
                Id = Guid.NewGuid(),
                Student = student,
                Date = DateTimeOffset.Now,
                IsPresent = isPresent
            };

            return await this.attendanceService.AddAttendanceAsync(attendance);
        }

        public async ValueTask<Attendance> RetrieveAttendanceByIdAsync(Guid Attendanceid) =>
            await this.attendanceService.RetrieveAttendanceByIdAsync(Attendanceid);

        public IQueryable<Attendance> RetrieveAllAttendances() =>
            this.attendanceService.RetrieveAllAttendances();

        public async ValueTask<Attendance> ModifyAttendanceAsync(Attendance Attendance) =>
            await this.attendanceService.ModifyAttendanceAsync(Attendance);

        public async ValueTask<Attendance> RemoveAttendanceAsync(Guid Attendanceid) =>
            await this.attendanceService.RemoveAttendanceAsync(Attendanceid);
    }
}
