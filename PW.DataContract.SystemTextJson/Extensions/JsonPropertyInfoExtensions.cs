using System.Text.Json.Serialization.Metadata;

namespace PW.DataContract.SystemTextJson.Extensions
{
    internal static class JsonPropertyInfoExtensions
    {
        public static void Ignore(this JsonPropertyInfo jsonPropertyInfo)
        {
            jsonPropertyInfo.Get = null;
            jsonPropertyInfo.Set = null;
            jsonPropertyInfo.ShouldSerialize = static (parent, prop) => false;
        }
    }
}
