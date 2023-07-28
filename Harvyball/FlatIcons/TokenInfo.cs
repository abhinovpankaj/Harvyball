using Newtonsoft.Json;

namespace FlatIcons
{
	public partial class TokenInfo
	{
        [JsonProperty("data")]
        public Data Data { get; set; }
    }

	public class Data
	{
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("expires")]
        public long Expires { get; set; }
    }

    public partial class TokenInfo
    {
        public static string Serialize(TokenInfo tokenInfo)
        {
            return JsonConvert.SerializeObject(tokenInfo);
        }

        public static TokenInfo Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<TokenInfo>(json);
        }
    } 
}