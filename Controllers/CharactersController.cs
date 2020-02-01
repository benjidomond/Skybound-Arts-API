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
    public class CharactersController : ControllerBase
    {
        private readonly SkyboundArtsContext _context;
        public CharactersController(SkyboundArtsContext context)
        {
            _context = context;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Characters>> GetAllCharacters()
        {
            return _context.Characters;
        }
        [HttpGet("{id}", Name = "GetCharacter")]
        public ActionResult<Characters> GetCharacter(int id)
        {
            var characterItem = _context.Characters.Find(id);
            if (characterItem == null)
            {
                return NotFound();
            }
            else
            {
                return characterItem;
            }
        }
        [HttpPost]
        public ActionResult<Characters> PostCharacter(Characters newCharacter)
        {
            _context.Characters.Add(newCharacter);
            _context.SaveChanges();
            return CreatedAtAction("GetCharacter", new { id = newCharacter.CharacterId }, newCharacter);
        }
        [HttpDelete("{id}")]
        public ActionResult<Characters> DeleteCharacter(int id)
        {
            var playerItem = _context.Characters.Find(id);
            if (playerItem == null)
            {
                return NotFound();
            }
            else
            {
                _context.Characters.Remove(playerItem);
                _context.SaveChanges();
                return playerItem;
            }
        }
    }
}
