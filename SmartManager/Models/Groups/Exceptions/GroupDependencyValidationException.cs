//===========================
// Copyright (c) Tarteeb LLC
// Managre quickly and easy
//===========================

using Xeptions;

namespace SmartManager.Models.Groups.Exceptions
{
    public class GroupDependencyValidationException : Xeption
    {
        public GroupDependencyValidationException(Xeption innerException)
           : base(message: "Group dependency validation error occurred, fix the errors and try again.",
                 innerException)
        { }
    }
}
