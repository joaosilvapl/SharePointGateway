using System.Net;
using CredentialManagement;

namespace SharePointGateway.TestApp
{
    public static class CredentialUtil
    {
        public static NetworkCredential GetCredential(string target, PersistanceType persistanceType)
        {
            var cm = new Credential { Target = target, PersistanceType = persistanceType };

            if (!cm.Load())
            {
                return null;
            }

            var networkCredential = new NetworkCredential(cm.Username, cm.Password);

            var userNameParts = networkCredential.UserName.Split('\\');
            if (userNameParts.Length > 1)
            {
                networkCredential.Domain = userNameParts[0];
                networkCredential.UserName = userNameParts[1];
            }

            return networkCredential;
        }

        public static bool SetCredentials(
            string target, string username, string password, PersistanceType persistenceType)
        {
            return new Credential
            {
                Target = target,
                Username = username,
                Password = password,
                PersistanceType = persistenceType
            }.Save();
        }
    }
}
