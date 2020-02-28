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
    public class MapVideoTagController : ControllerBase
    {
        private SkyboundArtsContext _context;
        public MapVideoTagController (SkyboundArtsContext context)
        {
            _context = context;
        }
        //Get all tags
        [HttpGet]
        public ActionResult<IEnumerable<MapVideoTag>> GetAllMapTags()
        {
            return _context.MapVideoTag;
        }
        //Get single tag association
        [HttpGet("{id}", Name = "GetMapTag")]
        public ActionResult<MapVideoTag> GetMapTag(int id)
        {
            var mappedTag = _context.MapVideoTag.Find(id);
            if (mappedTag == null)
            {
                return NotFound();
            }
            else
            {
                return mappedTag;
            }
        }
        //Add a new tag association
        [HttpPost]
        public ActionResult<MapVideoTag> AddMapping(MapVideoTag newTag)
        {
            _context.MapVideoTag.Add(newTag);
            _context.SaveChanges();
            return CreatedAtAction("GetMapTag", new {id = newTag.MapId}, newTag);
        }
        [HttpGet("search/{searchTagID?}")]
        public ActionResult<IEnumerable<MapVideoTag>> MapSearch([FromQuery]string searchTagID = null)
        {
            var mappedTagItem = from mappedtag in _context.MapVideoTag select mappedtag;
            var parseTagID = int.Parse(searchTagID);
            if (!String.IsNullOrEmpty(searchTagID))
            {
                mappedTagItem = mappedTagItem.Where(v => v.TagId == parseTagID);
            }
            return mappedTagItem.ToList();
        }
        [HttpGet("findVideo/{searchTagID?}/{searchVideoIDs?}")]
        public ActionResult<IEnumerable<MapVideoTag>> FindVideoTags([FromQuery]int[] searchVideoIDs, [FromQuery]int searchTagID)
        {
            /*
             Let's try to clean this up ourselves.
             Alright, so we have two values here - a videoID and a tagID. What this search command is supposed to do is
             check and see if all tthe videos contain a tagID. I think it'd be best to have one tagID for this search instead of multiple,
             due to the fact that users will be clicking on one tag at a time and the amount of queries will be smaller.

            I'd have to store the current video selections in the state of the react application, which would help immensely.
            But yeah, let's try downsizing this app to just one tag with multiple videos.
             */
            //this item will be used to access the mapped tags

            /*
             
            IMPORTANT CHANGES! changed searchvideoIDs to an int
             
             */
            var mappedTagItem = from mappedtag in _context.MapVideoTag
                                where searchVideoIDs.Contains(mappedtag.VideoId) && searchTagID == mappedtag.TagId
                              select mappedtag;
            /*
            foreach (string videoID in searchVideoIDs)
            {
                bool verifyVideo = !String.IsNullOrEmpty(videoID);
                foreach (string tagID in searchTagID)
                {
                    bool verifyTag = !String.IsNullOrEmpty(tagID);
                    if (verifyTag == true && verifyVideo == true)
                    {
                        var parsedVideo = int.Parse(videoID);
                        var parsedTag = int.Parse(tagID);
                        mappedTagItem = mappedTagItem.Where(v =>
                        (v.VideoId == parsedVideo)
                        &&
                        (v.TagId == parsedTag)
                        );
                    }
                }
            }
            */
            return mappedTagItem.ToList();
        }
    }
}
