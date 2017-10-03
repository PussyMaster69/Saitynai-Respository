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
    [Route("api/Scanner")]
    public class ScannerController : Controller
    {
        //[HttpPost]
        //public ActionResult SignIn( )
        //{
        //    return
        //}


    }
}

//[HttpGet]
//public ActionResult Get()
//{
//    return Ok(new List<Scanner> { new Scanner { Id = "Test" }, new Scanner { Id = "Test2" } });
//}

//[HttpGet("{id}")]
//public ActionResult Get(string id)
//{
//    return Ok(new Scanner { Id = "Test" });
//}

//[HttpPost]
//public StatusCodeResult Post([FromBody] Scanner scanner)
//{
//    //Bla vla bla
//    return new StatusCodeResult(StatusCodes.Status201Created);
//}

//[HttpPut("{id}")]
//public StatusCodeResult Put([FromBody] Scanner scanner)
//{
//    //TODO Implement update logic
//    return new StatusCodeResult(StatusCodes.Status200OK);
//}

//[HttpDelete("{id}")]
//public StatusCodeResult Delete(string id)
//{
//    var exists = true;
//    if (!exists)
//    {
//        return new StatusCodeResult(StatusCodes.Status404NotFound);
//    }
//    //TODO Implement update logic
//    return new StatusCodeResult(StatusCodes.Status200OK);
//}