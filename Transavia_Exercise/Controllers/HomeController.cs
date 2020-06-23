using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AiportWorkerProcess;
using AirportCore;
using AirportCore.Models;
using AirportData;
using AirportData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Transavia_Exercise.Funtions;
using Transavia_Exercise.Models;

namespace Transavia_Exercise.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAirportConnector _airportConnector;

        private const string sourceFrom = "FROM-DATABASE";

        private const string ErrorMessage = "Sorry! You can add only EU Airport";

        private const string SuccessMessage = "Added Successfully!";

        public HomeController(IAirportConnector airportConnector)
        {
            _airportConnector = airportConnector;
        }

        public async Task<IActionResult> Index(int? page, string iso)
        {
            Airport airport = new Airport();
            ViewData["iso"] = iso;
            int pageSize = 10;
            AirportResponse response;
            if (!string.IsNullOrEmpty(iso))
            {
                response = await _airportConnector.GetAllAirports(iso.ToUpper());
                
            }
            else
            {
                response = await _airportConnector.GetAllAirports();
            }
            airport.AirportDetails = (PaginatedList<AirportDetails>)PaginatedList<AirportDetails>.Create(response.Airports.AsQueryable(), page ?? 1, pageSize);
            airport.DataFrom = response.SourceFrom;
            
                Request.HttpContext.Response.Headers.Add("Received", airport.DataFrom);
            
            return this.View(airport);
        }

        [HttpGet("[action]")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost("[action]")]
        public IActionResult Create([Bind("Iata,Longitude,Iso,Status,Name,Continent,Type,Latitude,Size")] AirportDetails airportDetails)
        {
            var status = _airportConnector.addAirportinDatabase(airportDetails);
            if(!status)
            {
                ViewBag.result = ErrorMessage;
                return this.View("~/Views/Home/Create.cshtml");
            }
            ModelState.Clear();
            ViewBag.result = SuccessMessage;
            return this.View("~/Views/Home/Create.cshtml");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
