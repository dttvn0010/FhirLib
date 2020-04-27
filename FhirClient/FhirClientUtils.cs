using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using LightJson;

namespace FhirClient
{
    class FhirClientUtils
    {
        private static string createQueryString(Dictionary<string, string> param)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < param.Keys.Count; i++)
            {
                string key = param.Keys.ElementAt(i);
                string value = param[key];
                builder.Append(key).Append("=").Append(value);
                if (i + 1 < param.Keys.Count)
                {
                    builder.Append("&");
                }
            }
            return builder.ToString();
        }

        public static string get(string url, Dictionary<string, string> param)
        {
            HttpClient client = new HttpClient();
            if (param != null)
            {
                url += "?" + createQueryString(param);
            }

            var response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsStringAsync().Result;                
            }

            return null;
        }

        public static string post(string url, string body)
        {
            HttpClient client = new HttpClient();
            var content = new StringContent(body, Encoding.UTF8, "application/json");
            var response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsStringAsync().Result;
            }

            return null;
        }

    }
}
