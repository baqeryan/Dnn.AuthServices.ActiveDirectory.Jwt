using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using DotNetNuke.Entities.Users;

namespace Dnn.AuthServices.ActiveDirectory.Jwt.Services
{
    public static class ActiveDirectory
    {
        [System.Obsolete]
        public static string DomainName => System.Configuration.ConfigurationSettings.AppSettings.Get("ActiveDirectory.Jwt.DomainName");

        [System.Obsolete]
        public static bool IsAuthenticated(string userName, string password)
        {
            bool isValid;
            if (string.IsNullOrEmpty(userName))
            {
                return false;
            }
            else
            {
                using (var pc = new PrincipalContext(ContextType.Domain, DomainName))
                {
                    isValid = pc.ValidateCredentials(userName, password);
                }
            }
            return isValid;
        }

        [System.Obsolete]
        public static UserInfo GetUser(string userName)
        {
            if (string.IsNullOrEmpty(userName)) return null;
            //var entry = new DirectoryEntry(MySettings.ldap, MySettings.entryUser, MySettings.entryPass);
            var entry = new DirectoryEntry($"LDAP://{DomainName}");
            var search = new DirectorySearcher(entry)
            {
                CacheResults = true,
                Filter = "(SAMAccountName=" + userName + ")"
            };
            var result = search.FindOne();
            if (result == null) return null;
            return new UserInfo
            {
                Username = result.Properties["SAMAccountName"].Count > 0
                    ? result.Properties["SAMAccountName"][0].ToString()
                    : string.Empty,
                Email =
                    result.Properties["mail"].Count > 0 ? result.Properties["mail"][0].ToString() : string.Empty,
                FirstName = result.Properties["givenname"].Count > 0
                    ? result.Properties["givenname"][0].ToString()
                    : string.Empty,
                LastName = result.Properties["sn"].Count > 0 ? result.Properties["sn"][0].ToString() : string.Empty,
                DisplayName = result.Properties["displayname"].Count > 0
                    ? result.Properties["displayname"][0].ToString()
                    : string.Empty
            }; ;
        }

    }
}


