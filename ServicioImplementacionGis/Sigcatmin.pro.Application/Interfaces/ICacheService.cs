using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sigcatmin.pro.Application.Enums;

namespace Sigcatmin.pro.Application.Interfaces
{
    public interface ICacheService
    {
        void SetValue<T>(CacheKeysEnum key, T value, TimeSpan? expiration = null);
        T GetValue<T>(CacheKeysEnum key);
        bool ContainsKey(CacheKeysEnum key);
        void RemoveValue(CacheKeysEnum key);

    }
}
