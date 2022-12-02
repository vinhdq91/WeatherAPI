using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAPI
{
    public class WebApiCall : IWebApiCall
    {
        private HttpClient _httpClient;
        public WebApiCall(IHttpClientFactory clientFactory)
        {
            _httpClient = clientFactory.CreateClient();
        }
        public async Task<WeatherModel> CallApi(string zipCode)
        {
            WeatherModel model = new WeatherModel();
            try
            {
                var response = await _httpClient.GetAsync($"http://api.weatherstack.com/current?access_key=610acf4c1d203448cd6f671955c5e8aa&query={zipCode}");

                var stringResult = await response.Content.ReadAsStringAsync();
                model = JsonConvert.DeserializeObject<WeatherModel>(stringResult);
                if (model.location == null && model.current == null)
                    return null;
            }
            catch (Exception ex)
            {
                return null;
            }
            return model;
        }
    }
}
