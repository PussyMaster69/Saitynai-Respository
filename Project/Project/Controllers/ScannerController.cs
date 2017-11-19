using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.DbModels;
using Project.Models;

namespace Project.Controllers
{
    [Produces("application/json")]
    [Route("api/Scanner")]
    public class ScannerController : Controller
    {
        private readonly MyDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public ScannerController(MyDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        [HttpPost]
        [Authorize("Bearer")]
        public ActionResult CreateScanner([FromBody] Scanner scanner)
        {
            // See's if there are any 'Scanners' entries that shares the same Ip and User
            var sharedEntriesCount = _dbContext.Scanners.Count(
                s => s.Ip == scanner.Ip && s.User.Email == User.Identity.Name
            );

            // A count that is greater than zero, indicates a conflict
            if (sharedEntriesCount > 0)
                return new StatusCodeResult(StatusCodes.Status409Conflict);

            // If there is no conflict, creates a new 'Scanner' entry & stores in DB
            var scannerTableData = new ScannerTableModel
            {
                Ip = scanner.Ip,
                User = _userManager.FindByNameAsync(User.Identity.Name).Result,
                Datetime = scanner.Datetime,
                State = Scanner.Inactive
            };
            var entityEntry = _dbContext.Add(scannerTableData);
            _dbContext.SaveChanges();

            // Returns 201Created status code and a location header with a url to the created entrie
            var scannerId = entityEntry.Entity.Id;
            return CreatedAtRoute("GetById", new { id = scannerId }, null);
        }

        [HttpGet("{id}", Name = "GetById")]
        [Authorize("Bearer")]
        public ActionResult GetScanner(int id)
        {
            // Finds entity with ID
            var scannerEntity = _dbContext.Scanners.Find(id);
            
            // If no entity was found, return a 404NotFound code
            if (scannerEntity == null) 
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            
            // If an entity was found, return a scanner object derived from the entity
            Scanner scanner = new Scanner()
            {
                Id = id,
                Ip = scannerEntity.Ip,
                Datetime = scannerEntity.Datetime,
                State = scannerEntity.State
            };
            return Ok(scanner);
        }

        [HttpPut("{id}")]
        [Authorize("Bearer")]
        public ActionResult UpdateScanner(int id, [FromBody] Scanner scanner)
        {
            // Finds entity with ID
            var scannerEntity = _dbContext.Scanners.Find(id);
            
            // If no entity was found, return a 404NotFound code
            if (scannerEntity == null) 
                return new StatusCodeResult(StatusCodes.Status404NotFound);

            return Ok(scanner);
        }
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