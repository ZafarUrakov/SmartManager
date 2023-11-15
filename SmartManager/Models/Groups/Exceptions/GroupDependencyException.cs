//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using System;
using Xeptions;

namespace SmartManager.Models.Groups.Exceptions
{
    public class GroupDependencyException : Xeption
    {
        public GroupDependencyException(Exception innerException)
            : base(message: "Group dependency error occurred, contact support.", innerException)
        { }
    }
}
