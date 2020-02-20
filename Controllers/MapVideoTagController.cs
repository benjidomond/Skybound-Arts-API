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
    }
}
