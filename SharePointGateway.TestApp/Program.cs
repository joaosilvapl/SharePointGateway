using System;
using System.IO;
using System.Linq;
using CredentialManagement;
using SharePointGateway.Core;

namespace SharePointGateway.TestApp
{
    class Program
    {
        //The credential entry (target) name in the local computer
        private const string CredentialsEntryName = "SharePointGateway.TestApp.Credentials";

        //Name of config file to be placed in the User folder (c:\users\[userName])
        private const string ConfigFileName = "SharePointGateway.TestApp.Config.txt";

        static void Main()
        {
            var dataSourceInfo = LoadConfig();

            var listItemParser = new ListItemParser();

            var listItemRetriever = new ListItemRetriever(new SharePointConnector());
            var items = listItemRetriever.GetListItems(dataSourceInfo, listItemParser).Result.ToList();
        }

        public static DataSourceInfo LoadConfig()
        {
            var userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

            var configFilePath = Path.Combine(userFolder, ConfigFileName);

            var lines = File.ReadAllLines(configFilePath);

            var dataSourceInfo = new DataSourceInfo
            {
                SiteUri = lines[0],
                RestQueryData = new RestQueryData
                {
                    ListTitle = lines[1],
                    FilterQuery = lines[2],
                    SelectQuery = lines[3],
                    OrderBy = lines[4],
                    Expand = lines[5],
                    MaxResults = int.Parse(lines[6])
                },
                NetworkCredentials = CredentialUtil.GetCredential(CredentialsEntryName, PersistanceType.LocalComputer)
            };

            return dataSourceInfo;
        }
    }
}

