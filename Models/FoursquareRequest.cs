using System.Globalization;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;

namespace Places.Models
{
    public class FoursquareRequest
    {
        [JsonProperty("query")]
        public string? Query { get; set; }

        [JsonProperty("longitude")]
        public decimal? Longitude { get; set; }

        [JsonProperty("latitude")]
        public decimal? Latitude { get; set; }

        [JsonProperty("radius")]
        public int? Radius { get; set; }

        [JsonProperty("categories")]
        public string? Categories { get; set; }

        [JsonProperty("chains")]
        public string? Chains { get; set; }

        [JsonProperty("exclude_chains")]
        public string? ExcludeChains { get; set; }

        [JsonProperty("exclude_all_chains")]
        public bool? ExcludeAllChains { get; set; }

        [JsonProperty("fields")]
        public string? Fields { get; set; }

        [JsonProperty("min_price")]
        public int? MinPrice { get; set; }

        [JsonProperty("max_price")]
        public int? MaxPrice { get; set; }

        [JsonProperty("open_at")]
        public string? OpenAt { get; set; }

        [JsonProperty("open_now")]
        public bool? OpenNow { get; set; }

        [JsonProperty("ne")]
        public string? Ne { get; set; }

        [JsonProperty("sw")]
        public string? Sw { get; set; }

        [JsonProperty("near")]
        public string? Near { get; set; }

        [JsonProperty("polygon")]
        public string? Polygon { get; set; }

        [JsonProperty("sort")]
        public string? Sort { get; set; }

        [JsonProperty("limit")]
        public int? Limit { get; set; }

        [JsonProperty("session_token")]
        public string? SessionToken { get; set; }

        public string GetQueryString()
        {
            StringBuilder queryStringBuilder = new StringBuilder();

            PropertyInfo[] properties = GetType().GetProperties();

            if(Latitude != null && Longitude != null){
                queryStringBuilder.Append("ll=");
                queryStringBuilder.Append(Uri.EscapeDataString($"{DecimalToString(Latitude)},{DecimalToString(Longitude)}"));
                queryStringBuilder.Append("&");
            }

            foreach (PropertyInfo property in properties)
            {
                var jsonProperty = property.GetCustomAttribute<JsonPropertyAttribute>();
                if (jsonProperty != null)
                {
                    object value = property.GetValue(this, null);
                    if (value != null)
                    {
                        if (jsonProperty.PropertyName == "longitude" || jsonProperty.PropertyName == "latitude")
                        {
                            continue;
                        }

                        queryStringBuilder.Append(jsonProperty.PropertyName);
                        queryStringBuilder.Append("=");
                        queryStringBuilder.Append(Uri.EscapeDataString(value.ToString()));
                        queryStringBuilder.Append("&");
                    }
                }
            }

            if (queryStringBuilder.Length > 0)
            {
                queryStringBuilder.Length--;
            }

            return queryStringBuilder.ToString();
        }

        string DecimalToString(decimal? number){
            return ((decimal)number).ToString("0.################", CultureInfo.InvariantCulture);
        }
    }
}
