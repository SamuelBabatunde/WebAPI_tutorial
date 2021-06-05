using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using WepApi_1.Models;
using WepApi_1.Models.CustomModelBinder;

namespace WepApi_1.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

        // example base64 binary data.converts to a simple json object:

        //ew0KICAibnVtYmVyIjogMTIzLA0KICAib2JqZWN0Ijogew0KICAgICJhIjogImIiLA0KICAgICJjIjogImQiLA0
        //ffKICAgICJIIjogImYiDQogICJzdHJpbmciOiAiSGVsbG8gV29ybGQiDQp9

        [HttpGet, Route("binary/{array:base64:maxlength(512)}")]
        public string GetBinaryArray([ModelBinder(typeof(Base64ModelBinder))] byte[] array)
        {
            return System.Text.Encoding.UTF8.GetString(array);
        }

        [HttpGet, Route("complex")]
        public IHttpActionResult ComplexTest([FromUri] ComplexTypeDto obj)
        {
            return Json(obj);
        }

        [HttpPut, Route("BodyTest")]
        public string BodyTest([FromBody] string val)
        {
            return val;
        }

        //Wildcard Example 1
        [HttpGet, Route("dates/{*date:datetime}")]
        public string GetDate(DateTime date)
        {
            return date.ToLongDateString();
        }

        //Wildcard Example 2
        [HttpGet, Route("segments/{*array:maxlength(256)}")]
        public string[] GetSegments([ModelBinder(typeof(StringArrayWildCardBinder))]  string[] array)
        {
            return array;
        }
    }
}
