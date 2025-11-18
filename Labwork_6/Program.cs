using ClassLibrary;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
namespace Labwork_6
{
    public class Program
    {

        static async Task Main()
        {
            double l_min = -180;
            double l_max = 180;
            double s_min = -90;
            double s_max = 90;
            Random rnd = new();
            List<Weathers>dat = new();
            using HttpClient cli = new HttpClient();
            for (int i = 0; i < 50;)
            {
                double l_range = l_min + (rnd.NextDouble() * (l_max - l_min));
                double s_range = s_min + (rnd.NextDouble() * (s_max - s_min));
                HttpResponseMessage response = await cli.GetAsync($"https://api.openweathermap.org/data/2.5/weather?lat={s_range}&lon={l_range}&appid=02259164e7b6038f60788609f6c8d84e");
                string json = await response.Content.ReadAsStringAsync();
                Data wes = JsonSerializer.Deserialize<Data>(json);
                if (wes.Name == null || wes.sys.country == null)
                {
                    continue;
                }
                else
                {
                    Weathers x = new Weathers(wes.sys.country, wes.Name, wes.main.Temp, wes.weather[0].Description);
                    dat.Add(x);
                    i++;
                }
            }

            List<Weathers> TempCountrys = dat.OrderByDescending(x => x.Temp).ToList();
            Weathers MaxHot = dat.MaxBy(x => x.Temp);
            Weathers MaxCold = dat.MinBy(x => x.Temp);

            Console.WriteLine($"Самая большая температура в { MaxHot.Country } {MaxHot.City} {MaxHot.Temp}");
            Console.WriteLine($"Самая низкая температура в {MaxCold.Country}  {MaxCold.City} {MaxCold.Temp}");
            var AverTemp = dat.Average(x => x.Temp);
            Console.WriteLine($"Средняя температура по Миру: { AverTemp} ");

            var CountryCount = dat.Select(x => x.Country).Distinct().Count();
            Console.WriteLine($"Всего стран: {CountryCount} ");

            var FrstCountryCS = dat.Where(x => x.Description == "clear sky").FirstOrDefault();
            Console.WriteLine($"Первая страна у которой чистое небо {FrstCountryCS.Country}");

            var FrstCountryRain = dat.Where(x => x.Description == "rain").FirstOrDefault();
            Console.WriteLine($"Первая страна у которой дождь {FrstCountryRain.Country}");

            var FrstCountryFC = dat.Where(x => x.Description == "few clouds").FirstOrDefault();
            Console.WriteLine($"Первая страна у которой облачно {FrstCountryFC.Country}");


            foreach (var x in dat)
            {
                Console.WriteLine(x.Country + " " + x.City + " " + x.Temp + " " + x.Description);
            }
        }
    }

}

