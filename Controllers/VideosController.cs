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
    public class VideosController : ControllerBase
    {
        private readonly SkyboundArtsContext _context;
        public VideosController(SkyboundArtsContext context)
        {
            _context = context;
        }
        //Load All Videos
        [HttpGet]
        public ActionResult<IEnumerable<Videos>> GetAllVideos()
        {
            return _context.Videos;
        }
        //Getting ID of Video
        [HttpGet("{id}", Name = "GetVideo")]
        public ActionResult<Videos> GetVideo(int id)
        {
            var videoItem = _context.Videos.Find(id);
            if (videoItem == null)
            {
                return NotFound();
            }
            else
            {
                return videoItem;
            }
        }
        //Search Videos By Character
        [HttpGet("characters/{characterName}")]
        public ActionResult<IEnumerable<Videos>> SearchByCharacter(string characterName)
        {
            return _context.Videos.Where(v => v.P1Character.Contains(characterName) || v.P2Character.Contains(characterName)).ToList();
        }
        //Search By Player Name
        [HttpGet("players/{playerName}")]
        public ActionResult<IEnumerable<Videos>> SearchByPlayer(string playerName)
        {
            return _context.Videos.Where(v => v.P1Player.Contains(playerName) || v.P2Player.Contains(playerName)).ToList();
        }
        //Search By Event Name
        [HttpGet("events/{eventName}")]
        public ActionResult<IEnumerable<Videos>> SearchByEvents(string eventName) {
            return _context.Videos.Where(v => v.EventName.Contains(eventName)).ToList();
        }
        //Create Video
        [HttpPost]
        public ActionResult<Videos> PostVideo(Videos newVideo)
        {
            _context.Videos.Add(newVideo);
            _context.SaveChanges();
            return CreatedAtAction("GetVideo", new { id = newVideo.VideoId }, newVideo);
        }
        //Delete Video
        [HttpDelete("{id}")]
        public ActionResult<Videos> DeleteVideo(int id)
        {
            var videoItem = _context.Videos.Find(id);
            if (videoItem == null)
            {
                return NotFound();
            }
            else
            {
                _context.Videos.Remove(videoItem);
                _context.SaveChanges();
                return videoItem;
            }
        }
        //TEST THE LAST 3
        //Search By Winning Character
        [HttpGet("winnerCharacter/{winnerCharacter}")]
        public ActionResult<IEnumerable<Videos>> GetWinnerCharacter(string winnerCharacter)
        {
            return _context.Videos.Where(v => v.WinnerCharacter.Contains(winnerCharacter)).ToList();
        }
        //Search By Winning Player
        [HttpGet("winnerPlayer/{winnerPlayer}")]
        public ActionResult<IEnumerable<Videos>> GetWinnerPlayer(string winnerPlayer)
        {
            return _context.Videos.Where(v => v.WinnerPlayer.Contains(winnerPlayer)).ToList();
        }
        //Search By Winning Character & Player
        [HttpGet("winnerCharAndPlayer/{winnerCharacter}/{winnerPlayer}")]
        public ActionResult<IEnumerable<Videos>> SearchWinnerCharAndPlayer(string winnerCharacter, string winnerPlayer)
        {
            return _context.Videos.Where(v => v.WinnerCharacter.Contains(winnerCharacter) & v.WinnerPlayer.Contains(winnerPlayer)).ToList();
        }
        //stacking API calls?!? hmm...
    }
}
