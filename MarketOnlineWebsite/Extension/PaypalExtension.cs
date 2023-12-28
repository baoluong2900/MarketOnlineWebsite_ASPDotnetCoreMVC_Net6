using PayPal.Api;

namespace MarketOnlineWebsite.Extension
{
	public static class PaypalExtension
	{
		static PaypalExtension()
		{

		}
		public static Dictionary<string, string> GetConfig(string mode)
		{
			return new Dictionary<string, string>()
			{
				{"mode",mode }
			};

		}

		public static string GetAccessToken(string ClientId, string ClientSecret, string mode)
		{
			string accessToken = new OAuthTokenCredential(ClientId, ClientSecret, new Dictionary<string, string>
			{
				{"mode",mode }
			}).GetAccessToken();
			return accessToken;
		}
		public static APIContext GetAPIContext(string clientId, string clientSecret, string mode)
		{
			APIContext context = new APIContext(GetAccessToken(clientId, clientSecret, mode));
			context.Config = GetConfig(mode);
			return context;
		}
	}
}
