using System;

namespace PW.DataContract.SystemTextJson.Extensions
{
    internal static class TypeExtensions
    {
        public static object? GetDefaultValue(this Type type)
        {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }
    }
}
