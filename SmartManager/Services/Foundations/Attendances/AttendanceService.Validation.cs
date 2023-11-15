////===========================
//// Copyright (c) Tarteeb LLC
//// Managre quickly and easy
////===========================

//using SmartManager.Models.Attendances;
//using System;

//namespace SmartManager.Services.Foundations.Attendances
//{
//    public partial class AttendanceService
//    {
//        private void ValidateAttendanceOnAdd(Attendance attendance)
//        {
//            ValidateAttendanceNotNull(attendance);

//            Validate(
//                (Rule: IsInvalid(attendance.Id), Parameter: nameof(Attendance.Id)),
//                (Rule: IsInvalid(attendance.Date), Parameter: nameof(Attendance.Date)),
//                (Rule: IsInvalid(attendance.StudentId), Parameter: nameof(Attendance.StudentId)));
//        }

//        private void ValidateAttendanceOnModify(Attendance attendance)
//        {
//            ValidateAttendanceNotNull(attendance);

//            Validate(
//                (Rule: IsInvalid(attendance.Id), Parameter: nameof(Attendance.Id)),
//                (Rule: IsInvalid(attendance.Date), Parameter: nameof(Attendance.Date)),
//                (Rule: IsInvalid(attendance.StudentId), Parameter: nameof(Attendance.StudentId)));
//        }

//        private void ValidateAgainstStorageAttendanceOnModify(Attendance inputAttendance, Attendance storageAttendance)
//        {
//            ValidateStorageAttendance(storageAttendance, inputAttendance.Id);

//            Validate(
//                (Rule: IsInvalid(inputAttendance.Id), Parameter: nameof(Attendance.Id)),
//                (Rule: IsInvalid(inputAttendance.Date), Parameter: nameof(Attendance.Date)),
//                (Rule: IsInvalid(inputAttendance.StudentId), Parameter: nameof(Attendance.StudentId)));
//        }

//        private static void ValidateAttendanceNotNull(Attendance attendance)
//        {
//            if (attendance == null)
//            {
//                throw new NullAttendanceException();
//            }
//        }

//        private static dynamic IsInvalid(DateTimeOffset date) => new
//        {
//            Condition = date == default,
//            Message = "Date is required"
//        };

//        private dynamic IsInvalid(string text) => new
//        {
//            Condition = string.IsNullOrWhiteSpace(text),
//            Message = "Text is required"
//        };

//        private dynamic IsInvalid(Guid id) => new
//        {
//            Condition = id == default,
//            Message = "Id is required"
//        };

//        private void ValidateAttendanceId(Guid attendanceId) =>
//            Validate((Rule: IsInvalid(attendanceId), Parameter: nameof(Attendance.Id)));

//        private void ValidateStorageAttendance(Attendance maybeAttendance, Guid attendanceId)
//        {
//            if (maybeAttendance is null)
//            {
//            }
//        }


//        //private static void Validate(params (dynamic Rule, string Parameter)[] validations)
//        //{
//        //    var invalidAttendanceException = new ();

//        //    foreach ((dynamic rule, string parameter) in validations)
//        //    {
//        //        if (rule.Condition)
//        //        {
//        //            invalidAttendanceException.UpsertDataList(
//        //                key: parameter,
//        //                value: rule.Message);
//        //        }
//        //    }

//        //    invalidAttendanceException.ThrowIfContainsErrors();
//        //}
//    }
//}
