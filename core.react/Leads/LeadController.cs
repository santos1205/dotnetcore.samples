using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MovileWeb.Leads
{
    public class LeadController : Controller
    {

        [HttpGet("api/leads")]
        public object Get()
        {
            return new { message = "return sample" };
        }
    }
}