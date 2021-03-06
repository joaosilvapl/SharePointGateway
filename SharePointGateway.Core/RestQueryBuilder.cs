﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharePointGateway.Core
{
    internal class RestQueryBuilder
    {
        public string Build(RestQueryData restQueryData)
        {
            var expandClause = restQueryData.Expand == null ? null : $"&$expand={restQueryData.Expand}";

            return
                $"/_api/web/lists/GetByTitle('{restQueryData.ListTitle}')/items?$filter={restQueryData.FilterQuery}&$select={restQueryData.SelectQuery}&$orderby={restQueryData.OrderBy}&$top={restQueryData.MaxResults}{expandClause}";
        }
    }
}
