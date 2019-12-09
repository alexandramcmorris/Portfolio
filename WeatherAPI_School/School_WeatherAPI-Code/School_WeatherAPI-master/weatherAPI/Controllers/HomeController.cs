using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using weatherAPI.Classes;
using weatherAPI.Models;

namespace weatherAPI.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string cityName)
        {
            WeatherModel wm = new WeatherModel();

            string apiKey = "ca862bc98586462259cc6d14ec1037d1";
            HttpWebRequest apiRequest = WebRequest.Create("http://api.openweathermap.org/data/2.5/weather?q=" + cityName + "&appid=" + apiKey + "&units=metric") as HttpWebRequest;

            string apiResponse = "";
            using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                apiResponse = reader.ReadToEnd();
            }

            ResponseWeather rootObject = JsonConvert.DeserializeObject<ResponseWeather>(apiResponse);

            wm.cityName = cityName;
            wm.description = rootObject.weather[0].description;
            wm.clouds = rootObject.clouds.all.ToString();
            wm.humidity = rootObject.main.humidity.ToString();        
            wm.pressure = rootObject.main.pressure.ToString();
            wm.temp = Math.Round(((rootObject.main.temp * (9.00/5.00)) + 32), 0).ToString();
            wm.wind = rootObject.wind.speed.ToString();

            string pants = AppDomain.CurrentDomain.BaseDirectory;

            if (wm.description.Contains("rain"))
                wm.imageURL = "~/Images/rain.png";
            else if (wm.description.Contains("clear"))
                wm.imageURL = "~/Images/sky.png";
            else if (wm.description.Contains("clouds"))
                wm.imageURL = "~/Images/cloud.png";
            else if (wm.description.Contains("snow"))
                wm.imageURL = "~/Images/snow.png";
            else
                wm.imageURL = "~/Images/sky.png";

            TempData["weather"] = wm;

            return RedirectToAction("ShowWeather");
        }

        public ActionResult ShowWeather()
        {
            WeatherModel wm = new WeatherModel();

            wm = (WeatherModel)TempData["weather"];

            return View(wm);
        }

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
    }
}