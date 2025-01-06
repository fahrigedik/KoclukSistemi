using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.CoachSystem.Service.Client
{
    public class SOAPCoachRegisterClient
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task<string> CallSoapWebService(string soapEndpoint, string soapXml)
        {
            try
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(soapEndpoint),
                    Content = new StringContent(soapXml, Encoding.UTF8, "text/xml")
                };

                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();  // Ensure we got a valid response (2xx HTTP code)

                string responseContent = await response.Content.ReadAsStringAsync();
                return responseContent; // Return the SOAP response
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }
    }
}
