using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballStats.API.Infrastructure.Configuration
{
    public class ExternalServicesConfig
    {
        public const string FootballApi = "FootballApi";
        public const string ServiceApi = "ServiceApi";

        public string Url { get; set; }
        public int MinsToCache { get; set; }
        public string ApiKey { get; set; }
    }
}
