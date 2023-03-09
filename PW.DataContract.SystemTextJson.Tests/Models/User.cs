using System.Runtime.Serialization;

namespace PW.DataContract.SystemTextJson.Tests.Models
{
    [DataContract]
    internal class UserWithProperties
    {
        [DataMember]
        public int? Id { get; set; }

        [DataMember]
        public string? FirstName { get; set; }

        [DataMember(Name = "Surname")]
        public string? LastName { get; set; }

        [IgnoreDataMember]
        public string? FullName => $"{FirstName} {LastName}";

        [DataMember]
        public Permission Permission { get; set; }
    }

    [DataContract]
    internal enum Permission
    {
        [EnumMember(Value = "Admin")]
        Administrator,

        [EnumMember(Value = "Moderator")]
        Moderator,

        [EnumMember(Value = "Basic")]
        Basic
    }
}
