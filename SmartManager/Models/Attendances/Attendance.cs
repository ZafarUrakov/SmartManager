//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using SmartManager.Models.Students;
using System;

namespace SmartManager.Models.Attendances
{
    public class Attendance
    {
        public Guid Id { get; set; }
        public DateTimeOffset Date { get; set; }
        public bool IsPresent { get; set; }

        public Guid StudentId { get; set; }
        public Student Student { get; set; }
    }
}
