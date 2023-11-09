//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using SmartManager.Models.Groups;
using System;

namespace SmartManager.Models.StudentsStatistic
{
    public class StudentsStatistic
    {
        public Guid Id { get; set; }
        public int MaleStudents { get; set; }
        public int FemaleStudents { get; set; }

        public Guid GroupId { get; set; }
        public Group Group { get; set; }
    }
}
