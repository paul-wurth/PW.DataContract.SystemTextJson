using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PW.DataContract.SystemTextJson.Tests.Models
{
    public class Person
    {
        public string? FullName;
        public int Age;
    }

    public class PersonWithNonPublicMember
    {
        public string? FullName;
        public int Age = 21; // It was protected
    }

    public class PersonWithoutContractWithDataMember
    {
        [DataMember(Name = "full_name")]
        public string? FullName { get; set; }

        [IgnoreDataMember]
        public int Age { get; set; }
    }

    public class PersonWithIgnore
    {
        public string? FullName { get; set; }

        [IgnoreDataMember]
        public int Age { get; set; }
    }

    public class PersonGetSet
    {
        public string? FullName { get; set; }
        public int Age { get; set; }
    }

    [DataContract]
    public class PersonContract
    {
        [DataMember]
        public string? FullName;

        [DataMember(EmitDefaultValue = false)]
        public int Age;
    }

    [DataContract]
    public class PersonContractMemberGetter
    {
        [DataMember]
        public string? FullName { get; set; }

        [DataMember]
        public int Age { get; set; } = 21; // Without setter, it fails on .NET 7
    }

    [DataContract]
    public class PersonContractWithNonPublicMember
    {
        [DataMember]
        public string? FullName;

        [DataMember(EmitDefaultValue = false)]
        public int Age = 21; // It was protected
    }

    [DataContract]
    public class PersonContractWithoutDataMember
    {
        public string? FullName;
        public int Age = 21; // It was protected
    }

    [DataContract]
    public class PersonContractOverrideName
    {
        [DataMember(Name = "full_name", EmitDefaultValue = false)]
        public string? FullName { get; set; }

        [DataMember(Name = "age", EmitDefaultValue = false)]
        public int Age { get; set; }
    }

    [DataContract]
    public class PersonContractOrdered
    {
        [DataMember(EmitDefaultValue = false, Order = 2)]
        public string? FullName { get; set; }

        [DataMember(Order = 1)]
        public int Age { get; set; }
    }

    [DataContract]
    public class PersonWithDictionary
    {
        [DataMember(EmitDefaultValue = false, Order = 2)]
        public string? FullName { get; set; }

        [DataMember(Order = 1)]
        public int Age { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Dictionary<string, string>? Dict { get; set; }
    }

    [DataContract]
    public class PersonContractWithIgnore
    {
        [DataMember(EmitDefaultValue = false)]
        public string? FullName { get; set; }

        [IgnoreDataMember]
        public int Age { get; set; }
    }

    [DataContract]
    public class PersonContractWithStruct
    {
        [DataMember]
        public string? FullName { get; set; }

        [DataMember]
        public int Age { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public DateTime LastLogin { get; set; }
    }

    [DataContract]
    public class PersonContractWithNullable
    {
        [DataMember(EmitDefaultValue = false)]
        public string? FullName { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int? Age { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public DateTime? LastLogin { get; set; }
    }
}
