//using MauiWeather.MVVM.Models;
//using Newtonsoft.Json;
//using PropertyChanged;
//using RestSharp;
//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Linq;
//using System.Text;
//using System.Text.Json;
//using System.Threading.Tasks;
//using System.Windows.Input;

//namespace MauiWeather.MVVM.ViewModels
//{
//    [AddINotifyPropertyChangedInterface]
//    public class WeatherVM
//    {
//        public async Task<Location> GetCoordinates(string place)
//        {
//            string apiKey = "AIzaSyDtH4_fUZQYHGDZFUlH5WGVu1AZe1gj244";
//            string apiUrl = $"https://maps.googleapis.com/maps/api/geocode/json?address={Uri.EscapeDataString(place)}&key={apiKey}";

//            var client = new RestClient(apiUrl);
//            var request = new RestRequest();
//            var response = client.Execute(request);
//            Location location = new Location();
//            if (response.StatusCode == System.Net.HttpStatusCode.OK)
//            {
//                // Parse the response JSON
//                dynamic jsonResponse = JsonConvert.DeserializeObject(response.Content);

//                if (jsonResponse.results.Count > 0)
//                {

//                    double latitude = jsonResponse.results[0].geometry.location.lat;
//                    double longitude = jsonResponse.results[0].geometry.location.lng;
//                    location = jsonResponse.results[0].geometry.location;

//                }
//                else
//                {
//                    Console.WriteLine("No results found for the place: " + place);

//                }
//            }
//            else
//            {
//                Console.WriteLine("Geocoding request failed. Status code: " + response.StatusCode);
//            }
//            return location;
//        }


//        public WeatherConstraints WeatherConstraints { get; set; }
//        public string Location { get; set; }
//        public DateTime Date { get; set; } =
//             DateTime.Now;

//        public bool IsVisible { get; set; }
//        public bool IsLoading { get; set; }

//        private HttpClient client;

//        public WeatherVM()
//        {
//            client = new HttpClient();
//        }

//        public ICommand SearchWeather =>
//             new Command(async (searchText) =>
//             {
//                 Location = searchText.ToString();
//                 var location = await GetCoordinates(searchText.ToString());
//                 await GetWeather(location);
//             });


//        private async Task GetWeather(Location location)
//        {
//            var url =
//                 $"https://api.open-meteo.com/v1/forecast?latitude={location.Latitude}&longitude={location.Longitude}&daily=weathercode,temperature_2m_max,temperature_2m_min&current_weather=true&timezone=America%2FChicago";

//            IsLoading = true;

//            var response =
//              await client.GetAsync(url);

//            if (response.IsSuccessStatusCode)
//            {
//                using (var responseStream = await response.Content.ReadAsStreamAsync())
//                {
//                    var data = await System.Text.Json.JsonSerializer.DeserializeAsync<WeatherConstraints>(responseStream);
//                    WeatherConstraints = data;

//                    for (int i = 0; i < WeatherConstraints.new ate.Length; i++)
//                    {
//                        var nextDay = new NextDay
//                        {
//                            date = WeatherConstraints.daily.date[i],
//                            maxTemperature = WeatherConstraints.daily.maxTemperature[i],
//                            minTemperature = WeatherConstraints.daily.minTemperature[i],
//                            weathercode = WeatherConstraints.daily.weathercode[i]
//                        };
//                        WeatherConstraints.nextDay.Add(nextDay);
//                    }
//                    IsVisible = true;
//                }
//            }
//            IsLoading = false;
//        }

//        //private async Task<Location> GetCoordinatesAsync(string address)
//        //{
//        //    IEnumerable<Location> locations = await GetCoordinates();

//        //    Location location = locations?.FirstOrDefault();

//        //    if (location != null)
//        //        Console
//        //             .WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
//        //    return location;
//        //}

//    }
//}




//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace MauiWeather.MVVM.Models
//{

//    public class WeatherConstraints
//    {
//        public float latitude { get; set; }
//        public float longitude { get; set; }
//        public float generationtime_ms { get; set; }
//        public int utc_offset_seconds { get; set; }
//        public float elevation { get; set; }
//        public Current_Weather current_weather { get; set; }
//        public Daily_Units daily_units { get; set; }
//        public Day daily { get; set; } = new Day();
//        public ObservableCollection<NextDay> nextDay { get; set; } =
//           new ObservableCollection<NextDay>();
//    }

//    public class Current_Weather
//    {
//        public float temperature { get; set; }
//        public float windspeed { get; set; }
//        public float winddirection { get; set; }
//        public float weathercode { get; set; }
//        public string time { get; set; }
//    }

//    public class Daily_Units
//    {
//        public string date { get; set; }
//        public string weathercode { get; set; }
//        public string maxTemperature { get; set; }
//        public string minTemperature { get; set; }
//    }

//    public class Day
//    {
//        public string[] date { get; set; }
//        public float[] weathercode { get; set; }
//        public float[] maxTemperature { get; set; }
//        public float[] minTemperature { get; set; }
//    }

//    public class NextDay
//    {
//        public string date { get; set; }
//        public float weathercode { get; set; }
//        public float maxTemperature { get; set; }
//        public float minTemperature { get; set; }
//    }


//}


