using CommonLibrary.Models;
using CommonLibrary.Models.DBModelsHelpers;
using Microsoft.AspNetCore.Mvc;
using static CommonLibrary.Models.EnumsHelper;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly RabbitMQHelper _rabbitMQHelper;

        public UsersController(DataContext context, RabbitMQHelper rabbitMQHelper)
        {
            _context = context;
            _rabbitMQHelper = rabbitMQHelper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetAppUserById(string id)
        {
            bool validAppUser = await AppUserHelper.ValidAppUserAsync(context: _context, identifier: id);
            if (!validAppUser) { return BadRequest(new { message = $"AppUser not found." }); }

            var appUserDetails = await AppUserHelper.GetAppUserDetailsAsync(context: _context, identifier: id);

            return Ok(appUserDetails);
        }

        [HttpGet("{id}/trades")]
        public async Task<ActionResult> GetAppUserTrades(string id, int page = 1, int pageSize = 10)
        {
            bool validAppUser = await AppUserHelper.ValidAppUserAsync(context: _context, identifier: id);
            if (!validAppUser) { return BadRequest(new { message = $"AppUser not found." }); }

            var appUserTrades = await AppUserHelper.GetAppUserTradesAsync(context: _context, identifier: id, page: page, pageSize: pageSize);

            return Ok(appUserTrades);
        }

        [HttpPost("{id}/create-trade")]
        public async Task<ActionResult> CreateTrade(string id, TradeAction tradeAction, decimal price)
        {
            bool validAppUser = await AppUserHelper.ValidAppUserAsync(context: _context, identifier: id);
            if (!validAppUser) { return BadRequest(new { message = $"AppUser not found." }); }

            await AppUserHelper.CreateTrade(context: _context, rabbitMQHelper: _rabbitMQHelper, identifier: id, tradeAction: tradeAction, price: price);

            return Ok();
        }
    }
}
