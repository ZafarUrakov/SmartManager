//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using SmartManager.Models.Students;
using System;

namespace SmartManager.Models.ExternalStudents
{
    public class ExternalStudent
    {
        public Guid Id { get; set; }
        public string GivenName { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public DateTimeOffset BirthDate { get; set; }
        public Gender Gender { get; set; }
        public string GroupName { get; set; }
    }
}
