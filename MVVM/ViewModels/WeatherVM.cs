using MauiWeather.MVVM.Models;
using Newtonsoft.Json;
using PropertyChanged;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiWeather.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class WeatherVM
    {
        public async Task<Location> GetCoordinates(string place)
        {
            string apiKey = "AIzaSyDtH4_fUZQYHGDZFUlH5WGVu1AZe1gj244";
            string apiUrl = $"https://maps.googleapis.com/maps/api/geocode/json?address={Uri.EscapeDataString(place)}&key={apiKey}";

            // Create the REST client and request
            var client = new RestClient(apiUrl);
           var request = new RestRequest();
            // Execute the request and get the response
            var response = client.Execute(request);
            Location location =new Location();
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                // Parse the response JSON
                dynamic jsonResponse = JsonConvert.DeserializeObject(response.Content);
                 
                if (jsonResponse.results.Count > 0)
                {

                     double latitude = jsonResponse.results[0].geometry.location.lat;
                    double longitude = jsonResponse.results[0].geometry.location.lng;
                     location = jsonResponse.results[0].geometry.location;



                 }
                else
                {
                    Console.WriteLine("No results found for the place: " + place);

                }
            }
            else
            {
                Console.WriteLine("Geocoding request failed. Status code: " + response.StatusCode);
            }
            return location;
        }


        public WeatherConstraints WeatherConstraints { get; set; }
        public string  Location  { get; set; }
        public DateTime Date { get; set; } =
             DateTime.Now;

        public bool IsVisible { get; set; }
        public bool IsLoading { get; set; }

        private HttpClient client;

        public WeatherVM()
        {
            client = new HttpClient();
        }

        public ICommand SearchWeather =>
             new Command(async (searchText) =>
             {
                 Location = searchText.ToString();
                 var location= await  GetCoordinates(searchText.ToString());
                 await GetWeather(location);
             });


        private async Task GetWeather(Location location)
        {
            var url =
                 $"https://api.open-meteo.com/v1/forecast?latitude={location.Latitude}&longitude={location.Longitude}&daily=weathercode,temperature_2m_max,temperature_2m_min&current_weather=true&timezone=America%2FChicago";

            IsLoading = true;

            var response =
              await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    var data = await System.Text.Json.JsonSerializer.DeserializeAsync<WeatherConstraints>(responseStream);
                    WeatherConstraints = data;

                    for (int i = 0; i < WeatherConstraints.daily.time.Length; i++)
                    {
                        var day2 = new Day2
                        {
                            time = WeatherConstraints.daily.time[i],
                            temperature_2m_max = WeatherConstraints.daily.temperature_2m_max[i],
                            temperature_2m_min = WeatherConstraints.daily.temperature_2m_min[i],
                            weathercode = WeatherConstraints.daily.weathercode[i]
                        };
                        WeatherConstraints.day2.Add(day2);
                    }
                    IsVisible = true;
                }
            }
            IsLoading = false;
        }

       

    }
}
