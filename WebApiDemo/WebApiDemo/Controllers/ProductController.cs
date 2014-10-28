using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApiDemo.Controllers
{
    public class ProductController : ApiController
    {
        public IHttpActionResult Get()
        {
            //Should return a list of products
            return NotFound(); //But nothing was found

            return BadRequest();

            return InternalServerError(); //an exception occured
        }
    }
}
