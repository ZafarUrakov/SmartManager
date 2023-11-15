//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using System;
using Xeptions;

namespace SmartManager.Models.Groups.Exceptions
{
    public class GroupServiceException : Xeption
    {
        public GroupServiceException(Exception innerException)
            : base(message: "Group service error occurred, contact support.", innerException)
        { }
    }
}
