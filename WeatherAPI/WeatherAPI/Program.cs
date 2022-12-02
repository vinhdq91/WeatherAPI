using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace WeatherAPI
{
    public class Program
    {
        private static ServiceProvider serviceProvider = null;
        public async static Task<WeatherModel> CallWeatherApi(string zipCode)
        {
            if (serviceProvider == null)
            {
                serviceProvider = new ServiceCollection().AddHttpClient()
                                                         .AddTransient<IWebApiCall, WebApiCall>()
                                                         .BuildServiceProvider();
            }
            
            var webApi = serviceProvider.GetService<IWebApiCall>();
            WeatherModel result = await webApi.CallApi(zipCode/*"30076"*/);

            return result;
        }

        public static async Task GiveWeatherAdvices()
        {
            Console.Write("Enter the zip code: ");
            string zipCode = Console.ReadLine();
            WeatherModel model = await CallWeatherApi(zipCode);
            if (model != null)
            {
                var condition = model.current;
                bool isContinue = false;
                while (!isContinue)
                {
                    Console.WriteLine("===============Location Information=====================");
                    Console.WriteLine("Name: " + model.location.name);
                    Console.WriteLine("Country: " + model.location.country);
                    Console.WriteLine("Region: " + model.location.region);
                    Console.WriteLine("====================================");
                    foreach (var quest in QuestionList.ListQuestions())
                    {
                        Console.WriteLine(quest.Key + " - " + quest.Value);
                    }
                    Console.WriteLine("====================================");
                    Console.Write("Choose the order of question you want to ask: ");
                    var questNumber = Console.ReadLine();
                    string answer = string.Empty;
                    switch (questNumber)
                    {
                        case "1":
                            if (condition.weather_descriptions.Contains("rain"))
                                answer = "No, it's raining";
                            else
                                answer = "Yes, the weather now: " + string.Join(',', condition.weather_descriptions);
                            break;
                        case "2":
                            if (condition.uv_index > 3)
                                answer = "Yes, the uv is: " + condition.uv_index;
                            else
                                answer = "No, the uv is: " + condition.uv_index;
                            break;
                        case "3":
                            if (condition.weather_descriptions.Contains("rain") &&
                                condition.wind_speed > 15)
                                answer = "No, it's raining and win speed is: " + condition.wind_speed;
                            else
                                answer = "Yes, it's not raining and win speed is: " + condition.wind_speed;
                            break;
                        default:
                            answer = "Invalid input";
                            break;
                    }
                    Console.WriteLine(answer);

                    Console.WriteLine("**************************************************");
                    Console.Write("Do you want to change location? (Y/N): ");
                    string changeLocate = Console.ReadLine();
                    isContinue = changeLocate == "Y" || changeLocate == "y" ? true : false;
                    if (isContinue)
                    {
                        Console.Write("Enter the zip code: ");
                        zipCode = Console.ReadLine();
                        model = await CallWeatherApi(zipCode);
                        condition = model.current;
                        isContinue = false;
                    }
                }
            }
            else
                Console.WriteLine("No data found !");
        }

        static void Main(string[] args)
        {
            Task.Run(async () =>
            {
                await GiveWeatherAdvices();
            }).GetAwaiter().GetResult();
            Console.WriteLine("Hello World!");
        }
    }
}
