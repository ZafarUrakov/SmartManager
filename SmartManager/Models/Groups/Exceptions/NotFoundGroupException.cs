//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using System;
using Xeptions;

namespace SmartManager.Models.Groups.Exceptions
{
    public class NotFoundGroupException : Xeption
    {
        public NotFoundGroupException(Guid groupId)
            : base(message: $"Couldn't find group with id {groupId}.")
        { }
    }
}
