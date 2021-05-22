using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WepApi_1.Models;

namespace WepApi_1.Controllers
{
    public class ColorsController : ApiController
    {
        [HttpGet, Route("colors/{color:enum(WepApi_1.Models.ColorsEnum)}")]
        public string GetColor(ColorsEnum color)
        //public string GetColor(string color)
        {
            return color.ToString();
        }
    }
}
