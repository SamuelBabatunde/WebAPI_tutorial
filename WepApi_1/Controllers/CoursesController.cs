using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WepApi_1.Extensions;

namespace WepApi_1.Controllers
{

    [RoutePrefix("courses")]
    public class CoursesController : ApiController
    {

        //GET courses/
        [HttpGet, Route("")]
        public IEnumerable<string> Get()
        {

            var getByIdUrl = Url.Link("GetById", new { id = 123 });

            //return new string[] { "Math", "English", Request.GetApiKey() };
            return new string[] { 
                $"getByIdUrl : {getByIdUrl}",
                $"SelfReferenceBaseUrl : {Request.GetSelfReferenceBaseUrl()}" ,
                $"RebasedUrlForClient : {Request.RebaseUrlForClient(new Uri(getByIdUrl)).ToString()}" ,
                $"ApiKey: {Request.GetApiKey() }"
            };
        }


        //GET courses/5
        [HttpGet, Route("{id:int}",Name ="GetById")]
        public string Get(int id)
        {
            return "Math";
        }
    }
}
