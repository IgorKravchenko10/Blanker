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

        public async Task<List<Country>> GetCountries()
        {
            string urlCountries = "https://api.vk.com/method/database.getCountries?need_all=1&count=1000";
            HttpClient client = new HttpClient();
            var resp = await client.GetAsync(urlCountries);

            var json = resp.Content.ReadAsStringAsync().Result;

            var countries = JsonConvert.DeserializeObject<RootCountries>(json);

            return countries.response;

        }

        public async Task<List<City>> GetCities(int countryId, string filter)
        {
            string urlCities = String.Format("https://api.vk.com/method/database.getCities?country_id={0}&q={1}", countryId, filter);
            HttpClient client = new HttpClient();
            var resp = await client.GetAsync(urlCities);

            var json = resp.Content.ReadAsStringAsync().Result;

            var cities = JsonConvert.DeserializeObject<RootCities>(json);

            return cities.response;
        }

        public async Task<List<University>> GetUniversities(int countryId, int cityId, string filter)
        {
            string urlUnivirsities = String.Format("https://api.vk.com/method/database.getUniversities?q={0}&country_id={1}&city_id={2}", filter, countryId, cityId);
            HttpClient client = new HttpClient();
            var resp = await client.GetAsync(urlUnivirsities);

            var json = resp.Content.ReadAsStringAsync().Result;

            var universities = JsonConvert.DeserializeObject<RootUniversities>(json);

            return universities.response;
        }

    }
}
