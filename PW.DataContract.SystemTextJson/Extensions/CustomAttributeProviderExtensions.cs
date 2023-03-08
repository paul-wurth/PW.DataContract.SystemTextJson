using System;
using System.Reflection;

namespace PW.DataContract.SystemTextJson.Extensions
{
    internal static class CustomAttributeProviderExtensions
    {
        public static TAttribute[] GetCustomAttributes<TAttribute>(this ICustomAttributeProvider provider, bool inherit)
            where TAttribute : Attribute
        {
            return (TAttribute[])provider.GetCustomAttributes(typeof(TAttribute), inherit);
        }

        public static TAttribute? GetCustomAttribute<TAttribute>(this ICustomAttributeProvider provider, bool inherit)
            where TAttribute : Attribute
        {
            var attributes = provider.GetCustomAttributes(typeof(TAttribute), inherit);
            return attributes.Length > 0 ? (TAttribute)attributes[0] : null;
        }

        public static bool IsDefined<TAttribute>(this ICustomAttributeProvider provider, bool inherit)
        {
            return provider.IsDefined(typeof(TAttribute), inherit);
        }
    }
}
