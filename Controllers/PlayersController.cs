using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using buttoncheckDevAPI.Models;

namespace buttoncheckDevAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly SkyboundArtsContext _context;
        public PlayersController (SkyboundArtsContext context)
        {
            _context = context;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Players>> GetAllPlayers()
        {
            return _context.Players;
        }
        [HttpGet("{id}", Name = "GetPlayer")]
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
        public ActionResult<Players> PostPlayer([FromBody]Players newPlayer)
        {
            _context.Players.Add(newPlayer);
            _context.SaveChanges();
            return CreatedAtAction("GetPlayer", new { id = newPlayer.PlayerId }, newPlayer);
        }
        [HttpDelete("{id}")]
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
