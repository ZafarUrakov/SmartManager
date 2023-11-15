//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using System;
using Xeptions;

namespace SmartManager.Models.Groups.Exceptions
{
    public class LockedGroupException : Xeption
    {
        public LockedGroupException(Exception innerException)
            : base(message: "Group is locked, please try again.", innerException)
        { }
    }
}
