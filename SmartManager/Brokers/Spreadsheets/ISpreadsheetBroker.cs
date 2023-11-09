//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using SmartManager.Models.ExternalStudents;
using System.Collections.Generic;
using System.IO;

namespace SmartManager.Brokers.Spreadsheets
{
    public interface ISpreadsheetBroker
    {
        List<ExternalStudent> ImportStudents(MemoryStream stream);
    }
}
