using System.Collections.Generic;

namespace Net45WithNetCoreConfigApi.Configurations.Feed
{
    public class FeedOptions
    {
        public bool IsActive { get; set; }
        public List<Endpoint> Endpoints { get; set; }
    }
}
