//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using System;
using Xeptions;

namespace SmartManager.Models.Groups.Exceptions
{
    public class FailedGroupServiceException : Xeption
    {
        public FailedGroupServiceException(Exception innerException)
            : base(message: "Failed group service error occurred, please contact support.", innerException)
        { }
    }
}
