using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.DbModels;
using Project.Models;

namespace Project.Controllers
{
    [Produces("application/json")]
    [Route("api/PairHistory")]
    public class ConnectionPairsHistoryController : Controller
    {
        private readonly MyDbContext _dbContext;
        
        public ConnectionPairsHistoryController(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        [HttpPost]
        [Authorize(Policy = "Bearer")]
        public ActionResult AddPairToHistory([FromBody] PairRecord pairRecord)
        {
            // Get Pair with a given ID that belongs to the current user
            var pairEntry = _dbContext.Pairs.FirstOrDefault(p =>
                p.Id == pairRecord.PairId && p.User.Email == User.Identity.Name);
            
            // Get Scanner with a given Id belonging to the current user that found this device
            var scannerEntry = _dbContext.Scanners.FirstOrDefault(s =>
                s.Id == pairRecord.ScannerId && s.User.Email == User.Identity.Name);

            // If there is no such Scanner or Pair with the given Id's
            if (pairEntry == null || scannerEntry == null)
                return new StatusCodeResult(StatusCodes.Status409Conflict);
            
            // Create pair connection record and add it to the database
            ConnectionHistoryTable recordEntity = new ConnectionHistoryTable()
            {
                Pair = pairEntry,
                Datetime = pairRecord.ActivationTime,
                Scanner = scannerEntry
            };
            _dbContext.Entry(recordEntity).State = EntityState.Added;
            _dbContext.SaveChanges();

            return CreatedAtRoute("PairHistory/GetById", new {id = recordEntity.Id}, null);
        }
        
        [HttpGet("{id}", Name = "PairHistory/GetById")]
        [Authorize(Policy = "Bearer")]
        public ActionResult GetPairRecord(int id)
        {
            // Get pair connection record entry that belong to the current user
            var recordEntry = _dbContext.ConnectionHistories
                .Include(o => o.Pair).ThenInclude(o => o.User)
                .Include(o => o.Scanner)
                .FirstOrDefault(h => h.Id == id && h.Pair.User.Email == User.Identity.Name);

            // If there is no such pair connection entry
            if (recordEntry == null)
                return new StatusCodeResult(StatusCodes.Status404NotFound);

            // Return pair connection record to the user
            PairRecord pairRecord = new PairRecord()
            {
                Id = id,
                ActivationTime = recordEntry.Datetime,
                PairId = recordEntry.Pair.Id,
                ScannerId = recordEntry.Scanner.Id
            };
            return Ok(pairRecord);
        }

        [HttpGet]
        [Authorize(Policy = "Bearer")]
        public ActionResult GetAllPairsRecords()
        {
            // Get all pairs records entries that belong to the current user
            var recordsEntries = _dbContext.ConnectionHistories
                .Include(o => o.Pair).ThenInclude(o => o.User)
                .Include(o => o.Scanner)
                .Where(h => h.Pair.User.Email == User.Identity.Name);
            var entryCount = recordsEntries.Count();

            // If no entries were found, return a 404NotFound code
            if (entryCount <= 0)
                return new StatusCodeResult(StatusCodes.Status404NotFound);

            // Return a list of pairs objects to the user
            List<PairRecord> recordsList = new List<PairRecord>(entryCount);
            foreach (var entry in recordsEntries)
            {
                var record = new PairRecord()
                {
                    Id = entry.Id,
                    ActivationTime = entry.Datetime,
                    PairId = entry.Pair.Id,
                    ScannerId = entry.Scanner.Id
                };
                recordsList.Add(record);
            }
            return Ok(recordsList);
        }
        
        [HttpDelete("{id}")]
        [Authorize(Policy = "Bearer")]
        public ActionResult DeletePairRecord(int id)
        {
            // Get entity with ID
            var recordEntity = _dbContext.ConnectionHistories.FirstOrDefault(h =>
                h.Id == id && h.Pair.User.Email == User.Identity.Name);

            // If no entity was found, return a 404NotFound code
            if (recordEntity == null)
                return new StatusCodeResult(StatusCodes.Status404NotFound);

            // Delete the given entity
            _dbContext.Entry(recordEntity).State = EntityState.Deleted;
            _dbContext.SaveChanges();
            return Ok();
        }
        
        [HttpDelete]
        [Authorize(Policy = "Bearer")]
        public ActionResult DeleteAllPairsRecords()
        {
            // Get entities that belongs to the current user
            var recordsEntries = _dbContext.ConnectionHistories.Where(h =>
                h.Pair.User.Email == User.Identity.Name);
            var entryCount = recordsEntries.Count();
            
            // If no entries were found, return a 404NotFound code
            if (entryCount <= 0)
                return new StatusCodeResult(StatusCodes.Status404NotFound);

            // Delete all given entries
            foreach (var entry in recordsEntries)
                _dbContext.Entry(entry).State = EntityState.Deleted;
            _dbContext.SaveChanges();
            
            return Ok();
        }
    }
}