using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using buttoncheckDevAPI.Models;


// The methods in this class are used to retrieve, modify and add characters into the database.
namespace buttoncheckDevAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharactersController : ControllerBase
    {
        private readonly SkyboundArtsContext _context;
        public CharactersController(SkyboundArtsContext context)
        {
            // Inserting the DBContext into this class, enabling you to access the database
            _context = context;
        }
        [HttpGet]
        //Retrieving all the characters
        public ActionResult<IEnumerable<Characters>> GetAllCharacters()
        {
            return _context.Characters;
        }
        [HttpGet("{id}", Name = "GetCharacter")]
        //Retrieving individual character
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
        //Adds a character to the database
        public ActionResult<Characters> PostCharacter(Characters newCharacter)
        {
            _context.Characters.Add(newCharacter);
            _context.SaveChanges();
            return CreatedAtAction("GetCharacter", new { id = newCharacter.CharacterId }, newCharacter);
        }
        [HttpDelete("{id}")]
        //Removes a character from the database
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
