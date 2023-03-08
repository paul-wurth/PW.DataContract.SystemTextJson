using PW.DataContract.SystemTextJson.Extensions;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace PW.DataContract.SystemTextJson
{
    public class DataContractJsonResolver : DefaultJsonTypeInfoResolver
    {
        public DataContractJsonResolver()
        {
            Modifiers.Add(Modifier);
        }

        public static void Modifier(JsonTypeInfo typeInfo)
        {
            if (typeInfo.Kind != JsonTypeInfoKind.Object)
                return;

            foreach (var propertyInfo in typeInfo.Properties)
            {
                var provider = propertyInfo.AttributeProvider ??
                    throw new JsonException($"Unable to apply {nameof(DataContractJsonResolver)} modifier because the member '{propertyInfo.Name}' of type '{typeInfo.Type}' has no attribute provider.");

                if (provider.IsDefined<IgnoreDataMemberAttribute>(inherit: true))
                {
                    propertyInfo.Get = null;
                    propertyInfo.Set = null;
                    continue;
                }

                var dataMemberAttribute = provider.GetCustomAttribute<DataMemberAttribute>(inherit: true);
                if (dataMemberAttribute != null)
                {
                    if (dataMemberAttribute.IsNameSetExplicitly && dataMemberAttribute.Name != null)
                        propertyInfo.Name = dataMemberAttribute.Name;

                    if (dataMemberAttribute.Order > -1)
                        propertyInfo.Order = dataMemberAttribute.Order;

                    propertyInfo.ShouldSerialize = dataMemberAttribute.EmitDefaultValue
                        ? null // null = always serialize
                        : static (parent, prop) => !IsNullOrDefault(prop);

                    propertyInfo.IsRequired = dataMemberAttribute.IsRequired;
                }
            }
        }

        private static bool IsNullOrDefault(object? obj)
        {
            return obj == null || obj == obj.GetType().GetDefaultValue();
        }
    }
}
