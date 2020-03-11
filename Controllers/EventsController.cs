using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using buttoncheckDevAPI.Models;

// The methods in this class are used to retrieve, delete, and add events to the database
namespace buttoncheckDevAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly SkyboundArtsContext _context;
        public EventsController (SkyboundArtsContext context)
        {
            // Inserting the DBContext into this class, enabling you to access the database
            _context = context;
        }
        [HttpGet]
        //Retrieving all the events
        public ActionResult<IEnumerable<Events>> GetAllEvents()
        {
            return _context.Events;
        }
        [HttpGet("{id}", Name = "GetEvent")]
        //Retrieving individual event
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
        //Adds an event to the database
        public ActionResult<Events> PostEvents([FromBody]Events eventItem)
        {
            _context.Events.Add(eventItem);
            _context.SaveChanges();
            // YES THIS WORKS THANK GOD
            //The big issue that I was having with this is that the id parameter isn't from the model - I thought I was supposed to use the EventModel here but nah,
            //I'm supposed to use the id parameter from GetEvent....
            return CreatedAtAction("GetEvent", new { id = eventItem.EventId }, eventItem);
        }
        [HttpDelete("{id}")]
        //Removes an event from the database
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
