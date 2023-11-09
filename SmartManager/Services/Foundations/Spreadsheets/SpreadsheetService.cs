//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using SmartManager.Brokers.Spreadsheets;
using SmartManager.Models.ExternalStudents;
using System.Collections.Generic;
using System.IO;

namespace SmartManager.Services.Foundations.Spreadsheets
{
    public class SpreadsheetService : ISpreadsheetService
    {
        private readonly ISpreadsheetBroker spreadsheetBroker;

        public SpreadsheetService(ISpreadsheetBroker spreadsheetBroker)
        {
            this.spreadsheetBroker = spreadsheetBroker;
        }

        public List<ExternalStudent> GetExternalStudents(MemoryStream stream)
        {
            List<ExternalStudent> externalStudents =
                            this.spreadsheetBroker.ImportStudents(stream);

            return externalStudents;
        }
    }
}
