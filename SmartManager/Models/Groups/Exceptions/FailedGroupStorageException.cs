//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using System;
using Xeptions;

namespace SmartManager.Models.Groups.Exceptions
{
    public class FailedGroupStorageException : Xeption
    {
        public FailedGroupStorageException(Exception innerException)
            : base(message: "Failed group storage error occurred, contact support.", innerException)
        { }
    }
}
