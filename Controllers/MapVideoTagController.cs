using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using buttoncheckDevAPI.Models;

// The methods in this class are used on the MapVideoTag table, which is used to pair tags to video IDs
namespace buttoncheckDevAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MapVideoTagController : ControllerBase
    {
        private SkyboundArtsContext _context;
        public MapVideoTagController (SkyboundArtsContext context)
        {
            // Inserting the DBContext into this class, enabling you to access the database
            _context = context;
        }
        [HttpGet]
        //Retrieving all the mapped tag relations
        public ActionResult<IEnumerable<MapVideoTag>> GetAllMapTags()
        {
            return _context.MapVideoTag;
        }
        [HttpGet("{id}", Name = "GetMapTag")]
        //Get single video / tag association
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
        [HttpPost]
        //Add a new video / tag association
        public ActionResult<MapVideoTag> AddMapping(MapVideoTag newTag)
        {
            _context.MapVideoTag.Add(newTag);
            _context.SaveChanges();
            return CreatedAtAction("GetMapTag", new {id = newTag.MapId}, newTag);
        }
        [HttpGet("search/{searchTagID?}")]
        //Allows you to search for video IDs by passing in the ID of a tag
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
        //By passing in an array of tags and videos, you're able to check and see if all the videos passed include a tag within the database.
        public ActionResult<IEnumerable<MapVideoTag>> FindVideoTags([FromQuery]int[] searchVideoIDs, [FromQuery]int searchTagID)
        {
            var mappedTagItem = from mappedtag in _context.MapVideoTag
                                where searchVideoIDs.Contains(mappedtag.VideoId) && searchTagID == mappedtag.TagId
                              select mappedtag;
            return mappedTagItem.ToList();
        }
    }
}
