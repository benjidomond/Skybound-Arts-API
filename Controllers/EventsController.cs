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
    public class EventsController : ControllerBase
    {
        private readonly SkyboundArtsContext _context;
        public EventsController (SkyboundArtsContext context)
        {
            _context = context;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Events>> GetAllEvents()
        {
            return _context.Events;
        }
        //Works
        [HttpGet("{id}", Name = "GetEvent")]
        public ActionResult<Events> GetEvent(int id)
        {
            var eventItem = _context.Events.Find(id);
            if (eventItem == null)
            {
                return NotFound();
            }
            return eventItem;
        }
        [HttpPost]
        public ActionResult<Events> PostEvents(Events eventItem)
        {
            _context.Events.Add(eventItem);
            _context.SaveChanges();
            // YES THIS WORKS THANK GOD
            //The big issue that I was having with this is that the id parameter isn't from the model - I thought I was supposed to use the EventModel here but nah,
            //I'm supposed to use the id parameter from GetEvent....
            return CreatedAtAction("GetEvent", new { id = eventItem.EventId }, eventItem);
        }
        [HttpDelete("{id}")]
        public ActionResult<Events> DeleteEvent(int id)
        {
            var eventItem = _context.Events.Find(id);
            if (eventItem == null)
            {
                return NotFound();
            }
            else {
                _context.Events.Remove(eventItem);
                _context.SaveChanges();
                return eventItem;
            }
        }
    }
}
