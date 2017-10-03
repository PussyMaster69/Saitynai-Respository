using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Models;

namespace Project.Controllers
{
    [Produces("application/json")]
    [Route("api/Device")]
    public class DeviceController : Controller
    {
        [HttpPost]
        public ActionResult Pair([FromBody] Device device)
        { 
            return new StatusCodeResult(StatusCodes.Status201Created);
        }

        // Edit method
        [HttpPut("{id}")]
        public ActionResult Update([FromBody] Device device)
        {
            return new StatusCodeResult(StatusCodes.Status200OK);
        }

        // Remove method
        [HttpDelete("{id}")]
        public ActionResult Remove()
        {
            return new StatusCodeResult(StatusCodes.Status404NotFound);
        }

        // View one device method
        [HttpGet("{id}")]
        public ActionResult Get(string id)
        {
            return Ok(new Device { Id = "device-test" });
        }

        // View all devices method
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(new List<Device> { new Device { Id = "Test Uno" }, new Device { Id = "Test Dos" } });
        }
    }
}