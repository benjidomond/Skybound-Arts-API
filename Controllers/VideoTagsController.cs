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
    public class VideoTagsController : ControllerBase
    {
        private SkyboundArtsContext _context;
        public VideoTagsController (SkyboundArtsContext context)
        {
            _context = context;
        }
        // Getting all the VideoTags
        [HttpGet]
        public ActionResult<IEnumerable<VideoTags>> GetAllTags()
        {
            return _context.VideoTags;
        }
        // Get a single VideoTag
        [HttpGet("{id}", Name = "GetSingleTag")]
        public ActionResult<VideoTags> GetSingleTag(int id)
        {
            var tagItem = _context.VideoTags.Find(id);
            if (tagItem == null)
            {
                return NotFound();
            }
            else
            {
                return tagItem;
            }
        }
        //Post a VideoTag
        [HttpPost]
        public ActionResult<VideoTags> PostTag(VideoTags newTag)
        {
            _context.VideoTags.Add(newTag);
            _context.SaveChanges();
            return CreatedAtAction("GetSingleTag", new { id = newTag.TagId }, newTag);
        }
        //Delete a VideoTag
        [HttpDelete("{id}")]
        public ActionResult<VideoTags> DeleteTag(int id)
        {
            var tagItem = _context.VideoTags.Find(id);
            if (tagItem == null)
            {
                return NotFound();
            }
            else
            {
                _context.VideoTags.Remove(tagItem);
                _context.SaveChanges();
                return tagItem;
            }
        }
    }
}
