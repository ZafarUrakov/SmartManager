//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using System;
using Xeptions;

namespace SmartManager.Models.Groups.Exceptions
{
    public class AlreadyExistsGroupException : Xeption
    {
        public AlreadyExistsGroupException(Exception innerException)
            : base(message: "Group already exists.", innerException)
        { }
    }
}
