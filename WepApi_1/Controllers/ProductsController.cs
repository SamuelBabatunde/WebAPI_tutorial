using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WepApi_1.Controllers
{
    [RoutePrefix("api/products")]
    public class ProductsController : ApiController
    {

        #region Example 1
        // GET: api/Products

        [HttpGet, Route("")]
        [AcceptVerbs("GET","VIEW","BREW")]
        [Route("~/prods")]

        public IEnumerable<string> Get()
        {
            return new string[] { "product1", "product2" };
        }

        // GET: api/Products/5/
        [HttpGet, Route("{id:int:range(1000,3000)}", Name = "GetById")]

        public string Get(int id)
        {
            return $"product-" + id;
        }

        // GET: api/Products/status/a
        //[HttpGet, Route("status/{status:alpha?}")] -- Making status optional
        //[HttpGet, Route("status/{status:alpha=}")] -- Passing null value as default
        [HttpGet, Route("status/{status:alpha=pending}/{id:int=1}")]
        //public string GetProductsWithStatus(string status = null)
        public string GetProductsWithStatus(string status, int id)
        {
            return string.IsNullOrEmpty(status)?"NULL": status;
        }


        // GET: api/Products/5/orders/custid
        [HttpGet, Route("{id:int:range(1000,3000)}/orders/{custid}")]

        public string Get(int id, string custid)
        {
            return $"product-id-{id}-orders-customer-id-{custid}";
        }

        // POST: api/Products
        [HttpPost, Route("")]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Products/5
        [HttpPut, Route("{id:int:range(1000,3000)}")]

        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Products/5
        [HttpDelete, Route("{id:int:range(1000,3000)}")]

        public void Delete(int id)
        {
        }

        #endregion



        // GET: api/Products/5/orders/custid
        [HttpGet, Route("accounts/{accountId:validAccount}")]

        public string Get(string accountId)
        {
            return $"{accountId}";
        }



        // POST: api/Products/prodId
        [HttpPost, Route("{prodId:int:range(1000,3000)}")]

        public HttpResponseMessage CreateProduct([FromUri]int prodId)
        {
            //do some create logic, then...
            var response = Request.CreateResponse(HttpStatusCode.Created);

            //Create self-referencing link to the new item
            //and set the response Location header

            string uri = Url.Link("GetById", new { Id = prodId });
            response.Headers.Location = new Uri(uri);

            return response;
        }



    }
}
