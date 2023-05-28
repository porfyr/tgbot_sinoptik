using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace tgbot_synoptyk.Parsing
{
    class HtmlLoader
    {
        readonly HttpClient client;
        readonly string url;

        public HtmlLoader(IParserSettings settings)
        {
            client = new HttpClient();
            url = $"{settings.BaseUrl}{settings.Sufix}";
        }

        public async Task<string> GetSourcePageAsync()
        {
            var response = await client.GetAsync(url);

            if (response != null && response.StatusCode == HttpStatusCode.OK)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                Console.WriteLine("response = null || щось інше");
                return "Гімна на лопаті а не сторінку";
            }
        }
    }
}
