using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace timezonedb
{
    public class RestService
    {
        HttpClient _client;

        public RestService()
        {
            _client = new HttpClient();
        }

        public async Task<TimeZoneData> GetTZData(string query)
        {
            TimeZoneData tzData = null;
            try
            {
                var response = await _client.GetAsync(query);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    tzData = JsonConvert.DeserializeObject<TimeZoneData>(content);
                    JObject d = JObject.Parse(content);
                    Debug.WriteLine(d.Property("Title"));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\t\tERROR {0}", ex.Message);
            }

            return tzData;
        }
    }
}