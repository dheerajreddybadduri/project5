using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiWeather.MVVM.Models
{

     public class WeatherConstraints
     {
          public float latitude { get; set; }
          public float longitude { get; set; }
          public float generationtime_ms { get; set; }
          public int utc_offset_seconds { get; set; }
          public float elevation { get; set; }
          public Current_Weather current_weather { get; set; }
          public Daily_Units daily_units { get; set; }
          public Day daily { get; set; } = new Day();
          public ObservableCollection<Day2> day2 { get; set; } =
               new ObservableCollection<Day2>();
     }

     public class Current_Weather
     {
          public float temperature { get; set; }
          public float windspeed { get; set; }
          public float winddirection { get; set; }
          public float weathercode { get; set; }
          public string time { get; set; }
     }

     public class Daily_Units
     {
          public string time { get; set; }
          public string weathercode { get; set; }
          public string temperature_2m_max { get; set; }
          public string temperature_2m_min { get; set; }
     }

     public class Day
     {
          public string[] time { get; set; }
          public float[] weathercode { get; set; }
          public float[] temperature_2m_max { get; set; }
          public float[] temperature_2m_min { get; set; }
     }

     public class Day2
    {
          public string time { get; set; }
          public float weathercode { get; set; }
          public float temperature_2m_max { get; set; }
          public float temperature_2m_min { get; set; }
     }


}
