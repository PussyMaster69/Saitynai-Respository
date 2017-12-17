using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.DbModels;
using Project.Models;

namespace Project.Controllers
{
    [Produces("application/json")]
    [Route("api/Pair")]
    public class PairController : Controller
    {
        private readonly MyDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public PairController(MyDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        [HttpPost]
        [Authorize(Policy = "Bearer")]
        public ActionResult CreatePair([FromBody] Device device)
        {
            // Check if this device is already paired to this user
            var pairEntry = _dbContext.Pairs.FirstOrDefault(p =>
                p.Device.Address == device.Address && p.User.Email == User.Identity.Name);
            if (pairEntry != null)
                return new StatusCodeResult(StatusCodes.Status409Conflict);

            // Check if there is such a device in the database & if no, add it and pair it with the current user
            var deviceEntry = _dbContext.Devices.Find(device.Address);
            if (deviceEntry == null)
            {
                deviceEntry = new DeviceTable()
                {
                    Address = device.Address,
                    Name = device.Name
                };
                _dbContext.Entry(deviceEntry).State = EntityState.Added;
            }
            pairEntry = new PairTable()
            {
                FriendlyName = device.Name,
                User = _userManager.FindByNameAsync(User.Identity.Name).Result,
                Device = deviceEntry
            };
            _dbContext.Entry(pairEntry).State = EntityState.Added;
            _dbContext.SaveChanges();

            return CreatedAtRoute("Pair/GetById", new {id = pairEntry.Id}, null);
        }

        [HttpGet("{id}", Name = "Pair/GetById")]
        [Authorize(Policy = "Bearer")]
        public ActionResult GetPair(int id)
        {
            // Get pair entry that belong to the current user
            var pairEntry = _dbContext.Pairs
                .Include(p => p.Device)
                .FirstOrDefault(p => p.Id == id && p.User.Email == User.Identity.Name);

            if (pairEntry == null)
                return new StatusCodeResult(StatusCodes.Status404NotFound);

            var pairedDevice = _dbContext.Devices.FirstOrDefault(d => 
                d.Address == pairEntry.Device.Address);
            
            if (pairedDevice == null)
                return new StatusCodeResult(StatusCodes.Status404NotFound);
                
            PairExtended pairFull = new PairExtended()
            {
                Id = id,
                FriendlyName = pairEntry.FriendlyName,
                Name = pairedDevice.Name,
                Address = pairedDevice.Address
            };
            
            return Ok(pairFull);
        }

        [HttpGet]
        [Authorize(Policy = "Bearer")]
        public ActionResult GetAllPairs()
        {
            // Finds all pairs entries that belong to the current user
            var pairEntries = _dbContext.Pairs.Where(s => s.User.Email == User.Identity.Name);
            var entryCount = pairEntries.Count();

            // If no entities were found, return a 404NotFound code
            if (entryCount <= 0)
                return new StatusCodeResult(StatusCodes.Status404NotFound);

            // Return a list of pairs objects to the user
            List<Pair> pairsList = new List<Pair>(entryCount);
            foreach (var entry in pairEntries)
            {
                var pair = new Pair()
                {
                    Id = entry.Id,
                    FriendlyName = entry.FriendlyName,
                };
                pairsList.Add(pair);
            }
            return Ok(pairsList);
        }
    
        [HttpPut("{id}")]
        [Authorize(Policy = "Bearer")]
        public ActionResult UpdatePair(int id, [FromBody] Pair pair)
        {
            // Finds entity with ID
            var pairEntity = _dbContext.Pairs.FirstOrDefault(p =>
                p.Id == id && p.User.Email == User.Identity.Name);

            // If no entity was found, return a 404NotFound code
            if (pairEntity == null)
                return new StatusCodeResult(StatusCodes.Status404NotFound);

            // Updates pair's friendly name
            if (pair.FriendlyName.Trim() == "")
                return Ok();
            
            pairEntity.FriendlyName = pair.FriendlyName;
            _dbContext.Entry(pairEntity).State = EntityState.Modified;
            _dbContext.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "Bearer")]
        public ActionResult DeletePair(int id)
        {
            // Finds entity with ID
            var pairEntity = _dbContext.Pairs
                .Include(p => p.Device)
                .Include(p => p.User)
                .FirstOrDefault(p => p.Id == id && p.User.Email == User.Identity.Name);
            
            // If no entity was found, return a 404NotFound code
            if (pairEntity == null)
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            
            // Remove associated Device if only it's used by this entity alone
            var associatedPairsToDevice = _dbContext.Pairs.Where(p =>
                p.Device.Address == pairEntity.Device.Address);
            if (associatedPairsToDevice.Count() == 1)
                _dbContext.Entry(associatedPairsToDevice.First()).State = EntityState.Deleted;
            
            // Deletes the given entity
            _dbContext.Entry(pairEntity).State = EntityState.Deleted;
            _dbContext.SaveChanges();
            
            return Ok();
        }
    }
}