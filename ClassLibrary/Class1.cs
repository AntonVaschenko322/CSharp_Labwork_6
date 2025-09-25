using System.Text.Json.Serialization;

namespace ClassLibrary
{
    public class Weather
    {
        //[JsonPropertyName("main")]
        //public string Main { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }

    }

    public class Main
    {
        [JsonPropertyName("temp")]
        public double Temp { get; set; }
    }

    public class Sys
    {
        [JsonPropertyName("country")]
        public string country { get; set; }
    }

    public class Data
    {
        [JsonPropertyName("weather")]
        public Weather[] weather { get; set; }

        [JsonPropertyName("main")]
        public Main main { get; set; }

        [JsonPropertyName("sys")]
        public Sys sys { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

    }


    public struct Weathers
    {
        public string Country { get; set; }
        public string City { get; set; }
        public double Temp { get; set; }
        public string Description { get; set; }

        public Weathers(string country, string city, double temp, string description)
        {
            Country = country;
            City = city;
            Temp = temp;
            Description = description;
            
        }
    }



}
