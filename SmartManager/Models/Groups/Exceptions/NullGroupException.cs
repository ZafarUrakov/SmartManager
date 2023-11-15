//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using Xeptions;

namespace SmartManager.Models.Groups.Exceptions
{
    public class NullGroupException : Xeption
    {
        public NullGroupException()
            : base(message: "Group is null.")
        { }
    }
}
