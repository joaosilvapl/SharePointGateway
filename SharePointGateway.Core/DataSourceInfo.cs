using System.Net;

namespace SharePointGateway.Core
{
    public class DataSourceInfo
    {
        public string SiteUri;
        public string ListTitle;
        public string FilterQuery;
        public string SelectQuery;
        public string OrderBy;
        public int MaxResults;
        public NetworkCredential NetworkCredentials;
    }
}