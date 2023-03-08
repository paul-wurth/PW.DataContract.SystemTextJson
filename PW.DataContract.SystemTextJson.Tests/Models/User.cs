using System.Runtime.Serialization;

namespace PW.DataContract.SystemTextJson.Tests.Models
{
    internal class UserWithProperties
    {
        public int? Id { get; set; }

        public string? FirstName { get; set; }

        [DataMember(Name = "Surname")]
        public string? LastName { get; set; }

        [IgnoreDataMember]
        public string? FullName => $"{FirstName} {LastName}";

        public Permission Permission { get; set; }
    }

    internal enum Permission
    {
        Administrator,
        Moderator,
        Basic
    }
}
