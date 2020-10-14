using Library.Core.Requests;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Clients
{
    /// <summary>
    /// Defines Microblink client
    /// </summary>
    public class MicroblinkClient
    {
        private readonly HttpClient httpClient;
        private readonly MicroblinkSettings microblinkSettings;

        private readonly string InfoEndpoint = "/v1/info";
        private readonly string MRTDEndpoint = "/v1/recognizers/mrtd";


        /// <summary>
        /// Initializes new instance of <see cref="MicroblinkClient"/>.
        /// </summary>
        /// <param name="httpClient">Http client.</param>
        /// <param name="">Authorization settings.</param>
        public MicroblinkClient(HttpClient httpClient, IOptions<MicroblinkSettings> microblinkSettings)
        {
            this.microblinkSettings = microblinkSettings?.Value ?? throw new ArgumentNullException(nameof(microblinkSettings));

            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            this.httpClient.BaseAddress = new Uri(this.microblinkSettings.Host);
        }

        /// <summary>
        /// Call Get Info from microblink cloud api
        /// </summary>
        /// <returns>Returns response as string</returns>
        public async Task<string> GetInfo()
        {
            var infoUri = new Uri(InfoEndpoint, UriKind.Relative);

            HttpResponseMessage response = await httpClient.GetAsync(infoUri);

            response.EnsureSuccessStatusCode();

            return await ReadStringResult(response);
        }

        /// <summary>
        /// Call MRTDRecognizer from microblink cloud api
        /// </summary>
        /// <param name="request">MRTD request, only imageSource</param>
        /// <returns>Returns rawMrzString data from MRTDRecognizer</returns>
        public async Task<string> MRTDRecognizer(MRTDRequest request)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", microblinkSettings.AuthorizationHeader);

            var mrtdUri = new Uri(MRTDEndpoint, UriKind.Relative);

            var requestContent = JsonConvert.SerializeObject(request);

            using (var content = new StringContent(requestContent, Encoding.UTF8, MediaTypeNames.Application.Json))
            {
                HttpResponseMessage response = await httpClient.PostAsync(mrtdUri, content);

                response.EnsureSuccessStatusCode();

                return await ReadStringResult(response);
            }
        }

        /// <summary>
        /// Get string result from HttpResponseMessage object
        /// </summary>
        /// <param name="response">HttpMessage from api</param>
        /// <returns></returns>
        private async Task<string> ReadStringResult(HttpResponseMessage response)
        {
            //Read body 
            string responseBody = await response.Content.ReadAsStringAsync();

            return responseBody;
        }
    }
}
