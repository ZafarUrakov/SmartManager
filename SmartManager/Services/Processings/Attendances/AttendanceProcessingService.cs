using SmartManager.Models.Attendances;
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
        public async ValueTask<Attendance> AddAttendanceAsync(Attendance Attendance) =>
           await this.attendanceService.AddAttendanceAsync(Attendance);

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
