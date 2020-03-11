using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using buttoncheckDevAPI.Models;

// The methods in this class are used to add, retrieve, and delete video / tag relations.
namespace buttoncheckDevAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoTagsController : ControllerBase
    {
        private SkyboundArtsContext _context;
        public VideoTagsController (SkyboundArtsContext context)
        {
            // Inserting the DBContext into this class, enabling you to access the database.
            _context = context;
        }
        [HttpGet]
        //Retrieving all the video / tag pairs
        public ActionResult<IEnumerable<VideoTags>> GetAllTags()
        {
            return _context.VideoTags;
        }
        [HttpGet("{id}", Name = "GetSingleTag")]
        //Retrieving a single video / tag pair
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
        [HttpPost]
        //Creating a new video / tag relation
        public ActionResult<VideoTags> PostTag(VideoTags newTag)
        {
            _context.VideoTags.Add(newTag);
            _context.SaveChanges();
            return CreatedAtAction("GetSingleTag", new { id = newTag.TagId }, newTag);
        }
        [HttpDelete("{id}")]
        //Removes a video / tag relation
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
