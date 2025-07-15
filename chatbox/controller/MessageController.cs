using chatbox.model;
using chatbox.service;
using Microsoft.AspNetCore.Mvc;


namespace Backend.controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly MessageService _messageService;

        public MessageController(MessageService messageService)
        {
            _messageService = messageService;
        }



        //GET: api/message
        [HttpGet]
        public async Task<ActionResult<List<Message>>> Get()
        {
            var message = await _messageService.GetAllAsync();
            return Ok(message);
        }
        

        //POST: api/message
        [HttpPost]
        public async Task<ActionResult<Message>> Post(Message message)
        {
            var created = await _messageService.CreateAsync(message);
            return CreatedAtAction(nameof(Post), new { id = created.Id }, created);
        }

    }
}