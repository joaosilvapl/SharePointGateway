﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using SharePointGateway.Core;

namespace SharePointGateway.TestApp
{
    class Program
    {
        static void Main()
        {
            var dataSourceInfo = new DataSourceInfo
            {
                SiteUri = "[yoursiteUrl]",
                RestQueryData = new RestQueryData
                {
                    ListTitle = "MyList",
                    FilterQuery = "ID eq 12833 or ID eq 18912",
                    SelectQuery = "Id,Title,TimeSpent,Modified",
                    OrderBy = "Modified desc",
                    MaxResults = 2000,
                },
                NetworkCredentials = new NetworkCredential("userName", "password", "domain")
            };

            var listItemParser = new BasicListItemParser();

            var listItemRetriever = new ListItemRetriever(new SharePointConnector());
            var items = listItemRetriever.GetListItems<BasicListItemData>(dataSourceInfo, listItemParser).Result;
        }
    }
}
