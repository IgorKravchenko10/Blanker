using System;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;
using Blanker.Data;

namespace Blanker
{
    public class Adapter
    {

        public Adapter()
        {
        }

        private async Task<string> GetResponse(string url)
        {
            HttpClient client = new HttpClient();
            var resp = await client.GetAsync(url);
            var json = resp.Content.ReadAsStringAsync().Result;

            return json;

        }

        public async Task<List<Country>> GetCountries()
        {
            string json = await GetResponse(Properties.Resources.urlCountries);
            var countries = JsonConvert.DeserializeObject<RootCountries>(json);

            return countries.response;

        }

        public async Task<List<City>> GetCities(int countryId, string filter)
        {
            string json = await GetResponse(String.Format(Properties.Resources.urlCities, countryId, filter));
            var cities = JsonConvert.DeserializeObject<RootCities>(json);

            return cities.response;
        }

        public async Task<List<University>> GetUniversities(int countryId, int cityId, string filter)
        {
            string json = await GetResponse(String.Format(Properties.Resources.urlUniversities, filter, countryId, cityId));
            var universities = JsonConvert.DeserializeObject<RootUniversities>(EditJson(json));

            return universities.response;
        }

        private string EditJson(string json)
        {
            string newJson = json.Substring(0, json.IndexOf("[") + 1) + json.Substring(json.IndexOf(",")+1);

            return newJson;
        }

    }
}
