using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace DotNet8BlazorWebApp.Extensions
{
  public static class StringExtensions
    {
        public static T ConvertJsonTo<T>(this string jsonString)
        {
            return  JsonSerializer.Deserialize<T>(jsonString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }
}