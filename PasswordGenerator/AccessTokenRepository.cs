using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace PasswordGenerator
{
    public class AccessTokenRepository : IAccessTokenRepository
    {
        private readonly TimeSpan _ttl;
        private readonly MemoryCache _cache = new MemoryCache("tokens");

        public AccessTokenRepository(TimeSpan ttl)
        {
            _ttl = ttl;
        }

        public string Retrieve(string userId)
        {
            return _cache[userId] as string;
        }

        public void Add(string userId, string accessToken)
        {
            _cache.Add(userId, accessToken, DateTime.Now + _ttl);
        }
    }
}
