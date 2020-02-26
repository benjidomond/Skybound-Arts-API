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
        public ActionResult<IEnumerable<MapVideoTag>> FindVideoTags([FromQuery]string[] searchVideoIDs, [FromQuery]string[] searchTagID)
        {
            //this item will be used to access the mapped tags
            var mappedTagItem = from mappedtag in _context.MapVideoTag select mappedtag;
            /*
            for (int i = 0; i < searchTagID.Length; i++)
            {
                var parsedTag = int.Parse(searchTagID[i]);
                if(searchTagID[i] != "")
                {
                    mappedTagItem = mappedTagItem.Where(v => v.TagId == parsedTag);
                }
                for (int x = 0; x < searchVideoIDs.Length; x++)
                {
                    mappedTagItem = mappedTagItem.Where(v => v.VideoId == searchVideoIDs[i]);
                }
            }

            ex. video 1 and video 2
            5 tags
            10 iterations in total
            */
            /*
             Iteration without an index - is foreach what I'm looking for?

            Update - so I'm able to get the values by themselves when searching for
            tags but I can't get values together which is unfortunate.
            For example,
            http://localhost:5000/api/MapVideoTag/findVideo/?searchTagID=3&searchVideoIDs=1
            This command works when I enter in one video ID but when I enter in both
            ID 1 and 2 it fails to return both
             */
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
            return mappedTagItem.ToList();
        }
    }
}
