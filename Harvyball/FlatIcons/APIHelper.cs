using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace FlatIcons
{
	public static class APIHelper
	{
		public static DirectoryInfo HarvyballDir
		{
			get
			{
				return Directory.CreateDirectory($"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\Harvyball");
            }
		}

		public static string TokenFilePath
		{
			get
			{
				return $"{HarvyballDir.FullName}\\token.json";
			}
		}

		public static async void SaveAccessTokenAsync(string apiKey)
		{
			try
			{
				if (!File.Exists(TokenFilePath))
				{
					var json = await RequestAccessTokenAsync(apiKey);
                    File.WriteAllText(TokenFilePath, json);
                }

                TokenInfo tokenInfo = TokenInfo.Deserialize(File.ReadAllText(TokenFilePath));
				DateTimeOffset expire = DateTimeOffset.FromUnixTimeSeconds(tokenInfo.Data.Expires);
				DateTimeOffset now = new DateTimeOffset(DateTime.Now);
				if (now > expire)
				{
					var json = await RequestAccessTokenAsync(apiKey);
					File.WriteAllText(TokenFilePath, json);
				}
            }
			catch (Exception)
			{

				throw;
			}
		}

		public static async Task<string> RequestAccessTokenAsync(string apiKey)
		{
			try
			{
                using (HttpClient client = new HttpClient())
                using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://api.flaticon.com/v3/app/authentication"))
                {
                    request.Headers.Add("Accept", "application/json");
                    MultipartFormDataContent content = new MultipartFormDataContent();
                    content.Add(new StringContent(apiKey), "apikey");
                    request.Content = content;
                    HttpResponseMessage respose = await client.SendAsync(request);
                    respose.EnsureSuccessStatusCode();
                    var json = await respose.Content.ReadAsStringAsync();
					return json;
                }
            }
			catch (Exception)
			{

				throw;
			}
		}

	}
}