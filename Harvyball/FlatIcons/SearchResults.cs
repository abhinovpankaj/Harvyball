using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;
using System.Windows.Media;

namespace FlatIcons
{
	public partial class SearchResults
	{
        [JsonProperty("data")]
        public List<Icon> Icons { get; set; }

        [JsonProperty("metadata")]
        public Metadata Metadata { get; set; }
    }

    public partial class SearchResults
    {
        public static string Serialize(SearchResults searchResult)
        {
            return JsonConvert.SerializeObject(searchResult);
        }

        public static SearchResults Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<SearchResults>(json);
        }
    }

    public class Icon
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("colors")]
        public string Colors { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("shape")]
        public string Shape { get; set; }

        [JsonProperty("family_id")]
        public long FamilyId { get; set; }

        [JsonProperty("family_name")]
        public string FamilyName { get; set; }

        [JsonProperty("team_name")]
        public string TeamName { get; set; }

        [JsonProperty("added")]
        public long Added { get; set; }

        [JsonProperty("pack_id")]
        public long PackId { get; set; }

        [JsonProperty("pack_name")]
        public string PackName { get; set; }

        [JsonProperty("pack_items")]
        public long PackItems { get; set; }

        [JsonProperty("tags")]
        public string Tags { get; set; }

        [JsonProperty("equivalents")]
        public long Equivalents { get; set; }

        [JsonProperty("images")]
        public Dictionary<string, string> Images { get; set; }

        public string Thumbnail
        {
            get
            {
                string _thumbnail = "";

                if (Images.Any())
                {
                    _thumbnail = Images["64"];
                }

                return _thumbnail;
            }
        }
    }

    public partial class Metadata
    {
        [JsonProperty("page")]
        public long Page { get; set; }

        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("total")]
        public long Total { get; set; }
    }
}