using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
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
            var scannerTableData = new ScannerTable
            {
                Ip = scanner.Ip,
                User = _userManager.FindByNameAsync(User.Identity.Name).Result,
                Datetime = scanner.Datetime
            };
            _dbContext.Entry(scannerTableData).State = EntityState.Added;
            _dbContext.SaveChanges();

            // Returns 201Created status code and a location header with a url to the created entry
            var scannerId = scannerTableData.Id;
            return CreatedAtRoute("Scanner/GetById", new { id = scannerId }, null);
        }

        [HttpGet("{id}", Name = "Scanner/GetById")]
        [Authorize("Bearer")]
        public ActionResult GetScanner(int id)
        {
            // Finds entity with ID that belongs to the current user
            var scannerEntity = _dbContext.Scanners.FirstOrDefault(s =>
                s.Id == id && s.User.Email == User.Identity.Name);
            
            // If no entity was found, return a 404NotFound code
            if (scannerEntity == null) 
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            
            // If an entity was found, return a scanner object derived from that entity
            Scanner scanner = new Scanner()
            {
                Id = id,
                Ip = scannerEntity.Ip,
                Datetime = scannerEntity.Datetime            
            };
            return Ok(scanner);
        }

        [HttpGet]
        [Authorize("Bearer")]
        public ActionResult GetAllScanners()
        {
            // Finds all scanners entries that belong to the current user
            var scannerEntries = _dbContext.Scanners.Where(s => s.User.Email == User.Identity.Name);
            var entryCount = scannerEntries.Count();
            
            // If no entities were found, return a 404NotFound code
            if (entryCount <= 0)
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            
            // Return a list of scanner objects to the user
            List<Scanner> scannersList = new List<Scanner>(entryCount);
            foreach (var entry in scannerEntries)
            {
                var scanner = new Scanner()
                {
                    Id = entry.Id,
                    Ip = entry.Ip,
                    Datetime = entry.Datetime
                };
                scannersList.Add(scanner);
            }
            return Ok(scannersList);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "Bearer")]
        public ActionResult UpdateScanner(int id, [FromBody] Scanner scanner)
        {
            // Finds entity with ID
            var scannerEntity = _dbContext.Scanners.FirstOrDefault(s =>
                s.Id == id && s.User.Email == User.Identity.Name);
            
            // If no entity was found, return a 404NotFound code
            if (scannerEntity == null) 
                return new StatusCodeResult(StatusCodes.Status404NotFound);

            // Updates scanner datetime timestamp & saves it to the database
            scannerEntity.Datetime = scanner.Datetime;
            _dbContext.Entry(scannerEntity).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "Bearer")]
        public ActionResult DeleteScanner(int id)
        {
            // Finds entity with ID
            var scannerEntity = _dbContext.Scanners.FirstOrDefault(s =>
                s.Id == id && s.User.Email == User.Identity.Name);
            
            // If no entity was found, return a 404NotFound code
            if (scannerEntity == null) 
                return new StatusCodeResult(StatusCodes.Status404NotFound);

            // Deletes the given entity
            _dbContext.Entry(scannerEntity).State = EntityState.Deleted;
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}