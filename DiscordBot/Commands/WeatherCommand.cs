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
                string url = "http://api.openweathermap.org/data/2.5/weather?q=" + cityName + "&appid=bac1df9616bdc059267e7c5ef270e005#";

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
        
    }
}
