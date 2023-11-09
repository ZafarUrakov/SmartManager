//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using SmartManager.Models.Students;
using SmartManager.Models.StudentsStatistic;
using SmartManager.Services.Foundations.StudentsStatistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartManager.Services.Processings.StudentsStatistics
{
    public class StudentsStatisticProccessingService : IStudentsStatisticProccessingService
    {
        private readonly IStudentsStatisticService studentsStatisticService;

        public StudentsStatisticProccessingService(IStudentsStatisticService studentsStatisticService)
        {
            this.studentsStatisticService = studentsStatisticService;
        }
        public async ValueTask<StudentsStatistic> AddStudentsStatisticAsync(StudentsStatistic StudentsStatistic) =>
           await this.studentsStatisticService.AddStudentsStatisticAsync(StudentsStatistic);

        public async ValueTask<StudentsStatistic> RetrieveStudentsStatisticByIdAsync(Guid StudentsStatisticid) =>
            await this.studentsStatisticService.RetrieveStudentsStatisticByIdAsync(StudentsStatisticid);

        public IQueryable<StudentsStatistic> RetrieveAllStudentsStatistics() =>
            this.studentsStatisticService.RetrieveAllStudentsStatistics();

        public async ValueTask<StudentsStatistic> ModifyStudentsStatisticAsync(StudentsStatistic StudentsStatistic) =>
            await this.studentsStatisticService.ModifyStudentsStatisticAsync(StudentsStatistic);

        public async ValueTask<StudentsStatistic> RemoveStudentsStatisticAsync(Guid StudentsStatisticid) =>
            await this.studentsStatisticService.RemoveStudentsStatisticAsync(StudentsStatisticid);

        public async ValueTask<StudentsStatistic> UpdateStatisticsByStudentAsync(Student student)
        {
            StudentsStatistic studentsStatistic =
                (StudentsStatistic)this.studentsStatisticService
                .RetrieveAllStudentsStatistics().FirstOrDefault(s => s.GroupId == student.GroupId);

            if (studentsStatistic == null)
            {
                return await AddNewStatisticAsync(student);
            }

            if (student.Gender == Gender.Male)
            {
                studentsStatistic.MaleStudents++;
                return await ModifyStudentsStatisticAsync(studentsStatistic);
            }
            else
            {
                studentsStatistic.FemaleStudents++;
                return await ModifyStudentsStatisticAsync(studentsStatistic);
            }
        }

        public async ValueTask<StudentsStatistic> AddNewStatisticAsync(Student student)
        {
            StudentsStatistic studentsStatistic = new StudentsStatistic();
            studentsStatistic.GroupId = student.GroupId;

            if (student.Gender == Gender.Male)
            {
                studentsStatistic.MaleStudents += 1;
                return await AddStudentsStatisticAsync(studentsStatistic);
            }
            else
            {
                studentsStatistic.FemaleStudents += 1;
                return await AddStudentsStatisticAsync(studentsStatistic);

            }
        }

        public async Task CheckStatisticOfList(List<Student> students)
        {
            foreach (var item in students)
            {
                await UpdateStatisticsByStudentAsync(item);
            }
        }
    }
}

