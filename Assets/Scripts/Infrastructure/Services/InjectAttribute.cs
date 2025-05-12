using System;

namespace Infrastructure.Services
{
    [AttributeUsage(AttributeTargets.Constructor, AllowMultiple = false)]
    public class InjectAttribute : Attribute
    {
    }
}