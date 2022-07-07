using System;

namespace Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class NonInjectAttribute : Attribute
    {
    }
}