//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using SmartManager.Models.Students;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SmartManager.Services.Processings.Spreadsheets
{
    public interface ISpreadsheetsProcessingService
    {
        ValueTask<List<Student>> ProcessImportRequest(MemoryStream stream);
    }
}