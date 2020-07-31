using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using Newtonsoft.Json;
using unirest_net.http;

namespace DiscordBot.Commands
{
    public class WeatherCommand : BaseCommandModule
    {

        string FILE_PATH = "/Users/kevindang/Projects/DiscordBot/DiscordBot/cities.json";

        [Command("weather")]
        [Description("Get the current weather of a city!")]
        public async Task Weather(CommandContext ctx, string cityName)
        {
            string json = "{\"cod\": \"404\",\"message\": \"city not found\"}";
            string name = "We couldn't find the city you were looking for.";
            string country = "";
            string temp = "";
            string feelsLike = "";
            string tempMax = "";
            string tempMin = "";
            string humidity = "";
            string weather = "";
            string description = "";


            using (WebClient wc = new WebClient())
            {
                string url = "http://api.openweathermap.org/data/2.5/weather?q=" + cityName + "&appid=#";

                try
                {
                    json = wc.DownloadString(url);
                }
                catch (WebException e)
                {
                    await ctx.Channel.SendMessageAsync("We couldn't find the city you were looking for.").ConfigureAwait(false);
                }

                var result = JsonConvert.DeserializeObject<MyWeatherClass.Root>(json);
                name = result.name;
                country = result.sys.country;
                temp = Math.Round(result.main.temp - 273.15, 2).ToString();
                feelsLike = Math.Round(result.main.feels_like - 273.15, 2).ToString();
                tempMax = Math.Round(result.main.temp_max - 273.15, 2).ToString();
                tempMin = Math.Round(result.main.temp_min - 273.15, 2).ToString();
                humidity = result.main.humidity.ToString() + "%";
                weather = result.weather[0].main;
                description = result.weather[0].description;

            }

            
                await ctx.Channel.SendMessageAsync("City name: " + name + ", " + country).ConfigureAwait(false);
                await ctx.Channel.SendMessageAsync("Temp: " + temp).ConfigureAwait(false);
                await ctx.Channel.SendMessageAsync("Feels Like: " + feelsLike).ConfigureAwait(false);
                await ctx.Channel.SendMessageAsync("Max Temp: " + tempMax).ConfigureAwait(false);
                await ctx.Channel.SendMessageAsync("Min Temp: " + tempMin).ConfigureAwait(false);
                await ctx.Channel.SendMessageAsync("Humidity: " + humidity).ConfigureAwait(false);
                await ctx.Channel.SendMessageAsync("Weather: " + weather).ConfigureAwait(false);
                await ctx.Channel.SendMessageAsync("Weather Description: " + description).ConfigureAwait(false);
            

        }

        [Command("forecast")]
        [Description("Get the forecast for the weather of a city for the next 5 days every (multiplier * 3) hours!")]
        public async Task ForeCast(CommandContext ctx, string cityName, [Description("Provides info for the weather every (multiplier * 3) hours.")] int multiplier = 8)
        {
            string json = "{\"cod\": \"404\",\"message\": \"city not found\"}";
            string name = "";
            string country = "";
            List<MyForecastClass.List> list;

            using (WebClient wc = new WebClient())
            {
                string url = "http://api.openweathermap.org/data/2.5/forecast?q=" + cityName + "&appid=bac1df9616bdc059267e7c5ef270e005#";

                try
                {
                    json = wc.DownloadString(url);
                }
                catch (WebException e)
                {
                    await ctx.Channel.SendMessageAsync("We couldn't find the city you were looking for.").ConfigureAwait(false);
                }

                var result = JsonConvert.DeserializeObject<MyForecastClass.Root>(json);

                name = result.city.name; 
                country = result.city.country;


                list = result.list;
            }

            if (multiplier < 1)
            {
                await ctx.Channel.SendMessageAsync("Please input a multiplier thats >= 1.").ConfigureAwait(false);
            }
            else
            {
                await ctx.Channel.SendMessageAsync("City name: " + name + ", " + country).ConfigureAwait(false);
                for (int i = 0; i < list.Count; i = i + multiplier)
                {
                    var dt_txt = list[i].dt_txt;
                    var temp = list[i].main.temp;
                    var description = list[i].weather[0].description;
                    var rain = list[i].rain.threeH;
                    await ctx.Channel.SendMessageAsync($"Index: {i} \n Time : {dt_txt} \n Temp : {temp} \n Description : {description} \n Rain (Vol in last 3 hours in mm) : {rain} \n \n").ConfigureAwait(false);
                }
            }

        }

        [Command("find")]
        public async Task Find(CommandContext ctx)
        {

        }

    }
}
