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
        //Getting a collection of videos
        [HttpGet("find/{videoIDs?}")]
        public ActionResult<IEnumerable<Videos>> GetVideoCollection([FromQuery]int[] videoIDs)
        {
            var videoCollection = from video in _context.Videos where videoIDs.Contains(video.VideoId) select video;
            return videoCollection.ToList();
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
        //Create Video
        [HttpPost]
        public ActionResult<Videos> PostVideo([FromBody]Videos newVideo)
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
        //Search API command
        [HttpGet("search/{videoIDs?}/{playerName?}/{characterName?}/{eventName?}")]
        public ActionResult<IEnumerable<Videos>> SearchVideos([FromQuery]int[] videoIDs, [FromQuery]string playerName = null, [FromQuery]string characterName = null, [FromQuery]string eventName = null)
        {
            /*
             checks the length of the video IDs - if there is existing videoIDs, that means a search has been conducted.
            */
            if (videoIDs.Length > 0)
            {
                var videoItem = from video in _context.Videos where videoIDs.Contains(video.VideoId) select video;
                if (!String.IsNullOrEmpty(playerName))
                {
                    videoItem = videoItem.Where(p => p.P1Player.Contains(playerName) || p.P2Player.Contains(playerName));
                }
                if (!String.IsNullOrEmpty(characterName))
                {
                    videoItem = videoItem.Where(c => c.P1Character.Contains(characterName) || c.P2Character.Contains(characterName));
                }
                if (!String.IsNullOrEmpty(eventName))
                {
                    videoItem = videoItem.Where(e => e.EventName.Contains(eventName));
                }
                return videoItem.ToList();
            }
            else
            {
                //represents a single video in the database
                var videoItem = from video in _context.Videos select video;
                if (!String.IsNullOrEmpty(playerName))
                {
                    videoItem = videoItem.Where(p => p.P1Player.Contains(playerName) || p.P2Player.Contains(playerName));
                }
                if (!String.IsNullOrEmpty(characterName))
                {
                    videoItem = videoItem.Where(c => c.P1Character.Contains(characterName) || c.P2Character.Contains(characterName));
                }
                if (!String.IsNullOrEmpty(eventName))
                {
                    videoItem = videoItem.Where(e => e.EventName.Contains(eventName));
                }
                return videoItem.ToList();
            }
        }
    }
}
