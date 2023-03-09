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

            var hasDataContractAttribute = typeInfo.Type.IsDefined<DataContractAttribute>(inherit: true);

            foreach (var propertyInfo in typeInfo.Properties)
            {
                var provider = propertyInfo.AttributeProvider ??
                    throw new JsonException($"Unable to apply {nameof(DataContractJsonResolver)} modifier because the member '{propertyInfo.Name}' of type '{typeInfo.Type}' has no attribute provider.");

                // When there is a DataContractAttribute on the type, only members with the DataMemberAttribute are serialized.
                // When there is no DataContractAttribute, only public members without the IgnoreDataMemberAttribute are serialized.
                // See https://learn.microsoft.com/en-us/dotnet/framework/wcf/feature-details/using-data-contracts#notes
                // and https://learn.microsoft.com/en-us/dotnet/framework/wcf/samples/poco-support.
                if (hasDataContractAttribute)
                {
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
                    else
                    {
                        propertyInfo.Ignore();
                    }
                }
                else
                {
                    if (provider.IsDefined<IgnoreDataMemberAttribute>(inherit: true))
                    {
                        propertyInfo.Ignore();
                    }
                }
            }
        }

        private static bool IsNullOrDefault(object? obj)
        {
            return obj == null || obj.Equals(obj.GetType().GetDefaultValue());
        }
    }
}
