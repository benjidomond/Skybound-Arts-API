using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using buttoncheckDevAPI.Models;

//The methods in this class are used to retrieve, delete, and add players to the database.
namespace buttoncheckDevAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly SkyboundArtsContext _context;
        public PlayersController (SkyboundArtsContext context)
        {
            // Inserting the DBContext into this class, enabling you to access the database.
            _context = context;
        }
        [HttpGet]
        //Retrieving all the players
        public ActionResult<IEnumerable<Players>> GetAllPlayers()
        {
            return _context.Players;
        }
        [HttpGet("{id}", Name = "GetPlayer")]
        //Retrieving individual player
        public ActionResult<Players> GetPlayer(int id)
        {
            var playerItem = _context.Players.Find(id);
            if (playerItem == null)
            {
                return NotFound();
            }
            return playerItem;
        }
        [HttpPost]
        //Adds a player to the database
        public ActionResult<Players> PostPlayer([FromBody]Players newPlayer)
        {
            _context.Players.Add(newPlayer);
            _context.SaveChanges();
            return CreatedAtAction("GetPlayer", new { id = newPlayer.PlayerId }, newPlayer);
        }
        [HttpDelete("{id}")]
        //Removes a player from the database
        public ActionResult<Players> DeletePlayer(int id)
        {
            var playerItem = _context.Players.Find(id);
            if (playerItem == null)
            {
                return NotFound();
            }
            else
            {
                _context.Players.Remove(playerItem);
                _context.SaveChanges();
                return playerItem;
            }
        }
    }
}
