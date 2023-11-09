using SmartManager.Models.Students;
using SmartManager.Models.StudentsStatistic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartManager.Services.Processings.StudentsStatistics
{
    public interface IStudentsStatisticProccessingService
    {
        ValueTask<StudentsStatistic> AddStudentsStatisticAsync(StudentsStatistic studentsStatistic);
        ValueTask<StudentsStatistic> RetrieveStudentsStatisticByIdAsync(Guid studentsStatisticId);
        IQueryable<StudentsStatistic> RetrieveAllStudentsStatistics();
        ValueTask<StudentsStatistic> ModifyStudentsStatisticAsync(StudentsStatistic studentsStatistic);
        ValueTask<StudentsStatistic> RemoveStudentsStatisticAsync(Guid studentsStatisticId);
        ValueTask<StudentsStatistic> UpdateStatisticsByStudentAsync(Student student);
        Task CheckStatisticOfList(List<Student> students);
    }
}
