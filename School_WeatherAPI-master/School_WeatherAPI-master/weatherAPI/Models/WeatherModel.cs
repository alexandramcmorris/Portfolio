using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace weatherAPI.Models
{
    public class WeatherModel
    {
        public string cityName { get; set; }
        public string description { get; set; }
        public string temp { get; set; }
        public string pressure { get; set; }
        public string wind { get; set; }
        public string clouds { get; set; }
        public string humidity { get; set; }
        public string imageURL { get; set; }
    }
}