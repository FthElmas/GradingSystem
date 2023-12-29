using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GradingSystem.BLL.Extensions
{
    public static class SessionExtension
    {
        public static void SetSession<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T? GetSession<T>(this ISession session, string key)
        {
            var value = session.GetString(key);

            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }


    }
}
