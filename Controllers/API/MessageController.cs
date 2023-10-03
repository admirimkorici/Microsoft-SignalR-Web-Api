using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SignalR.Data;
using SignalR.Models;

namespace SignalR.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private ApplicationDbContext _context;
        private readonly IHubContext<ChatHub> _hubContext;

        public MessageController(ApplicationDbContext context,
            IHubContext<ChatHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }
        [HttpGet]
        [Route("GetMessages")]
        public async Task<IActionResult> GetMessages()
        {
            var result = await _context.Messages.ToListAsync();

            return Ok(result);
        }

        [HttpPost]
        [Route("SendMessage")]
        public async Task<IActionResult> SendMessage([FromBody] Message model)
        {
            if (ModelState.IsValid)
            {
                await _context.AddAsync(model);
                await _context.SaveChangesAsync();
                await _hubContext.Clients.All.SendAsync("ReceiveMessage", model);
                return Ok(model);
            }
            else
                return BadRequest();
        }
    }
}
