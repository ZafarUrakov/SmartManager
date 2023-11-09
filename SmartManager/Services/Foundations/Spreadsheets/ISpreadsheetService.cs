//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using SmartManager.Models.ExternalStudents;
using System.Collections.Generic;
using System.IO;

namespace SmartManager.Services.Foundations.Spreadsheets
{
    public interface ISpreadsheetService
    {
        List<ExternalStudent> GetExternalStudents(MemoryStream stream);
    }
}
