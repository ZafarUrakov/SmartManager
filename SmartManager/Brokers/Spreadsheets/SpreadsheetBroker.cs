//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using OfficeOpenXml;
using SmartManager.Models.ExternalStudents;
using SmartManager.Models.Students;
using System;
using System.Collections.Generic;
using System.IO;

namespace SmartManager.Brokers.Spreadsheets
{
    public class SpreadsheetBroker : ISpreadsheetBroker
    {
        public List<ExternalStudent> ImportStudents(MemoryStream stream)
        {
            var importStudents = new List<ExternalStudent>();

            using (var package = new ExcelPackage(stream))
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                var worksheet = package.Workbook.Worksheets[0];

                for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                {
                    var externalStudent = new ExternalStudent();

                    externalStudent.Id = Guid.NewGuid();
                    externalStudent.GivenName = worksheet.Cells[row, 1].Value?.ToString();
                    externalStudent.Surname = worksheet.Cells[row, 2].Value?.ToString();
                    externalStudent.PhoneNumber = worksheet.Cells[row, 3].Value?.ToString();
                    string genderString = worksheet.Cells[row, 4].Value?.ToString();
                    externalStudent.Gender = ConvertToGender(genderString);
                    externalStudent.GroupName = worksheet.Cells[row, 5].Value?.ToString();

                    importStudents.Add(externalStudent);
                }
            }

            return importStudents;
        }

        private Gender ConvertToGender(string genderString)
        {
            if (Enum.TryParse<Gender>(genderString, out Gender gender))
            {
                return gender;
            }
            else
            {
                return Gender.Unknown;
            }
        }
    }
}
