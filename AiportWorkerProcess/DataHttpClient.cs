using AirportData.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using AirportData.Repository;
using AirportCore.Service;
using AirportCore;
using AirportData;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions;
using Microsoft.Extensions.Configuration;
using System.IO;
using AirportCore.Services;

namespace AiportWorkerProcess
{
    public class DataHttpClient
    {
        public static IConfiguration Configuration { get; set; }

        public const string Airport = "airport";

        public const string continent = "EU";
        static void Main(string[] args)
        {

            Task apiProcess = new Task(AirportApiCall);
            apiProcess.Start();
            Console.WriteLine("Json data........");
            Console.ReadLine();
        }

        static async void AirportApiCall()
        {
            using (var client = new System.Net.Http.HttpClient())
            {
                var builder = new ConfigurationBuilder();
                builder.SetBasePath(Directory.GetCurrentDirectory());
                builder.AddJsonFile("appsettings.json");
                Configuration = builder.Build();
                var databaseConnectionString = Configuration["Database"];
                var apiURL = Configuration["ApiURL"];

                HttpResponseMessage response = await client.GetAsync(apiURL);
                response.EnsureSuccessStatusCode();

                using (HttpContent content = response.Content)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseBody.Substring(0, 50) + "........");
                    var deserializedData = JsonConvert.DeserializeObject<IEnumerable<AirportDetails>>(responseBody);
                    var europeAirports = deserializedData.Where(x => x.Type.ToLower() == Airport && x.Continent.ToUpper().Equals(continent)).ToList();
                    SaveandClearDatabase(europeAirports, databaseConnectionString);
                }
            }
        }
        public static void SaveandClearDatabase(IEnumerable<AirportDetails> airportDetails, string ConnectionString)
        {
            try
            {
                IServiceProvider serviceProvider = new ServiceCollection()
               .AddSingleton<IAirportRepository, AirportRepository>()
               .AddSingleton<IAirportConnector, AirportConnector>()
               .AddSingleton<ICachingService, CachingService>()
               .AddDbContext<AirportDbContext>(options => options.UseSqlServer(ConnectionString))
               .BuildServiceProvider();
                var repository = serviceProvider.GetService<IAirportConnector>();
                repository.clearDatabase();
                repository.addAirportDetailsinDatabase(airportDetails);
                Console.WriteLine("Successfully Added");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error is : ",e);
            }
        }
    }
}
