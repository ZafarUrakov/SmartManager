using SmartManager.Brokers.Storages;
using SmartManager.Models.Attendances;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartManager.Services.Foundations.Attendances
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IStorageBroker storageBroker;

        public AttendanceService(IStorageBroker storageBroker)
        {
            this.storageBroker = storageBroker;
        }

        public async ValueTask<Attendance> AddAttendanceAsync(Attendance attendance) =>
            await this.storageBroker.InsertAttendanceAsync(attendance);

        public async ValueTask<Attendance> RetrieveAttendanceByIdAsync(Guid attendanceid) =>
            await this.storageBroker.SelectAttendanceByIdAsync(attendanceid);

        public IQueryable<Attendance> RetrieveAllAttendances() =>
            this.storageBroker.SelectAllAttendances();

        public async ValueTask<Attendance> ModifyAttendanceAsync(Attendance attendance) =>
            await this.storageBroker.UpdateAttendanceAsync(attendance);

        public async ValueTask<Attendance> RemoveAttendanceAsync(Guid attendanceid)
        {
            Attendance attendance = await this.storageBroker.SelectAttendanceByIdAsync(attendanceid);

            return await this.storageBroker.DeleteAttendanceAsync(attendance);
        }
    }
}
